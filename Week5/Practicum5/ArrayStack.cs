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
            {
                throw new ArgumentOutOfRangeException();
            }
            values = new string[capacity];
            size = 0;
        }

        public void Push(string value)
        {
            if (IsFull())
            {
                throw new InvalidOperationException();
            }

            values[size] = value;
            size++;
        }

        public string Pop()
        {
            if (IsEmpty())
            {
                return null;
            }

            string temp = values[size - 1];
            values[size - 1] = null;
            size--;
            return temp;
        }

        public string Peek()
        {
            if (IsEmpty())
            {
                return null;
            }

            return values[size - 1];
        }

        public bool IsEmpty()
        {
            if (values[0] == null)
            {
                return true;
            }

            return false;
        }

        public bool IsFull()
        {
            if (size == values.Length)
            {
                return true;
            }

            return false;
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