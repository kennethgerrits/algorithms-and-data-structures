using Alg1.Practica.Practicum7;
using Alg1.Practica.Utils.Models;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.Practicum7.Test.Workshop
{
    public class TestableLogFile : LogFile
    {
        public static bool ShouldLog = true;
        public bool Accessed { get; set; }
        public string Method = "";

        public new LogFileLink Head
        {
            get
            {
                return base.Head;
            }
            set
            {
                base.Head = value;
            }

        }

        void LogCall(string method)
        {
            if (!ShouldLog) return;
            Accessed = true;
            Method = method;
        }

        public override void Insert(string key, NAW value)
        {
            LogCall("Insert");
            ExeTimed(() => base.Insert(key, value));
        }

        public void SafeInsert(string key, NAW value)
        {
            Head = new LogFileLink(key: key, value: value, next: Head);
        }

        public bool Contains(string key, NAW value)
        {
            for(var link = Head; link != null; link = link.Next) 
                if (Head.Key.Equals(key) && Head.Value.Equals(value))
                    return true;
            return false;
        }

        public override NAW Delete(string key)
        {
            LogCall("Delete");
            return ExeTimed(() => base.Delete(key));
        }

        public override NAW Find(string key)
        {
            LogCall("Find");
            return ExeTimed(() => base.Find(key));
        }

    }
}
