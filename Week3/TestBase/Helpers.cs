using System;
using NUnit.Framework;
using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Logging;


namespace Alg1.Practica.TestBase
{
    public static class Helpers
    {
        public static void WithoutCallingArrayMethods(Action a)
        {
            Globals.Alg1NawArrayMethodCalled = false;
            a();
            Assert.IsFalse(Globals.Alg1NawArrayMethodCalled, "Er wordt een methode van Alg1NawArray gebruikt!");
        }



        public static void WithoutLogging(Action f)
        {
            Logger.Instance.Enabled = false;
            f();
            Logger.Instance.Enabled = true;
        }
    }
}
