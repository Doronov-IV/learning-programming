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
/// v 1.1.4
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
            // <sys settings>
            Console.Title = "Timer++";
            Console.CursorVisible = false;
            // </sys settings>

            // <timer>
            Timer timer = new Timer();
            timer.Start();
            // </timer>
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


        #region CONSTRUCTION


        /// <summary>
        /// The instance of a class;
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
        /// Get instance of a class;
        /// <br />
        /// Получить экземпляра класса;
        /// </summary>
        /// <returns>
        /// A pointer to the instance;
        /// <br />
        /// Указатель на экземпляр;
        /// </returns>
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
