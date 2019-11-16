using Alg1.Practica.Utils.Exceptions;
using Alg1.Practica.Utils.Models;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.Practicum6.Test.Workshop
{
    public class TestableNawQueueArray
    {
        private NawQueueArray Inner { get; }

        public TestableNawQueueArray(NawQueueArray inner)
        {
            Inner = inner;
        }

        public static TestableNawQueueArray New(int capacity)
        {
            var inner = ExeTimed(() => new NawQueueArray(capacity));
            return new TestableNawQueueArray(inner);
        }

        public int Front
        {
            get { return ExeTimed(() => Inner.Front); }
            set { ExeTimed(() => Inner.Front = value); }
        }

        public int Rear
        {
            get { return ExeTimed(() => Inner.Rear); }
            set { ExeTimed(() => Inner.Rear = value); }
        }


        public void Enqueue(NAW naw) => ExeTimed(() => Inner.Enqueue(naw));
        public NAW Dequeue() => ExeTimed(() => Inner.Dequeue());
        public int Count() => ExeTimed(() => Inner.Count());

    }
}