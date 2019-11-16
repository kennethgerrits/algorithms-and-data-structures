using System;
using System.Collections;
using Alg1.Practica.Utils.Models;
using System.Collections.Generic;


namespace Alg1.Practica.Practicum6
{
    public class NawPriorityQueue
    {
        private SortedList<int, NawQueueLinkedList> _priorityQueue = new SortedList<int, NawQueueLinkedList>();


        public void Enqueue(int priority, NAW naw)
        {
            if (priority < 0)
            {
                return;
            }

            NawQueueLinkedList list;

            if (_priorityQueue.ContainsKey(priority))
            {
                list = new NawQueueLinkedList();
                _priorityQueue.TryGetValue(priority, out list);
                list.Enqueue(naw);
            }
            else
            {
                list = new NawQueueLinkedList();
                list.Enqueue(naw);

                _priorityQueue.Add(priority, list);
            }
        }

        public NAW Dequeue()
        {
            if (_priorityQueue.Count == 0)
            {
                return null;
            }
            var min = _priorityQueue.Keys[0];
            NawQueueLinkedList temp;
            _priorityQueue.TryGetValue(min, out temp);
            NAW tempNaw = temp.Dequeue();
            if (temp.Count() == 0)
            {
                _priorityQueue.Remove(min);
            }
            return tempNaw;

        }

        public int Count()
        {
            int total = 0;
            IList<int> keys = _priorityQueue.Keys;
            foreach (var t in keys)
            {
                total += _priorityQueue[t].Count();
            }
            return total;
        }

        public void Show()
        {
            IList<int> keys = _priorityQueue.Keys;
            IList<NawQueueLinkedList> lists = _priorityQueue.Values;
            Console.WriteLine("===========================Count: " + Count() + " ==============================");
            for (int i = 0; i < keys.Count; i++)
            {
                Console.WriteLine("|| Key: " + keys[i]);
                var temp = lists[i].First;
                while (temp != null)
                {
                    temp.Naw.Show();
                    temp = temp.Next;
                }
            }


            // Deze methode is handig bij het debuggen maar wordt niet nagekeken
        }
    }
}
