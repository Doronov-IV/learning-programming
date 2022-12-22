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
            var s1 = string.Format("{0},{1}", "abc", "cba");
            var s2 = "abc";
            var s4 = "cba";
            s2 = s2 + s4;
            var s3 = "abccba";

            Console.WriteLine(s1 == s2);
            Console.WriteLine((object)s1 == (object)s2);
            Console.WriteLine(s2 == s3);
            Console.WriteLine((object)s2 == (object)s3);

            return;
            Class @class = new() { name = "Aoba", age = 100 };

            ChangeClass(@class);

            Console.WriteLine(@class.name);

            int a = 10;
            int unkn;

            ChangeVariable(a, out unkn);

            Console.WriteLine(unkn);
        }

        public static void ChangeClass(Class @class) => @class.name = "Hej";
        public static void ChangeVariable(int variable, out int res) => res = variable;
    }


    public class Class
    {
        public string name;
        public int age;
    }

    
}