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
            // Instantiate the secure string.
            SecureString securePwd = new SecureString();
            ConsoleKeyInfo key;

            Console.Write("Enter password: ");
            do
            {
                key = Console.ReadKey(true);

                // Ignore any key out of range.
                if (((int)key.Key) >= 65 && ((int)key.Key <= 90))
                {
                    // Append the character to the password.
                    securePwd.AppendChar(key.KeyChar);
                    Console.Write("*");
                }
                // Exit if Enter key is pressed.
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            try
            {
                string password = new System.Net.NetworkCredential(string.Empty, securePwd).Password;
                Console.WriteLine(password);
            }
            catch (Win32Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                securePwd.Dispose();
            }
        
        
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