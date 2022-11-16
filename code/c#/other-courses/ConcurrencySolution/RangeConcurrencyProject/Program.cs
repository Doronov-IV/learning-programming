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
            var tcs = new TaskCompletionSource<int>();

            new Thread (() =>
            {
                Thread.Sleep (3000);
                tcs.SetResult (42);
            })
            { IsBackground = true }.Start ();

            Task<int> task = tcs.Task;
            Console.WriteLine (task.Result);
        }


        private static void WaitThreeSecAsync()
        {
            Task.Delay(3000).Wait();
        }
    }
}