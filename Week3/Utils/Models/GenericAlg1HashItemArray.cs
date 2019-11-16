using Alg1.Practica.Utils.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alg1.Practica.Utils.Exceptions;

namespace Alg1.Practica.Utils.Models
{
    public class Alg1HashItemArray<K,V>
    {
        private HashItem<K,V>[] _array;
        public int Size { get; private set; }

        public Alg1HashItemArray(int size)
        {
            Size = size;
            _array = new HashItem<K,V>[size];
        }

        public void SetValues(HashItem<K,V>[] newArray)
        {
            Alg1.Practica.Utils.Globals.Alg1HashItemArrayMethodCalled = true;
            if (Alg1.Practica.Utils.Globals.ShowAlg1HashItemArrayAlerts) { System.Console.WriteLine("\n\n!!!! Aanroepen van methodes op Alg1HashItemArray Class is niet toegestaan"); }
            newArray.CopyTo(_array, 0);
        }

        public HashItem<K,V> this[int index]
        {
            get
            {
                if (_array != null && Logger.Instance.Enabled)
                {
                    NAW nawValue;
                    if (_array[index] != null)
                    {
                        nawValue = new NAW("HashLink", "empty", "empty");
                    }
                    else
                    {
                        nawValue = new NAW("empty", "empty", "empty");
                    }
                    Logger.Instance.Log(

                    new LogItem
                    {

                        NewNaw1 = nawValue,
                        Index1 = index,
                        ArrayAction = ArrayAction.GET
                    }
                    );
                }
                return _array[index];
            }
            set
            {
                if (_array != null && Logger.Instance.Enabled)
                {
                    NAW nawValue;
                    if (_array[index] != null)
                    {
                        nawValue = new NAW("HashLink", "empty", "empty");
                    }
                    else
                    {
                        nawValue = new NAW("empty", "empty", "empty");
                    }

                    Logger.Instance.Log(
                        new LogItem
                        {
                            NewNaw1 = nawValue,
                            OldNaw1 = nawValue,
                            Index1 = index,
                            ArrayAction = ArrayAction.SET
                        }
                    );
                }

                _array[index] = value;
            }
        }

        public HashItem<K,V>[] ToArray()
        {
            Alg1.Practica.Utils.Globals.Alg1HashItemArrayMethodCalled = true;
            if (Alg1.Practica.Utils.Globals.ShowAlg1HashItemArrayAlerts) { System.Console.WriteLine("\n\n!!!! Aanroepen van methodes op Alg1HashItemArray Class is niet toegestaan"); }
            return (HashItem<K,V>[])_array.Clone();
        }


    }
}
