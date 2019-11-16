using Alg1.Practica.TestBase.Attributes;
using static Alg1.Practica.TestBase.Utils.TimedOperations;
using Alg1.Practica.Utils.Logging;
using Alg1.Practica.Utils.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace Alg1.Practica.Practicum5.Test.Workshop
{
    [TestFixture]
    public class StackTest
    {
        private Stack stack { get; set; }

        #region Setup and Teardown
        [SetUp]
        public void Stack_Initialize()
        {
            stack = new Stack();
        }
        #endregion

        #region Timed Stack Operations
        private void Push(Stack s, string text)
        {
            ExeTimed(() => s.Push(text));
        }
        private void Push(string text)
        {
            Push(stack, text);
        }

        private string Pop(Stack s)
        {
            return ExeTimed(() => s.Pop());
        }

        private string Pop()
        {
            return Pop(stack);
        }

        private string Peek(Stack s)
        {
            return ExeTimed(() => s.Peek());
        }

        private string Peek()
        {
            return Peek(stack);
        }

        private bool IsEmpty()
        {
            return ExeTimed(() => stack.IsEmpty());
        }
        #endregion
        
        

        #region Push Pop
        [Test]
        [Timeout(3000)]
        [Points(1.0)]
        [Code("Stack", "Pop")]
        [Category("WS5 Stack")]
        [Scenario(@"We maken een lege Stack aan en roepen pop() aan. Dit zou null moeten teruggeven.")]
        public void Stack_Pop_Empty_ShouldReturnNull()
        {
            Assert.IsNull(Pop(), "Stack.pop(): Een lege stack moet null teruggeven.");
        }

        [Test]
        [Timeout(3000)]
        [Points(1.0)]
        [Code("Stack", "Pop")]
        [Category("WS5 Stack")]
        [Scenario(@"We maken een Stack aan en roepen deze aan met push(""test""). De aanroep van pop() moet deze waarde weer teruggeven.")]
        public void Stack_PushPop_ShouldReturnPushed()
        {
            Push("test");

            Assert.AreEqual("test", Pop(), "Stack.pop(): Een stack pop() moet teruggeven wat met push() is gegeven.");
        }

        [Test]
        [Timeout(3000)]
        [Points(1.0)]
        [Code("Stack", "Pop")]
        [Category("WS5 Stack")]
        [Scenario(@"We maken een Stack aan en roepen deze aan met push(""test1"") en push(""test2""). De pop methode moet deze waardes in de volgorde ""test2"", ""test1"" weer teruggeven")]
        public void Stack_PushPushPopPop_ShouldReturnPushed()
        {
            Push("test1");
            Push("test2");

            Assert.AreEqual("test2", Pop(), "Stack.pop(): de string 'test2' was als laatste in de stack gepusht. Dit moet dan ook bij de eerste aanroep van pop() weer worden teruggegeven.");
            Assert.AreEqual("test1", Pop(), "Stack.pop(): de string 'test1' werd als een na laatste gepusht. Dit moet bij de tweede aanroep van pop() worden teruggegeven.");
            Assert.AreEqual(null, Pop(), "Stack.pop(): Er zijn twee elementen gepusht en twee elementen gepopt. Daarna zou de stack bij een volgende aanroep van pop() null moeten teruggeven.");
        }
        #endregion

        #region IsEmpty
        [Test]
        [Timeout(3000)]
        [Points(1.0)]
        [Code("Stack", "IsEmpty")]
        [Category("WS5 Stack")]
        [Scenario(@"We maken een lege stack aan en roepen er IsEmpty() op aan. Deze zou true moeten returnen.")]
        public void Stack_IsEmpty_EmptyStack_ShouldReturnTrue()
        {
            Assert.IsTrue(IsEmpty(), "Stack.IsEmpty(): Op de lege stack zou IsEmpty true moeten returnen.");
        }

        [Test]
        [Timeout(3000)]
        [Points(1.0)]
        [Code("Stack", "IsEmpty")]
        [Category("WS5 Stack")]
        [Scenario(@"We maken een lege stack aan, pushen en poppen een element en roepen er IsEmpty() op aan. Deze zou true moeten returnen.")]
        public void Stack_IsEmpty_PushPop_ShouldReturnTrue()
        {
            Push("test");
            Pop();

            Assert.IsTrue(IsEmpty(), "Stack.IsEmpty(): Na een push en een pop is de stack weer leeg en zou IsEmpty true moeten returnen.");
        }

        [Test]
        [Timeout(3000)]
        [Points(1.0)]
        [Code("Stack", "IsEmpty")]
        [Category("WS5 Stack")]
        [Scenario(@"We maken een stack aan en pushen er een element in. IsEmpty() zou dan false moeten returnen.")]
        public void Stack_IsEmpty_Push_ShouldReturnFalse()
        {
            Push("test1");

            Assert.IsFalse(IsEmpty(), "Stack.IsEmpty(): Na een push zit er iets in de Stack, IsEmpty() is dan false.");
        }
        #endregion
    }
}
