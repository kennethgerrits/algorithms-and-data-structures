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
            int output, input;

            for (output = _used - 1; output >= 1; output--)
            {
                for (input = 0; input < output; input++)
                {
                    if (_nawArray[input].CompareTo(_nawArray[input + 1]) == 1)
                    {
                        _nawArray.Swap(input, input + 1);
                    }
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
                    {
                        _nawArray.Swap(j, j + 1);
                    }
                }
            }
        }
    }
}
