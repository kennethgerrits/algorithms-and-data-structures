using System;
using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Models;
namespace Alg1.Practica.Utils
{
    public interface ITestableNawArray : INawArray
    {
        Alg1NawArray Array { get; }
    }
}
