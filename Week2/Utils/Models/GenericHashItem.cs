using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg1.Practica.Utils.Models
{
    public class HashItem<K,V>
    {
        public K Key;
        public V Value;
        public bool IsDeleted;

        public HashItem(K key, V value)
        {
            Key = key;
            Value = value;
            IsDeleted = false;
        }

        public void Show()
        {
            System.Console.WriteLine("HashItem[ key = \"{0}\", value ={1}, IsDeleted = {2} ]", Key, Value.ToString(), IsDeleted);
        }
    }
    
}
