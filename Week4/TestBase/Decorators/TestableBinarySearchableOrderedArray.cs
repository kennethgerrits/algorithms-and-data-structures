using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Models;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.TestBase.Utils.Decorators
{
    public class TestableBinarySearchableOrderedArray<T> : TestableOrderedArray, IBinarySearch
        where T : IBinarySearch, INawArrayOrdered
    {
        private IBinarySearch Inner { get; }

        public TestableBinarySearchableOrderedArray(T inner) : base(inner)
        {
            Inner = inner;
        }

        public void AddBinary(NAW item) => ExeTimed(() => Inner.AddBinary(item));

        public int FindBinary(NAW item) => ExeTimed(() => Inner.FindBinary(item));
    }
}