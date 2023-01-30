using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingRangeProject
{
    public class WhiteConsoleLogService : ILogService
    {
        private static string style = "white on black";

        public void Write(string message)
        {

            AnsiConsole.Write(new Markup($"[{style}][[{DateTime.Now.ToString("dd.MM.yy HH:mm:ss:fff")}]] {message}.[/]\n"));
        }
    }
}
