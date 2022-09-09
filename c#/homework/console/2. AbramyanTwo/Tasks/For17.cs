using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Tools.Formatting;

using Spectre.Console;


namespace Homework.NET
{

    public partial class Application
    {

        /// <summary>
        /// For 17 метод;
        /// 
        /// ƒано вещественное число A и целое число N(> 0). »спользу€ один цикл, найти сумму
        ///     1 + A + A2 + A3 + Е + AN.
        /// 
        /// </summary>
        private class For17 : Task, IExecutable
        {

            public For17()
            {
                TaskSentense = "";
                TaskName = "For 17";

                TaskSentense += ("\tƒано вещественное число A и целое число N(> 0). »спользу€ один цикл, найти сумму\n");
                TaskSentense += ("\t\t\t1 + A + A2 + A3 + Е + AN.");
            }

            public void Execute()
            {

                // <input>

                #region INPUT

                double dDecimalNumber = default(double);
                int nIntegerNumber = default(int);

                bool aintNoExceptions = true; // :p

                do // Decimal;
                {
                    try
                    {
                        PrintTasknameAndSentence();
                        AnsiConsole.Write(new Markup("\tPlease, enter a decimal value.\n\n\tYou: "));
                        dDecimalNumber = Convert.ToDouble(Console.ReadLine());
                        aintNoExceptions = true;
                    }
                    catch (FormatException e)
                    {
                        aintNoExceptions = false;
                        AnsiConsole.Write(new Markup("\n\t[red]Please, enter a proper one.[/]"));
                        Thread.Sleep(2000);
                        Console.Clear();
                    }

                } while (!aintNoExceptions);

                Console.Clear();

                do // Integer;
                {
                    try
                    {
                        PrintTasknameAndSentence();
                        AnsiConsole.Write(new Markup("\tPlease, enter an integer value.\n\n\tYou: "));
                        nIntegerNumber = int.Parse(Console.ReadLine());
                        aintNoExceptions = true;
                    }
                    catch (FormatException e)
                    {
                        aintNoExceptions = false;
                        AnsiConsole.Write(new Markup("\n\t[red]Please, enter a proper one.[/]"));
                        Thread.Sleep(2000);
                        Console.Clear();
                    }

                } while (!aintNoExceptions);

                #endregion INPUT

                // </input>

                // <loop>

                #region LOOP

                double dTemp = 1.0d;
                double dResult = 1.0d;

                for (int i = 2, nSize = nIntegerNumber; i <= nSize; ++i)
                {
                    dTemp *= dDecimalNumber;
                    dResult += dTemp;
                }

                #endregion LOOP

                // </loop>


                // <result>

                AnsiConsole.Write(new Markup("\n\t[green]Result:[/]\t" + "[underline]" + dResult + "[/]\n"));

                // </result>

            }

        }

    }

}