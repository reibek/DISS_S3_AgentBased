using System;
using System.Threading;

namespace DataStructures
{
    public class Queue<T>
    {
        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }
        }

        private Node Head { get; set; }
        private Node Tail { get; set; }
        public int Size { get; private set; }
        public int Count => Size;

        public Queue()
        {
            Head = null;
            Tail = null;
            Size = 0;
        }

        public bool IsEmpty()
        {
            return Head == null;
        }

        public void Enqueue(T data)
        {
            Node temp = new Node { Data = data, Next = null };
            if (IsEmpty())
            {
                Head = temp;
                Tail = temp;
                Size++;
                return;
            }
            Tail.Next = temp;
            Tail = temp;
            Size++;
        }

        public T Dequeue()
        {
            if (IsEmpty()) throw new Exception("Queue is empty.");
            Node temp = Head;
            Head = Head.Next;
            Size--;
            if (IsEmpty()) Tail = null;
            return temp.Data;
        }

        public T Peek()
        {
            if (IsEmpty()) throw new Exception("Queue is empty.");
            return Head.Data;
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Size = 0;
        }
    }
}
