using System;
using Alg1.Practica.Utils.Logging;
using NUnit.Framework;

namespace Alg1.Practica.Practicum3.Test.Workshop
{
    public static class LogItemExtensions
    {
        public static void AssertAreEqual(this LogItem logItem, int expectedIndex1, string expectedWoonplaats, string message)
        {
            Assert.AreEqual(expectedIndex1, logItem.Index1);
            Assert.AreEqual(expectedWoonplaats, logItem.NewNaw1.Woonplaats, message);
        }
    }
}
