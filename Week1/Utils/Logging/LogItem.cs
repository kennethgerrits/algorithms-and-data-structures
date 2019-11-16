using Alg1.Practica.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg1.Practica.Utils.Logging
{
    public class LogItem
    {
        public NAW OldNaw1 { get; set; }
        public NAW NewNaw1 { get; set; }
        public NAW OldNaw2 { get; set; }
        public NAW NewNaw2 { get; set; }
        public ArrayAction ArrayAction { get; set; }
        public int Index1 { get; set; }
        public int Index2 { get; set; }

        public Link CurrentLink { get; set; }
        public Link NextLink { get; set; }
        public Link PreviousLink { get; set; }

        public override string ToString()
        {
            string output = "";
            if (NewNaw1 != null)
            {
                output = String.Format("{0} index {1}, item: \r\n{2}", ArrayAction, Index1, NewNaw1.ToString());

                if (ArrayAction == ArrayAction.SET && OldNaw1 != null)
                {
                    output += String.Format("\r\n\tOverwritten item: {0}", OldNaw1.ToString());
                }

                if (NewNaw2 != null)
                {
                    output += String.Format("\r\n\tindex {0}, item: \r\n{1}", Index2, NewNaw2.ToString());
                }
            }

            return output;
        }

        public override bool Equals(Object obj)
        {
            Func<Object, Object, bool> EqualOrBothNull = (obj1, obj2) =>
                obj1 == obj2 || (obj1?.Equals(obj2) ?? false);
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (LogItem)obj;
            return
                EqualOrBothNull(OldNaw1, other.OldNaw1)
                && EqualOrBothNull(NewNaw1, other.NewNaw1)
                && EqualOrBothNull(OldNaw2, other.OldNaw2)
                && EqualOrBothNull(NewNaw2, other.NewNaw2)
                && this.ArrayAction == other.ArrayAction
                && Index1 == other.Index1
                && Index2 == other.Index2
                && EqualOrBothNull(CurrentLink, other.CurrentLink)
                && EqualOrBothNull(NextLink, other.NextLink)
                && EqualOrBothNull(PreviousLink, other.PreviousLink);
        }

        public static LogItem SetItem(int index, NAW oldNAW, NAW newNAW) => new LogItem
        {
            OldNaw1 = oldNAW,
            NewNaw1 = newNAW,
            Index1 = index,
            ArrayAction = ArrayAction.SET
        };

        public static LogItem GetItem(int index, NAW val) => new LogItem
        {
            NewNaw1 = val,
            Index1 = index,
            ArrayAction = ArrayAction.GET
        };

    }

    public enum ArrayAction
    {
        GET, SET, SWAP, GETNEXTLINK, GETPREVIOUSLINK, SETNEXTLINK, SETPREVIOUSLINK, COMPARE
    }
}
