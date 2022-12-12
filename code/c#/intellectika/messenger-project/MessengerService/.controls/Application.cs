global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Linq;
global using System.Text;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Diagnostics;
global using System.Net;
global using System.Net.Sockets;
global using System.Runtime.CompilerServices;

global using Tools.Interfaces;
global using Tools.Collections.Generic;
global using Tools.Formatting;

global using NetworkingAuxiliaryLibrary.Packages;
global using NetworkingAuxiliaryLibrary.Processing;
global using NetworkingAuxiliaryLibrary.Dependencies;
global using NetworkingAuxiliaryLibrary.Objects;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MessengerService.Controls
{
    public class Application
    {


        #region API


        /// <summary>
        /// Loop the 'Run' method.
        /// <br />
        /// Зациклить метод "Run".
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
            ProcessingStatus.ToggleCompletion();
            var task1 = Service.ListenAuthorizerAsync();
            var task2 = Service.ListenClientsAsync();
            await Task.WhenAny(task1, task2);
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

        }


        #endregion CONSTRUCTION



    }
}
