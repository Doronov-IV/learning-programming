﻿global using System.Collections.Generic;
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
            var w = new Wrap();
            var wraps = new Wrap[3];
            for (int i = 0; i < wraps.Length; i++)
            {
                wraps[i] = w;
            }

            var values = wraps.Select(x => x.Value);
            var results = Square(values);
            int sum = 0;
            int count = 0;
            foreach (var r in results)
            {
                count++;
                sum += r;
            }
            Console.WriteLine("Count {0}", count);
            Console.WriteLine("Sum {0}", sum);

            Console.WriteLine("Count {0}", results.Count());
            Console.WriteLine("Sum {0}", results.Sum());
        }

    }
}