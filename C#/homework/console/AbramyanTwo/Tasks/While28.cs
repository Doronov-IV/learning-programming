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
        /// While 28 метод;
        /// 
        /// Дано вещественное число ε (> 0). Последовательность вещественных чисел AK определяется следующим образом:
        /// A1 = 2,        AK = 2 + 1/Av(K−1),    K = 2, 3, … .
        /// Найти первый из номеров K, для которых выполняется условие: 
        /// |AvK − Av(K−1)| < ε, и вывести этот номер, а также числа Av(K−1) и AvK.
        /// </summary>
        private class While28 : Task, IExecutable
        {

            public While28()
            {
                TaskSentense = "";
                TaskName = "While 28";

                TaskSentense += ("Дано вещественное число E (> 0). Последовательность вещественных чисел AK определяется следующим образом:\n");
                TaskSentense += ("\t\tA1 = 2,        AK = 2 + 1/Av(K-1),    K = 2, 3, ... .\n");
                TaskSentense += ("\tНайти первый из номеров K, для которых выполняется условие:\n");
                TaskSentense += ("\t\t|AvK - Av(K-1)| < E, и вывести этот номер, а также числа Av(K-1) и AvK.");
            }

            public void Execute()
            {
                // <input>

                #region INPUT

                double dDecimalNumber = default(double);

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

                #endregion INPUT

                // </input>


                // <loop>

                #region LOOP

                double dAk1 = 0.0d, dAk = 2.0d;
                int k = 1;
                while (Math.Abs(dAk - dAk1) >= dDecimalNumber)
                {
                    ++k;
                    dAk1 = dAk;
                    dAk = 2 + (1 / dAk1);
                }

                #endregion LOOP

                // </loop>


                // <result>

                AnsiConsole.Write(new Markup("\n\t[green]Result:[/]\t" + "[underline]" + "k = " + k + ", " + "Av(k-1) = " + dAk1 + ", "+ "Avk = " + dAk + "[/].\n"));

                // </result>
            }

        }

    }

}