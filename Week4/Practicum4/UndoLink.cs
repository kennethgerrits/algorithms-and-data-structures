using Alg1.Practica.Utils.Models;
using System;

namespace Alg1.Practica.Practicum4
{
    public enum Operation
    {
        Dummy, Add, Remove
    }

    public class UndoLink
    {
        public Operation Operation { get; set; }
        public NAW Naw { get; set; }

        public UndoLink Previous { get; set; }

        public UndoLink Next { get; set; }
    }
}
