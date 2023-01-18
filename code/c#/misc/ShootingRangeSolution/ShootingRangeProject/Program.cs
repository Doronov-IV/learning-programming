global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.Linq;
global using System.Text;
global using System;
using Tools.Toolbox;
using Tools.Formatting;

namespace ShootingRange
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            
            string A = DateTime.Now.ToString("HH:mm:ss:fff");
            string B = new string(A); 
            string C = new string(A);

            var bRes = IsTimeAproximatelyEqual(B, C);
        }

        public static void ChangeClass(Class @class) => @class.name = "Hej";
        public static void ChangeVariable(int variable, out int res) => res = variable;


        public static bool IsTimeAproximatelyEqual(string timeOne, string timeTwo)
        {
            int nTimeOne = Int32.Parse(StringDateTime.RemoveSeparation(timeOne));
            int nTimeTwo = Int32.Parse(StringDateTime.RemoveSeparation(timeTwo));

            int debugResOne = nTimeOne;
            int debugResTwo = nTimeTwo;

            return debugResOne == debugResTwo;
        }
    }


    public class Class
    {
        public string name;
        public int age;
    }

    
}