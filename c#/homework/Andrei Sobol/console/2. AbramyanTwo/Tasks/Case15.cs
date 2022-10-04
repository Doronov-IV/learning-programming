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
        /// Case 15;
        /// 
        ///     Мастям игральных карт присвоены порядковые номера: 1 — пики, 2 — трефы, 3 — бубны, 4 — червы. Достоинству 
        /// карт старших десятки, присвоены номера: 11 — валет, 12 — дама, 13 — король, 14 — туз.Даны два целых числа: 
        /// N — достоинство(6 ≤ N ≤ 14) и M — масть карты(1 ≤ M ≤ 4). Вывести название соответствующей карты вида 
        /// «шестерка бубен», «дама червей», «туз треф» и т.п.
        /// 
        /// </summary>
        private class Case15 : Task, IExecutable
        {

            public Case15()
            {
                TaskName = "Case 15";
                TaskSentense = "";

                TaskSentense += ("\tМастям игральных карт присвоены порядковые номера: 1 — пики, 2 — трефы, 3 — бубны, 4 — червы.\n");
                TaskSentense += ("\tДостоинству карт старших десятки, присвоены номера: 11 — валет, 12 — дама, 13 — король, 14 — туз.\n");
                TaskSentense += ("\tДаны два целых числа: N — достоинство(6 ≤ N ≤ 14) и M — масть карты(1 ≤ M ≤ 4).\n");
                TaskSentense += ("\tВывести название соответствующей карты вида «шестерка бубен», «дама червей», «туз треф» и т.п.\n");
            }

            public void Execute()
            {

                // <input>

                #region INPUT

                char cSuitInput = default(char);
                int nValueInput = default(int);

                string sSuitResult = default(string);
                string sValueResult = default(string);

                do // Character;
                {
                    PrintTasknameAndSentence();
                    AnsiConsole.Write(new Markup("\tPlease, enter the suit (1 - Spades, 2 - Clubs, 3 - Diamonds, 4 - Hearts):\n\n\tYou: "));
                    cSuitInput = Console.ReadKey().KeyChar;
                    Console.Clear();

                } while (cSuitInput != '1' && cSuitInput != '2' && cSuitInput != '3' && cSuitInput != '4');

                do // Number;
                {
                    try
                    {
                        PrintTasknameAndSentence();
                        AnsiConsole.Write(new Markup("\tPlease, enter the value (6 - 14):\n\n\tYou: "));
                        nValueInput = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException e)
                    {
                        AnsiConsole.Write(new Markup("\n\t[red]Please, enter a proper one.[/]"));
                        Thread.Sleep(2000);
                        Console.Clear();
                    }

                    Console.Clear();

                } while (!(6 <= nValueInput && nValueInput <= 14));

                #endregion INPUT

                // </input>


                // <switch>

                #region SWITCH

                switch (nValueInput)
                {
                    case 6:
                        sValueResult = "Six";
                        break;
                    case 7:
                        sValueResult = "Seven";
                        break;
                    case 8:
                        sValueResult = "Eight";
                        break;
                    case 9:
                        sValueResult = "Nine";
                        break;
                    case 10:
                        sValueResult = "Ten";
                        break;
                    case 11:
                        sValueResult = "Jack";
                        break;
                    case 12:
                        sValueResult = "Queen";
                        break;
                    case 13:
                        sValueResult = "King";
                        break;
                    case 14:
                        sValueResult = "Ace";
                        break;
                }

                switch (cSuitInput)
                {
                    case '1':
                        sSuitResult = "of Pikes.";
                        break;
                    case '2':
                        sSuitResult = "of Clubs.";
                        break;
                    case '3':
                        sSuitResult =  "of Diamonds.";
                        break;
                    case '4':
                        sSuitResult = "of Hearts.";
                        break;
                }

                #endregion SWITCH

                // </switch>


                // <result>

                PrintTasknameAndSentence();
                AnsiConsole.Write(new Markup("\n\n\t\t[green]Result[/]: [underline]" + sValueResult + " " + sSuitResult + "[/]\n\n"));

                // </result>
            }

        }

    }

}