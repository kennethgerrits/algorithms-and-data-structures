using System;
using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Models;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.TestBase.Utils.Decorators
{
    public class TestableArray : INawArray
    {
        private INawArray Inner { get; }

        public TestableArray(INawArray inner)
        {
            Inner = inner;
        }
        public static T Fill<T>(T array, NAW[] values) where T : TestableArray
        {
            for (int i = 0; i < values.Length; ++i)
                array.Array[i] = values[i];
            array.Count = values.Length;
            return array;
        }
        
        public void Add(NAW item) => ExeTimed(() => Inner.Add(item));

        public bool Remove(NAW item) => ExeTimed(() => Inner.Remove(item));

        public int Find(NAW item) => ExeTimed(() => Inner.Find(item));

        public NAW ItemAtIndex(int index) => ExeTimed(() => Inner.ItemAtIndex(index));


        public void RemoveAtIndex(int index) => ExeTimed(() => Inner.RemoveAtIndex(index));


        public void Show() => ExeTimed(() => Inner.Show());

        public int Count {
            get { return ExeTimed(() => Inner.Count); }
            set { ExeTimed(() => Inner.Count = value); }
        }
        public int ItemCount() => ExeTimed(() => Inner.ItemCount());

        public Alg1NawArray Array => ExeTimed(() => Inner.Array);

        private static bool AreDesc(NAW left, NAW right) => left.CompareTo(right) >= 0;
        private static bool AreAsc(NAW left, NAW right) => left.CompareTo(right) <= 0;
        public bool IsSorted(bool ascending = true)
        {
            var count = Count;
            for (var i = 0; i < count - 1; ++i)
            {
                var left = Array[i];
                var right = Array[i + 1];
                if (left != null && right != null)
                {
                    if (ascending ? !AreAsc(left, right) : !AreDesc(left, right))
                        return false;
                }
            }
            return true;
        }
    }
}