using Alg1.Practica.TestBase.Attributes;
using Alg1.Practica.Utils.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.Practicum4.Test.Workshop
{
    [TestFixture]
    public class UndoableNawArrayTest
    {
        private NAW naw0 = new NAW("Paul", "De Remise", "Eindhoven");
        private NAW naw1 = new NAW("Martijn", "Dorpstraat", "Oss");
        private NAW naw2 = new NAW("Koen", "Kerkstraat", "Veldhoven");

        private static void SetFirstLink(UndoableNawArray list, UndoLink first, bool setCurrentUndoLink = true)
        {
            //var firstProp = list.GetType().GetProperty("First", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var firstProp = list.GetType().GetProperty("First");
            firstProp.SetValue(list, first);

            if (setCurrentUndoLink)
            {
                SetCurrentUndoLink(list, first);
            }
        }

        private static void SetCurrentUndoLink(UndoableNawArray list, UndoLink currentUndoLink)
        {
            var currentProp = list.GetType().GetProperty("Current");
            currentProp.SetValue(list, currentUndoLink);
        }

        #region Add

        [Test]
        [Timeout(10000)]
        [Category("WS5 Undoable Naw Array Add")]
        [Code("UndoableNawArray", "Add")]
	[Scenario("We voegen een element toe aan een lege UndoableNawArray " +
		  "met een capaciteit van 10. We verwachten dat deze op index " +
		  "0 wordt geplaatst en dat de eigenschap Count gelijk is aan 1.")]
	[Points(0.5)]
        public void Add_SingleItem()
        {
            // Arrange
            UndoableNawArray list = new UndoableNawArray(10);

            // Act
            ExeTimed(() => list.Add(naw1));

            // Assert
            Assert.AreEqual(1, ExeTimed(() => list.Count), "Na het toevoegen in een lege lijst verwachten we dat de count 1 is.");
            Assert.AreEqual(0, ExeTimed(() => list.Find(naw1)), "Het nieuwe item moet gevonden kunnen worden in de lijst.");
            Assert.AreEqual(-1, ExeTimed(() => list.Find(naw0)), "Een item dat niet is toegevoegd moet niet gevonden kunnen worden in de lijst.");
        }

        #endregion Add

        #region remove

        [Test]
        [Timeout(10000)]
        [Category("WS5 Undoable Naw Array Remove")]
        [Code("UndoableNawArray", "Remove")]
	[Scenario("We verwijderen een element uit een UndoableNawArray " +
		  "met een capaciteit van 10. Deze array bevat oorspronkelijk 2 " +
		  "elementen. Na het verwijderen van een element verwachten we dat " +
		  "dat de eigenschap Count gelijk is aan 1 en dat waar nodig de overige " +
		  "elementen zijn verschoven om gaten te voorkomen.")]
	[Points(0.5)]
        public void Remove_SingleItem()
        {
            // Arrange
            UndoableNawArray list = new UndoableNawArray(10);
            list.Array[0] = naw0;
            list.Array[1] = naw1;
            list.Count = 2;

            // Act
            ExeTimed(() => list.Remove(naw0));

            // Assert
            Assert.AreEqual(1, list.Count, "Na het verwijderen uit een lijst met 2 items verwachten we dat de count 1 is.");
            Assert.AreEqual(0, list.Find(naw1), "We verwachten dat het item dat niet verwijderd is nog steed in de lijst zit.");
            Assert.AreEqual(-1, list.Find(naw0), "We verwachten dat het item dat verwijderd is niet meer in de lijst zit.");
        }

        #endregion remove

        #region AddOperation

        [Test]
        [Timeout(10000)]
        [Category("WS5 Undoable Naw Array AddOperation")]
        [Points(1.0)]
        [Code("UndoableNawArray", "AddOperation")]
	[Scenario("We maken een nieuwe UndoableNawArray aan " +
		  "met een capaciteit van 10. We voegen hier een " +
		  "element aan toe. Na afloop verwachten we dat de " +
		  "eigenschap First van deze array deze verandering heeft " +
		  "geregistreerd.")]
        public void Add_AddOperation_LinkContainsOperation()
        {
            // Arrange
            UndoableNawArray list = new UndoableNawArray(10);

            // Act
            ExeTimed(() => list.Add(naw1));

            // Assert
            Assert.AreEqual(list.First.Operation, Operation.Add, "De Link die aan de Undo-lijst is toegevoegd geeft niet aan dat er een Add operatie heeft plaatsgevonden.");
            Assert.AreEqual(list.First.Naw, naw1, "De Link die aan de Undo-lijst is toegevoegd geeft niet het juiste Naw-object aan waarop een Add operatie heeft plaatsgevonden.");
        }

        [Test]
        [Timeout(10000)]
        [Category("WS5 Undoable Naw Array RemoveOperation")]
        [Points(1.0)]
        [Code("UndoableNawArray", "RemoveOperation")]
	[Scenario("We maken een nieuwe UndoableNawArray aan " +
		  "met een capaciteit van 10. We voegen hier een " +
		  "element aan toe. Na afloop verwachten we dat de " +
		  "eigenschap First van deze array deze verandering heeft " +
		  "geregistreerd.")]
        public void Remove_AddOperation_LinkContainsOperation()
        {
            // Arrange
            UndoableNawArray list = new UndoableNawArray(10);
            list.Array[0] = naw1;
            list.Count = 1;

            // Act
            ExeTimed(() => list.Remove(naw1));

            // Assert
            Assert.AreEqual(list.First.Operation, Operation.Remove, "De Link die aan de Undo-lijst is toegevoegd geeft niet aan dat er een Remove operatie heeft plaatsgevonden.");
            Assert.AreEqual(list.First.Naw, naw1, "De Link die aan de Undo-lijst is toegevoegd geeft niet het juiste Naw-object aan waarop een Remove operatie heeft plaatsgevonden.");
        }

        [Test]
        [Timeout(10000)]
        [Category("WS5 Undoable Naw Array AddOperation")]
        [Points(1.0)]
        [Code("UndoableNawArray", "AddOperation")]
	[Scenario("We beginnen met een UndoableNawArray met een capaciteit van 10. " +
		  "Deze heeft reeds één element p index 0. De eigenschap Count is daarom " +
		  "gelijk aan 1. Ook verwijst de eigenschap First naar een UndoLink met " +
		  "de eigenschap Operation gelijk aan Operation.ADD en Naw gelijk aan het NAW-" +
		  "object op de eerste plek.\n\n" +
		  "We voegen nu nog een element toe aan de array.\n\n" +
		  "De keten van UndoLinks moet nu worden aangepast. We verwachten dat First.Next " +
		  "nu verwijst naar een nieuwe UndoLink die deze wijziging registreert.")]
        public void Add_AddOperation_BothNextAndPreviousSet()
        {
            // Arrange
            UndoableNawArray list = new UndoableNawArray(10);
            list.Array[0] = naw0;
            list.Count = 1;
            SetFirstLink(list, new UndoLink() { Naw = naw0, Operation = Operation.Add });

            // Act
            ExeTimed(() => list.Add(naw1));

            // Assert
            Assert.AreEqual(list.First.Next.Naw, naw1, "Er wordt geen juiste referentie naar de Link die aan de Undo-lijst is toegevoegd gelegd vanuit de voorgaande link.");
            Assert.AreEqual(list.First.Next.Previous.Naw, naw0, "Er wordt geen juiste referentie van de Link die aan de Undo-lijst is toegevoegd gelegd naar de voorgaande link.");
        }

        [Test]
        [Timeout(10000)]
        [Category("WS5 Undoable Naw Array RemoveOperation")]
        [Points(1.0)]
        [Code("UndoableNawArray", "RemoveOperation")]
	[Scenario("We beginnen met een UndoableNawArray met een capaciteit van 10. " +
		  "Deze heeft reeds één element p index 0. De eigenschap Count is daarom " +
		  "gelijk aan 1. Ook verwijst de eigenschap First naar een UndoLink met " +
		  "de eigenschap Operation gelijk aan Operation.ADD en Naw gelijk aan het NAW-" +
		  "object op de eerste plek.\n\n" +
		  "We verwijderen nu het element uit de array.\n\n" +
		  "De keten van UndoLinks moet nu worden aangepast. We verwachten dat First.Next " +
		  "nu verwijst naar een nieuwe UndoLink die deze wijziging registreert.")]
        public void Add_RemoveOperation_BothNextAndPreviousSet()
        {
            UndoableNawArray list = new UndoableNawArray(10);
            list.Array[0] = naw1;
            list.Count = 1;
            SetFirstLink(list, new UndoLink() { Operation = Operation.Add, Naw = naw1 });

            // Act
            ExeTimed(() => list.Remove(naw1));

            Assert.AreEqual(ExeTimed(() => list.First.Next.Operation), Operation.Remove, "De Link die aan de Undo-lijst is toegevoegd geeft niet aan dat er een Remove operatie heeft plaatsgevonden.");
            Assert.AreEqual(ExeTimed(() => list.First.Next.Naw), naw1, "De Link die aan de Undo-lijst is toegevoegd geeft niet het juiste Naw-object aan waarop een Remove operatie heeft plaatsgevonden.");
        }

        [Test]
        [Timeout(10000)]
        [Category("WS5 Undoable Naw Array AddOperation")]
        [Points(1.0)]
        [Code("UndoableNawArray", "AddOperation")]
	[Scenario("We beginnen met nieuwe, lege UndoableNawArray met een capaciteit van 10. " +
		  "We voegen één element toe aan de array.\n\n" +
		  "De keten van UndoLinks moet nu worden aangepast. Zowel de eigenschap First " +
		  "als de eigenschap Current wijzen naar dezelfde UndoLink")]
        public void Add_AddOperation_EmptyList_Current()
        {
            UndoableNawArray list = new UndoableNawArray(10);

            // Act
            ExeTimed(() => list.Add(naw0));

			Assert.AreSame(ExeTimed(() => list.First), ExeTimed(() => list.Current), "Na het invoegen van een element aan een lege UndoableNawArray moet de eigenschap Current naar dezelfde UndoLink als First wijzen.");
        }


        #endregion AddOperation

    }
}
