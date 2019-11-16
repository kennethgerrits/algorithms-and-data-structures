using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alg1.Practica.TestBase.Utils
{
    public static class RandomStringGenerator
    {
        private static Random random = new Random();

        public static string New(int? length = null)
        {
            if (!length.HasValue)
            {
                length = random.Next(1, 10);
            }

            var chars = "abcdefghijklmnopqrstuvwxyz";

            return new String(Enumerable.Repeat(chars, length.Value)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
        }
    }
}
