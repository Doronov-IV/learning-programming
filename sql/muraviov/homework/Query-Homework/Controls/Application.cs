// Sic Parvis Magna

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



#pragma warning disable



namespace SQL.Homework.Controls
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

            // write your code here ...

        }


        /// <summary>
        /// Loop Run method;
        /// <br />
        /// Зациклить метод Run;
        /// </summary>
        public void Start()
        {
            // <sys settings>
            Console.Title = "SQL Homework";
            Console.CursorVisible = false;
            // <sys settings />

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
