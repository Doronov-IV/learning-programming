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
/// v 1.0.0
/// </summary>


namespace Homework.NET
{
    /// <summary>
    /// An "application" which starts the program;
    /// <br />
    /// "Приложение", которое запускает программу;
    /// </summary>
    public partial class Application
    {

        #region MAIN

        /// <summary>
        /// Menu-method;
        /// <br />
        /// Метод-меню;
        /// </summary>
        public void Run()
        {
            // <sys settings>
            Console.Title = "Students";
            Console.CursorVisible = false;
            // </sys settings>

            var demo = CommunityHub.GetInstance();

            System.ConsoleKey c_cont = ConsoleKey.Y;
            while (c_cont != ConsoleKey.N)
            {
                Console.WriteLine("\n");
                AnsiConsole.Write(new Markup("[yellow]\tMade by Doronov4, using \"Spectre Console\".[/]"));
                Console.WriteLine("\n");

                string sUserInput = Toolbox.GetUserInputUponMenu("Grades Table", "Average grades", "Average grades for subjects",
                    "Group's average", "Min max grades", "Exit");

                Console.Clear();
                Console.WriteLine("\n\n\n");


                switch (sUserInput)
                {
                    case "Grades Table":
                        demo.PrintAllStudentsGrades();
                        break;
                    case "Average grades":
                        demo.PrintEachStudentAvg();
                        break;
                    case "Group's average":
                        demo.PrintGroupsAvg();
                        break;
                    case "Average grades for subjects":
                        demo.PrintEachStudentAvgForSubjects();
                        break;
                    case "Min max grades":
                        demo.PrintMinMax();
                        break;
                    case "Exit":
                        Toolbox.DrawFarewell();
                        return;
                    default:
                        break;
                }
                Console.WriteLine("\n\n\n");
                Toolbox.ShowContinue();
                c_cont = Console.ReadKey().Key;
                Console.Clear();
            }
            Toolbox.DrawFarewell();

        }


        /// <summary>
        /// Test application;
        /// <br />
        /// Протестировать приложение;
        /// </summary>
        public void Debug()
        {
            Console.WriteLine("\n");

            // write your code here ...

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
