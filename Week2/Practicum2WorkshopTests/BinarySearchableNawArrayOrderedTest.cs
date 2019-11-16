using System;
using NUnit.Framework;
using Alg1.Practica.Utils.Models;
using Alg1.Practica.TestBase.Utils;
using Alg1.Practica.Utils.Logging;
using static Alg1.Practica.TestBase.Utils.Complexity;
using System.Linq;
using Alg1.Practica.TestBase.Attributes;
using Alg1.Practica.TestBase.Utils.Decorators;

namespace Alg1.Practica.Practicum2.Test.Workshop
{
    [TestFixture]
    public class BinarySearchableNawArrayOrderedTest
    {
        [SetUp]
        public void BinarySearchNawArray_TestInitialize()
        {
            Logger.Instance.ClearLog();
            Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts = false;
        }

        [TearDown]
        public void BinarySearchNawArray_TestCleanup()
        {
            Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts = true;
        }

        public static TestableBinarySearchableOrderedArray<BinarySearchableNawArrayOrdered> TestSubject(int size) =>
            new TestableBinarySearchableOrderedArray<BinarySearchableNawArrayOrdered>(
                new BinarySearchableNawArrayOrdered(size));

        public static TestableBinarySearchableOrderedArray<BinarySearchableNawArrayOrdered> TestSubject(int size,
            out NAW[] testSet)
        {
            var array = TestSubject(size);
            testSet = Decorator.GenerateAndFill(array, size, Decorator.DesiredOrdering.Ascending);
            return array;
        }
        
        public static TestableBinarySearchableOrderedArray<BinarySearchableNawArrayOrdered> TestSubject(int size,
            int filled,
            out NAW[] testSet)
        {
            var array = TestSubject(size);
            testSet = Decorator.GenerateAndFill(array, filled, Decorator.DesiredOrdering.Ascending);
            return array;
        }

        [Test]
        [Points(1.0)]
        [Category("WS2 Binary Search")]
        [Code("BinarySearchableNawArrayOrdered", "FindBinary")]
        [Scenario("We maken een BinarySearchableNawArrayOrdered aan maar laten deze leeg. We " +
                  "zoeken met FindBinary naar een item. We verwachten dat jouw implementatie doorheeft dat" +
                  "de array leeg is en dus niet gaat zoeken.")]
        public void NawArrayOrdered_FindBinary_EmptyArray_ShouldHaveNoSteps()
        {
            // Arrange
            var expectedLength = 10;
            var emptyArray = TestSubject(expectedLength);

            // Act
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;
            emptyArray.FindBinary(new NAW("Naam", "Adres", "Woonplaats"));

            // Assert
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.AreEqual(0, Logger.Instance.LogItems.Count(), "\n\nNawArrayOrdered.FindBinary(): Er wordt onnodig gezocht in een lege array.\n");
        }



        [Test]
        [Points(2.0)]
        [Category("WS2 Binary Search")]
        [Code("BinarySearchableNawArrayOrdered", "FindBinary")]
        [Scenario("We maken een BinarySearchableNawArrayOrdered met 100 NAW-items. Vervolgens " +
                  "zoeken we met FindBinary naar een item. We verwachten dat jouw implementatie  dit in Log2(100) " +
                  " stappen (ongeveer 7) kan.")]
        public void NawArrayOrdered_FindBinary_LastItem_ShouldNotTakeMoreThanLogN()
        {
            int[] expectedIndices = { 1, 23, 47, 99 };
            foreach (var expectedIndex in expectedIndices)
            {
                Logger.Instance.ClearLog();
                // Arrange
                NAW[] testSet;
                var expectedLength = 100;
                var expectedSearches = Math.Ceiling(Log2(expectedLength));
                var array = TestSubject(100, out testSet);

                // Act
                Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

                var actualIndex = array.FindBinary(testSet[expectedIndex]);

                // Assert
                Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

                Assert.AreEqual(expectedIndex, actualIndex, "Je hebt niet de juiste index gevonden.");
                var actualSearches = Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.GET).Count();

                // ik keur hier het bekijken of het eerste item kleiner of het laaste item groter dan het gezochte item is goed.
                // vandaar de +2.
                Assert.LessOrEqual(actualSearches, expectedSearches + 2, $"We verwachten dat FindBinary maximaal " +
                                   $"{expectedSearches + 2} items in de array moet bekijken. Jouw implementatie moest " +
                                   $"dit {actualSearches} doen.");

            }
        }

    }
}
