using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Alg1.Practica.TestBase.Attributes
{
    public class PointsAttribute : PropertyAttribute
    {

        public double Points => (Properties.Get("Points") as double?) ?? 0.0;

        public PointsAttribute(double points) : base(points)
        {
        }
    }
}
