global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.Linq;
global using System.Text;
global using System;
using Tools.Toolbox;

namespace ShootingRange
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine(Utilizer.GetLocalIPAddress());
        }
    }
}