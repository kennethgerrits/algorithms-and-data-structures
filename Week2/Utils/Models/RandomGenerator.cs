using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg1.Practica.Utils.Models
{
    public static class RandomGenerator
    {
        private static Random _random = new Random();

        private static string generateString(int? length = null)
        {

            if (!length.HasValue)
            {
                length = _random.Next(1, 10);
            }

            var chars = "abcdefghijklmnopqrstuvwxyz";

            return new String(Enumerable.Repeat(chars, length.Value)
                          .Select(s => s[_random.Next(s.Length)])
                          .ToArray());
        }

    

        public static int GenerateInt(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static NAW GenerateNaw()
        {
            var naw = new NAW();

            naw.Naam = RandomGenerator.generateString(_random.Next(1, 5));
            naw.Adres = RandomGenerator.generateString(_random.Next(1, 5));
            naw.Woonplaats = RandomGenerator.generateString(_random.Next(1, 5));

            return naw;

        }
    }
}
