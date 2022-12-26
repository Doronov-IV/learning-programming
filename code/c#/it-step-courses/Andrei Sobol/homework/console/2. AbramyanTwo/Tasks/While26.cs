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
        /// While 26;
        /// 
        /// Дано целое число N (> 1), являющееся числом Фибоначчи: N = FvK (определение чисел Фибоначчи дано в задании While24). 
        /// Найти целые числа Fv(K−1) и Fv(K+1) — предыдущее и последующее числа Фибоначчи.
        /// 
        /// </summary>
        private class While26 : Task, IExecutable
        {

            public While26()
            {
                TaskSentense = "";
                TaskName = "While 26";

                TaskSentense += ("Дано целое число N (> 1), являющееся числом Фибоначчи: N = FvK\n");
                TaskSentense += ("\t(определение чисел Фибоначчи дано в задании While24).\n");
                TaskSentense += ("\tНайти целые числа Fv(K−1) и Fv(K+1) — предыдущее и последующее числа Фибоначчи.\n");
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

                int nFibonacci1 = 1, nFibonacci2 = 1, nFibonacci0 = 0;

                while (nFibonacci0 < nIntegerNumber)
                {
                    nFibonacci0 = nFibonacci2 + nFibonacci1;
                    nFibonacci2 = nFibonacci1;
                    nFibonacci1 = nFibonacci0;
                }

                #endregion LOOP

                // </loop>


                // <result>

                AnsiConsole.Write(new Markup("\n\t[green]Result:[/]\t" + "[underline]" + "Fv(k-1) = " + nFibonacci2 + ", " + "Fv(k+1) = " + (nFibonacci2 + nFibonacci1) + "[/]\n"));

                // </result>

            }

        }

    }

}