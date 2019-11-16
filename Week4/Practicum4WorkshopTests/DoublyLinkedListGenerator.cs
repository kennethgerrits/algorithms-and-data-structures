using System;
using Alg1.Practica.Utils.Models;
using Alg1.Practica.TestBase.Utils;

namespace Alg1.Practica.Practicum4.Test
{
    public class DoublyLinkedListGenerator
    {
        public static NawDoublyLinkedList FromArray(NAW[] array) 
        {
            var result = new NawDoublyLinkedList();
            foreach(var item in array)
            {
                var link = new DoubleLink
                {
                    Naw = item,
                    Previous = result.Last,
                    Next = null
                };

                if (result.First == null)
                {
                    result.First = result.Last = link;
                }
                else
                {
                    result.Last.Next = link;
                    result.Last = link;
                }
            }
            return result;
        }

        public static NawDoublyLinkedList Random(int min, int max, out int count)
        {
            var result = new NawDoublyLinkedList();
            var r = new Random();
            count = r.Next(min, max);
            DoubleLink prev = null;
            for (var i = 0; i < count; ++i)
            {
                var newLink = new DoubleLink
                {
                    Naw = RandomNawGenerator.New(10)
                };
                if (prev == null)
                    result.First = newLink;
                else
                    prev.Next = newLink;
                newLink.Previous = prev;
                prev = newLink;
                result.Last = prev;
            }
            return result;
        }

        public static NawDoublyLinkedList Random(out int count) => Random(3, 10, out count);
        
    }
}
