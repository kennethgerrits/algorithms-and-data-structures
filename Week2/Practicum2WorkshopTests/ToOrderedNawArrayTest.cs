using System;
using NUnit.Framework;
using Alg1.Practica.TestBase.Attributes;
using Alg1.Practica.Utils.Logging;
using Alg1.Practica.Utils.Models;
using Alg1.Practica.TestBase.Utils;
using Alg1.Practica.Practicum1;
using Alg1.Practica.TestBase.Utils.Decorators;

namespace Alg1.Practica.Practicum2.Test
{
    [TestFixture]
    public class ToOrderedNawArrayTest
    {
        #region Setup and Teardown

        [SetUp]
        public void ToOrderedNawArray_TestInitialize()
        {
            Logger.Instance.ClearLog();
            Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts = false;
        }

        [TearDown]
        public void ToOrderedNawArray_TestCleanup()
        {
            Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts = true;
        }

        #endregion

        static TestableOrderableNawArrayUnordered<OrderableNawArrayUnordered> TestSubject(int size) =>
            new TestableOrderableNawArrayUnordered<OrderableNawArrayUnordered>(new OrderableNawArrayUnordered(size));

        static TestableOrderableNawArrayUnordered<OrderableNawArrayUnordered> TestSubject(int size, out NAW[] testSet)
        {
            var array = TestSubject(size);
            testSet = Decorator.GenerateAndFill(array, size);
            return array;
        }
        #region ToNawOrdered

        [Test]
        [Timeout(30000)]
        [Category("WS2 Array ToNawArrayOrdered")]
        [Points(1.0)]
        [Scenario(@"We maken een NawArrayUnordered aan en hier voegen we willekeurige items aan toe.
We roepen vervolgens ToNawArrayOrdered aan en testen of de NawArrayOrdered dezelfde items heeft en of het er evenveel zijn.")]
        [Code("OrderableNawArrayUnordered", "ToNawArrayOrdered")]
        public void NawArrayUnordered_ToNawOrdered_OrderedArray_ShouldHaveSameItems()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = TestSubject(expectedLength, out testSet);

            // Act
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;
            var orderedArray = array.ToNawArrayOrdered();

            // Assert
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            // Act
            var matches = 0;
            for (var i = 0; i < orderedArray.Count; i++)
            {
                for (var j = 0; j < testSet.Length; ++j)
                {
                    if (testSet[j] != null && testSet[j].CompareTo(orderedArray.Array[i]) == 0)
                    {
                        testSet[j] = null;
                        ++matches;
                        break;
                    }
                }
            }
            

            // Assert
            Assert.IsTrue(orderedArray.ItemCount() == expectedLength, "\n\nNawArrayUnordered.ToNawArrayOrdered(): De geordende array die wordt teruggegeven heeft een andere itemCount ({0}) dan de oorspronkelijke ongesorteerde array ({1}).\n", orderedArray.ItemCount(), expectedLength);
            Assert.IsTrue(matches == expectedLength, "\n\nNawArrayUnordered.ToNawArrayOrdered(): De geordende array die wordt teruggegeven bevat niet alle elementen van de oorspronkelijke ongesorteerde array.\n");
        }


        [Test]
        [Timeout(30000)]
        [Category("WS2 Array ToNawArrayOrdered")]
        [Points(1.0)]
        [Scenario(@"We maken een NawArrayUnordered aan en hier voegen we willekeurige items aan toe.
We roepen vervolgens ToNawArrayOrdered aan en testen of de NawArrayOrdered geordend is.")]
        [Code("OrderableNawArrayUnordered", "ToNawArrayOrdered")]
        public void NawArrayUnordered_ToNawOrdered_OrderedArray_ShouldBeOrdered()
        {
            // Arrange
            NAW[] testSet;
            var expectedLength = 10;
            var array = TestSubject(expectedLength, out testSet);

            // Act
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

            var orderedArray = array.ToNawArrayOrdered();

            // Assert
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");

            Assert.IsTrue(orderedArray.CheckIsGesorteerd(), "\n\nNawArrayUnordered.ToNawArrayOrdered(): De geordende array die wordt teruggegeven is niet correct geordend.\n");
        }

        #endregion ToNawOrdered
    }

}
