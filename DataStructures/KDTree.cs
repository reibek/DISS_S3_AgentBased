using System;
using System.Collections.Generic;

namespace DataStructures
{
    /// <summary>
    /// KDTree class implements a k-dimensional tree data structure.
    /// </summary>
    public class KDTree<T> where T : IComparableKD<T>
    {
        /// <summary> Inner class representing node of tree. </summary>
        public class Node
        {
            public T Data { get; set; }
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }

            /// <summary> KDTree node constructor. </summary>
            /// <param name="data"> Actual data stored in node </param>
            public Node(T data)
            {
                Data = data;
            }
        }

        /// <summary> Number of dimensions of data. </summary>
        private readonly int _k;

        /// <summary> Root of tree. </summary>
        public Node Root { get; private set; }

        /// <summary> Size of tree. </summary>
        public int Size { get; private set; }


        /// <summary> KDTree constructor. </summary>
        /// <param name="k"> Number of dimensions </param>
        public KDTree(int k)
        {
            _k = k;
            Size = 0;
            Root = null;
        }

        /// <summary> Check, if tree is empty. </summary>
        /// <returns> True if tree is empty, false otherwise </returns>
        public bool IsEmpty()
        {
            return Root == null;
        }

        /// <summary> Add new node to a tree. </summary>
        /// <returns> True, if adding was successful </returns>
        public bool Insert(T newData)
        {
            Node curr = Root;
            Node newNode = new Node(newData);

            if (IsEmpty())
            {
                Root = newNode;
                newNode.Height = 0;                                 /* height of root is 0 */
                Size++;
                return true;
            }

            while (true)
            {
                /* Compare new item and item on current node by (h mod k) key. */
                int cmp = newData.CompareDimensionKey(curr.Data, curr.Height % _k);
                if (cmp <= 0)
                {
                    if (curr.Left != null)
                        curr = curr.Left;
                    else
                    {
                        curr.Left = newNode;
                        newNode.Parent = curr;
                        newNode.Height = curr.Height + 1;           /* height of new node is 1 greater than current */
                        Size++;
                        return true;
                    }
                }
                else
                {
                    if (curr.Right != null)
                        curr = curr.Right;
                    else
                    {
                        curr.Right = newNode;
                        newNode.Parent = curr;
                        newNode.Height = curr.Height + 1;
                        Size++;
                        return true;
                    }
                }
            }
        }

        /// <summary> Find data stored in KDTree. </summary>
        /// <param name="item"> Data in node to be found </param>
        /// <returns> Found data or null if not found </returns>
        public T Find(T item)
        {
            Node foundNode = FindNode(item);
            return foundNode.Data;
        }

        /// <summary> Find node, where is item stored. </summary>
        /// <param name="item"> Item data in node to be found </param>
        /// <returns> Node with item data or null if not found </returns>
        private Node FindNode(T item)
        {
            Node curr = Root;

            while (curr != null)
            {
                int cmp = item.CompareDimensionKey(curr.Data, curr.Height % _k);
                if (cmp < 0)
                    curr = curr.Left;
                else if (cmp > 0)
                    curr = curr.Right;
                else if (item.Equals(curr.Data))
                    return curr;
                else
                    curr = curr.Left;
            }

            return null;
        }

        /// <summary>
        /// Find all items, which have same key attributes as function parameter item.
        /// </summary>
        /// <param name="item"> item, that has set key attributes </param>
        /// <returns> list of items with same key attributes as parameter item </returns>
        public List<T> FindAt(T item)
        {
            List<T> list = new List<T>();
            Node curr = Root;

            while (curr != null)
            {
                int cmp = item.CompareDimensionKey(curr.Data, curr.Height % _k);
                if (cmp < 0)
                    curr = curr.Left;
                else if (cmp > 0)
                    curr = curr.Right;
                else
                {
                    if (item.KeysAreEqual(curr.Data))
                        list.Add(curr.Data);
                    curr = curr.Left;
                }
            }

            return list;
        }

