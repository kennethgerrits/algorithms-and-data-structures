using System;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum7
{
    public class LogFileLink
    {
        public string Key { get; }
        public NAW Value { get; }
        public LogFileLink Next { get; set; }

        public LogFileLink(string key, NAW value, LogFileLink next)
        {
            Key = key;
            Value = value;
            Next = next;
        }
    }
}
