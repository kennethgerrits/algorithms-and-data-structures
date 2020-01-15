using Alg1.Practica.Practicum1;
using Alg1.Practica.Utils;

namespace Alg1.Practica.Practicum2
{
    public class BubbleSortableNawArrayUnordered : NawArrayUnordered, IBubbleSort
    {
        public BubbleSortableNawArrayUnordered(int initialSize) : base(initialSize)
        {
        }

        public void BubbleSort()
        {
            int outer, inner;

            for (outer = _used - 1; outer >= 1; outer--)
            {
                for (inner = 0; inner < outer; inner++)
                {
                    if (_nawArray[inner].CompareTo(_nawArray[inner + 1]) == 1)
                        _nawArray.Swap(inner, inner + 1);
                }
            }
        }

        public void BubbleSortInverted()
        {
            for (int i = 0; i <= _used - 2; i++)
            {
                for (int j = _used - 2; j >= i; j--)
                {
                    if (_nawArray[j].CompareTo(_nawArray[j + 1]) == 1)
                        _nawArray.Swap(j, j + 1);
                }
            }
        }
    }
}
