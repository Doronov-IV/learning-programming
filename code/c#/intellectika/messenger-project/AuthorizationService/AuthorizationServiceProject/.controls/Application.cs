global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.Net.Sockets;
global using System.Text;
global using System.Linq;
global using System.Net;
global using System;

global using Tools.Flags;
global using Tools.Formatting;

global using NetworkingAuxiliaryLibrary.Processing;
global using NetworkingAuxiliaryLibrary.Packages;
global using NetworkingAuxiliaryLibrary.Objects;


global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.EntityFrameworkCore;
using AuthorizationServiceProject.Net;
using AuthorizationServiceProject.Model.Context;

namespace AuthorizationServiceProject.Controls
{
    public class Application
    {



        #region STATE


         


        #endregion STATE




        #region API


        /// <summary>
        /// Loop private 'Run' method.
        /// <br />
        /// Зациклить приватный метод "Run".
        /// </summary>
        public void Start()
        {
            Run();
        }


        #endregion API




        #region LOGIC



        /// <summary>
        /// Run the application.
        /// <br />
        /// Запустить приложение.
        /// </summary>
        private void Run()
        {
            ServiceController controller = new();
            controller.SendServiceOutput += Console.WriteLine;
            controller.Run();
        }



        #endregion LOGIC




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Application()
        {
            using (AuthorizationDatabaseContext context = new()) { }
        }


        #endregion CONSTRUCTION



    }
}
