using System;
using Alg1.Practica.Practicum2;
using Alg1.Practica.Utils.Logging;
using static Alg1.Practica.Practicum2.PerformanceTest;
namespace Alg1.Practica.Practicum3
{
    public class PerformanceTest
    {
        public static void TestSortPerformance()
        {
            Logger.Instance.Enabled = false;
            var sizes = new int[] { 10, 100, 1000, 10000 };
            Console.WriteLine("Size\tSelection (ms)\tBubble (ms)");
            for (var sizeIx = 0; sizeIx < sizes.Length; ++sizeIx)
            {
                var size = sizes[sizeIx];
                long bubble = 0;
                long selection = 0;
                const int repetitions = 5;
                for (int repetition = 0; repetition < repetitions; ++repetition)
                {
                    var bubArr = new BubbleSortableNawArrayUnordered(size);
                    var selArr = new SelectionSortableNawArrayUnordered(size);
                    WorstCaseSortScenario(selArr, size);
                    WorstCaseSortScenario(bubArr, size);
                    bubble += Time(1, () => bubArr.BubbleSort());
                    selection += Time(1, () => selArr.SelectionSort());
                }
                Console.WriteLine($"{size}\t{selection / 1000}\t\t{bubble / 1000}");
            }

        }
    }
}
