using System;
using System.Reflection;
using Alg1.Practica.Practicum1;
using Alg1.Practica.Utils;
namespace Alg1.Practica.Practicum3
{
    public class SelectionSortableNawArrayUnordered : NawArrayUnordered, ISelectionSort
    {
        public SelectionSortableNawArrayUnordered(int initialSize) : base(initialSize)
        {
        }

        public void SelectionSort()
        {
            if (_used == 0)
            {
                return;
            }

            int outer, inner, min;
            for (outer = 0; outer < _used - 1; outer++)
            {
                min = outer;
                for (inner = outer + 1; inner < _used; inner++)
                {
                    if (_nawArray[inner].CompareTo(_nawArray[min]) < 0)
                    {
                        min = inner;
                    }
                }

                if (min != outer)
                {
                    _nawArray.Swap(outer, min);
                }
            }
        }

        public void SelectionSortInverted()
        {
            if (_used == 0)
            {
                return;
            }

            int outer, inner, max = 0;
            for (outer = _used - 1; outer > 0; outer--)
            {
                max = outer;
                for (inner = outer - 1; inner >= 0; inner--)
                {
                    if (_nawArray[inner].CompareTo(_nawArray[max]) > 0)
                    {
                        max = inner;
                    }
                }

                if (max != outer)
                {
                    _nawArray.Swap(outer, max);
                }
            }



        }
    }
}