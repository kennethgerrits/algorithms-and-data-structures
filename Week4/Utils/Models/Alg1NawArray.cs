using Alg1.Practica.Utils.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alg1.Practica.Utils.Exceptions;

namespace Alg1.Practica.Utils
{
    public static class Globals
    {
        public static bool ShowAlg1NawArrayAlerts = true;
        public static bool Alg1NawArrayMethodCalled = false;
        public static bool ShowAlg1HashItemArrayAlerts = true;
        public static bool Alg1HashItemArrayMethodCalled = false;
    }
}

namespace Alg1.Practica.Utils.Models
{
    public class Alg1NawArray
    {
        private NAW[] _array;
        public int Size { get; private set; }

        public Alg1NawArray(int size)
        {
            Size = size;
            _array = new NAW[size];
        }

        public void SetValues(NAW[] newArray)
        {
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = true;
            if (Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts) { System.Console.WriteLine("\n\n!!!! Aanroepen van methodes op Alg1NawArray Class is niet toegestaan"); }
            newArray.CopyTo(_array, 0);
        }

        public NAW this[int index]
        {
            get
            {
                if (_array != null && Logger.Instance.Enabled)
                {
                    Logger.Instance.Log(
                        LogItem.GetItem(index, _array[index])
                    );
                }
                return _array[index];
            }
            set
            {
                if (_array != null && Logger.Instance.Enabled)
                {
                    Logger.Instance.Log(LogItem.SetItem(index, _array[index], value));
                }

                _array[index] = value;
            }
        }

        public NAW[] ToArray()
        {
            Alg1.Practica.Utils.Globals.Alg1NawArrayMethodCalled = true;
            if (Alg1.Practica.Utils.Globals.ShowAlg1NawArrayAlerts) { System.Console.WriteLine("\n\n!!!! Aanroepen van methodes op Alg1NawArray Class is niet toegestaan"); }
            return (NAW[])_array.Clone();
        }

        public void Swap(int firstIndex, int secondIndex)
        {
            // Alg1_Practicum_Utils.Globals.Alg1NawArrayMethodCalled = true;
            // if (Alg1_Practicum_Utils.Globals.ShowAlg1NawArrayAlerts) { System.Console.WriteLine("\n\n!!!! Aanroepen van methodes op Alg1NawArray Class is niet toegestaan"); }
            // Toegestaan om correct aantal swaps bij sorteren toe te staan.

            var lowestIndex = firstIndex < secondIndex ? firstIndex : secondIndex;
            var highestIndex = firstIndex > secondIndex ? firstIndex : secondIndex;
            if (Logger.Instance.Enabled)
            {
                Logger.Instance.Log(
                new LogItem
                {
                    NewNaw1 = _array[highestIndex],
                    OldNaw1 = _array[lowestIndex],
                    Index1 = lowestIndex,
                    NewNaw2 = _array[lowestIndex],
                    OldNaw2 = _array[highestIndex],
                    Index2 = highestIndex,
                    ArrayAction = ArrayAction.SWAP
                });
            }

            NAW temp = _array[firstIndex];
            _array[firstIndex] = _array[secondIndex];
            _array[secondIndex] = temp;
        }
    }
}
