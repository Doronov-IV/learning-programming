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
            Task<int> primeBumberTask = Task.Run(() =>
            Enumerable.Range(2, 20000000).Count(n =>
            Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

            Console.WriteLine("Task running.....");
            Console.WriteLine("The answer is " + primeBumberTask.Result);
        }


        private static void WaitThreeSecAsync()
        {
            Task.Delay(3000).Wait();
        }
    }
}