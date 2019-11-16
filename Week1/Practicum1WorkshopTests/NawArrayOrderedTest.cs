using System;
using NUnit.Framework;
using Alg1.Practica.Utils.Models;
using Alg1.Practica.TestBase.Utils;
using Alg1.Practica.Utils.Exceptions;
using System.Linq;
using Alg1.Practica.Utils.Logging;
using Alg1.Practica.TestBase.Attributes;
using Alg1.Practica.TestBase.Utils.Decorators;
using Alg1.Practica.Utils;
using static Alg1.Practica.Practicum1.Test.TestHelpers;

namespace Alg1.Practica.Practicum1.Test.Workshop
{
    [TestFixture]
    public class NawArrayOrderedTest
    {
        #region Setup and Teardown

        [SetUp]
        public void NawArrayOrdered_TestInitialize()
        {
            Logger.Instance.ClearLog();
            Globals.ShowAlg1NawArrayAlerts = false;
        }

        [TearDown]
        public void NawArrayOrdered_TestCleanup()
        {
            Globals.ShowAlg1NawArrayAlerts = true;
        }

        #endregion

        private static INawArray ArrayOfSize(int size) => new TestableArray(new NawArrayOrdered(size));

        #region Constructor

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS1 Array Ordered")]
        [Scenario(
            @"We roepen de constructor van NawArrayOrdered aan met initiële groottes van één tot en met één miljoen. Je constructor mag hierbij geen uitzonderingen opwerpen")]
        public void NawArrayOrdered_Constructor_ValidInitialSize_ThrowsNoException()
        {
            int size = 1;
            while (size <= 1000000)
            {
                Assert.DoesNotThrow(
                    () => ArrayOfSize(size),
                    $"Bij een geldige grootte mag je constructor geen uitzondering opwerpen. Jouw constructor doet dit toch voor een initiële grootte van {size}"
                );
                size *= 10;
            }
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS1 Array Ordered")]
        [Scenario(
            @"We roepen de constructor van NawArrayOrdered aan met een aantal groottes kleiner dan 1 (dus 0 of negatieve waardes). We verwachten dat jouw constructor een NawArrayOrderedInvalidSizeException opwerpt")]
        public void NawArrayOrdered_Constructor_Size0_ThrowsException()
        {
            int[] initialSizes = {0, -100, -1};
            foreach (var initialSize in initialSizes)
            {
                Assert.Throws<NawArrayOrderedInvalidSizeException>(
                    () => ArrayOfSize(initialSize),
                    $"Bij een initiële grootte van {initialSize} werpt jouw constructor geen NawArrayOrderedInvalidSizeException op."
                );
            }
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS1 Array Ordered")]
        [Scenario(
            @"We roepen de constructor van NawArrayOrdered aan met een aantal groottes groter dan één miljoen. We verwachten dat jouw constructor een NawArrayOrderedInvalidSizeException opwerpt")]
        public void NawArrayOrdered_Constructor_Size1000001_ThrowsException()
        {
            const int maxInitialSize = 1000000;
            int[] offsets = {1, 100, 1000000};
            foreach (var offset in offsets)
            {
                var initialSize = maxInitialSize + offset;
                Assert.Throws<NawArrayOrderedInvalidSizeException>(
                    () => ArrayOfSize(initialSize),
                    $"Jouw constructor werpt geen NawArrayOrderedInvalidSizeException bij een initiële grootte van {initialSize}"
                );
            }
        }

        #endregion

        #region Add

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Code("NawArrayOrdered", "Add")]
        [Category("WS1 Array Ordered")]
        [Scenario(
            @"We maken een NawArrayOrdered aan met een grootte van 10, hier voegen we 10 items aan toe. Dit mag geen foutmelding opleveren.")]
        public void NawArrayOrdered_Add_FillWholeArray_ShouldFit()
        {
            // Arrange
            var expectedSize = 10;
            var expectedNaws = RandomNawGenerator.NewArray(expectedSize);

            var array = ArrayOfSize(expectedSize);

            // Act

            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;
            for (int i = 0; i < expectedSize; i++)
            {
                try
                {
                    array.Add(expectedNaws[i]);
                    Assert.AreEqual(i + 1, array.Count,
                        "\n\nNawArrayOrdered.Add(): Het aantal elementen in de array komt niet overeen met het aantal toegevoegde items.");
                }
                catch (NawArrayOrderedOutOfBoundsException)
                {
                    // Assert
                    Assert.Fail(
                        "\n\nNawArrayOrdered.Add(): Er konden maar {0} NAW-objecten aan een array die met omvang {1} is geinitialiseerd worden toegevoegd",
                        i, expectedSize);
                }
            }

            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled,
                "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Code("NawArrayOrdered", "Add")]
        [Category("WS1 Array Ordered")]
        [Scenario(@"We maken een NawArrayOrdered aan met een grootte van 3, hier voegen we 4 items aan toe. 
Dit moet een NawArrayOrderedOutOfBoundsException opleveren.")]
        public void NawArrayOrdered_Add_OneTooMany_ShouldThrowException()
        {
            // Arrange
            NAW[] testSet;
            var array = InitializeTestsubject(3, 3, out testSet);
            var oneTooMany = RandomNawGenerator.New();

            // Act

            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

            Assert.Throws<NawArrayOrderedOutOfBoundsException>(() => array.Add(oneTooMany),
                "NawArrayOrdered.Add(): Toevoegen van 11e element aan array met omvang van 10 geeft geen exceptie");

            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled,
                "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS1 Array Ordered")]
        [Code("NawArrayOrdered", "Add")]
        [Scenario(@"We maken een NawArrayOrdered aan en hier voegen we willekeurige items aan toe. 
De array moet elke keer bij het toevoegen in de juiste volgorde blijven staan.")]
        public void NawArrayOrdered_Add_Valid_ShouldAddInOrder()
        {
            // Arrange
            char[] woonplaatsen = "abcde".ToCharArray();
            NAW[] testSet = RandomNawGenerator.NewArray(5);
            testSet[0].Woonplaats = woonplaatsen[3].ToString();
            testSet[1].Woonplaats = woonplaatsen[2].ToString();
            testSet[2].Woonplaats = woonplaatsen[4].ToString();
            testSet[3].Woonplaats = woonplaatsen[0].ToString();
            testSet[4].Woonplaats = woonplaatsen[1].ToString();

            WithOrderedArray(capacity: 20, filled: 0, f: array =>
            {
                WithoutCallingArrayMethods(() =>
                {
                    foreach (var naw in testSet)
                    {
                        array.Add(naw);
                        Assert.IsTrue(array.CheckIsGesorteerd(),
                            "De Add-methode van NawArrayOrdered moet ervoor zorgen dat de elementen in de array altijd in de juiste volgorde staan. Jouw methode doet dit niet.");
                    }
                });
            });
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS1 Array Ordered")]
        [Code("NawArrayOrdered", "Add")]
        [Scenario(@"We maken een NawArrayOrdered aan en hier voegen we willekeurige items aan toe. 
De array moet elke keer ruimte maken om het nieuwe item in te voegen. Hij mag niet te veel items verschuiven.")]
        public void NawArrayOrdered_Add_Valid_ShouldMoveTheRightNumberOfItems()
        {
            // Arrange
            char[] woonplaatsen = "acegi".ToCharArray();
            NAW[] testSet = RandomNawGenerator.NewArray(5);
            testSet[0].Woonplaats = woonplaatsen[3].ToString();
            testSet[1].Woonplaats = woonplaatsen[2].ToString();
            testSet[2].Woonplaats = woonplaatsen[4].ToString();
            testSet[3].Woonplaats = woonplaatsen[0].ToString();
            testSet[4].Woonplaats = woonplaatsen[1].ToString();

            var array = ArrayOfSize(20);

            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = false;

            for (int i = 0; i < testSet.Length; i++)
            {
                // Act
                array.Add(testSet[i]);
            }

            Logger.Instance.ClearLog();
            array.Add(new NAW() {Woonplaats = "f"});

            var setters = Logger.Instance.LogItems.Where(li => li.ArrayAction == ArrayAction.SET);
            Assert.AreEqual(3, setters.Count(),
                "\n\nNawArrayOrdered.Add(): Er worden te veel of te weinig elementen verschoven ({0}, i.p.v. {1}) om ruimte te maken voor het nieuwe element.",
                setters.Count(), 3);
            Assert.IsTrue(setters.Any(li => li.NewNaw1.Woonplaats == "f"),
                "Naw met woonplaats f had verplaatst moeten worden, dit is niet gebeurd.");
            Assert.IsTrue(setters.Any(li => li.NewNaw1.Woonplaats == "g"),
                "Naw met woonplaats g had verplaatst moeten worden, dit is niet gebeurd.");
            Assert.IsTrue(setters.Any(li => li.NewNaw1.Woonplaats == "i"),
                "Naw met woonplaats i had verplaatst moeten worden, dit is niet gebeurd.");
            //     Assert.IsTrue(array.CheckIsGesorteerd()); Wordt al bij andere testcase beoordeeld.
            Assert.IsFalse(Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled,
                "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");
        }

        #endregion

        #region ItemCount

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Code("NawArrayOrdered", "ItemCount")]
        [Category("WS1 Array Ordered")]
        [Scenario(
            @"We maken verschillende NawArrayOrdered instanties aan en vullen deze met een aantal elementen. ItemCount moet altijd het aantal elementen in de array teruggeven")]
        public void NawArrayOrdered_ItemCount_ShouldReturnItemCount()
        {
            int[] filleds = {0, 10, 100};
            int[] capacities = {1, 10, 100};
            for (int c = 0; c < capacities.Length; ++c)
            {
                for (int f = 0; f <= c; ++f)
                {
                    var capacity = capacities[c];
                    var filled = filleds[f];
                    WithOrderedArray(capacity, filled, arr =>
                        Assert.AreEqual(filled, arr.ItemCount(),
                            $"We vulden een NawArrayOrdered van grootte {capacity} met {filled} items. Jouw versie van ItemCount() zegt echter dat er {arr.ItemCount()} items inzitten.")
                    );
                }
            }
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Code("NawArrayOrdered", "ItemCount")]
        [Category("WS1 Array Ordered")]
        [Scenario(
            @"We maken verschillende NawArrayOrdered aan en vullen deze met een aantal elementen. ItemCount moet het aantal elementen kunnen teruggeven zonder naar de onderliggende Alg1NawArray te kijken.")]
        public void NawArrayOrdered_ItemCount_ShouldRunInConstantTime()
        {
            int[] filleds = {0, 10, 100};
            int[] capacities = {1, 10, 100};
            for (int c = 0; c < capacities.Length; ++c)
            {
                for (int f = 0; f <= c; ++f)
                {
                    var capacity = capacities[c];
                    var filled = filleds[f];
                    WithOrderedArray(capacity, filled, arr =>
                    {
                        Logger.Instance.ClearLog();
                        arr.ItemCount();
                        var logItems = Logger.Instance.LogItems.Count();
                        Assert.AreEqual(logItems, 0,
                            $"Een efficiente implementatie van de methode ItemCount hoeft niet naar de onderliggende Alg1NawArray te kijken. Jouw implementatie doet dit wel.");
                    });
                }
            }
        }

        #endregion


        public static INawArray InitializeTestsubject(int maxSize, int initialFilledSize, out NAW[] testSet,
            int? maxStringLenght = null)
        {
            testSet = RandomNawGenerator.NewArray(initialFilledSize).OrderAscending();
            var array = ArrayOfSize(maxSize);

            Array.ForEach(testSet, naw => array.Add(naw));

            // We have to clear the log because adding to the array will cause the logger to log as well.
            Logger.Instance.ClearLog();
            return array;
        }
    }
}