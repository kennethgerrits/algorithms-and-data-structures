using System;
using NUnit.Framework;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Alg1.Practica.TestBase.Attributes;
using Alg1.Practica.Utils.Logging;
using Alg1.Practica.Utils.Models;
using Alg1.Practica.TestBase.Utils;
using Alg1.Practica.Practicum2;
using Alg1.Practica.TestBase.Utils.Decorators;
using Alg1.Practica.Utils;


namespace Alg1.Practica.Practicum2.Test.Workshop
{
    [TestFixture]
    public class BubbleSortableNawArrayUnorderedTest
    {
        [SetUp]
        public void BubbleSortableNawArrayUnordered_TestInitialize()
        {
            Logger.Instance.ClearLog();
            Globals.ShowAlg1NawArrayAlerts = false;
        }

        [TearDown]
        public void BubbleSortableNawArrayUnordered_TestCleanup()
        {
            Globals.ShowAlg1NawArrayAlerts = true;
        }
        
        public static TestableBubbleSortableArray<BubbleSortableNawArrayUnordered> TestSubject(int size) =>
            new TestableBubbleSortableArray<BubbleSortableNawArrayUnordered>(new BubbleSortableNawArrayUnordered(size));

        public static TestableBubbleSortableArray<BubbleSortableNawArrayUnordered> TestSubject(int size, out NAW[] testSet)
        {
            var array =
                new TestableBubbleSortableArray<BubbleSortableNawArrayUnordered>(new BubbleSortableNawArrayUnordered(size));
            testSet = Decorator.GenerateAndFill(array, size);
            return array;
        }
        