        /// <summary>
        /// Find all items, which have key attributes between lower item and upper item.
        /// </summary>
        /// <param name="lower"> lower bound of interval </param>
        /// <param name="upper"> upper bound of interval </param>
        /// <returns> list of items, which key attributes are between key attributes of lower item
        /// and upper item </returns>
        public List<T> FindBetween(T lower, T upper)
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");
            if (lower == null)
                throw new ArgumentNullException(nameof(lower));
            if (upper == null)
                throw new ArgumentNullException(nameof(upper));

            List<T> list = new List<T>();
            Node lowCurr = Root;
            Node upCurr = Root;

            /* Help structure (modification of stack), that has ability to set flag on each item. */
            FlagStack<Node> stack = new FlagStack<Node>();

            /* Find first item in interval. */
            while (true)
            {
                int cmp = lower.CompareDimensionKey(lowCurr.Data, lowCurr.Height % _k);
                if (cmp <= 0)
                {
                    if (lowCurr.Left != null)
                    {
                        stack.Push(lowCurr, false);
                        lowCurr = lowCurr.Left;
                    }
                    else
                        break;
                }
                else
                {
                    if (lowCurr.Right != null)
                    {
                        /* If next node will be in right subtree, set flag. */
                        stack.Push(lowCurr, true);
                        lowCurr = lowCurr.Right;
                    }
                    else
                        break;
                }
            }

            /* Find last item in interval. */
            while (true)
            {
                int cmp = upper.CompareDimensionKey(upCurr.Data, upCurr.Height % _k);
                if (cmp <= 0)
                    if (upCurr.Left != null)
                        upCurr = upCurr.Left;
                    else
                        break;
                else
                    if (upCurr.Right != null)
                        upCurr = upCurr.Right;
                    else
                        break;
            }

            /* Perform in-order traverse. */
            while (lowCurr != null || !stack.IsEmpty())
            {
                while (lowCurr != null)
                {
                    stack.Push(lowCurr, false);
                    lowCurr = lowCurr.Left;
                }

                /* If we found last item, break loop. */
                if (lowCurr == upCurr)
                    break;

                bool isRight = stack.PeekFlag();
                lowCurr = stack.Pop();

                /* If current item has key attributes between key attributes of lower and upper bound,
                add item to list. */
                if (lowCurr.Data.IsBetween(lower, upper))
                    list.Add(lowCurr.Data);

                /* If current item was not added from parent's right son, go to it's right subtree. */
                if (!isRight)
                    lowCurr = lowCurr.Right;
            }

