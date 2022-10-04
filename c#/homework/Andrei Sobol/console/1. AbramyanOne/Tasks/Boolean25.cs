using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


using Tools.Formatting;
using Tools.FileReader;


using Spectre.Console;


namespace Homework.NET
{

    public partial class Application
    {

        /// <summary>
        /// Boolean 25 метод;
        /// 
        ///     Даны числа x, y. Проверить истинность высказывания: «Точка с
        /// координатами(x, y) лежит во второй координатной четверти»;
        /// 
        /// </summary>
        public void Boolean25()
        {
            Console.WriteLine("\n");

            string sTaskSentense = "";
            string sTaskName = "Boolean 25";

            sTaskSentense += ("\t\tДаны числа x, y. Проверить истинность высказывания:\n");
            sTaskSentense += ("\t \"Точка с координатами(x, y) лежит во второй координатной четверти\".\n");

            Console.WriteLine(sTaskSentense);
            Console.WriteLine("\n");

            int nXCoordinate = 0;
            int nYCoordinate = 0;


            Console.Write("\tPlease, enter an X coordinate\n\n\tYou: ");


            #region COORDINATES_INPUT


            try
            {
                nXCoordinate = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                AnsiConsole.Write(new Markup("\n\t[red]Please, enter a digital number.[/]\n"));
                return;
            }

            Console.WriteLine("\n");

            Console.Write("\tPlease, enter an Y coordinate\n\n\tYou: ");

            try
            {
                nYCoordinate = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                AnsiConsole.Write(new Markup("\n\t[red]Please, enter a digital number.[/]\n"));
                return;
            }


            #endregion COORDINATES_INPUT


            Console.WriteLine("\n");


            #region RESULT_CALCULATION_AND_OUTPUT


            int bRes = 0;
            if (nXCoordinate < 0 && nYCoordinate > 0) bRes = 1;
            else if (nXCoordinate == 0 || nYCoordinate == 0) bRes = 0;
            else if (nXCoordinate > 0 || nYCoordinate < 0) bRes = -1;

            switch (bRes)
            {
                case 1:
                    AnsiConsole.Write(new Markup("\t[green]Indeed: The point specified by those coordinates " +
                        "\n\tof yours lies in the 2nd quarter.[/]"));
                    break;
                case 0:
                    AnsiConsole.Write(new Markup("\t[Blue]One of your coordinates is equal to zero. \n\tTry proper specification.[/]"));
                    break;

                case -1:
                    AnsiConsole.Write(new Markup("\t[Blue]I'm afraid, the point specified by those coordinates " +
    "\n\tof yours doesn't lie in the 2nd quarter.[/]"));
                    break;
            }


            #endregion RESULT_CALCULATION_AND_OUTPUT


            Console.WriteLine("\n");
        }

    }

}