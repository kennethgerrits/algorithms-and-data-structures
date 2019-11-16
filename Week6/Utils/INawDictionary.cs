using System;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Utils
{
    public interface INawDictionary
    {

        void Insert(string key, NAW value);
        NAW Find(string key);
        NAW Delete(string key);
    }
}
