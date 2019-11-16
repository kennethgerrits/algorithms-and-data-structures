using Alg1.Practica.Utils.Models;
using static Alg1.Practica.TestBase.Utils.TimedOperations;
namespace Alg1.Practica.Practicum6.Test.Workshop
{
    public class TestableNawQueueLinkedList
    {
        private NawQueueLinkedList Inner { get; }

        public TestableNawQueueLinkedList(NawQueueLinkedList inner)
        {
            Inner = inner;
        }

        public static TestableNawQueueLinkedList New() => 
            new TestableNawQueueLinkedList(ExeTimed(() => new NawQueueLinkedList()));
        
        public void Enqueue(NAW naw) => ExeTimed(() => Inner.Enqueue(naw));

        public NAW Dequeue() => ExeTimed(() => Inner.Dequeue());

        public int Count() => ExeTimed(() => Inner.Count());
    }
}