        [Test]
        [Category("WS2 Array Bubble Sort")]
        [Points(0.5)]
        [Code("BubbleSortableNawArrayUnordered", "BubbleSort")]
        [Scenario(@"We maken een lege BubbleSortableNawArrayUnordered aan en voeren een BubbleSort uit.
We verwachten dat er geen stappen ondernomen zijn omdat de lijst leeg is.")]
        public void BubbleSortableNawArrayUnordered_BubbleSort_EmptyArray_ShouldNotSort()
        {
            // Arrange
            var array = TestSubject(10);

            // Act
            Globals.Alg1NawArrayMethodCalled = false;

            array.BubbleSort();

            // Assert
            Assert.IsFalse(Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.AreEqual(0, Logger.Instance.LogItems.Count(), "Je hebt de array benaderd terwijl deze leeg is. Dit had niet gehoeven omdat je dit al wist door je ItemCount.");
        }

        [Test]
        [Category("WS2 Array Bubble Sort")]
        [Points(0.5)]
        [Code("BubbleSortableNawArrayUnordered", "BubbleSort")]
        [Scenario(@"We maken een BubbleSortableNawArrayUnordered aan en vullen die met geordende data.
We testen of er een bubblesort uitgevoerd is, dus dat er een hoop items gecheckt zijn.
We testen of er geen items van plek gewisseld zijn omdat de array al geordend was en er dus niets naar boven gebubbeld is.")]
        public void BubbleSortableNawArrayUnordered_BubbleSort_SortedArray_ShouldNotSetAnyNewIndexes()
        {
            // Arrange
            var expectedLength = 10;
            var array = TestSubject(expectedLength);
            Decorator.GenerateAndFill(array, expectedLength, Decorator.DesiredOrdering.Ascending);

            // Act
            Globals.Alg1NawArrayMethodCalled = false;

            array.BubbleSort();

            // Assert
            Assert.IsFalse(Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.IsSorted(), "De array is na de bubbleSort niet geordend. De items staan niet op de juiste volgorde.");
            Assert.IsTrue(Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.GET).Count() >= expectedLength, "Bent te weinig door de lijst gelopen om hem middels een bubbleSort te ordenen.");
            Assert.AreEqual(0, Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.SWAP && li.NewNaw1 != li.OldNaw1).Count(),
                        "Items die op hun plek moesten blijven staan zijn toch opnieuw gezet met dezelfde waarde.");
        }

        [Test]
        [Category("WS2 Array Bubble Sort")]
        [Points(0.5)]
        [Code("BubbleSortableNawArrayUnordered", "BubbleSort")]
        [Scenario(@"We maken een BubbleSortableNawArrayUnordered aan en vullen die met aflopend gesorteerde data.
We testen of er een bubblesort uitgevoerd is, dus dat er een hoop items gecheckt zijn.
We testen of de array geordend is.
We testen of alle items (op 1 na) van plek gewisseld zijn omdat op de laatste na elk item minstens één keer moet bubbelen.")]
        public void BubbleSortableNawArrayUnordered_BubbleSort_SortedArrayDescending_ShouldSetAllItemsButOne()
        {
            // Arrange
            var expectedLength = 100;
            var array = TestSubject(expectedLength);
            Decorator.GenerateAndFill(array, expectedLength, Decorator.DesiredOrdering.Descending);

            // Act
            Globals.Alg1NawArrayMethodCalled = false;

            array.BubbleSort();

            // Assert
            Assert.IsFalse(Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.IsSorted(), "De array is na de bubbleSort niet geordend. De items staan niet op de juiste volgorde.");
            var swaps = Logger.Instance.RealSwaps;
            Assert.AreEqual(expectedLength.SumAllSmallerIncSelf() - expectedLength, swaps.Count(),
                        "Het aantal swaps zou gelijk moeten zijn aan een O(Log(n)), dit is niet het geval.");
        }

        [Test]
        [Category("WS2 Array Bubble Sort")]
        [Points(0.5)]
        [Code("BubbleSortableNawArrayUnordered", "BubbleSort")]
        [Scenario(@"We maken een BubbleSortableNawArrayUnordered aan en vullen die met items met allemaal dezelfde woonplaats.
We testen of de CompareTo wordt uitgevoerd op de items zodat alsnog op de naam en het adres wordt geordend.")]
        public void BubbleSortableNawArrayUnordered_BubbleSort_AllTheSameWoonplaats_IsInOrder()
        {
            // Arrange
            var expectedLength = 100;
            var array = TestSubject(expectedLength);
            var testSet = Decorator.GenerateAndFill(array, expectedLength);

            foreach (var item in testSet)
            {
                item.Woonplaats = "Allemaal dezelfde woonplaats";
            }

            // Act
            Globals.Alg1NawArrayMethodCalled = false;

            array.BubbleSort();

            // Assert
            Assert.IsFalse(Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.IsSorted(), "De array is na de bubbleSort niet geordend. De items staan niet op de juiste volgorde.");
        }

        [Test]
        [Category("WS2 Array Bubble Sort")]
        [Points(0.5)]
        [Code("BubbleSortableNawArrayUnordered", "BubbleSort")]
        [Scenario(@"We maken een BubbleSortableNawArrayUnordered aan en vullen die met items met allemaal dezelfde woonplaats en naam.
We testen of de CompareTo wordt uitgevoerd op de items zodat alsnog op het adres wordt geordend.")]
        public void BubbleSortableNawArrayUnordered_BubbleSort_AllTheSameWoonplaatsAndNaam_IsInOrder()
        {
            // Arrange
            var expectedLength = 10;
            var array = TestSubject(expectedLength);
            var testSet = Decorator.GenerateAndFill(array, expectedLength);

            foreach (var item in testSet)
            {
                item.Woonplaats = "Allemaal dezelfde woonplaats";
                item.Naam = "Allemaal dezelfde naam";
            }

            // Act
            Globals.Alg1NawArrayMethodCalled = false;

            array.BubbleSort();

            // Assert
            Assert.IsFalse(Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.IsSorted(), "De array is na de bubbleSort niet geordend. De items staan niet op de juiste volgorde.");
        }

        [Test]
        [Category("WS2 Array Bubble Sort")]
        [Points(0.5)]
        [Code("BubbleSortableNawArrayUnordered", "BubbleSort")]
        [Scenario(@"We maken een BubbleSortableNawArrayUnordered aan en vullen die met woonplaats ""0"" tot ""9"", daarna wisselen we de 4 en de 8.
Bij een bubbelsort testen we nu of:
    8 tot aan de 9 omhoog bubbelt:  4 stappen
    7 voorbij de 4 bubbelt:         1 stap
    6 voorbij de 4 bubbelt:         1 stap
    5 voorbij de 4 bubbelt:         1 stap
    ------------------------------------------
                                    7 stappen in totaal.")]
        public void BubbleSortableNawArrayUnordered_BubbleSort_EightAndFourSwapped_ShouldHaveRightBounds()
        {
            // Arrange
            var expectedLength = 10;
            var array = TestSubject(expectedLength);
            var testSet = Decorator.GenerateAndFill(array, expectedLength);

            testSet[0].Woonplaats = "0";
            testSet[1].Woonplaats = "1";
            testSet[2].Woonplaats = "2";
            testSet[3].Woonplaats = "3";
            testSet[4].Woonplaats = "8";
            testSet[5].Woonplaats = "5";
            testSet[6].Woonplaats = "6";
            testSet[7].Woonplaats = "7";
            testSet[8].Woonplaats = "4";
            testSet[9].Woonplaats = "9";

            // Act
            Globals.Alg1NawArrayMethodCalled = false;

            array.BubbleSort();

            Logger.Instance.Print();

            // Assert
            Assert.IsFalse(Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.IsSorted(), "De array is na de bubbleSort niet geordend. De items staan niet op de juiste volgorde.");
            Assert.AreEqual(7, Logger.Instance.RealSwaps.Count(), "Het aantal swaps dat gemaakt wordt is niet 7.");
        }

        [Test]
        [Category("WS2 Array Bubble Sort")]
        [Points(0.5)]
        [Code("BubbleSortableNawArrayUnordered", "BubbleSort")]
        [Scenario(@"We maken een BubbleSortableNawArrayUnordered aan en vullen die met woonplaats ""0"" tot ""9"", daarna wisselen we de 4 en de 8.
Bij een bubbelsort testen we nu of:
    8 tot aan de 9 omhoog bubbelt:  4 stappen
    7 voorbij de 4 bubbelt:         1 stap
    6 voorbij de 4 bubbelt:         1 stap
    5 voorbij de 4 bubbelt:         1 stap
    ------------------------------------------
                                    7 stappen in totaal.
De overige items wisselen niet van plaats, we testen of deze ook geset worden met het originele item als ze niet wisselen.")]
        public void BubbleSortableNawArrayUnordered_BubbleSort_EightAndFourSwapped_ShouldNotSetWhenNotSwapped()
        {
            // Arrange
            var expectedLength = 10;
            var array = TestSubject(expectedLength);
            var testSet = Decorator.GenerateAndFill(array, expectedLength);

            testSet[0].Woonplaats = "0";
            testSet[1].Woonplaats = "1";
            testSet[2].Woonplaats = "2";
            testSet[3].Woonplaats = "3";
            testSet[4].Woonplaats = "8";
            testSet[5].Woonplaats = "5";
            testSet[6].Woonplaats = "6";
            testSet[7].Woonplaats = "7";
            testSet[8].Woonplaats = "4";
            testSet[9].Woonplaats = "9";

            // Act
            Globals.Alg1NawArrayMethodCalled = false;

            array.BubbleSort();

            // Assert
            Assert.IsFalse(Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.IsSorted(), "De array is na de bubbleSort niet geordend. De items staan niet op de juiste volgorde.");
            Assert.AreEqual(7, Logger.Instance.RealSwaps.Count(), "Het aantal swaps dat gemaakt wordt is niet 7.");
            Assert.AreEqual(0, Logger.Instance.LogItems.Count(li => li.ArrayAction == ArrayAction.SWAP && li.Index1 == li.Index2),
                    "Je zet verschillende items opnieuw met dezelfde waarde. Dit is niet nodig.");
        }

        [Test]
        [Category("WS2 Array Bubble Sort")]
        [Points(0.5)]
        [Code("BubbleSortableNawArrayUnordered", "BubbleSort")]
        [Scenario(@"We maken een BubbleSortableNawArrayUnordered aan en vullen die met woonplaats ""0"" tot ""0"", daarna wisselen we de 0 en de 4.
Bij een bubbelsort testen we nu of:
    4 tot aan de 5 omhoog bubbelt:  4 stappen
    3 voorbij de 0 bubbelt:         1 stap
    2 voorbij de 0 bubbelt:         1 stap
    1 voorbij de 0 bubbelt:         1 stap
    ------------------------------------------
                                    7 stappen in totaal.
We testen of de 0 als laatst geset is (dus of hij vooraan begonnen is met de bubbelsort.")]
        public void BubbleSortableNawArrayUnordered_BubbleSort_ShouldStartWithFirst()
        {
            // Arrange
            var expectedLength = 10;
            var array = TestSubject(expectedLength);
            var testSet = Decorator.GenerateAndFill(array, expectedLength);

            testSet[0].Woonplaats = "4";
            testSet[1].Woonplaats = "1";
            testSet[2].Woonplaats = "2";
            testSet[3].Woonplaats = "3";
            testSet[4].Woonplaats = "0";
            testSet[5].Woonplaats = "5";
            testSet[6].Woonplaats = "6";
            testSet[7].Woonplaats = "7";
            testSet[8].Woonplaats = "8";
            testSet[9].Woonplaats = "9";

            // Act
            Globals.Alg1NawArrayMethodCalled = false;

            array.BubbleSort();

            // Assert
            Assert.IsFalse(Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.IsSorted(), "De array is na de bubbleSort niet geordend. De items staan niet op de juiste volgorde.");

            var swaps = Logger.Instance.RealSwaps.ToList();
            // We moeten eerst alles naar achteren schuiven voordat er voor de 0 plaats is.
            Assert.AreEqual(7, swaps.Count, "Het aantal swaps dat gemaakt wordt is niet 7.");
            Assert.AreEqual("0", swaps[6].NewNaw1.Woonplaats, "Het laatste item dat gezet wordt is niet degene op de eerste plaats. Hij bubbelt niet van voor naar achter.");
        }
    }
}
