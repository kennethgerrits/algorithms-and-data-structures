using System;
using Alg1.Practica.Utils;
using Alg1.Practica.Utils.Logging;
using Alg1.Practica.Utils.Models;

namespace Alg1.Practica.Practicum2
{
    public static class PerformanceTest
    {
        /// <summary>
        /// Creates a fully filled BinarySearchableNawArrayOrdered of a specific capacity
        /// </summary>
        /// <returns>A filled BinarySearchableNawArrayOrdered.</returns>
        /// <param name="size">The size of the array.</param>
        private static BinarySearchableNawArrayOrdered FilledArrayOfSize(int size)
        {
            var array = new BinarySearchableNawArrayOrdered(size);
            for (int i = 0; i < size; ++i)
            {
                var s = i.ToString();
                array.Add(new NAW(s, s, s));
            }
            return array;
        }

        public static void WorstCaseSortScenario(INawArray array, int size)
        {
            for (int i = size - 1; i >= 0; --i)
            {
                var s = i.ToString();
                array.Add(new NAW(s, s, s));
            }
        }

        public static long Time(int repetitions, Action fun)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (var i = 0; i < repetitions; ++i)
                fun();
            watch.Stop();
            return watch.ElapsedTicks;
        }

        /// <summary>
        /// Tests the complexity of binary and linear search and outputs the results
        /// to standard output.
        /// </summary>
        public static void TestSearchPerformance()
        {
            Logger.Instance.Enabled = false;
            var sizes = new int[] { 100, 1000, 10000 };

            //Func<int, Action, double> time = (repetitions, fun) =>
            //{
            //    var watch = System.Diagnostics.Stopwatch.StartNew();
            //    for (var i = 0; i < repetitions; ++i)
            //        fun();
            //    watch.Stop();
            //    return watch.ElapsedMilliseconds;
            //};

            var tries = 100;
            var celestialTeapot = new NAW("77777777777", "77777777777", "77777777777");
            Console.WriteLine("Size\tLinear (ms)\tBinary (ms)");
            for (var sizeIx = 0; sizeIx < sizes.Length; ++sizeIx)
            {
                var size = sizes[sizeIx];
                var array = FilledArrayOfSize(size);
                double lin = Time(tries, () => array.Find(celestialTeapot)) / 1000;
                double bin = Time(tries, () => array.FindBinary(celestialTeapot)) / 1000;
                Console.WriteLine($"{sizes[sizeIx]}\t{lin}\t\t{bin}");
            }
        }

        public static void TestSortPerformance()
        {
            Logger.Instance.Enabled = false;
            var sizes = new int[] { 10, 100, 1000, 10000 };
            Console.WriteLine("Size\tToOrdered (ms)\tBubble (ms)");
            for (var sizeIx = 0; sizeIx < sizes.Length; ++sizeIx)
            {
                var size = sizes[sizeIx];
                long bubble = 0;
                long toOrdered = 0;
                const int repetitions = 1;
                for (int repetition = 0; repetition < repetitions; ++repetition)
                {
                    var bubArr = new BubbleSortableNawArrayUnordered(size);
                    var ordArr = new OrderableNawArrayUnordered(size);
                    WorstCaseSortScenario(ordArr, size);
                    WorstCaseSortScenario(bubArr, size);
                    bubble += Time(1, () => bubArr.BubbleSort());
                    toOrdered += Time(1, () => ordArr.ToNawArrayOrdered());
                }
                Console.WriteLine($"{size}\t{toOrdered / 1000}\t\t{bubble / 1000}");
            }
        }
    }
}
