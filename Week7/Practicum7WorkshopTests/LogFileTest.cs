using NUnit.Framework;
using Alg1.Practica.Utils.Models;
using Alg1.Practica.TestBase.Attributes;
using Alg1.Practica.Practicum7;
using System;

namespace Alg1.Practica.Practicum7.Test.Workshop
{
    [TestFixture()]
    public class LogFileTest
    {
        readonly string nonExistingKey = "Non Existing Key";
        readonly NAW nonExistingNaw = new NAW("non", "existing", "naw");
        readonly string pietHeinDonnerKey = "Piet-Hein Donner";
        readonly NAW pietHeinDonnerNaw = new NAW("Piet-Hein Donner", "Binnenhof 1", "Den Haag");
        readonly string janWillemsenKey = "Jan Willemsen";
        readonly NAW janWillemsenNaw = new NAW("Jan Willemsen", "De Gracht 3", "Zaandam");

        private TestableLogFile testLogFile;
        [SetUp]
        public void SetUpTest()
        {
            testLogFile = new TestableLogFile();
            testLogFile.Head = new LogFileLink(key: "Piet-Hein Donner",
                                     value: pietHeinDonnerNaw,
                                     next: new LogFileLink(
                                        key: "Jan Willemsen",
                                         value: janWillemsenNaw,
                                         next: null
                                        )
                                    );

        }

