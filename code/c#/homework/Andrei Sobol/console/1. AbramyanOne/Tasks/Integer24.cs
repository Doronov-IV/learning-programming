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
        /// Integer 24 метод;
        /// 
        ///     ƒни недели пронумерованы следующим образом: 0 Ч воскресенье,
        /// 1 Ч понедельник, 2 Ч вторник, . . . , 6 Ч суббота. ƒано целое число K,
        /// лежащее в диапазоне 1Ц365. ќпределить номер дн€ недели дл€ K-го дн€
        /// года, если известно, что в этом году 1 €нвар€ было понедельником;
        /// 
        /// </summary>
        public void Integer24()
        {
            Console.WriteLine("\n");
            string sTaskSentense = "";
            sTaskSentense += ("\tƒни недели пронумерованы следующим образом: 0 Ч воскресенье,\n");
            sTaskSentense += ("\t1 Ч понедельник, 2 Ч вторник, . . . , 6 Ч суббота. ƒано целое число K,\n");
            sTaskSentense += ("\tлежащее в диапазоне 1Ц365. ќпределить номер дн€ недели дл€ K-го дн€\n");
            sTaskSentense += ("\tгода, если известно, что в этом году 1 €нвар€ было понедельником;\n");

            Console.WriteLine(sTaskSentense);
            Console.WriteLine("\n");

            Console.Write("\tPlease, enter a number (1-365)\n\n\tYou: ");

            int nDayNumber = 0;

            try
            {
                nDayNumber = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                AnsiConsole.Write(new Markup("\n\t[red]Please, enter a digital number.[/]\n"));
                return;
            }

            Console.WriteLine("\n");

            while (nDayNumber > 6)
            {
                nDayNumber %= 6;
            }

            nDayNumber -= Toolbox.NUMERATION_ISSUE;

            Console.Write("\tThe day you entered is: ");

            switch (nDayNumber)
            {
                case 0:
                    Console.WriteLine("Sunday.");
                    break;
                case 1:
                    Console.WriteLine("Monday.");
                    break;
                case 2:
                    Console.WriteLine("Tuesday.");
                    break;
                case 3:
                    Console.WriteLine("Wednesday.");
                    break;
                case 4:
                    Console.WriteLine("Thursday.");
                    break;
                case 5:
                    Console.WriteLine("Friday.");
                    break;
                case 6:
                    Console.WriteLine("Saturday.");
                    break;
                default:
                    break;
            }

            Console.WriteLine("\n");
        }
    }

}