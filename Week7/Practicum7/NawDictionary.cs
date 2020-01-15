using System;
using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum7
{
    public class NawDictionary : INawDictionary
    {
        public int Size { get; }
        protected LogFile[] logFiles;
        private int _count;

        public NawDictionary(int size)
        {
            Size = size;
            logFiles = new LogFile[size];
            for (int i = 0; i < logFiles.Length; ++i)
                logFiles[i] = new LogFile();
        }

        protected int KeyToIndex(string key)
        {
            if (key == null)
                throw new ArgumentNullException();

            var hashValue = key.GetHashCode();
            var compressedHashValue = hashValue % Size;
            var index = Math.Abs(compressedHashValue);

            return index;
        }

        public void Insert(string key, NAW value)
        {
            if (key == null)
                throw new ArgumentNullException();

            var i = KeyToIndex(key);
            logFiles[i].Insert(key, value);
            _count++;
        }

        public NAW Find(string key)
        {
            if (key == null)
                throw new ArgumentNullException();

            var newKey = KeyToIndex(key);
            return logFiles[newKey].Find(key);
        }

        public NAW Delete(string key)
        {
            if (key == null)
                throw new ArgumentNullException();

            var temp = Find(key);
            var newKey = KeyToIndex(key);
            logFiles[newKey].Delete(key);
            _count--;
            return temp;
        }


        public int Count
        {
            get
            {
                return _count;
            }
        }

        public double LoadFactor
        {
            get
            {
                return (double) Count / Size;
            }
        }
    }
}
