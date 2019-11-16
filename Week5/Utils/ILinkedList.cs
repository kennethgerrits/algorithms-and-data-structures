using Alg1.Practica.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg1.Practica.Utils
{
    public interface ILinkedList
    {
        void InsertHead(NAW naw);

        int CountCalculated(); 

        int CountStored(); 

        int FindNaw(NAW naw);

        void RemoveAllNaw(NAW naw);

        void Show();

        NAW GetNawAt(int index);

        void SetNawAt(int index, NAW naw);
    }
}
