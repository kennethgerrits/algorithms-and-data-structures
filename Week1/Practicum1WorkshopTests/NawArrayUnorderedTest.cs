using System;
using NUnit.Framework;
using Alg1.Practica;
using Alg1.Practica.TestBase.Utils;
using Alg1.Practica.TestBase.Utils.Decorators;
using Alg1.Practica.Utils.Exceptions;
using Alg1.Practica.Utils.Models;
using System.Linq;
using System.Diagnostics;
using Alg1.Practica.Utils.Logging;
using Alg1.Practica.TestBase.Attributes;
using static Alg1.Practica.Practicum1.Test.TestHelpers;
using Alg1.Practica.Utils;

namespace Alg1.Practica.Practicum1.Test.Workshop
{

    [TestFixture]
    public class NawArrayUnorderedTest
    {
        private static INawArray ArrayOfSize(int size) => new TestableArray(new NawArrayUnordered(size));

        #region Setup and Teardown
        [SetUp]
        public void NawArrayUnordered_TestInitialize()
        {
            Logger.Instance.ClearLog();
            Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts = false;
        }

        [TearDown]
        public void NawArrayUnordered_TestCleanup()
        {
            Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts = true;
        }

        #endregion

        #region Constructor
        [Test]
        [Timeout(10000)]
        [Category("WS1 Array Unordered")]
        [Scenario(@"We roepen de constructor van NawArrayUnordered aan met initiële groottes van één tot en met één miljoen. Je constructor mag hierbij geen uitzonderingen opwerpen")]
        public void NawArrayUnordered_Constructor_ValidInitialSize_ThrowsNoException()
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
        [Category("WS1 Array Unordered")]
        [Scenario(@"We roepen de constructor van NawArrayUnordered aan met een aantal groottes kleiner dan 1 (dus 0 of negatieve waardes). We verwachten dat jouw constructor een NawArrayUnorderedInvalidSizeException opwerpt")]
        public void NawArrayUnordered_Constructor_Size0_ThrowsException()
        {
            int[] initialSizes = { 0, -100, -1 };
            foreach (var initialSize in initialSizes)
            {
                Assert.Throws<NawArrayUnorderedInvalidSizeException>(
                    () => ArrayOfSize(initialSize),
                    $"Bij een initiële grootte van {initialSize} werpt jouw constructor geen NawArrayUnorderedInvalidSizeException op."
                );
            }
        }

        [Test]
        [Timeout(10000)]
        [Category("WS1 Array Unordered")]
        [Scenario(@"We roepen de constructor van NawArrayUnordered aan met een aantal groottes kleiner dan één miljoen. We verwachten dat jouw constructor een NawArrayUnorderedInvalidSizeException opwerpt")]
        public void NawArrayUnordered_Constructor_Size1000001_ThrowsException()
        {
            const int maxInitialSize = 1000000;
            int[] offsets = { 1, 100, 1000000 };
            foreach (var offset in offsets)
            {
                var initialSize = maxInitialSize + offset;
                Assert.Throws<NawArrayUnorderedInvalidSizeException>(
                    () => ArrayOfSize(initialSize),
                    $"Jouw constructor werpt geen NawArrayUnorderedInvalidSizeException bij een initiële grootte van {initialSize}"
                );
            }
        }
        #endregion

        #region ItemCount
        [Test]
        [Timeout(10000)]
        [Points(1)]
        [Code("NawArrayUnordered", "ItemCount")]
        [Category("WS1 Array Unordered")]
        [Scenario(@"We maken verschillende NawArrayUnordered instanties aan en vullen deze met een aantal elementen. ItemCount moet altijd het aantal elementen in de array teruggeven")]
        public void NawArrayUnordered_ItemCount_ShouldReturnItemCount()
        {
            int[] filleds = { 0, 10, 100 };
            int[] capacities = { 1, 10, 100 };
            for (int c = 0; c < capacities.Length; ++c)
            {
                for (int f = 0; f <= c; ++f)
                {
                    var capacity = capacities[c];
                    var filled = filleds[f];
                    WithUnordenedArray(capacity, filled, arr => Assert.AreEqual(filled, arr.ItemCount(), $"We vulden een NawArrayOrdered van grootte {capacity} met {filled} items. Jouw versie van ItemCount() zegt echter dat er {arr.ItemCount()} items inzitten."));
                }
            }
        }