            return list;
        }

        /// <summary>
        /// Remove item from a KDTree.
        /// </summary>
        /// <param name="item"> item, that needs to have set key attributes and attributes, that
        /// evaluate equality of items. </param>
        /// <returns> true, if removing was successful </returns>
        public bool Remove(T item)
        {
            if (IsEmpty())
                throw new Exception("Tree is empty");

            /* Find node to be deleted. */
            Node nodeToRemove = FindNode(item);
            if (nodeToRemove == null)
                return false;

            if (Size == 1)
            {
                Root = null;
                Size--;
                return true;
            }

            /* Find node, that will replace node to delete. Replace them until last of them is leaf. */
            while (nodeToRemove.Left != null || nodeToRemove.Right != null)
                nodeToRemove = MoveNode(nodeToRemove);

            /* Check if node is not root of tree. If not, remove reference from parent. */
            if (nodeToRemove != Root)
            {
                if (nodeToRemove.Parent.Left == nodeToRemove)
                    nodeToRemove.Parent.Left = null;
                else
                    nodeToRemove.Parent.Right = null;
            }

            /* Set node (leaf) delete to null. */
            nodeToRemove = null;
            Size--;
            return true;
        }

        /// <summary> Find best node to replace current node. </summary>
        /// <param name="item"> Item node to be replaced </param>
        /// <returns> Node, that replace item </returns>
        private Node MoveNode(Node item)
        {
            Node nodeToMove = null;

            /* If item has left subtree, find inorder predecessor and replace data in deleted node with data from
            found node. */
            if (item.Left != null)
            {
                nodeToMove = MaxByDimension(item.Left, item.Height % _k);
                item.Data = nodeToMove.Data;
            }

            /* Or if item has right subtree find node with maximum value of key on current dimension, replace data
            in deleted node with data from found node and set right subtree as left subtree. Also set right subtree
            as null. */
            else if (item.Right != null)
            {
                nodeToMove = MaxByDimension(item.Right, item.Height % _k);
                item.Data = nodeToMove.Data;
                item.Left = item.Right;
                item.Right = null;
            }

            return nodeToMove;
        }

        /// <summary>
        /// Find node with minimal value on dimension (dim), in subtree with root in parameters. 
        /// </summary>
        /// <param name="root"> root of subtree to be searched </param>
        /// <param name="dim"> dimension of searching node </param>
        /// <returns></returns>
        private Node MinByDimension(Node root, int dim)
        {
            Node curr = root;
            Node min = root;
            Stack<Node> nodesToCheck = new Stack<Node>();

            while (curr != null || !nodesToCheck.IsEmpty())
            {
                if (curr == null)
                    curr = nodesToCheck.Pop();

                if (curr.Data.CompareDimensionKey(min.Data, dim) < 0)
                    min = curr;

                if (dim != curr.Height % _k)
                    if (curr.Right != null)
                        nodesToCheck.Push(curr.Right);

                curr = curr.Left;
            }

            return min;
        }

        /// <summary>
        /// Find node with maximal value on dimension (dim), in subtree with root in parameters. 
        /// </summary>
        /// <param name="root"> root of subtree to be searched </param>
        /// <param name="dim"> dimension of searching node </param>
        /// <returns></returns>
        private Node MaxByDimension(Node root, int dim)
        {
            Node curr = root;
            Node max = curr;
            Stack<Node> nodesToCheck = new Stack<Node>();

            while (curr != null || !nodesToCheck.IsEmpty())
            {

                if (curr == null)
                    curr = nodesToCheck.Pop();

                if (curr.Data.CompareDimensionKey(max.Data, dim) > 0)
                    max = curr;

                if (dim != curr.Height % _k)
                    if (curr.Left != null)
                        nodesToCheck.Push(curr.Left);

                curr = curr.Right;
            }

            return max;
        }

        /// <summary>
        /// Inorder enumerator.
        /// </summary>
        /// <returns> Data of KDTree Node </returns>
        public IEnumerator<T> GetInOrderEnumerator()
        {
            Node curr = Root;
            if (curr != null)
            {
                Stack<Node> stack = new Stack<Node>();
                while (curr != null || !stack.IsEmpty())
                {
                    while (curr != null)
                    {
                        stack.Push(curr);
                        curr = curr.Left;
                    }
                    curr = stack.Pop();
                    yield return curr.Data;
                    curr = curr.Right;
                }
            }
            else
            {
                throw new Exception("Tree is empty");
            }
        }

        /// <summary>
        /// Level order enumerator.
        /// </summary>
        /// <returns> Data of KDTree Node </returns>
        public IEnumerator<T> GetLevelOrderEnumerator()
        {
            Node curr = Root;
            if (curr != null)
            {
                Queue<Node> queue = new Queue<Node>();
                queue.Enqueue(curr);
                while (!queue.IsEmpty())
                {
                    curr = queue.Dequeue();
                    yield return curr.Data;
                    if (curr.Left != null) queue.Enqueue(curr.Left);
                    if (curr.Right != null) queue.Enqueue(curr.Right);
                }
            }
            else
            {
                throw new Exception("Tree is empty");
            }
        }
    }
}
