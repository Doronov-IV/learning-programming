// Sic Parvis Magna

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Spectre.Console;

using Tools.Formatting;
using Tools.Formatting;


/// <summary>
/// v 1.2.2
/// </summary>


namespace Homework.NET
{
    /// <summary>
    /// Класс "Приложение", в котором всё происходит;
    /// </summary>
    public partial class Application
    {

        #region MAIN

        /// <summary>
        /// Метод-меню класса;
        /// </summary>
        public void Run()
        {

            System.ConsoleKey c_cont = ConsoleKey.Y;
            while (c_cont != ConsoleKey.N)
            {
                Console.WriteLine("\n");
                AnsiConsole.Write(new Markup("[yellow]\tMade by Doronov4, using \"Spectre Console\".[/]"));
                Console.WriteLine("\n");

                string sUserInput = Toolbox.GetUserInputUponMenu("Integer 24", "Integer 29", "Boolean 25",
                    "Boolean 30", "Boolean 35", "Exit");

                Console.Clear();
                switch (sUserInput)
                {
                    case "Integer 24":
                        Integer24();
                        break;
                    case "Integer 29":
                        Integer29();
                        break;
                    case "Boolean 25":
                        Boolean25();
                        break;
                    case "Boolean 30":
                        Boolean30();
                        break;
                    case "Boolean 35":
                        Boolean35();
                        break;
                    case "Exit":
                        Toolbox.DrawFarewell();
                        return;
                    default:
                        break;
                }
                Console.WriteLine("\n");
                Toolbox.ShowContinue();
                c_cont = Console.ReadKey().Key;
                Console.Clear();
            }
            Toolbox.DrawFarewell();
        }


        /// <summary>
        /// Метод для тестиования всего;
        /// </summary>
        public void Debug()
        {
            Console.WriteLine("\n");

            // write your code here...

            Console.WriteLine("\n");
        }

        #endregion MAIN


        #region SINGLETON_STUFF

        /// <summary>
        /// Экземпляр приложения;
        /// </summary>
        private static Application instance;

        /// <summary>
        /// Конструктор по умолчанию;
        /// </summary>
        private Application()
        {

        }

        /// <summary>
        /// Геттер экземпляра класса;
        /// </summary>
        /// <returns>Указатель на экземпляр;</returns>
        public static Application GetInstance()
        {
            if (instance == null)
            {
                instance = new Application();
            }
            return instance;
        }

        #endregion SINGLETON_STUFF

    }
}
