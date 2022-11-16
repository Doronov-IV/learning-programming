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
            await Task.Run(() => WaitThreeSecAsync()).ContinueWith((a) => Console.WriteLine("Done!"));
        }


        private static void WaitThreeSecAsync()
        {
            Task.Delay(3000).Wait();
        }
    }
}