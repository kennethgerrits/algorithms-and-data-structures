using System;
using Alg1.Practica.Practicum6;
using Alg1.Practica.Utils.Models;

namespace Practicum6
{
    class MainClass
    {
        public static void Main(string[] args)
        {
        NawPriorityQueue queue = new NawPriorityQueue();

            queue.Enqueue(1,new NAW("kennont", "straat 1", "9"));
            queue.Enqueue(1,new NAW("kennont", "straat 2", "8"));
            queue.Enqueue(2,new NAW("kennont", "straat 3", "7"));
            queue.Enqueue(2,new NAW("kennont", "straat 4", "6"));
            queue.Enqueue(3, new NAW("kennonts", "straat 3", "6"));
            queue.Enqueue(4, new NAW("kennonts", "straat 4", "6"));

            queue.Show();
            queue.Dequeue();
            queue.Show();

            queue.Dequeue();
            queue.Show();
            queue.Dequeue();
            queue.Show();
            queue.Dequeue();
            queue.Show();
            queue.Dequeue();
            queue.Show();
            queue.Dequeue();
            queue.Show();
            queue.Dequeue();
            queue.Show();
            Console.ReadKey();

        }
    }
}
