// Sic Parvis Magna

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Tools.Formatting;

using Spectre.Console;


/// <summary>
/// v 1.1.1
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

                string sUserInput = Toolbox.GetUserInputUponMenu("Case 10", "Case 15", "For 17",
                    "For 38", "While 26", "While 28", "Minmax 25",  "Exit");

                Console.Clear();

                IExecutable CurrentTask;

                switch (sUserInput)
                {
                    case "Case 10":
                        CurrentTask = new Case10();
                        CurrentTask.Execute();
                        break;
                    case "Case 15":
                        CurrentTask = new Case15();
                        CurrentTask.Execute();
                        break;
                    case "For 17":
                        CurrentTask = new For17();
                        CurrentTask.Execute();
                        break;
                    case "For 38":
                        CurrentTask = new For38();
                        CurrentTask.Execute();
                        break;
                    case "While 26":
                        CurrentTask = new While26();
                        CurrentTask.Execute();
                        break;
                    case "While 28":
                        CurrentTask = new While28();
                        CurrentTask.Execute();
                        break;
                    case "Minmax 25":
                        CurrentTask = new Minmax25();
                        CurrentTask.Execute();
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
