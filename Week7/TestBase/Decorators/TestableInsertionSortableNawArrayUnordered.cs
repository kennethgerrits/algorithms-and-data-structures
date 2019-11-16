using Alg1.Practica.Utils;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.TestBase.Utils.Decorators
{
    public class TestableInsertionSortableNawArrayUnordered<T> : TestableArray, IInsertionSort 
        where T : INawArray, IInsertionSort
    {
        private IInsertionSort Inner { get; }
        public TestableInsertionSortableNawArrayUnordered(T inner) : base(inner)
        {
            Inner = inner;
        }

        public void InsertionSort()
            => ExeTimed(() => Inner.InsertionSort());

        public void InsertionSortInverted()
            => ExeTimed(() => Inner.InsertionSortInverted());
    }
}