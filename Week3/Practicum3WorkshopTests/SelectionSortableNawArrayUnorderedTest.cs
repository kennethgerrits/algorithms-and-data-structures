using Alg1.Practica.TestBase.Attributes;
using Alg1.Practica.TestBase.Utils;
using Alg1.Practica.Utils.Logging;
using Alg1.Practica.Utils.Models;
using Alg1.Practica.Practicum3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alg1.Practica.TestBase.Utils.Decorators;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.Practicum3.Test
{
    [TestFixture]
    public class SelectionSortableNawArrayUnorderedTest
    {
        #region Setup and Teardown
        [SetUp]
        public void SelectionSortableNawArrayUnordered_TestInitialize()
        {
            Logger.Instance.ClearLog();
            Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts = false;
        }

        [TearDown]
        public void SelectionSortableNawArrayUnordered_TestCleanup()
        {
            Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts = true;
        }

        #endregion

        #region SelectionSort
        
        private static TestableSelectionSortableNawArrayUnordered<SelectionSortableNawArrayUnordered> New(int capacity)
            => new TestableSelectionSortableNawArrayUnordered<SelectionSortableNawArrayUnordered>(
                ExeTimed(() => new SelectionSortableNawArrayUnordered(capacity)));

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS3 Array SelectionSort")]
        [Scenario(@"We maken een lege SelectionSortableNawArrayUnordered aan en gaan hier een SelectionSort op uitvoeren.
De methode moet niets uitvoeren en meteen stoppen.")]
        [Code("SelectionSortableNawArrayUnordered", "SelectionSort")]
        public void SelectionSortableNawArrayUnordered_SelectionSort_EmptyArray_ShouldNotSort()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 0;
            var array = ArrayExtensions.InitializeTestSubject(New(10), expectedLength, out testSet);
            var newNaw = RandomNawGenerator.New();

            // Act
            array.SelectionSort();

            // Assert
            Assert.AreEqual(0, Logger.Instance.LogItems.Count(), "Je controleert items terwijl de array leeg is. Dit is niet nodig.");
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS3 Array SelectionSort")]
        [Scenario(@"We maken een SelectionSortableNawArrayUnordered aan zorgen dat de items in de juiste volgorde staan. We gaan hier een SelectionSort op uitvoeren.
De methode moet alle items checken en er dan achter komen dat hij niets hoeft te wisselen.")]
        [Code("SelectionSortableNawArrayUnordered", "SelectionSort")]
        public void SelectionSortableNawArrayUnordered_SelectionSort_SortedArray_ShouldNotSwapDifferentIndexes()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = ArrayExtensions.InitializeTestSubject(New(expectedLength), expectedLength, out testSet, orderAscending: true);
            var newNaw = RandomNawGenerator.New();

            // Act
            array.SelectionSort();

            // Assert
            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de SelectionSort");
            Assert.IsTrue(Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.GET).Count() >= expectedLength, "Je bent niet door de hele lijst heen gelopen om te controleren of hij geordend is.");
            Assert.AreEqual(0, Logger.Instance.RealSwaps.Count(), "De lijst was vooraf al geordend. Toch heb je items geswitched. Dit klopt niet.");
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS3 Array SelectionSort")]
        [Scenario(@"We maken een SelectionSortableNawArrayUnordered aan zorgen dat de items in de juiste volgorde staan. We gaan hier een SelectionSort op uitvoeren.
De methode moet alle items checken en items niet swappen als ze op dezelfde index staan (ruilen met zichzelf).")]
        [Code("SelectionSortableNawArrayUnordered", "SelectionSort")]
        public void SelectionSortableNawArrayUnordered_SelectionSort_SortedArray_ShouldNotSwapAtAll()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = ArrayExtensions.InitializeTestSubject(New(expectedLength), expectedLength, out testSet, orderAscending: true);
            var newNaw = RandomNawGenerator.New();

            // Act
            array.SelectionSort();

            // Assert
            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de SelectionSort");
            Assert.IsTrue(Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.GET).Count() >= expectedLength, "Je bent niet door de hele lijst heen gelopen om te controleren of hij geordend is.");
            Assert.AreEqual(0, Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.SWAP).Count()
                , "De lijst was vooraf al geordend. Toch heb je items geswitched (ook met gelijke index). Dit klopt niet.");
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS3 Array SelectionSort")]
        [Scenario(@"We maken een SelectionSortableNawArrayUnordered aan zorgen dat de items precies verkeerd om staan. 
We voeren een SelectionSort uit, deze moet in de helft aan stappen van het aantal items alles om kunnen draaien.")]
        [Code("SelectionSortableNawArrayUnordered", "SelectionSort")]
        public void SelectionSortableNawArrayUnordered_SelectionSort_SortedArrayDescending_ShouldSwapAllItems()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = ArrayExtensions.InitializeTestSubject(New(expectedLength), expectedLength, out testSet, orderDescending: true);
            var newNaw = RandomNawGenerator.New();

            // Act
            array.SelectionSort();

            // Assert
            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de SelectionSort");
            Assert.IsTrue(Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.GET).Count() >= expectedLength, "Je bent niet door de hele lijst heen gelopen om te controleren of hij geordend is.");
            // We expect half the length of the array to be swapped.
            // This occurs because when ordered descending when we swap we coincidentally set two items right.
            Assert.AreEqual(expectedLength / 2, Logger.Instance.RealSwaps.Count(), "De array was vooraf descending geordend. Met de helft van de lengte aan swaps zou je een geordende array moeten hebben.");
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS3 Array SelectionSort")]
        [Scenario(@"We maken een SelectionSortableNawArrayUnordered aan zorgen dat de items goed staan, behalve dat het item dat achteraan moet juist vooraan staat. 
We voeren een SelectionSort uit, deze moet de laagste elke keer wisselen met deze laatste waardoor die omhoog bubbelt.")]
        [Code("SelectionSortableNawArrayUnordered", "SelectionSort")]
        public void SelectionSortableNawArrayUnordered_SelectionSort_NineOnFront_WholeArrayShifted()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = ArrayExtensions.InitializeTestSubject(New(expectedLength), expectedLength, out testSet);
            var newNaw = RandomNawGenerator.New();

            testSet[0].Woonplaats = "9";
            testSet[1].Woonplaats = "0";
            testSet[2].Woonplaats = "1";
            testSet[3].Woonplaats = "2";
            testSet[4].Woonplaats = "3";
            testSet[5].Woonplaats = "4";
            testSet[6].Woonplaats = "5";
            testSet[7].Woonplaats = "6";
            testSet[8].Woonplaats = "7";
            testSet[9].Woonplaats = "8";

            // Act
            array.SelectionSort();

            // Assert
            var realSwaps = Logger.Instance.RealSwaps.ToList();
            var logItems = Logger.Instance.LogItems.ToList();
            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de SelectionSort");
            Assert.AreEqual(expectedLength - 1, realSwaps.Count, "Het laatste item staat vooraan, de rest staat juist. We verwachten dat je het totale aantal -1 nodig hebt om de rest voor de laatste te zetten.");

            for (int i = 0; i < expectedLength - 1; i++)
            {
                Assert.AreEqual(i, realSwaps[i].Index1);
                Assert.AreEqual(i + 1, realSwaps[i].Index2);

                var indexOfSwap = logItems.IndexOf(realSwaps[i]);

                // We assert to not touch previous touched items.
                Assert.IsFalse(logItems.Skip(indexOfSwap + 1).Any(li => li.Index1 <= realSwaps[i].Index1), "Wanneer je een deel van je lijst gesorteerd hebt, moet je de rest laten staan.");
            }
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS3 Array SelectionSort")]
        [Scenario(@"We maken een SelectionSortableNawArrayUnordered aan zorgen dat alle items dezelfde woonplaats hebben.
De SelectionSort moet dan nog steeds op de andere properties sorteren.")]
        [Code("SelectionSortableNawArrayUnordered", "SelectionSort")]
        public void SelectionSortableNawArrayUnordered_SelectionSort_AllTheSameWoonplaats_IsInOrder()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = ArrayExtensions.InitializeTestSubject(New(expectedLength), expectedLength, out testSet);
            var newNaw = RandomNawGenerator.New();

            foreach (var item in testSet)
            {
                item.Woonplaats = "Allemaal dezelfde woonplaats";
            }

            // Act
            array.SelectionSort();

            // Assert
            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de SelectionSort");
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS3 Array SelectionSort")]
        [Scenario(@"We maken een SelectionSortableNawArrayUnordered aan zorgen dat alle items dezelfde woonplaats en naam hebben.
De SelectionSort moet dan nog steeds op de andere properties sorteren.")]
        [Code("SelectionSortableNawArrayUnordered", "SelectionSort")]
        public void SelectionSortableNawArrayUnordered_SelectionSort_AllTheSameWoonplaatsAndNaam_IsInOrder()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = ArrayExtensions.InitializeTestSubject(New(expectedLength), expectedLength, out testSet);
            var newNaw = RandomNawGenerator.New();

            foreach (var item in testSet)
            {
                item.Woonplaats = "Allemaal dezelfde woonplaats";
                item.Naam = "Allemaal dezelfde naam";
            }

            // Act
            array.SelectionSort();

            // Assert
            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de SelectionSort");
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS3 Array SelectionSort")]
        [Scenario(@"We maken een SelectionSortableNawArrayUnordered aan met de items: 0, 1, 2, 3, 8, 5, 6, 7, 4, 9
De SelectionSort moet dan de 4 met de 8 ruilen en daarna niets meer swappen.")]
        [Code("SelectionSortableNawArrayUnordered", "SelectionSort")]
        public void SelectionSortableNawArrayUnordered_SelectionSort_SortedArrayDescending_ShouldHaveRightBounds()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = ArrayExtensions.InitializeTestSubject(New(expectedLength), expectedLength, out testSet);
            var newNaw = RandomNawGenerator.New();

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
            array.SelectionSort();

            // Assert
            var realSwaps = Logger.Instance.RealSwaps.ToList();
            var logItems = Logger.Instance.LogItems.ToList();
            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de SelectionSort");
            Assert.AreEqual(1, realSwaps.Count, "Wanneer je de 4 hebt gevonden en ruilt met de 8 zou je daarna geen swaps meer hoeven doen.");

            // We assert to not touch previous touched items.
            var indexOfSwap = logItems.IndexOf(realSwaps[0]);
            Assert.IsFalse(logItems.Skip(indexOfSwap + 1).Any(li => li.Index1 <= realSwaps[0].Index1), "We verwachten dat je geen swaps doet voor index 4.");
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS3 Array SelectionSort")]
        [Scenario(@"We maken een SelectionSortableNawArrayUnordered aan met de items: 9, 0, 1, 2, 3, 4, 5, 6, 7, 8
De SelectionSort moet starten van voor naar achteren waardoor de 9 als laatste goed gezet wordt.")]
        [Code("SelectionSortableNawArrayUnordered", "SelectionSort")]
        public void SelectionSortableNawArrayUnordered_SelectionSort_ShouldStartWithFirst()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = ArrayExtensions.InitializeTestSubject(New(expectedLength), expectedLength, out testSet);
            var newNaw = RandomNawGenerator.New();

            testSet[0].Woonplaats = "9";
            testSet[1].Woonplaats = "0";
            testSet[2].Woonplaats = "1";
            testSet[3].Woonplaats = "2";
            testSet[4].Woonplaats = "3";
            testSet[5].Woonplaats = "4";
            testSet[6].Woonplaats = "5";
            testSet[7].Woonplaats = "6";
            testSet[8].Woonplaats = "7";
            testSet[9].Woonplaats = "8";

            // Act
            array.SelectionSort();

            // Assert
            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de SelectionSort");

            var logItems = Logger.Instance.LogItems.ToList();
            var realSwaps = Logger.Instance.RealSwaps.ToList();

            Assert.AreEqual(expectedLength - 1, realSwaps.Count, "We verwachten dat je alle items voor de 9 moet gaan zetten, daarom heb je 8 swaps nodig.");
            for (int i = 0; i < realSwaps.Count; i++)
            {
                var indexOf = logItems.IndexOf(realSwaps[i]);
                var range = logItems.Take(indexOf);
                Assert.AreEqual(i, range.Min(li => li.Index1), "Je hebt niet de kleinste uit de overgebleven set gepakt.");
                Assert.AreEqual(expectedLength - 1, range.Max(li => li.Index1), "Je hebt niet de kleinste uti de overgebleven set gepakt.");

                logItems = logItems.Skip(indexOf + 1).ToList();
            }
        }

        #endregion

    }
}
