using ShootingRangeProject;
using System;

namespace ShootingRange
{
    public static class Program
    {
        public class ObjectX : IInterfaceable, IIable
        {
            public string value;

            public void Foo()
            {
                dynamic obj = new object();
                obj.Bar();
            }
        }
        static void Main(string[] args)
        {
            ObjectX A = new();
            A.value = "abc";
            ObjectX B = A;
            Console.WriteLine(B.value);
            A.value = "hello";
            Console.WriteLine(B.value);
            A.Foo();
        }


        
    }
}