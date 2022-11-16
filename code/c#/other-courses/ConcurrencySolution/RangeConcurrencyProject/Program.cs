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
            Task<int> primeNumberTask = Task.Run (() =>
            Enumerable.Range (2, 9000000).Count(n =>
            Enumerable.Range (2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

            await primeNumberTask.ContinueWith(antecedent =>
            {
                int result = antecedent.Result;
                Console.WriteLine(result);
            });
        }


        private static void WaitThreeSecAsync()
        {
            Task.Delay(3000).Wait();
        }
    }
}