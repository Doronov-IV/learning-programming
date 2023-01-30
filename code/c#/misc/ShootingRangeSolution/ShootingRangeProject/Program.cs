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

namespace ShootingRange
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            IInterfaceable clas = new Class();
            Console.WriteLine(clas.GetStuff(args));

            // equals

            Class clas1 = new Class();
            Console.WriteLine(((IInterfaceable)clas1).GetStuff(args));

        }

    }


    public class Class : IInterfaceable, ISomethingable
    {

        string IInterfaceable.GetStuff(string[] args)
        {
            string res = "interfaceable";
            return res;
        }

        string ISomethingable.GetStuff(string[] args)
        {
            string res = "somethingable";
            return res;
        }


        public string Type
        {
            get
            {
                return GetType().ToString();
            }
        }

        public string Clas
        {
            get
            {
                return "Clas";
            }
        }

        public string value = "Hello";

        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }

        public int id = 1;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }

    public interface IInterfaceable
    {
        public string GetStuff(string[] args);
    }


    public interface ISomethingable 
    {
        public string GetStuff(string[] args);
    }
   
}