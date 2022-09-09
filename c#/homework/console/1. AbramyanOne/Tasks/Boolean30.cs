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
        /// Boolean 30 метод;
        /// 
        ///         Даны целые числа a, b, c, являющиеся сторонами некоторого треугольника. 
        /// Проверить истинность высказывания: «Треугольник со сторонами a, b, c является равносторонним»;
        /// 
        /// </summary>
        public void Boolean30()
        {
            Console.WriteLine("\n");

            string sTaskSentense = "";
            string sTaskName = "Boolean 30";

            sTaskSentense += ("\t\tДаны целые числа a, b, c, являющиеся сторонами некоторого треугольника. \n");
            sTaskSentense += ("\t \"Проверить истинность высказывания: \"Треугольник со сторонами a, b, c является равносторонним\".\n");

            Console.WriteLine(sTaskSentense);
            Console.WriteLine("\n");

            double[] aUserInput = new double[3];

            for (int i = 1; i <= 3; i++)
            {
                Console.Write($"\n\tPlease, enter value {i}\n\tYou: ");

                try
                {
                    aUserInput[i - Toolbox.NUMERATION_ISSUE] = Double.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    i--;
                    AnsiConsole.Write(new Markup("\n\t[red]Please, enter a digital number.[/]\n"));
                }
            }

            double dResultData = aUserInput[0];

            foreach (double unit in aUserInput)
            {
                if (unit != dResultData)
                {
                    AnsiConsole.Write(new Markup("\n\t[Blue] I'm afraid, your triangle's not equilateral.\n\n[/]"));
                    return;
                }
            }

            AnsiConsole.Write(new Markup("\n\t[Green] Indeed, the triangle is equilateral.\n\n[/]"));

            Console.WriteLine("\n");
        }

    }

}