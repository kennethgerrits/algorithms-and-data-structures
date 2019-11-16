using System;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum1
{
    class MainClass
    {

        private static void week1_UnorderedArray_Testaanroepen()
        {
            // >>>> Testaanroepen week 1 - Unordered Array <<<<


            NawArrayUnordered array = new NawArrayUnordered(20);
            array.Add(new NAW("Persoon 1", "Adres 1", "Woonplaats 1"));
            array.Add(new NAW("Persoon 2", "Adres 2", "Woonplaats 2"));
            array.Add(new NAW("Persona non grata", "Adres 3", "Woonplaats 3"));
            array.Add(new NAW("Persoon 4", "Adres 4", "Woonplaats 2"));
            array.Add(new NAW("Persoon 1", "Adres 5", "Woonplaats 1"));
            array.Add(new NAW("Persoon 2", "Adres 6", "Woonplaats 2"));
            array.Add(new NAW("Persona non grata", "Adres 7", "Woonplaats 3"));
            array.Add(new NAW("Persoon 2", "Adres 8", "Woonplaats 2"));
            array.Add(new NAW("Persoon 9", "Adres 9", "Woonplaats 1"));
            array.Add(new NAW("Persoon 10", "Adres 10", "Woonplaats 2"));

            System.Console.WriteLine("De ongeordende array na initialisatie maar voor bewerkingen is:");
            array.Show();

            // Activeer onderstaande Huiswerk testaanroepen wanneer je de bijhorende methode hebt geimplementeerd door de commentaarhaken weg te halen.


/*            array.RemoveAtIndex(3);
            System.Console.WriteLine("\nNa verwijderen van index 3:");
            array.Show();*/


            /*
            array.RemoveFirstNaam("Persoon 1");
            System.Console.WriteLine("\nNa verwijderen 1e persoon met naam 'Persoon 1':");
            array.Show();
            */

            /*
            array.RemoveLastNaam("Persoon 2");
            System.Console.WriteLine("\nNa verwijderen van laatste persoon met naam 'Persoon 2':");
            array.Show();
            */

            /*
            array.RemoveAllNaam("Persona non grata");
            System.Console.WriteLine("\nNa verwijderen van alle personen met naam 'Persona non grata':");
            array.Show();
            */

        }

        private static void week1_OrderedArray_Testaanroepen()
        {
            // >>>> Testaanroepen week 1 - Ordered Array <<<<

            // Testaanroep constructor:

            NawArrayOrdered orderedArray = new NawArrayOrdered(20);

            // Testaanroepen Add:

            orderedArray.Add(new NAW("Persoon 1", "Adres 1", "Woonplaats 1"));
            orderedArray.Add(new NAW("Persoon 2", "Adres 2", "Woonplaats 2"));
            orderedArray.Add(new NAW("Persona non grata", "Adres 3", "Woonplaats 3"));
            orderedArray.Add(new NAW("Persoon 4", "Adres 4", "Woonplaats 2"));
            orderedArray.Add(new NAW("Persoon 1", "Adres 5", "Woonplaats 1"));
            orderedArray.Add(new NAW("Persoon 2", "Adres 6", "Woonplaats 2"));
            orderedArray.Add(new NAW("Persona non grata", "Adres 7", "Woonplaats 3"));
            orderedArray.Add(new NAW("Persoon 2", "Adres 8", "Woonplaats 2"));
            orderedArray.Add(new NAW("Persoon 9", "Adres 9", "Woonplaats 1"));
            orderedArray.Add(new NAW("Persoon 10", "Adres 10", "Woonplaats 2"));

            System.Console.WriteLine("\n\nDe geordende array na initialisatie maar voor bewerkingen is:");
            orderedArray.Show();

            // Activeer onderstaande testaanroepen wanneer je de bijhorende methode hebt geimplementeerd door de commentaarhaken weg te halen.

            // Testaanroepen Find:


           /* NAW gezochteNAW = new NAW("Persoon 4", "Adres 4", "Woonplaats 2");
            NAW nietbestaandeNAW = new NAW("Onbekende Persoon", "Adres 3", "Woonplaats 1");

            int gezochteNAWIndex = orderedArray.Find(gezochteNAW);
            System.Console.WriteLine("\nPersoon 4 gevonden op index {0}:", gezochteNAWIndex);

            int gezochteNAWIndex2 = orderedArray.Find(nietbestaandeNAW);
            System.Console.WriteLine("\nOnbekende Persoon gevonden op index {0}:", gezochteNAWIndex2);*/


            // Testaanroep Update:

/*
            orderedArray.Update(gezochteNAW, nietbestaandeNAW);
            System.Console.WriteLine("\n\nDe geordende array na update van persoon 4:");
            orderedArray.Show();*/

        }


        static void Main(string[] args)
        {

            week1_UnorderedArray_Testaanroepen();

            week1_OrderedArray_Testaanroepen();

            System.Console.ReadKey();

        }
    }
}
