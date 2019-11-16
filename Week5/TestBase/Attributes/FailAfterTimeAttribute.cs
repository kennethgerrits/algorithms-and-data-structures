using System;
using System.Threading.Tasks;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using NUnit.Framework.Internal;

namespace Alg1.Practica.TestaBase.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class FailAfterTimeAttribute : PropertyAttribute, IWrapTestMethod
    {
        private double Milliseconds { get; }
        private class FailAfterTimeCommand : DelegatingTestCommand
        {
            private double Milliseconds { get; }
            public FailAfterTimeCommand(TestCommand command, double milliseconds) 
                : base(command)
            {
                Milliseconds = milliseconds;
            }

            public override TestResult Execute(TestExecutionContext context)
            {
                var t = new Task(() => innerCommand.Execute(context));
                if (!t.Wait(TimeSpan.FromMilliseconds(Milliseconds)))
                {
                   context.CurrentResult.SetResult(ResultState.Failure,
                                                   $"Je methode heeft te veel tijd nodig. Controleer je code op oneindige lussen of recursie."); 
                }
                else
                {
                    context.CurrentResult.SetResult(ResultState.Success);
                }
                return context.CurrentResult;
            }
        }
        
        public FailAfterTimeAttribute(double milliseconds)
        {
            Milliseconds = milliseconds;
        }

        public TestCommand Wrap(TestCommand command)
        {
            return new FailAfterTimeCommand(command, Milliseconds);
            
        }
    }
}
