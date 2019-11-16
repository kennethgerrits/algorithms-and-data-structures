using System;
using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Models;


namespace Alg1.Practica.Practicum7
{
    public class LogFile : INawDictionary
    {
        protected LogFileLink Head { get; set; }

        public virtual void Insert(string key, NAW value)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }

            var newLink = new LogFileLink(key, value, Head);
            Head = newLink;
        }

        public virtual NAW Find(string key)
        {
            if (key == null)
            {
                return null;
            }

            LogFileLink temp = Head;

            while (temp != null)
            {
                if (temp.Key.Equals(key))
                {
                    return temp.Value;
                }

                temp = temp.Next;
            }

            return null;

        }

        public virtual NAW Delete(string key)
        {
            if (key == null)
            {
                return null;
            }

            LogFileLink temp = Head;
            LogFileLink previous = null;

            while (temp != null)
            {
                if (temp.Key.Equals(key))
                {
                    if (temp == Head)
                    {
                        Head = temp.Next;
                    }
                    else
                    {
                        previous.Next = temp.Next;
                    }

                    NAW target = temp.Value;
                    temp.Next = null;
                    return target;
                }

                previous = temp;
                temp = temp.Next;
            }

            return null;
        }
    }

}
