global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.Linq;
global using System.Text;
global using System;
using Tools.Toolbox;
using Tools.Formatting;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using ShootingRangeProject;

namespace ShootingRange
{

    public static class Program
    {
        static IEnumerable<int> Square(IEnumerable<int> a)
        {
            foreach (var r in a)
            {
                Console.WriteLine(r * r);
                yield return r * r;
            }
        }
        class Wrap
        {
            private static int init = 0;
            public int Value
            {
                get { return ++init; }
            }
        }
        static void Main(string[] args)
        {
            Classs clas = 5;

            clas.A();

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);

            Console.ReadKey();
        }


        public interface ISomethingable
        {
            public void A();
        }


        public interface IInterfaceable : ISomethingable
        {
            public void B();
        }



        public class Classs : IInterfaceable
        {
            public int a = 5;

            public unsafe void A()
            {


                unsafe
                {
                    const int array_size = 99999999;
                    int[] array = new int[array_size];
                    int* arrayPtr = (int*)&array;
                    int i = 0;

                    array = null;

                    Thread.Sleep(5000);
                }

            }

            public void B()
            {
                throw new NotImplementedException();
            }
        }


    }
}