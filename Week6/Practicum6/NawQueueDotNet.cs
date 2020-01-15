using Alg1.Practica.Utils.Models;
using System.Collections.Generic;


namespace Alg1.Practica.Practicum6
{
    public class NawQueueDotNet
    {
        private Queue<NAW> queue = new Queue<NAW>();
        private int _count;
        public void Enqueue(NAW naw)
        {
            queue.Enqueue(naw);
            _count++;
        }

        public NAW Dequeue()
        {
            if (queue.Count <= 0) return null;
            _count--;
            return queue.Dequeue();
        }

        public int Count()
        {
            return _count;
        }
    }
}
