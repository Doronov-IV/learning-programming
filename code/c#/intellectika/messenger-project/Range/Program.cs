using System;
using Newtonsoft.Json;
using NetworkingAuxiliaryLibrary.Processing;
using NetworkingAuxiliaryLibrary.Packages;
using System.Text;

namespace Range // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine($"Main start. Thread {Thread.CurrentThread.ManagedThreadId}");
            Application app = new();
            app.Run();
            Console.WriteLine($"Main end.");
            Console.ReadKey();
        }
    }
}