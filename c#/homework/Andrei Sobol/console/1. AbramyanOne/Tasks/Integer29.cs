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
        /// Integer 29 метод;
        /// 
        ///     Даны целые положительные числа A, B, C. На прямоугольнике 
        /// размера A × B размещено максимально возможное количество квадратов со
        /// стороной C (без наложений). Найти количество квадратов, размещенных
        /// на прямоугольнике, а также площадь незанятой части прямоугольника;
        /// 
        /// </summary>
        public void Integer29()
        {
            Console.WriteLine("\n");

            string sTaskSentense = "";
            string sTaskName = "Integer 29";

            sTaskSentense += ("\tДаны целые положительные числа A, B, C. На прямоугольнике \n");
            sTaskSentense += ("\tразмера A × B размещено максимально возможное количество квадратов со\n");
            sTaskSentense += ("\tстороной C (без наложений). Найти количество квадратов, размещенных\n");
            sTaskSentense += ("\tна прямоугольнике, а также площадь незанятой части прямоугольника;\n");


            int[] arrayInput = new int[3];

            bool bIsUserInputCorrect;

            do
            {
                Console.WriteLine(sTaskName);
                Console.WriteLine("\n");
                Console.WriteLine(sTaskSentense);
                Console.WriteLine("\n");
                for (int i = 1; i <= 3; i++)
                {
                    Console.Write($"\n\tPlease, enter value {i}\n\tYou: ");

                    try
                    {
                        arrayInput[i - Toolbox.NUMERATION_ISSUE] = Int32.Parse(Console.ReadLine());
                    }
                    catch (FormatException e)
                    {
                        i--;
                        AnsiConsole.Write(new Markup("\n\t[red]Please, enter a digital number.[/]\n"));
                    }
                }

                bIsUserInputCorrect = arrayInput[2] < arrayInput[1] && arrayInput[2] < arrayInput[0];

                if (!bIsUserInputCorrect)
                {
                    Console.Clear();
                    AnsiConsole.Write(new Markup("\n\n\t[red]Your square size is larger than rectangle's one.[/]\n\n\n"));
                }

            } while (!bIsUserInputCorrect);

            int nRectangleSize = arrayInput[0] * arrayInput[1];

            int nSquareSize = arrayInput[2] * arrayInput[2];

            int nFitCounter = 0;

            while (nRectangleSize >= nSquareSize)
            {
                nRectangleSize -= nSquareSize;
                nFitCounter += 1;
            }

            nRectangleSize = arrayInput[0] * arrayInput[1];

            int nSizeLeft = 0;

            nSizeLeft = nRectangleSize - (nSquareSize * nFitCounter);


            Console.WriteLine("\n");
            AnsiConsole.Write(new Markup($"\t[underline]Your square fits {nFitCounter} times.[/]"));
            if (nSizeLeft > 0) AnsiConsole.Write(new Markup($"\t[underline] Size left: {nSizeLeft} units.[/]"));
        }

    }

}