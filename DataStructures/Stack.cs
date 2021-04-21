using System;

namespace DataStructures
{
    public class Stack<T>
    {
        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }
        }

        private Node Top { get; set; }
        public int Size { get; set; }

        public Stack()
        {
            Top = null;
            Size = 0;
        }

        public bool IsEmpty()
        {
            return Top == null;
        }

        public void Push(T data)
        {
            Node temp = new Node { Data = data, Next = Top };
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
    }
}
