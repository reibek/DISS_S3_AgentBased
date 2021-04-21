using System;

namespace DataStructures
{
    public class FlagStack<T>
    {
        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }
            public bool Flag { get; set; }
        }

        private Node Top { get; set; }
        public int Size { get; set; }

        public FlagStack()
        {
            Top = null;
            Size = 0;
        }

        public bool IsEmpty()
        {
            return Top == null;
        }

        public void Push(T data, bool flag)
        {
            Node temp = new Node
            {
                Data = data, 
                Next = Top,
                Flag = flag
            };
            Top = temp;
            Size++;
        }

        public T Pop()
        {
            if (IsEmpty()) throw new Exception("Stack is empty.");
            Node temp = Top;
            Top = Top.Next;
            Size--;
            return temp.Data;
        }

        public T Peek()
        {
            if (IsEmpty()) throw new Exception("Stack is empty.");
            return Top.Data;
        }

        public bool PeekFlag()
        {
            if (IsEmpty()) throw new Exception("Stack is empty.");
            return Top.Flag;
        }
    }
}
