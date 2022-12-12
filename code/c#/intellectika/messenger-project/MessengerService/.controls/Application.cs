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
global using NetworkingAuxiliaryLibrary;
global using NetworkingAuxiliaryLibrary.Objects;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MessengerService.Datalink;
using Spectre.Console;
using NetworkingAuxiliaryLibrary.Style.Common;

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
        public async Task Start()
        {
            await Run();
        }


        #endregion API





        #region LOGIC



        /// <summary>
        /// Run the application.
        /// <br />
        /// Запустить приложение.
        /// </summary>
        private async Task Run()
        {
            AnsiConsole.Write(new Markup(ConsoleServiceStyleCommon.GetMessengerGreeting()));
            ServiceController controller = new();
            var task1 = controller.ListenAuthorizerAsync();
            var task2 = controller.ListenClientsAsync();
            await Task.WhenAll(task1, task2);
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
