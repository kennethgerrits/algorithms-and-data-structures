using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg1.Practica.Utils.Models
{
    public class HashItem
    {
        public string Key;
        public NAW Value;
        public bool IsDeleted;

        public HashItem(string key, NAW value)
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
