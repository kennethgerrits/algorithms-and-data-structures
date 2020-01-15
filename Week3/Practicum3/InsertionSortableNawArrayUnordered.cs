using System;
using System.Globalization;
using Alg1.Practica.Practicum1;
using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Models;
namespace Alg1.Practica.Practicum3
{
    public class InsertionSortableNawArrayUnordered : NawArrayUnordered, IInsertionSort
    {
        public InsertionSortableNawArrayUnordered(int initialSize) : base(initialSize)
        {
        }

        public void InsertionSort()
        {

            for (int outer = 1; outer < _used; ++outer)
            {
                NAW temp = _nawArray[outer];
                int ind = outer;
                while (ind > 0 && _nawArray[ind - 1].CompareTo(temp) > 0)
                {
                    _nawArray[ind] = _nawArray[ind - 1];
                    --ind;
                }

                if (ind != outer)
                    _nawArray[ind] = temp;
            }
        }

        public void InsertionSortInverted()
        {
            if (_used == 0) return;
            for (int outer = _used - 1; outer >= 0; outer--)
            {
                NAW temp = _nawArray[outer];
                int ind = outer;
                while (ind < _used - 1 && _nawArray[ind + 1].CompareTo(temp) <= 0)
                {
                    _nawArray[ind] = _nawArray[ind + 1];
                    ind++;
                }

                if (ind != outer)
                    _nawArray[ind] = temp;
            }
        }
    }
}
