using System;
using System.ComponentModel;
using System.Threading.Tasks;
namespace Alg1.Practica.TestBase.Utils
{
    public class TimedOperations
    {
        private const double TimeoutTime = 500.0;

        private class OperationTimedOutException : Exception
        {
            public OperationTimedOutException()
            : base("Je code had te lang nodig. Controleer je code op oneindige lussen of recursie.")
            {
                
            }
        }

        public static void ExeTimed(Action a)
        {
            try
            {
                var t = Task.Run(a);
                if (!t.Wait(TimeSpan.FromMilliseconds(TimeoutTime)))
                {
                    throw new OperationTimedOutException();
                }
            }
            catch (AggregateException ae)
            {
                Console.WriteLine($"Error while timing operation:\n\n{ae.InnerException.StackTrace}");
                throw ae.InnerException;
            }


        }

        public static T ExeTimed<T>(Func<T> f)
        {
            try
            {
                var t = Task.Run(f);
                if (!t.Wait(TimeSpan.FromMilliseconds(TimeoutTime)))
                {
                    throw new OperationTimedOutException();
                }

                return t.Result;
            }
            catch (AggregateException ae)
            {
                Console.WriteLine($"Error while timing operation:\n\n{ae.InnerException.StackTrace}");
                throw ae.InnerException;
            }
        }
        
    }
}