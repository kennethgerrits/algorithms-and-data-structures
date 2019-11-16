using System;
using NUnit.Framework;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Alg1.Practica.Utils.Logging;
using Alg1.Practica.Utils.Models;
using Alg1.Practica.TestBase.Utils;
using Alg1.Practica.Practicum3;
using Alg1.Practica.TestBase.Attributes;
using Alg1.Practica.TestBase.Utils.Decorators;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.Practicum3.Test.Workshop
{
    [TestFixture]
    public class InsertionSortableNawArrayUnorderedTest
    {
        #region Setup and Teardown
        [SetUp]
        public void InsertionSortableNawArrayUnordered_TestInitialize()
        {
            Logger.Instance.ClearLog();
            Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts = false;
        }

        [TearDown]
        public void InsertionSortableNawArrayUnordered_TestCleanup()
        {
            Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts = true;
        }
        #endregion

        private static TestableInsertionSortableNawArrayUnordered<InsertionSortableNawArrayUnordered> New(int capacity)
            => new TestableInsertionSortableNawArrayUnordered<InsertionSortableNawArrayUnordered>(
                ExeTimed(() => new InsertionSortableNawArrayUnordered(capacity)));
        #region InsertionSort

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS3 Array InsertionSort")]
        [Scenario(@"We maken een lege InsertionSortableNawArrayUnordered aan en gaan hier een InsertionSort op uitvoeren.
De methode moet niets uitvoeren en meteen stoppen.")]
        [Code("InsertionSortableNawArrayUnordered", "InsertionSort")]
        public void InsertionSortableNawArrayUnordered_InsertionSort_EmptyArray_ShouldNotSort()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 0;
            var array = ArrayExtensions.InitializeTestSubject(New(10), expectedLength, out testSet);
            var newNaw = RandomNawGenerator.New();

            // Act
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

            array.InsertionSort();

            // Assert
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.AreEqual(0, Logger.Instance.LogItems.Count(), "Je controleert items terwijl de array leeg is. Dit is niet nodig.");
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS3 Array InsertionSort")]
        [Scenario(@"We maken een InsertionSortableNawArrayUnordered aan zorgen dat de items in de juiste volgorde staan. We gaan hier een InsertionSort op uitvoeren.
De methode moet alle items checken en er dan achter komen dat hij niets hoeft te wisselen.")]
        [Code("InsertionSortableNawArrayUnordered", "InsertionSort")]
        public void InsertionSortableNawArrayUnordered_InsertionSort_SortedArray_ShouldNotSetAnyNewIndexes()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = ArrayExtensions.InitializeTestSubject(New(expectedLength), expectedLength, out testSet, orderAscending: true);
            var newNaw = RandomNawGenerator.New();

            // Act
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

            array.InsertionSort();

            // Assert
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de InsertionSort.");
            Assert.IsTrue(Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.GET).Count() >= expectedLength, "Je bent niet door de hele lijst heen gelopen om te controleren of hij geordend is.");
            Assert.AreEqual(0, Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.SET &&
                                                                    li.NewNaw1 != li.OldNaw1).Count(), "De lijst was vooraf al geordend. Toch heb je items geswitched. Dit klopt niet.");
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS3 Array InsertionSort")]
        [Scenario(@"We maken een InsertionSortableNawArrayUnordered aan zorgen dat de items precies verkeerd om staan. 
We voeren een InsertionSort uit, deze moet in n!-1 alle items juist invoegen omdat hij elke keer alles moet verschuiven.")]
        [Code("InsertionSortableNawArrayUnordered", "InsertionSort")]
        public void InsertionSortableNawArrayUnordered_InsertionSort_SortedArrayDescending_ShouldSetAllItemsButOne()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 100;
            var array = ArrayExtensions.InitializeTestSubject(New(expectedLength), expectedLength, out testSet, orderDescending: true);
            var newNaw = RandomNawGenerator.New();

            // Act
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

            array.InsertionSort();

            // Assert
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de InsertionSort.");
            Assert.AreEqual(expectedLength.SumAllSmallerIncSelf() - 1, Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.SET).Count(),
                "We verwachten dat je na elke loop 1 item minder hoeft te zetten, dus dat je bij een lijst van 100, 100 faculteit - 1 items zet.");
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS3 Array InsertionSort")]
        [Scenario(@"We maken een InsertionSortableNawArrayUnordered aan zorgen dat alle items dezelfde woonplaats hebben.
De InsertionSort moet dan nog steeds op de andere properties sorteren.")]
        [Code("InsertionSortableNawArrayUnordered", "InsertionSort")]
        public void InsertionSortableNawArrayUnordered_InsertionSort_AllTheSameWoonplaats_IsInOrder()
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
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

            array.InsertionSort();

            // Assert
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de InsertionSort.");
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS3 Array InsertionSort")]
        [Scenario(@"We maken een InsertionSortableNawArrayUnordered aan zorgen dat alle items dezelfde woonplaats en naam hebben.
De InsertionSort moet dan nog steeds op de andere properties sorteren.")]
        [Code("InsertionSortableNawArrayUnordered", "InsertionSort")]
        public void InsertionSortableNawArrayUnordered_InsertionSort_AllTheSameWoonplaatsAndNaam_IsInOrder()
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
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

            array.InsertionSort();

            // Assert
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de InsertionSort.");
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS3 Array InsertionSort")]
        [Scenario(@"We maken een InsertionSortableNawArrayUnordered aan met de items: 0, 1, 2, 3, 8, 5, 6, 7, 4, 9
De InsertionSort moet dan ruimte maken voor de 5, 6, 7, 4 en deze vervolgens zetten.")]
        [Code("InsertionSortableNawArrayUnordered", "InsertionSort")]
        public void InsertionSortableNawArrayUnordered_InsertionSort_EightAndFourSwapped_ShouldHaveRightBounds()
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
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

            array.InsertionSort();

            // Assert
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            // Logger.Instance.Print();

            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de InsertionSort.");

            int lowerBound = 4, upperBound = 8;

            NAW toMoveUp = testSet[lowerBound];
            var toMoveUpSetters = (from li in Logger.Instance.LogItems
                                   where li.NewNaw1 == toMoveUp && li.ArrayAction == ArrayAction.SET
                                    && li.NewNaw1 != li.OldNaw1 // This will be checked in another test
                                   orderby li.Index1
                                   select li).ToArray();
            // This one bubbles up slowly.
            for (int i = lowerBound + 1; i <= upperBound; i++)
            {
                Assert.IsTrue(toMoveUpSetters[i - lowerBound - 1].Index1 == i, "Er zouden 4 items verplaatst moeten zijn om ruimte te maken in het geordende deel.");
            }

            NAW toBeInsertedDown = testSet[upperBound];
            var toBeInsertedDownSetters = (from li in Logger.Instance.LogItems
                                           where li.NewNaw1 == toBeInsertedDown && li.ArrayAction == ArrayAction.SET
                                            && li.NewNaw1 != li.OldNaw1 // This will be checked in another test
                                           orderby li.Index1
                                           select li).ToArray();
            // This one moves down in an insertion way.
            Assert.AreEqual(1, toBeInsertedDownSetters.Count(), "Meerdere items zijn geïnsert in de sortering, dit had er 1 moeten zijn.");
            Assert.AreEqual(lowerBound, toBeInsertedDownSetters.First().Index1, "Item met woonplaats 4 zou op de 4e index geïnsert moeten zijn.");


            // All setters must be in between the bounds.
            Assert.IsTrue(Logger.Instance.LogItems
                    .Where(li => li.ArrayAction == ArrayAction.SET && li.NewNaw1 != li.OldNaw1)
                    .All(li => li.Index1 >= lowerBound && li.Index1 <= upperBound), "Je hebt items verplaatst die niet verplaatst hadden hoeven worden omdat ze al goed stonden.");
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS3 Array InsertionSort")]
        [Scenario(@"We maken een InsertionSortableNawArrayUnordered aan met de items: 0, 1, 2, 3, 8, 5, 6, 7, 4, 9
De InsertionSort moet dan ruimte maken voor de 5, 6, 7, 4 en deze vervolgens zetten, daarbij moet hij ook alle items die niet hoeven te verplaatsen negeren.")]
        [Code("InsertionSortableNawArrayUnordered", "InsertionSort")]
        public void InsertionSortableNawArrayUnordered_InsertionSort_EightAndFourSwapped_ShouldNotSetWhenNotSwapped()
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
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

            array.InsertionSort();

            // Assert
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de InsertionSort.");

            int lowerBound = 4, upperBound = 8;

            NAW toMoveUp = testSet[lowerBound];
            var toMoveUpSetters = (from li in Logger.Instance.LogItems
                                   where li.NewNaw1 == toMoveUp && li.ArrayAction == ArrayAction.SET
                                   orderby li.Index1
                                   select li).ToArray();

            // This one bubbles up slowly.
            for (int i = lowerBound + 1; i <= upperBound; i++)
            {
                Assert.IsTrue(toMoveUpSetters[i - lowerBound - 1].Index1 == i, "Er zouden 4 items verplaatst moeten zijn om ruimte te maken in het geordende deel.");
            }

            NAW toBeInsertedDown = testSet[upperBound];
            var toBeInsertedDownSetters = (from li in Logger.Instance.LogItems
                                           where li.NewNaw1 == toBeInsertedDown && li.ArrayAction == ArrayAction.SET
                                           orderby li.Index1
                                           select li).ToArray();
            // This one moves down in an insertion way.
            Assert.AreEqual(1, toBeInsertedDownSetters.Count(), "Meerdere items zijn geïnsert in de sortering, dit had er 1 moeten zijn.");
            Assert.AreEqual(lowerBound, toBeInsertedDownSetters.First().Index1, "Item met woonplaats 4 zou op de 4e index geïnsert moeten zijn.");


            // All setters must be in between the bounds.
            Assert.IsTrue(Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.SET).All(li => li.Index1 >= lowerBound && li.Index1 <= upperBound)
                , "Je hebt items gezet waarbij de oude index gelijk was aan de nieuwe, dit is niet nodig.");
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS3 Array InsertionSort")]
        [Scenario(@"We maken een InsertionSortableNawArrayUnordered aan met de items: 4, 1, 2, 3, 0, 5, 6, 7, 8, 9
De InsertionSort moet het geordende deel vooraan beginnen en de 4 als eerst naar achter verplaatsen.")]
        [Code("InsertionSortableNawArrayUnordered", "InsertionSort")]
        public void InsertionSortableNawArrayUnordered_InsertionSort_ShouldStartWithFirst()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = ArrayExtensions.InitializeTestSubject(New(expectedLength), expectedLength, out testSet);
            var newNaw = RandomNawGenerator.New();

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
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

            array.InsertionSort();

            // Assert
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(array.CheckIsGesorteerd(), "De array is niet gesorteerd na de InsertionSort.");

            var setters = Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.SET && li.OldNaw1 != li.NewNaw1).ToList();
            // We moeten eerst alles naar achteren schuiven voordat er voor de 0 plaats is.
            setters.Last().AssertAreEqual(0, "0", "Je moet het gesorteerde deel van voor naar achteren laten opbouwen. Dit gebeurt nu niet.");
        }

        #endregion

    }
}
