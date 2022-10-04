// Sic Parvis Magna

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Tools.Formatting;
using Delivery.Service;
using Delivery.BaseClasses;
using Delivery.Builder;
using Delivery.Visual;

using Spectre.Console;


/// <summary>
/// v 0.1.0
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
        private void Run()
        {

            // <first input> 
            int nDaysInput = 0;

            int nPresetInput = 0;

            AnsiConsole.Write(new Markup("\n\n\t\tChoose the amount of [underline]Days[/] you want to make order for (1-14).\n\n\t\tYou: "));

            nDaysInput = Toolbox.GetSimpleUserInput(1, 14);
            // <first input /> 


            // <second input> 
            AnsiConsole.Write(new Markup("\n\n\t\tChoose the Preset:\n\n"));

            string sUserPresetInput = Toolbox.GetUserInputUponMenu("Light (2 courses);", "Medium (3 courses);", "Hard (4 courses).");

            switch (sUserPresetInput)
            {
                case "Light (2 courses);":
                    nPresetInput = 1;
                    break;
                case "Medium (3 courses);":
                    nPresetInput = 2;
                    break;
                case "Hard (4 courses).":
                    nPresetInput = 3;
                    break;
            }
            // <second input /> 


            // <result>
            Console.Clear();

            OrderDirector director = new OrderDirector();

            director.DaysNumber = nDaysInput;

            director.PresetNumber = nPresetInput;

            OrderDecorator decorator = new OrderDecorator(director.Build());

            decorator.Print();
            // <result />

        }


        /// <summary>
        /// Loop Run method;
        /// <br />
        /// Зациклить метод Run;
        /// </summary>
        public void Start()
        {
            // <sys settings>
            Console.Title = "Delivery";
            Console.CursorVisible = false;
            // <sys settings />


            System.ConsoleKey c_cont = ConsoleKey.Y;

            do
            {
                Console.Clear();

                Run();

                Console.WriteLine("\n");

                Toolbox.ShowContinue();

                c_cont = Console.ReadKey().Key;

                Console.Clear();

            } while (c_cont != ConsoleKey.N);

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



        #region CONSTRUCTION


        /// <summary>
        /// An instance of the class;
        /// <br />
        /// Экземпляр класса;
        /// </summary>
        private static Application instance;


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        private Application()
        {

        }


        /// <summary>
        /// Get the instance;
        /// <br />
        /// Получить экземпляра класса;
        /// </summary>
        /// <returns>A pointer to the instance;<br />Указатель на экземпляр;</returns>
        public static Application GetInstance()
        {
            if (instance == null)
            {
                instance = new Application();
            }
            return instance;
        }


        #endregion CONSTRUCTION


    }
}
