using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace Lab_Tatyana_2
{
    class Program
    {
        public static void SortByCounting (int[] array)
        {
            int[] arraySorted = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int place = 0;
                if (array[i] != 0)
                {
                    for (int j = 0; j < array.Length; j++)
                    {
                        place += array[i] > array[j] ? 1 : 0;
                    }
                    while (arraySorted[place] == array[i])
                    {
                        place++;
                    }
                    arraySorted[place] = array[i];
                }
            }
            array = arraySorted;
        }

        public static void SortByInclusion (int[] array)
        {
            List<int> arraySorted = new List<int>();
            arraySorted.Add(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                int j = 0;
                while (arraySorted.Count > j && arraySorted[j] < array[i])
                {
                    j++;
                }
                if (arraySorted.Count < j)
                {
                    arraySorted.Add(array[i]);
                }
                else
                {
                    arraySorted.Insert(j, array[i]);
                }
            }
            int[] result = new int[arraySorted.Count];
            arraySorted.CopyTo(result);
            array = result;
        }

        public static void SortByExtraction(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int[] slice = array.TakeLast(array.Length - i).ToArray();
                int indexOfMinEl = Array.IndexOf(slice, slice.Min()) + i;
                int temp = array[indexOfMinEl];
                array[indexOfMinEl] = array[i];
                array[i] = temp;
            }
        }
        private static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int t = array[j + 1];
                        array[j + 1] = array[j];
                        array[j] = t;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            string allNumbers = File.ReadAllText(@"C:\Users\24122\source\repos\Lab_Tatyana_2\RandNumbers.txt");
            string[] allStrNumers = allNumbers.Split(" ");
            int[] allIntNumbers = new int[allStrNumers.Length];
            for (int i = 0; i < allStrNumers.Length; i++)
            {
                bool sec = int.TryParse(allStrNumers[i], out allIntNumbers[i]);
            }

            Stopwatch stopWatch = new Stopwatch();
            long frequency = Stopwatch.Frequency;
            long nanosecPerTick = (1000L * 1000L * 1000L) / frequency;

            int[] arr = allIntNumbers.Take(30000).ToArray();
            int[] sample3 = new int[arr.Length];
            Array.Copy(arr, sample3, arr.Length);
            long[] fullts3 = new long[11];
            for (int i = 0; i < 11; i++)
            {
                stopWatch.Start();
                SortByExtraction(sample3);
                stopWatch.Stop();
                long ts = stopWatch.ElapsedTicks;
                if (i == 0)
                {
                    stopWatch.Reset();
                }
                else
                {
                    fullts3[i] = ts;
                    stopWatch.Reset();
                }
            }
            long min = fullts3.TakeLast(10).ToArray().Min();
            long max = fullts3.TakeLast(10).ToArray().Max();
            Console.WriteLine("SortByExtraction\nNanoseonds in test {0}", (max+min)/2);
            } 
    }
}
