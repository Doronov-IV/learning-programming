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
            Classs clas = new();

            Console.WriteLine()
        }


        public interface ISomethingable
        {
            public int aaaa { set; get; }

            public virtual void A()
            {
                aaaa = 5;
            }
        }


        public interface IInterfaceable 
        {
            public int aaaa { set; get; }


            public virtual void B()
            {
                aaaa = 5;
            }
        }


        public class Classs : IInterfaceable, ISomethingable
        { 

            int IInterfaceable.aaaa { set; get; }
            int ISomethingable.aaaa { set; get; }

            public void A()
            {
                aaaa = 5;
            }

            public void B()
            {
                aaaa = 10;
            }
        }


    }
}