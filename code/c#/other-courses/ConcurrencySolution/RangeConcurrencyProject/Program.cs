global using System;
using System.Diagnostics;

namespace Range
{
    public class Program
    {

        public static Stopwatch sw = new();

        public static List<Task> tasks = new();

        public static async Task Main(string[] args)
        {
            await Task.Run(() => DisplayPrimesCount());
        }


        private static void WaitThreeSecAsync()
        {
            Task.Delay(3000).Wait();
        }


        public static async Task DisplayPrimesCount()
        {
            int result = await GetPrimesCountAsync(2, 1000000);
            Console.WriteLine (result);
        }


        public static Task<int> GetPrimesCountAsync (int start, int count)
        {
            return Task.Run(() =>
                ParallelEnumerable.Range(start, count).Count(n =>
                Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
        }
    }
}