using System;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum7
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            NawDictionary a = new NawDictionary(10);
            a.Insert("1", new NAW("a1", "b", "c"));
            a.Insert("1", new NAW("a1", "b", "c"));
            a.Insert("1", new NAW("a1", "b", "c"));
            a.Insert("1", new NAW("a1", "b", "c"));
            a.Insert("1", new NAW("a1", "b", "c"));
            Console.WriteLine($"count: {a.Count}");
            Console.WriteLine($"LF: {a.LoadFactor}");


            Console.ReadLine();

        }
    }
}
