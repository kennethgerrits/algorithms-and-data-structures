using Alg1.Practica.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg1.Practica.Utils
{
    public interface INawArray
    {
        void Add(NAW item);
        bool Remove(NAW item);
        int Find(NAW item);
        NAW ItemAtIndex(int index);
        void RemoveAtIndex(int index);

        void Show();

        int Count { get; set; }

        int ItemCount();

        Alg1NawArray Array { get; }
    }
}
