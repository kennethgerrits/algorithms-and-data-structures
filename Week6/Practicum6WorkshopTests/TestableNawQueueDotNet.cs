using System;
using Alg1.Practica.Utils.Models;
using static Alg1.Practica.TestBase.Utils.TimedOperations;
namespace Alg1.Practica.Practicum6.Test.Workshop
{
    public class TestableNawQueueDotNet
    {
        private NawQueueDotNet Inner { get; }

        public TestableNawQueueDotNet(NawQueueDotNet inner)
        {
            Inner = inner;
        }
        
        public static TestableNawQueueDotNet New() => new TestableNawQueueDotNet(ExeTimed(() => new NawQueueDotNet()));


        public void Enqueue(NAW naw)
            => ExeTimed(() => Inner.Enqueue(naw));

        public NAW Dequeue()
            => ExeTimed(() => Inner.Dequeue());

        public int Count()
            => ExeTimed(() => Inner.Count());
    }
}