using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public class BinarySearchTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        public class Node
        {
            public T Data { get; set; }
            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(T data)
            {
                Data = data;
            }
        }

        public Node Root { get; private set; }
        public int Size { get; private set; }

        public BinarySearchTree()
        {
            Root = null;
            Size = 0;
        }

        public bool IsEmpty()
        {
            return Root == null;
        }

        public bool Add(T newData)
        {
            Node curr = Root;
            Node newNode = new Node(newData);

            if (curr == null)
            {
                Root = newNode;
                newNode.Parent = null;
                Size++;
                return true;
            }

            while (true)
            {
                int cmp = newData.CompareTo(curr.Data);
                // If new data are less than current data,
                if (cmp < 0)
                {
                    // move to left if possible,
                    if (curr.Left != null)
                        curr = curr.Left;
                    // if not, create new left son.
                    else
                    {
                        curr.Left = newNode;
                        newNode.Parent = curr;
                        Size++;
                        return true;
                    }
                }
                // If new data are less than current data,
                else if (cmp > 0)
                {
                    // move to left if possible,
                    if (curr.Right != null)
                        curr = curr.Right;
                    // if not, create new right son.
                    else
                    {
                        curr.Right = newNode;
                        newNode.Parent = curr;
                        Size++;
                        return true;
                    }
                }
                // New data are already in tree.
                else
                {
                    Console.WriteLine("Item already in tree.");
                    return false;
                }
            }
        }

        public object Find(T element)
        {
            Node curr = Root;
            while (curr != null)
            {
                int cmp = element.CompareTo(curr.Data);
                if (cmp < 0)
                    curr = curr.Left;
                else if (cmp > 0)
                    curr = curr.Right;
                else
                    return curr.Data;
            }

            return null;
        }

        private Node FindNode(T element)
        {
            Node curr = Root;
            while (curr != null)
            {
                int cmp = element.CompareTo(curr.Data);
                if (cmp < 0)
                    curr = curr.Left;
                else if (cmp > 0)
                    curr = curr.Right;
                else
                    return curr;
            }

            return null;
        }

        public bool Remove(T element)
        {
            Node curr = FindNode(element);
            Node parent = curr.Parent;

            if (curr.Right == null)
            {
                if (parent.Right == curr)
                    parent.Right = curr.Left;
                else if (parent.Left == curr)
                    parent.Left = curr.Left;
                else
                    Root = curr.Left;
            }
            else if (curr.Left == null)
            {
                if (parent.Right == curr)
                    parent.Right = curr.Right;
                else if (parent.Left == curr)
                    parent.Left = curr.Right;
                else
                    Root = curr.Right;
            }
            else
            {
                Node leftmost = curr.Right;
                Node leftmostParent = curr;

                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }

                curr.Data = leftmost.Data;

                if (leftmostParent == curr)
                    curr.Right = leftmost.Right;
                else
                    leftmostParent.Left = leftmost.Right;
            }

            return false;
        }

        /// <summary>
        /// Inorder iterator.
        /// </summary>
        /// <returns> Data of BinarySearchTree Node </returns>
        public IEnumerator<T> GetEnumerator()
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
                Console.WriteLine("Tree is empty.");
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Level order iterator.
        /// </summary>
        /// <returns> Data of BinarySearchTree Node </returns>
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
                Console.WriteLine("Tree is empty.");
            }
        }
    }
}
