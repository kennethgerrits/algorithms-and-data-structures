using Alg1.Practica.Utils;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.TestBase.Utils.Decorators
{
    public class TestableBubbleSortableArray<T> : TestableArray, IBubbleSort where T : INawArray, IBubbleSort
    {
        private IBubbleSort Inner { get; }
        public TestableBubbleSortableArray(T inner) : base(inner)
        {
            Inner = inner;
        }

        public void BubbleSort() => ExeTimed(() => Inner.BubbleSort());
        public void BubbleSortInverted() => ExeTimed(() => Inner.BubbleSortInverted());
    }
}