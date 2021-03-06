using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum5
{
    public class Stack : IStack
    {
        protected StackLink First { get; set; }

        public void Push(string value)
        {
            if (value == null) return;
            StackLink temp = new StackLink { String = value, Next = First };
            First = temp;
        }

        public string Pop()
        {
            if (IsEmpty())
                return null;
            
            string temp = First.String;
            First = First.Next;
            return temp;
        }

        public string Peek()
        {
            if (!IsEmpty())
                return First.String;

            return null;
        }

        public bool IsEmpty()
        {
            return First == null;
        }
    }
}