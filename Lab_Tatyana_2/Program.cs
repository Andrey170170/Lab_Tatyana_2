using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lab_Tatyana_2
{
    internal class Program
    {
        public static void SortByCounting(int[] array)
        {
            var arraySorted = new int[array.Length];
            foreach (var t1 in array)
            {
                var place = 0;
                if (t1 == 0) continue;
                place += array.Sum(t => t1 > t ? 1 : 0);

                while (arraySorted[place] == t1) place++;

                arraySorted[place] = t1;
            }
        }

        public static void SortByInclusion(int[] array)
        {
            var arraySorted = new List<int> { array[0] };
            for (var i = 1; i < array.Length; i++)
            {
                var j = 0;
                while (arraySorted.Count > j && arraySorted[j] < array[i]) j++;

                arraySorted.Insert(j, array[i]);
            }

            var result = new int[arraySorted.Count];
            arraySorted.CopyTo(result);
        }

        public static void SortByExtraction(int[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                var slice = array.TakeLast(array.Length - i).ToArray();
                var indexOfMinEl = Array.IndexOf(slice, slice.Min()) + i;
                (array[indexOfMinEl], array[i]) = (array[i], array[indexOfMinEl]);
            }
        }

        private static void BubbleSort(int[] array)
        {
            for (var i = 0; i < array.Length; i++)
                for (var j = 0; j < array.Length - 1; j++)
                    if (array[j] > array[j + 1])
                    {
                        (array[j + 1], array[j]) = (array[j], array[j + 1]);
                    }
        }

        private static void Main()
        {
            var allNumbers = File.ReadAllText(@"C:\Users\24122\source\repos\Lab_Tatyana_2\RandNumbers.txt");
            var allStrNumbers = allNumbers.Split(" ");
            var allIntNumbers = new int[allStrNumbers.Length];
            for (var i = 0; i < allStrNumbers.Length; i++)
            {
                int.TryParse(allStrNumbers[i], out allIntNumbers[i]);
            }

            var stopWatch = new Stopwatch();

            var arr = allIntNumbers.Take(1000).ToArray();
            var sample3 = new int[arr.Length];
            Array.Copy(arr, sample3, arr.Length);
            var fullts3 = new long[11];
            for (var i = 0; i < 11; i++)
            {
                stopWatch.Start();
                SortByCounting(sample3);
                SortByInclusion(sample3);
                SortByExtraction(sample3);
                BubbleSort(sample3);
                stopWatch.Stop();
                var ts = stopWatch.ElapsedTicks;
                if (i != 0) fullts3[i] = ts;

                stopWatch.Reset();
            }

            var min = fullts3.TakeLast(10).ToArray().Min();
            var max = fullts3.TakeLast(10).ToArray().Max();
            Console.WriteLine("SortByExtraction\nNanoseonds in test {0}", (max + min) / 2);
        }
    }
}