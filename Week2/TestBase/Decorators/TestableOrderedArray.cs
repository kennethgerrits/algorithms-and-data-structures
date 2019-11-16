using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Models;
using static Alg1.Practica.TestBase.Utils.TimedOperations;

namespace Alg1.Practica.TestBase.Utils.Decorators
{
    public class TestableOrderedArray : TestableArray, INawArrayOrdered
    {
        private INawArrayOrdered Inner { get; }

        public TestableOrderedArray(INawArrayOrdered inner) : base(inner)
        {
            Inner = inner;
        }

        public bool Update(NAW oldValue, NAW newValue) => ExeTimed(() => Inner.Update(oldValue, newValue));
    }
}