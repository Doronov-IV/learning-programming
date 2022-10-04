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
        /// For 38 метод;
        /// 
        /// Дано целое число N (> 0). Найти сумму
        ///     1^N + 2^(N−1) + … + N^1.
        /// Чтобы избежать целочисленного переполнения, вычислять слагаемые этой суммы с помощью вещественной
        /// переменной и выводить результат как вещественное число.
        /// 
        /// </summary>
        private class For38 : Task, IExecutable
        {

            public For38()
            {
                TaskSentense = "";
                TaskName = "For 38";

                TaskSentense += ("Дано целое число N (> 0). Найти сумму:\n");
                TaskSentense += ("\t\t1^N + 2^(N−1) + … + N^1.\n");
                TaskSentense += ("\tЧтобы избежать целочисленного переполнения, вычислять слагаемые этой суммы с помощью\n");
                TaskSentense += ("\tвещественной переменной и выводить результат как вещественное число.\n");
            }

            public void Execute()
            {

                // <input>

                #region INPUT

                int nIntegerNumber = default(int);

                bool aintNoExceptions = true; // :p

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

                double dResult = 0.0d;
                double dTemp = 0.0d;

                for (int i = 1, nSize = nIntegerNumber; i <= nSize; ++i)
                {
                    dTemp = i;

                    for (int i2 = 1; i2 <= nSize - i; ++i2)
                        dTemp *= i;

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