using System;
using NUnit.Framework;
using Alg1.Practica.TestBase.Utils;
using Alg1.Practica.TestBase.Utils.Decorators;
using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Logging;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum1.Test
{
    public static class TestHelpers
    {
        private static INawArray UnorderedArrayOfSize(int size) =>
            new TestableArray(new NawArrayUnordered(size));
        private static INawArrayOrdered OrderedArrayOfSize(int size) =>
            new TestableOrderedArray(new NawArrayOrdered(size));
        private static INawArray UnorderedArrayOfSize(int size, NAW[] values) =>
            TestableArray.Fill(new TestableArray(new NawArrayUnordered(size)), values);
        private static INawArrayOrdered OrderedArrayOfSize(int size, NAW[] values) =>
            TestableArray.Fill(new TestableOrderedArray(new NawArrayOrdered(size)), values);

        public static void WithoutLogging(Action f)
        {
            Logger.Instance.Enabled = false;
            f();
            Logger.Instance.Enabled = true;
        }

        public static void WithUnordenedArray(int capacity, int filled, Action<INawArray> f)
        {
            var array = UnorderedArrayOfSize(capacity);
            WithoutLogging(() =>
            {
                var testSet = RandomNawGenerator.NewArray(filled);
                foreach (var i in testSet) array.Add(i);
            });
            f(array);
        }

        public static void WithUniqueUnorderedArray(int capacity, int filled, Action<INawArray> f)
        {
            var testSet = RandomNawGenerator.NewArrayOfUniqueEntries(filled);
            var arr = UnorderedArrayOfSize(capacity, testSet);
            f(arr);
        }

        public static void WithOrderedArray(int capacity, int filled, Action<INawArrayOrdered> f)
        {
            var array = OrderedArrayOfSize(capacity);
            WithoutLogging(() =>
            {
                var testSet = RandomNawGenerator.NewArray(filled);
                foreach (var i in testSet) array.Add(i);
            });
            f(array);
        }

        public static void WithUniqueOrderedArray(int capacity, int filled, Action<INawArrayOrdered> f)
        {
            var testSet = RandomNawGenerator.NewArrayOfUniqueEntries(filled);
            var arr = OrderedArrayOfSize(capacity, testSet);
            f(arr);
        }

        public static void WithoutCallingArrayMethods(Action a)
        {
            Globals.Alg1NawArrayMethodCalled = false;
            a();
            Assert.IsFalse(Globals.Alg1NawArrayMethodCalled, "\n\nEr wordt een methode van Alg1NawArray gebruikt!\n\n");
        }
    }
}