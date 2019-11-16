using Alg1.Practica.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg1.Practica.TestBase.Utils
{
    public static class RandomNawGenerator
    {
        public static NAW New(int? maxStringLength = null)
        {
            var naam = RandomStringGenerator.New(maxStringLength);
            var adres = RandomStringGenerator.New(maxStringLength);
            var woonplaats = RandomStringGenerator.New(maxStringLength);

            return new NAW(naam, adres, woonplaats);
        }

        public static NAW[] NewArray(int length, int? maxStringLength = null)
        {
            var result = new NAW[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = New(maxStringLength);
            }

            return result;
        }

        public static NAW[] NewArrayOfUniqueEntries(int length)
        {
            var result = new NAW[length];
            for (int i = 0; i < length; ++i)
            {
                var s = String.Format("naam_{0:D10}", i);
                result[i] = new NAW(s, s, s);
            }
            return result;
        }
    }
}
