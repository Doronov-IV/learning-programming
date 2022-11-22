using System;

namespace ShootingRange
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var s = new S();
            using (s)
            {
                Console.WriteLine(s.GetDispose());
            }
            Console.WriteLine(s.GetDispose());
        }
    }



    public struct S : IDisposable
    {
        public bool dispose;
        public void Dispose()
        {
            dispose = true;
        }
        public bool GetDispose()
        {
            return dispose;
        }
    }
}