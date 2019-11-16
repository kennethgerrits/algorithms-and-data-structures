using Alg1.Practica;
using Alg1.Practica.TestBase.Attributes;
using Alg1.Practica.Utils.Models;
using Alg1.Practica.Utils.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Alg1.Practica.TestBase.Utils;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.Practicum4.Test.Workshop
{
    [TestFixture]
    public class NawDoublyLinkedListTest
    {
        private NawDoublyLinkedList lijst { get; set; }

        private NAW naw0 = new NAW("Paul", "De Remise", "Eindhoven");
        private NAW naw1 = new NAW("Martijn", "Dorpstraat", "Oss");
        private NAW naw2 = new NAW("Koen", "Kerkstraat", "Veldhoven");

        private NAW new_naw = new NAW("Bart", "Parklaan", "Tilburg");

        [SetUp]
        public void NawDoublyDoublyLinkedList_Initialize()
        {
            lijst = new NawDoublyLinkedList();
            lijst.First = new DoubleLink() { Naw = naw0 };
            lijst.First.Next = new DoubleLink() { Naw = naw1 };
            lijst.First.Next.Previous = lijst.First;

            lijst.First.Next.Next = lijst.Last = new DoubleLink() { Naw = naw2 };
            lijst.First.Next.Next.Previous = lijst.First.Next;
        }

        #region InsertHead
        [Test]
        [Timeout(10000)]
        [Category("WS5 Double Linked List InsertHead")]
        [Points(1.5)]
        [Code("NawDoublyLinkedList", "InsertHead")]
        [Scenario("We voegen een nieuw element toe aan de kop van een bestaande lijst met minimaal drie elementen. " +
                  "Je moet ervoor zorgen dat de kop van de lijst naar dat nieuwe element refereert.")]
        public void DoublyLinkedList_InsertInBeginning_FilledList_ChangesList()
        {
            DoubleLink oldFirst = lijst.First;
            ExeTimed(() => lijst.InsertHead(new_naw));

            Assert.AreEqual(new_naw, lijst.First.Naw, "Het nieuwe element is nu niet het eerste element geworden.");
            Assert.AreEqual(lijst.First.Previous, null, "De previous van de nieuwe Link wijst niet naar null.");
            Assert.AreEqual(lijst.First.Next, oldFirst, "De next van de nieuwe link wijst niet naar de Link die de eerste in de lijst was.");

        }


        [Test]
        [Timeout(10000)]
        [Category("WS5 Double Linked List InsertHead")]
        [Points(1.5)]
        [Code("NawDoublyLinkedList", "InsertHead")]
        [Scenario(@"We voegen een element toe aan een lege lijst. Jouw implementatie werkt de verwijzingen naar " +
                  "de kop en de staart juist bij.")]
        public void DoublyLinkedList_InsertInBeginning_EmptyList_ChangesList()
        {
            NawDoublyLinkedList lijst = new NawDoublyLinkedList();
            ExeTimed(() => lijst.InsertHead(new_naw));

            Assert.AreEqual(new_naw, lijst.First.Naw, "Het nieuwe element is nu niet het eerste element geworden.");
            Assert.AreEqual(new_naw, lijst.Last.Naw, "Het nieuwe element is ingevoegd in een lege lijst maar niet het laatste element geworden.");
            Assert.AreEqual(lijst.First.Previous, null, "De previous van de nieuwe Link wijst niet naar null wanneer een eerste link aan een lege lijst wordt toegevoegd.");
            Assert.AreEqual(lijst.First.Next, null, "De next van de nieuwe link wijst niet naar null wanneer een eerste link aan een lege lijst wordt toegevoegd.");
        }

        [Test]
        [Timeout(10000)]
        [Category("WS5 Double Linked List InsertHead")]
        [Points(1.0)]
        [Code("NawDoublyLinkedList", "InsertHead")]
        [Scenario(@"We voegen een element toe aan een lijst met minimaal drie elemnenten. Na afloop moet de staart van jouw lijst nog steeds naar het juiste element wijzen")]
        public void DoublyLinkedList_InsertInBeginning_FilledList_LastStillValid()
        {
            DoubleLink oldTail = lijst.Last;
            ExeTimed(() => lijst.InsertHead(new_naw));

            Assert.AreEqual(oldTail, lijst.Last, "Door het invoegen van het nieuwe element aan het begin van de lijst is de waarde van _last onterecht gewijzigd.");
        }

        #endregion

        #region ItemAtIndex
        //[Test]
        //[Timeout(10000)]
        //[Category("WS4 Double Linked List InsertHead")]
        //[Points(1.0)]
        //[Code("NawDoublyLinkedList", "ItemAtIndex")]
        //[Scenario(@"We vullen een NawDoublyLinkedList met 10 willekeurige waarden. Vervolgens roepen we ItemAtIndex aan met geldige indices. Jouw methode geeft altijd een verwijzing naar het juiste NAW-object terug.")]
        //public void DoublyLinkedList_ItemAtIndex_ReturnsValidValuesForValidIndices()
        //{
        //    var arr = RandomNawGenerator.NewArrayOfUniqueEntries(10);
        //    var dlist = DoublyLinkedListGenerator.FromArray(arr);
        //    var r = new Random();
        //    for (var i = 0; i < 100; ++i)
        //    {
        //        var index = r.Next(10);
        //        Assert.AreSame(arr[index], ExeTimed(() => dlist.ItemAtIndex(index)),
        //                       $"We vroegen het element op index {index} op maar kregen niet het juiste resultaat. " +
        //                       "Controleer jouw implementatie van ItemAtIndex");
        //    }
        //}


        //[Test]
        //[Timeout(10000)]
        //[Category("WS4 Double Linked List InsertHead")]
        //[Points(1.0)]
        //[Code("NawDoublyLinkedList", "ItemAtIndex")]
        //[Scenario(@"We vullen een NawDoublyLinkedList met 10 willekeurige waarden. Vervolgens roepen we ItemAtIndex aan indices groter dan 9. We verwachten dat jouw methode een IndexOutOfRangeException opwerpt.")]
        //public void DoublyLinkedList_ItemAtIndex_ThrowsOnInvalidIndex()
        //{
        //    var arr = RandomNawGenerator.NewArrayOfUniqueEntries(10);
        //    var dlist = DoublyLinkedListGenerator.FromArray(arr);
        //    var r = new Random();
        //    for (var i = 0; i < 20; ++i)
        //    {
        //        var index = r.Next(10, 100);
        //        Assert.Throws<IndexOutOfRangeException>(
        //            () => ExeTimed(() => dlist.ItemAtIndex(index)),
        //            $"We riepen ItemAtIndex aan met index {index}. Deze valt buiten het bereik van je lijst. Er moet dan een IndexOutOfRangeException worden opgeroepen. Jouw code doet dit niet."
        //       );
        //    }
        //      
        //}

        #endregion

        #region SwapLinkWithNext

        [Test]
        [Timeout(10000)]
        [Category("WS5 Double Linked List SwapLinkWithNext")]
        [Points(1.5)]
        [Code("NawDoublyLinkedList", "SwapLinkWithNext")]
        [Scenario("We wisselen het tweede en derde element van een lijst met minimaal vier elementen. " +
                  "Jouw methode werkt alle referenties van de links bij en zorgt dat de kop en de staart " +
                  "van de lijst nog naar de juiste elementen verwijzen.")]
        public void DoublyLinkedList_SwapLinkWithNext_ItemsSwapped()
        {
            var count = 0;
            lijst = DoublyLinkedListGenerator.Random(4, 10, out count);
            // Arrange
            // lijst.First = new DoubleLink() { Naw = new NAW() { Naam = "naam", Adres = "adres", Woonplaats = "woonplaats" }, Next = lijst.First };
            // lijst.First.Next.Previous = lijst.First;

            DoubleLink first = lijst.First;
            DoubleLink second = first.Next;
            DoubleLink third = second.Next;
            DoubleLink fourth = third.Next;

            // Act
            ExeTimed(() => lijst.SwapLinkWithNext(second));

            Assert.AreEqual(first, lijst.First, "De eerste is gewijzigd. Dit was niet de bedoeling.");
            Assert.AreEqual(fourth, lijst.First.Next.Next.Next, "De laatste is gewijzigd. Dit was niet de bedoeling.");

            Assert.AreEqual(third, lijst.First.Next, "De eerste zou nu naar de derde moeten wijzen, dit doet hij niet.");
            Assert.AreEqual(lijst.First, third.Previous, "De derde zou nu naar de eerste terug moeten wijzen, dit doet hij niet.");

            Assert.AreEqual(second, third.Next, "De derde zou nu naar de tweede moeten wijzen, dit doet hij niet.");
            Assert.AreEqual(third, second.Previous, "De tweede zou nu naar de derde terug moeten wijzen, dit doet hij niet.");

            Assert.AreEqual(second, fourth.Previous, "De tweede zou nu naar de vierde moeten wijzen, dit doet hij niet.");
            Assert.AreEqual(fourth, second.Next, "De vierde zou nu naar de tweede terug moeten wijzen, dit doet hij niet.");
        }

        [Test]
        [Timeout(10000)]
        [Category("WS5 Double Linked List SwapLinkWithNext")]
        [Points(1.5)]
        [Code("NawDoublyLinkedList", "SwapLinkWithNext")]
        [Scenario("We wisselen de eerste link met de tweede. Jouw methode zorgt dat de kop van de lijst correct wordt bijgewerkt.")]
        public void DoublyLinkedList_SwapLinkWithNext_FirstSwapped_FirstIsStillValid()
        {
            // Arrange

            DoubleLink first = lijst.First;
            DoubleLink second = first.Next;

            // Act
            ExeTimed(() => lijst.SwapLinkWithNext(first));

            Assert.AreEqual(lijst.First, second, "Na het omdraaien van de eerste met de tweede link in een lijst wijst first niet naar de juiste link.");
        }

        [Test]
        [Timeout(10000)]
        [Category("WS5 Double Linked List SwapLinkWithNext")]
        [Points(1.5)]
        [Code("NawDoublyLinkedList", "SwapLinkWithNext")]
        [Scenario("We verwisselen de enerlaatste en laatste schakel in een lijst van minimaal 3 elementen. Jouw implementatie werkt de verwijzing naar de staart van de lijst bij.")]
        public void DoublyLinkedList_SwapLinkWithNext_LastSwapped_LastIsStillValid()
        {
            // Arrange

            DoubleLink second = lijst.Last;
            DoubleLink first = second.Previous;

            // Act
            ExeTimed(() => lijst.SwapLinkWithNext(first));

            Assert.AreEqual(lijst.Last, first, "Na het omdraaien van de een na laatste met de laatste link in een lijst wijst last niet naar de juiste link.");
        }

        [Test]
        [Timeout(10000)]
        [Category("WS5 Double Linked List SwapLinkWithNext")]
        [Points(1.5)]
        [Code("NawDoublyLinkedList", "SwapLinkWithNext")]
        [Scenario("We wisselen in een lijst twee schakels. De eerste schakel geven we als argument mee aan de methode. Deze geeft vervolgens de tweede waarde terug.")]
        public void DoublyLinkedList_SwapLinkWithNext_ReturnsCorrectLink()
        {
            // Arrange

            DoubleLink second = lijst.Last;
            DoubleLink first = second.Previous;

            // Act
            DoubleLink result = ExeTimed(() => lijst.SwapLinkWithNext(first));

            Assert.AreEqual(result, second, "De return-waarde is niet gelijk aan de Link waarmee geswapt is.");
        }

        #endregion SwapLinkWithNext

    }
}
