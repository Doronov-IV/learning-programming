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
        public static void Main(string[] args)
        {
            Class aaaaaa1 = new Class();
            Class aaaaaa2 = new Class();
            Struct bbbbbb1 = new Struct();
            Struct bbbbbb2 = new Struct();
            Struc cccccc1 = new Struc();
            Struc cccccc2 = new Struc();

            bbbbbb2 = bbbbbb1;
            cccccc2 = cccccc1;

            ChangeShit(bbbbbb1);
            ChangeShit(ref bbbbbb2);
            Console.WriteLine(bbbbbb1.id);
            Console.WriteLine(bbbbbb2.id);

            ChangeShit(cccccc1);
            ChangeShit(ref cccccc2);
            Console.WriteLine(cccccc1.id);
            Console.WriteLine(cccccc2.id);



        }

        public static void ChangeShit(Class Class)
        {
            Class.Value = "AAAAAAAAAA";
            Class.Id = 999;
        }

        public static void ChangeShit(ref Class Class)
        {
            Class.Value = "AAAAAAAAAA";
            Class.Id = 999;
        }


        public static void ChangeShit(Struct Struct)
        {
            Struct.Value = "AAAAAAAAAA";
            Struct.Id = 999;
        }

        public static void ChangeShit(ref Struct Struct)
        {
            Struct.Value = "AAAAAAAAAA";
            Struct.Id = 999;
        }

        public static void ChangeShit(Struc Struc)
        {
            Struc.Value = "AAAAAAAAAA";
            Struc.Id = 999;
        }

        public static void ChangeShit(ref Struc Struc)
        {
            Struc.Value = "AAAAAAAAAA";
            Struc.Id = 999;
        }

    }


    public class Class
    {
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

        public Struct structa = new();
    }


    public struct Struct 
    {
        public string Struc
        {
            get
            {
                return "Struc";
            }
        }

        public string value;
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }


    public ref struct Struc
    {
        public string Stru
        {
            get
            {
                return "Stru";
            }
        }

        public string value;
        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }






}