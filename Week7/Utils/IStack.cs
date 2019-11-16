using Alg1.Practica.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg1.Practica.Utils
{
    public interface IStack
    {
        void Push(string s);

        string Peek();
        string Pop();

        bool IsEmpty();

        
    }
}
