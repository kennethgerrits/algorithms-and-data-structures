using Alg1.Practica.Utils;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.TestBase.Utils.Decorators
{
    public class TestableSelectionSortableNawArrayUnordered<T> : TestableArray, ISelectionSort
        where T : INawArray,ISelectionSort
    {
        private ISelectionSort Inner { get; }
        public TestableSelectionSortableNawArrayUnordered(T inner) : base(inner)
        {
            Inner = inner;
        }

        public void SelectionSort()
            => ExeTimed(() => Inner.SelectionSort());

        public void SelectionSortInverted()
            => ExeTimed(() => Inner.SelectionSortInverted());
    }
}