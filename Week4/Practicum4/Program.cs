using System;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum4
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            /* NawDoublyLinkedList ll = new NawDoublyLinkedList();

             ll.InsertHead(new NAW("Persoon 1", "Adres 1", "Woonplaats 3"));
             ll.InsertHead(new NAW("Persoon 2", "Adres 2", "Woonplaats 2"));
             ll.InsertHead(new NAW("Persona non grata", "Adres 3", "Woonplaats 1"));
             ll.InsertHead(new NAW("Persoon 4", "Adres 4", "Woonplaats 4"));
             //ll.InsertHead(new NAW("Persoon 1", "Adres 5", "Woonplaats 1"));
             //ll.InsertHead(new NAW("Persoon 2", "Adres 6", "Woonplaats 2"));
             //ll.InsertHead(new NAW("Persona non grata", "Adres 7", "Woonplaats 3"));
             //ll.InsertHead(new NAW("Persoon 2", "Adres 8", "Woonplaats 2"));
             //ll.InsertHead(new NAW("Persoon 9", "Adres 9", "Woonplaats 1"));
             //ll.InsertHead(new NAW("Persoon 10", "Adres 10", "Woonplaats 2"));

             for (var current = ll.First; current != null; current = current.Next)
             {
                Console.WriteLine(current.Naw.Woonplaats); 
             }

             for (var current = ll.Last; current != null; current = current.Previous)
             {
                 Console.WriteLine(current.Naw.Woonplaats);
             }
             Console.ReadLine();

             ll.BubbleSort();

             Console.WriteLine("Na de sort:");

             for (var current = ll.First; current != null; current = current.Next)
             {
                 current.Naw.Show();
             }


             Console.ReadKey();*/

            UndoableNawArray unArray = new UndoableNawArray(10);
            unArray.Add(new NAW("kenneth", "straat 6", "4"));
            unArray.Undo();
           // unArray.Undo();

            printContent();

            unArray.Add(new NAW("kenneth", "straat 1", "9"));
            unArray.Add(new NAW("kenneth", "straat 2", "8"));
            unArray.Add(new NAW("kenneth", "straat 3", "7"));
            unArray.Add(new NAW("kenneth", "straat 4", "6"));
            
            printContent();

            unArray.Undo();
            unArray.Undo();

            printContent();

            unArray.Redo();
            unArray.Redo();

            printContent();

            unArray.Undo();

            printContent();

            unArray.Add(new NAW("nelus", "straat 5", "5"));

            printContent();

            unArray.Redo(); unArray.Redo();
            unArray.Redo();
            unArray.Redo();
            unArray.Redo();


            printContent();


            void printContent()
            {
                Console.WriteLine("====================Count = " + unArray.Count + " =======================");
                UndoLink temp = unArray.First;
                for (int i = 0; i < unArray.Count; i++)
                {
                    temp.Naw.Show();
                    temp = temp.Next;
                }
            }

            Console.ReadLine();

        }
    }
}
