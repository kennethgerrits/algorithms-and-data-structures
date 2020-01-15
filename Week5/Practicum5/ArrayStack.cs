using Alg1.Practica.Utils;
using System;

namespace Alg1.Practica.Practicum5
{
    public class ArrayStack : IStack
    {
        protected string[] values;
        protected int size;

        public ArrayStack(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException();
            
            values = new string[capacity];
            size = 0;
        }

        public void Push(string value)
        {
            if (IsFull())
                throw new InvalidOperationException();

            values[size] = value;
            size++;
        }

        public string Pop()
        {
            if (IsEmpty())
                return null;
            
            string temp = values[size - 1];
            values[size - 1] = null;
            size--;
            return temp;
        }

        public string Peek()
        {
            return IsEmpty() ? null : values[size - 1];
        }

        public bool IsEmpty()
        {
            return values[0] == null;
        }

        public bool IsFull()
        {
            return size == values.Length;
        }

        public void readAll()
        {
            foreach (var e in values)
            {
                Console.WriteLine(e);
            }
        }
    }
}