        [Test]
        [Timeout(10000)]
        [Points(1)]
        [Code("NawArrayUnordered", "ItemCount")]
        [Category("WS1 Array Unordered")]
        [Scenario(@"We maken verschillende NawArrayUnordered aan en vullen deze met een aantal elementen. ItemCount moet het aantal elementen kunnen teruggeven zonder naar de onderliggende Alg1NawArray te kijken.")]
        public void NawArrayUnordered_ItemCount_ShouldRunInConstantTime()
        {
            int[] filleds = { 0, 10, 100 };
            int[] capacities = { 1, 10, 100 };
            for (int c = 0; c < capacities.Length; ++c)
            {
                for (int f = 0; f <= c; ++f)
                {
                    var capacity = capacities[c];
                    var filled = filleds[f];
                    WithUnordenedArray(capacity, filled, arr =>
                    {
                        Logger.Instance.ClearLog();
                        arr.ItemCount();
                        var logItems = Logger.Instance.LogItems.Count();
                        Assert.AreEqual(logItems, 0, $"Een efficiente implementatie van de methode ItemCount hoeft niet naar de onderliggende Alg1NawArray te kijken. Jouw implementatie doet dit wel.");
                    });
                }
            }
        }

        #endregion

        #region Add
        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Code("NawArrayUnordered", "Add")]
        [Category("WS1 Array Unordered")]
        [Scenario(@"We maken een NawArrayUnordered aan met een grootte van 10, hier voegen we 10 items aan toe. Dit mag geen foutmelding opleveren.")]
        public void NawArrayUnordered_Add_FillWholeArray_ShouldFit()
        {
            const int capacity = 10;
            WithUnordenedArray(capacity, 0, arr =>
            {
                var expectedNaws = RandomNawGenerator.NewArray(capacity);
                WithoutCallingArrayMethods(() =>
                {
                    Assert.DoesNotThrow(() =>
                    {
                        foreach (var item in expectedNaws)
                            arr.Add(item);
                    }, $"Bij het invoegen van {capacity} items in een NawArrayUnordered met een grootte van {capacity} mogen geen exceptions optreden. Bij jouw NawArrayUnordered gebeurt dit wel.");

                });
            });
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Code("NawArrayUnordered", "Add")]
        [Category("WS1 Array Unordered")]
        [Scenario(@"We maken een NawArrayUnordered aan met een grootte van 3, hier voegen we 4 items aan toe. 
Dit moet een NawArrayOrderedOutOfBoundsException opleveren.")]
        public void NawArrayUnordered_Add_OneTooMany_ShouldThrowException()
        {
            int[] capacities = { 1, 10, 100 };
            foreach (var capacity in capacities)
            {
                WithoutCallingArrayMethods(() =>
                {
                    WithUnordenedArray(capacity, capacity, arr =>
                    {
                        Assert.Throws<NawArrayUnorderedOutOfBoundsException>(
                            () => arr.Add(RandomNawGenerator.New()),
                            $"Jouw implementatie van de Add methode werpt geen uitzondering op wanneer we een element aan een volle array proberen toe te voegen");
                    });
                });
            }
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Code("NawArrayUnordered", "Add")]
        [Category("WS1 Array Unordered")]
        [Scenario(@"We maken een NawArrayUnordered aan en gaan hier items aan toevoegen
De array moet deze nieuwe items elke keer aan het eind invoegen zonder rekening te houden met de ordening.")]
        public void NawArrayUnordered_Add_Valid_ShouldAddAtTheEnd()
        {
            const int capacity = 10;
            const int filled = 5;
            WithUnordenedArray(capacity, filled, arr =>
            {
                var prevState = new NAW[filled];
                WithoutLogging(() =>
                {
                    for (int i = 0; i < filled; ++i)
                        prevState[i] = arr.Array[i];
                });

                WithoutCallingArrayMethods(() =>
                {
                    arr.Add(RandomNawGenerator.New());
                });

                for (int i = 0; i < filled; ++i)
                {
                    Assert.AreSame(prevState[i], arr.Array[i], "Jouw Add methode van NAWArrayUnordered doet ook iets met de elementen die al in de array zaten. Dit is niet de bedoeling");
                }

            });
        }

        [Test]
        [Timeout(10000)]
        [Points(2.0)]
        [Code("NawArrayUnordered", "Add")]
        [Category("WS1 Array Unordered")]
        [Scenario("We maken een NawArrayUnordered aan met een capaciteit van 10 en vullen deze met vijf elementen. Daarna voegen we nog een zesde element toe. Jouw methode plaatst dit element op index 6 zonder naar de waardes op andere indices te kijken.")]
        public void NawArrayUnordered_Add_ShouldNotUseForloop()
        {
            const int filled = 5;
            WithUnordenedArray(10, filled, arr =>
            {
                WithoutCallingArrayMethods(() =>
                {
                    var toAdd = RandomNawGenerator.New();
                    arr.Add(toAdd);
                    Console.WriteLine(Logger.Instance.LogItems.FirstOrDefault());
                    Assert.IsTrue(
                        Logger.Instance.LogItems.SequenceEqual(new LogItem[] { LogItem.SetItem(filled, null, toAdd) }),
                        "Het enige dat Add moet doen is het nieuwe element opslaan in de onderliggende Alg1NawArray. Dat kan direct op de index _used (voordat je hem verhoogt). Je hoeft verder niets met Alg1NawArray te doen."
                    );

                });

            });
        }

        #endregion

        #region Find
        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Code("NawArrayUnordered", "Find")]
        [Category("WS1 Array Unordered")]
        [Scenario("We zoeken in een NawArrayOrdered naar het item op index i. We verwachten dat Find als index i terug geeft")]
        public void NawArrayUnordered_Find_ShouldReturnIndexOfItem()
        {
            const int filled = 10;
            WithUniqueUnorderedArray(100, filled, arr =>
            {
                WithoutCallingArrayMethods(() =>
                {
                    for (int i = 0; i < filled; ++i)
                    {
                        Assert.AreEqual(i, arr.Find(arr.Array[i]));
                    }
                });
            });
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Code("NawArrayUnordered", "Find")]
        [Category("WS1 Array Unordered")]
        [Scenario("We zoeken in een NawArrayOrdered item op index i. We verwachten dat jouw algoritme dit item vindt door maximaal naar i+1 of (_used - i) elementen te kijken.")]
        public void NawArrayUnordered_Find_RunsInLinearTime()
        {
            const int filled = 10;
            WithUniqueUnorderedArray(100, filled, arr =>
            {
                for (var i = 0; i < 10; ++i)
                {
                    var item = arr.Array[i];
                    Logger.Instance.ClearLog();
                    arr.Find(item);
                    var count = Logger.Instance.LogItems.Count();
                    Assert.That(count,
                                Is.EqualTo(i + 1).Or.EqualTo(filled - i),
                                $"We verwachten dat je na {i + 1} of {filled - i} elementen gezien te hebben het element hebt gevonden"
                               );

                }
            });
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Code("NawArrayUnordered", "Find")]
        [Category("WS1 Array Unordered")]
        [Scenario("We zoeken in een NawArrayOrdered naar het item op index i. We verwachten dat Find als index i terug geeft")]
        public void NawArrayUnordered_Find_ShouldReturnMinus1IfNotPresent()
        {
            const int filled = 10;
            WithUniqueUnorderedArray(100, filled, arr =>
            {
                WithoutCallingArrayMethods(() => Assert.AreEqual(-1, arr.Find(new NAW())));
            });
        }
        #endregion
    }
}
