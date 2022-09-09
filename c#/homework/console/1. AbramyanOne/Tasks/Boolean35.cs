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
        /// Boolean 35 метод;
        /// 
        ///     Даны координаты двух различных полей шахматной доски x1,
        /// y1, x2, y2(целые числа, лежащие в диапазоне 1–8). Проверить истинность
        /// высказывания: «Данные поля имеют одинаковый цвет»;
        /// 
        /// </summary>
        public void Boolean35()
        {
            Console.WriteLine("\n");

            // <sentence>
            string sTaskSentense = "";
            string sTaskName = "Boolean 35";
            sTaskSentense += ("\t\tДаны координаты двух различных полей шахматной доски x1, \n");
            sTaskSentense += ("\t y1, x2, y2(целые числа, лежащие в диапазоне 1–8). Проверить истинность\n");
            sTaskSentense += ("\t\tвысказывания: \"Данные поля имеют одинаковый цвет\"; \n");
            // </sentence>


            // <input>
            int[] aUserInput = new int[4];
            bool bIsUserInputCorrect;
            do
            {
                Console.WriteLine(sTaskName);
                Console.WriteLine("\n");
                Console.WriteLine(sTaskSentense);
                Console.WriteLine("\n");

                bIsUserInputCorrect = true;
                for (int i = 1; i <= 4; i++)
                {
                    try
                    {
                        AnsiConsole.Write(new Markup($"\n\tPlease, enter an unsigned digit (1-8) {i}/4.\n"));
                        AnsiConsole.Write(new Markup("\tYou: "));
                        aUserInput[i - 1] = Int32.Parse(Console.ReadLine());
                        if (aUserInput[i - 1] > 8)
                        {
                            bIsUserInputCorrect = false;
                            break;
                        }
                    }
                    catch (FormatException e)
                    {
                        AnsiConsole.Write(new Markup("\n\t[red]Please, enter a digital number.[/]\n"));
                        return;
                    }
                    finally
                    {
                        AnsiConsole.Write(new Markup("\n"));
                    }
                }

                if (!bIsUserInputCorrect)
                {
                    AnsiConsole.Write(new Markup("\n\n\t[red]Please, enter an unsigned number between 1 and 8.[/]\n\n\n"));
                    return;
                }

            } while (!bIsUserInputCorrect);
            // </input>


            //<logic>
            if ((aUserInput[0] + aUserInput[1]) % 2 == (aUserInput[2] + aUserInput[3]) % 2)
            {
                AnsiConsole.Write(new Markup("\n\t[Green] Indeed, the squares are of the same color.\n\n[/]"));
            }
            else
            {
                AnsiConsole.Write(new Markup("\n\t[Blue] I'm afraid, the squares do not share the same color.\n\n[/]"));
            }
            //</logic>

            Console.WriteLine("\n");
        }

    }

}