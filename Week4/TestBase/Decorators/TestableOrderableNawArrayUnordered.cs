using Alg1.Practica.Utils;
using static Alg1.Practica.TestBase.Utils.TimedOperations;
using NUnit.Framework.Internal;

namespace Alg1.Practica.TestBase.Utils.Decorators
{
    public class TestableOrderableNawArrayUnordered<T> : TestableArray, IToNawArrayOrdered
        where T: INawArray, IToNawArrayOrdered
    {
        private T Inner { get; }

        public TestableOrderableNawArrayUnordered(T inner) : base(inner)
        {
            Inner = inner;
        }

        public INawArrayOrdered ToNawArrayOrdered() => ExeTimed(() => Inner.ToNawArrayOrdered());
    }
}