using System;
using NUnit.Framework;

namespace Alg1.Practica.TestBase.Attributes
{
    public class ScenarioAttribute : PropertyAttribute
    {
        public string Scenario => Properties.Get("Scenario") as string;


        public ScenarioAttribute(String scenario) : base(scenario)
        {
        }
    }
}
