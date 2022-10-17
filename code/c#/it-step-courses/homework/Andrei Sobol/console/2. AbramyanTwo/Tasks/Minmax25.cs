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
        /// Minmax 25 метод;
        /// 
        /// Дано целое число N (> 1) и набор из N чисел. Найти номера двух соседних чисел из данного набора, 
        /// произведение которых является минимальным, и вывести вначале меньший, а затем больший номер.
        /// 
        /// </summary>
        private class Minmax25 : Task, IExecutable
        {

            public Minmax25()
            {
                TaskSentense = "";
                TaskName = "Minmax 25";

                TaskSentense += ("Дано целое число N (> 1) и набор из N чисел. Найти номера двух соседних чисел из данного набора,\n");
                TaskSentense += ("\tпроизведение которых является минимальным, и вывести вначале меньший, а затем больший номер.\n");
            }

            public void Execute()
            {

                // <input and loop>

                #region INPUT_AND_LOOP

                int nAmountOfDecimals = default(int);

                bool aintNoExceptions = true; // :p

                PrintTasknameAndSentence();

                do // Integer;
                {
                    try
                    {
                        AnsiConsole.Write(new Markup("\tPlease, enter an integer value.\n\n\tYou: "));
                        nAmountOfDecimals = int.Parse(Console.ReadLine());
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


                double dTemp1 = 0.0d;
                double dTemp2 = 0.0d;
                double dMinMultiplier = 0.0d;

                int nResult = default(int);


                for (int i = 1, nSize  = nAmountOfDecimals; i <= nSize; ++i)
                {
                    dTemp2 = dTemp1;

                    do // Decimal;
                    {

                        Console.Clear();
                        PrintTasknameAndSentence();

                        try
                        {
                            //TasknameAndSentence();
                            AnsiConsole.Write(new Markup($"\n\t{i}/{nAmountOfDecimals}: "));
                            dTemp1 = Convert.ToDouble(Console.ReadLine());
                            AnsiConsole.Write(new Markup("\n\t"));
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

                    if ((i == 2) || (dTemp1 * dTemp2 < dMinMultiplier))
                    {
                        dMinMultiplier = dTemp1 * dTemp2;
                        nResult = i;
                    }

                    Console.Clear();
                    PrintTasknameAndSentence();

                }

                #endregion INPUT_AND_LOOP

                // </input and loop>


                // <result>

                AnsiConsole.Write(new Markup("\n\t[green]Result:[/]\t" + "[underline]" + (nResult-1) + ", " + nResult + "[/]\n"));

                // </result>
            }

        }

    }

}