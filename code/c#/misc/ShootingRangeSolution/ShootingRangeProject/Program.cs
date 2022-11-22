using System;

namespace ShootingRange
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            int i = 1;
            object obj = i;
            ++i;
            Console.WriteLine(i);
            Console.WriteLine(obj);
            Console.WriteLine(obj);
        }
    }
}