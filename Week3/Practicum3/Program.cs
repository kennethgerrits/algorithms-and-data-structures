using System;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum3
{
    class Program
    {
        public static void Main(string[] args)
        {
            InsertionSortableNawArrayUnordered array = new InsertionSortableNawArrayUnordered(4);
            array.Add(new NAW("AA", "Adres 1", "Woonplaats 9"));
            array.Add(new NAW("AA", "Adres 1", "Woonplaats 1"));
            array.Add(new NAW("AA", "Adres 1", "Woonplaats 2"));
            array.Add(new NAW("AA", "Adres 1", "Woonplaats 8"));

            InsertionSortableNawArrayUnordered array2 = new InsertionSortableNawArrayUnordered(4);
            array2.Add(new NAW("AA", "Adres 1", "Woonplaats 9"));
            array2.Add(new NAW("AA", "Adres 1", "Woonplaats 1"));
            array2.Add(new NAW("AA", "Adres 1", "Woonplaats 2"));
            array2.Add(new NAW("AA", "Adres 1", "Woonplaats 8"));

            Console.WriteLine("Normal: ");
            array.Show();

            Console.WriteLine("Selection sort:");
            array.InsertionSort();
            array.Show();

            Console.WriteLine("Inverted: ");
            array2.InsertionSortInverted();
            array2.Show();

            // PerformanceTest.TestSortPerformance();
            Console.WriteLine("end");
            Console.ReadKey();

        }
    }
}