using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Exceptions;
using Alg1.Practica.Utils.Models;
using Alg1.Practica.Practicum1;


namespace Alg1.Practica.Practicum2
{
    public class BinarySearchableNawArrayOrdered : NawArrayOrdered, IBinarySearch
    {
        public BinarySearchableNawArrayOrdered(int initialSize) : base(initialSize)
        {
        }

        public int FindBinary(NAW item)
        {

            int lowerBound = 0;
            int upperBound = _used - 1;
            int curIn;

            while (lowerBound <= upperBound)
            {
                curIn = (lowerBound + upperBound) / 2;
                if (_nawArray[curIn] == item)
                {
                    return curIn; //found it
                }
                else if (lowerBound > upperBound)
                {
                    return _used; //  can't find it
                }
                else
                {
                    if (_nawArray[curIn].CompareTo(item) == -1)
                    {
                        lowerBound = curIn + 1; // it's in upper half
                    }
                    else
                    {
                        upperBound = curIn - 1; // it's in lower half
                    }
                }
            }
            return -1;
        }

        public void AddBinary(NAW item)
        {
            if (_used >= _size)
                throw new NawArrayOrderedOutOfBoundsException();

            int lowerBound = 0;
            int upperBound = _used - 1;
            int newIndex = 0;

            while (lowerBound <= upperBound)
            {
                int curIn = (lowerBound + upperBound) / 2;

                if (_nawArray[curIn].CompareTo(item) == 1)
                {
                    lowerBound = curIn + 1;
                }
                else
                {
                    upperBound = curIn - 1;
                }
            }

            if (upperBound < lowerBound)
            {
                newIndex = lowerBound;
            }
            else
            {
                newIndex = (lowerBound + upperBound) / 2;
            }

            for (int i = _used; i > newIndex; --i)
            {
                _nawArray[i] = _nawArray[i - 1];
                _nawArray[newIndex] = item;
            }
        }
    }
}
