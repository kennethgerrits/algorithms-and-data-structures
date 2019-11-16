using System;

namespace Alg1.Practica.Practicum5
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var xm = new XmlValidator();

            Console.Write("Should be True:");
            Console.WriteLine();
            Console.WriteLine(xm.Validate(""));
            Console.WriteLine(xm.Validate("<html>Hallo Wereld</html>"));
            Console.WriteLine(xm.Validate("<html><body>Hallo Wereld</body></html>"));
            Console.WriteLine(xm.Validate("<html><body><i>Hallo</i> <b>Wereld</b> !</body></html>"));

            Console.WriteLine("");

            Console.Write("Should be False:");
            Console.WriteLine();
            Console.WriteLine(xm.Validate("<html><body>Hallo Wereld</body>"));
            Console.WriteLine(xm.Validate("<html><body>Hallo Wereld</html></body>"));
            Console.WriteLine(xm.Validate("<html><body>Hallo Wereld</body></html></xml>"));
            Console.WriteLine(xm.Validate("<html><body> Hallo Wereld </html>"));

            Console.ReadKey();

        }
    }
}