        [Test()]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS6 LogFile")]
        [Code("LogFile", "LogFile")]
        [Scenario(@"We maken een lege LogFile aan. Deze mag nog geen BucketLinks bevatten.")]
        public void LogFile_ShouldBeEmptyAfterConstruction()
        {
            var bl = new TestableLogFile();
            Assert.IsNull(bl.Head);
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS6 LogFile")]
        [Code("LogFile", "Insert")]
        [Scenario(@"We koppelen een nog niet bestaande sleutel aan een waarde. Jouw LogFile moet deze " +
                  "vooraan in de gelinkte lijst opslaan.")]
        public void LogFile_ShouldContainItemAfterInsert()
        {
            var prevHead = testLogFile.Head;
            testLogFile.Insert(nonExistingKey, nonExistingNaw);
            Assert.NotNull(testLogFile.Head, "Na het invoegen is de eigenschap Head van jouw LogFile null. " +
                           "Er zit een fout in je Insert-implementatie.");
            Assert.AreEqual(nonExistingKey, testLogFile.Head.Key, "De eigenschap Key van de nieuwe BucketLink is niet " +
                            "gelijk aan de sleutel die mee werd gegeven aan Insert");
            Assert.AreEqual(nonExistingNaw, testLogFile.Head.Value, "De waarde van de zojuist toegevoegde BucketLink" +
                            "is niet gelijk aan de waarde de mee werd gegeven aan Insert.");
            Assert.AreSame(prevHead, testLogFile.Head.Next, "We verwachten dat jouw log file nieuwe items aan de voorkant " +
                           "toevoegt. Bij het invoegen van een nieuw item, moet er een BucketLink worden aangemaakt " +
                           "waarvan Next naar de vorige Head verwijst. Jouw implementatie lijkt dit niet doen.");
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS6 LogFile")]
        [Code("LogFile", "Insert")]
        [Scenario(@"We proberen een waarde aan de sleutel null te koppelen. Jouw implementatie mag geen null-sleutels " +
                  "accepteren. In plaats van daarvan moet Insert een ArgumentNullException opwerpen.")]
        public void LogFile_ShouldNotAcceptNullForAKey()
        {
            var bl = new LogFile();
            Assert.Throws<ArgumentNullException>(() => bl.Insert(null, null), "Sleutels met de waarde null zijn niet" +
                                                 "toegestaan. Jouw implementatie zou een ArgumentNullException moeten" +
                                                 "opwerperen wanneer Insert wordt aangeroepen met een null-sleutel." +
                                                 "Controleer jouw implementatie.");
        }

        [Test]
        [Timeout(10000)]
        [Points(2.0)]
        [Category("WS6 LogFile")]
        [Code("LogFile", "Delete")]
        [Scenario(@"We proberen de waarde gekoppelend aan een bestaande sleutel uit je LogFile te verwijderen. " +
                  "Jouw LogFile moet de bijbehorende BucketLink verwijderen en de eigenschap Next van de vorige " +
                  "BucketLink bijwerken.")]
        public void LogFile_ShouldDeleteItemAssociatedWithKey()
        {
            testLogFile.Delete(janWillemsenKey);
            Assert.IsNotNull(testLogFile.Head);
            Assert.AreEqual(testLogFile.Head.Key, pietHeinDonnerKey);
            Assert.AreEqual(testLogFile.Head.Value, pietHeinDonnerNaw);
            Assert.IsNull(testLogFile.Head.Next);
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS6 LogFile")]
        [Code("LogFile", "Delete")]
        [Scenario(@"We proberen de waarde gekoppelend aan een bestaande sleutel uit je LogFile te verwijderen. " +
                  "Jouw LogFile moet de bijbehorende waarde teruggeven.")]
        public void LogFile_ShouldReturnAssociatedValueUponDeletion()
        {
            var naw = testLogFile.Delete(janWillemsenKey);
            Assert.AreEqual(janWillemsenNaw, naw);
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS6 LogFile")]
        [Code("LogFile", "Delete")]
        [Scenario(@"We proberen het laatst toegevoegde element uit je LogFile te verwijderen. Je moet ervoor zorgen dat de " +
                  "eigenschap Head van jouw LogFile correct wordt bijgewerkt. Dit betekent ook dat Head null moet zijn " + "" +
                  "na het verwijderen van het laatste element")]
        public void LogFile_ShouldUpdateHead()
        {
            var nextLink = testLogFile.Head.Next;
            testLogFile.Delete(testLogFile.Head.Key);
            Assert.AreSame(nextLink, testLogFile.Head, "Na het verwijderen van het voorste element moet Head worden " +
                           "bijgewerkt. Head moet nu naar de BucketLink verwijzen die voorheen op de tweede plaats" +
                           "van de gelinkte lijst stond. Jouw implementatie doet dit niet");
            testLogFile.Delete(testLogFile.Head.Key);
            Assert.IsNull(testLogFile.Head, "Na het verwijderen van de laatste waarde uit je LogFile moet de eigenschap " +
                          "Head null zijn. Bij jouw implementatie is dit niet het geval.");
        }

        [Test]
        [Timeout(10000)]
        [Points(1.0)]
        [Category("WS6 LogFile")]
        [Code("LogFile", "Find")]
        [Scenario(@"We proberen bestaande waardes op te vragen aan de hand van hun sleutels. Jouw implementatie " +
                  "moet voor elke sleutel de gekoppelde waarde teruggeven.")]
        public void LogFile_ShouldFindItemAssociatedWithKey()
        {
            Assert.AreEqual(pietHeinDonnerNaw, testLogFile.Find(pietHeinDonnerKey), "Jouw implementatie geeft" +
                            "niet de juiste waarde bij een sleutel terug. Controleer je implementatie van Find.");
            Assert.AreEqual(janWillemsenNaw, testLogFile.Find(janWillemsenKey), "Jouw implementatie geeft" +
                            "niet de juiste waarde bij een sleutel terug. Controleer je implementatie van Find.");
        }

        [Test]
        [Timeout(10000)]
        [Points(0.5)]
        [Category("WS6 LogFile")]
        [Code("LogFile", "Find")]
        [Scenario(@"We roepen Find aan met een sleutel waaraan geen waarde is gekoppeld. Jouw implementatie " +
                  "moet dan null teruggeven.")]
        public void LogFile_ShouldReturnNullForNonExistingKey()
        {
            Assert.IsNull(testLogFile.Find(nonExistingKey), "Als er geen waarde gekoppeld is aan een sleutel moet de" +
                          "methode Find null teruggeven. Jouw implementatie doet dit niet.");
        }
    }
}
