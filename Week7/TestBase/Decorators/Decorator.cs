using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.TestBase.Utils.Decorators
{
    public class Decorator
    {
        public enum DesiredOrdering
        {
            None,
            Ascending,
            Descending
        }

        
        public static NAW[] GenerateAndFill<T>(T subject, int n, DesiredOrdering ordering = DesiredOrdering.None) 
            where T : INawArray
        {
            var tmp = RandomNawGenerator.NewArray(n);
            Func<NAW, NAW> id = (naw) => naw;
            if (ordering == DesiredOrdering.Ascending)
                tmp = tmp.OrderBy(id).ToArray();
            else if (ordering == DesiredOrdering.Descending)
                tmp = tmp.OrderByDescending(id).ToArray();
            Helpers.WithoutLogging(() =>
            {
                subject.Array.SetValues((NAW[])tmp.Clone());
                subject.Count = tmp.Length;
            });
            // We have to clear the log because adding to the array will cause the logger to log as well.
            return tmp;
        }
        
    }
}