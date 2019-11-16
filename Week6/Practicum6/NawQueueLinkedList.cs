using System;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum6
{
    public class NawQueueLinkedList
    {
        public Link First { get; set; }

        protected Link Last { get; set; }

        protected int _count;


        public void Enqueue(NAW naw)
        {
            Link newLink = new Link { Naw = naw };

            if (First == null)
            {
                First = newLink;
                Last = First;
            }
            else
            {
                Last.Next = newLink;
                Last = Last.Next;
            }
            _count++;
        }

        public NAW Dequeue()
        {
            if (First == null)
            {
                return null;
            }

            NAW result = First.Naw;
            First = First.Next;
            _count--;
            return result;
        }

        public int Count()
        {
            return _count;
        }
    }

}