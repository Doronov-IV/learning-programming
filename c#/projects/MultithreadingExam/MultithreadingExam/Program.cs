global using System.IO;
global using System.Text.RegularExpressions;
global using System.Text;
global using System.Diagnostics;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;

using Doronov.ConcurrencyExam.Forms;

namespace Doronov.ConcurrencyExam.Main
{
    internal static class Program
    {
        private static Mutex mutex = null;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string appName = "MultithreadingExam";
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                return;
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new TreeMainForm());
        }
    }
}