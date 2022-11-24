using System;

namespace ShootingRange
{
    public static class Program
    {
        public struct /*class*/ ObjectX
        {
            public string value;
        }
        static void Main(string[] args)
        {
            ObjectX A = new();
            A.value = "abc";
            ObjectX B = A;
            Console.WriteLine(B.value);
            A.value = "hello";
            Console.WriteLine(B.value);
        }


        
    }
}