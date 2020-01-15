using System;
using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Exceptions;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum1
{
    public class NawArrayOrdered : INawArray, INawArrayOrdered
    {
        protected Alg1NawArray _nawArray;

        public Alg1NawArray Array => _nawArray;

        protected int _size;
        protected int _used;

        public NawArrayOrdered(int initialSize)
        {
            if (initialSize < 1 || initialSize > 1000000)
                throw new NawArrayOrderedInvalidSizeException();

            _size = initialSize;
            _nawArray = new Alg1NawArray(initialSize);
        }
        public void Show()
        {
            System.Console.WriteLine("Array contains {0} of {1} items.", _used, _size);
            for (int i = 0; i < _size; i++)
            {
                if (i == _used)
                    System.Console.WriteLine("------------------------------------------------------");

                System.Console.Write("Item {0} contains: ", i);

                if (_nawArray[i] != null)
                    _nawArray[i].Show();
                else
                    System.Console.WriteLine("nothing");
            }
        }

        public int Count
        {
            get => ItemCount();
            set => _used = value;
        }

        public int ItemCount()
        {
            return _used;
        }

        public virtual void Add(NAW item)
        {
            if (_used == 0)
            {
                _nawArray[0] = item;
                _used++;
            }
            else if (_used < _size)
            {
                bool inserted = false;

                for (int i = 0; !inserted && (i < _used); i++)
                {
                    if (_nawArray[i].CompareTo(item) >= 0)
                    {
                        for (int j = _used; j > i; j--)
                        {
                            _nawArray[j] = _nawArray[j - 1];
                        }
                        _nawArray[i] = item;
                        _used++;
                        inserted = true;
                    }
                }

                if (inserted) return;
                _nawArray[_used] = item;
                _used++;
            }
            else
                throw new NawArrayOrderedOutOfBoundsException();
        }

        public void RemoveAtIndex(int index)
        {
            if (index >= _used || index < 0)
                throw new NawArrayOrderedOutOfBoundsException();

            for (int j = index + 1; j < _used; j++)
            {
                _nawArray[index] = _nawArray[j];
                index++;
            }
            _used--;
            _nawArray[_used] = null;
        }

        public virtual bool Remove(NAW item)
        {
            int x = Find(item);
            if (x == -1)
                return false;

            RemoveAtIndex(x);
            return true;
        }

        public NAW ItemAtIndex(int index)
        {
            if (index >= _used || index < 0)
                throw new NawArrayOrderedOutOfBoundsException();

            return _nawArray[index];
        }

        public int Find(NAW item)
        {
            if (item.CompareTo(_nawArray[0]) == -1 || item.CompareTo(_nawArray[_used - 1]) == 1)
                return -1;

            for (int i = 0; i < _used; i++)
            {
                if (item.Equals(_nawArray[i]))
                    return i;
            }
            return -1;
        }

        public bool Update(NAW oldValue, NAW newValue)
        {
            if (!Remove(oldValue)) return false;
            Add(newValue);
            return true;
        }
    }
}


