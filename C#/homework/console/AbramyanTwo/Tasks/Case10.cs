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
        /// Case 10 метод;
        /// 
        ///  Робот может перемещаться в четырех направлениях(«С» — север, «З» — запад, «Ю» — юг, «В» — восток)
        ///  и принимать три цифровые команды: 0 — продолжать движение, 1 — поворот налево, −1 — поворот направо.
        ///  Дан символ C — исходное направление робота и целое число N — посланная ему команда.Вывести направление 
        ///  робота после выполнения полученной команды.
        /// </summary>
        private class Case10 : Task, IExecutable
        {

            public Case10() 
            {
                TaskSentense = "";
                TaskName = "Case 10";

                TaskSentense += ("\tРобот может перемещаться в четырех направлениях(«С» — север, «З» — запад, «Ю» — юг, «В» — восток)\n");
                TaskSentense += ("\tи принимать три цифровые команды: 0 — продолжать движение, 1 — поворот налево, −1 — поворот направо.\n");
                TaskSentense += ("\tДан символ C — исходное направление робота и целое число N — посланная ему команда.Вывести направление\n");
                TaskSentense += ("\tробота после выполнения полученной команды.\n");
            }

            public void Execute()
            {

                // <input>

                #region INPUT

                char cPrimaryDirectionInput = default(char);
                int nSecondaryDirectionInput = default(int);

                do // Character;
                {
                    PrintTasknameAndSentence();
                    AnsiConsole.Write(new Markup("\tPlease, enter the starting direction (N,S,W,E):\n\n\tYou: "));
                    cPrimaryDirectionInput = Console.ReadKey().KeyChar;
                    Console.Clear();

                } while (cPrimaryDirectionInput != 'N' && cPrimaryDirectionInput != 'S' && cPrimaryDirectionInput != 'W' && cPrimaryDirectionInput != 'E');

                do // Number;
                {
                    try
                    {
                        PrintTasknameAndSentence();
                        AnsiConsole.Write(new Markup("\tPlease, enter action (1,0,-1):\n\n\tYou: "));
                        nSecondaryDirectionInput = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException e)
                    {
                        AnsiConsole.Write(new Markup("\n\t[red]Please, enter a proper one.[/]"));
                    }
                    finally
                    {
                        Thread.Sleep(2000);
                        Console.Clear();
                    }

                } while (nSecondaryDirectionInput != 0 && nSecondaryDirectionInput != 1 && nSecondaryDirectionInput != -1);

                #endregion INPUT

                // </input>


                // <switch>

                #region SWITCH

                switch (cPrimaryDirectionInput)
                {
                    case 'N':

                        switch (nSecondaryDirectionInput)
                        {
                            case 1:
                                cPrimaryDirectionInput = 'W';
                                break;
                            case 0:
                                cPrimaryDirectionInput = 'N';
                                break;
                            case -1:
                                cPrimaryDirectionInput = 'E';
                                break;
                        }
                        break;

                    case 'E':
                        switch (nSecondaryDirectionInput)
                        {
                            case 1:
                                cPrimaryDirectionInput = 'N';
                                break;
                            case 0:
                                cPrimaryDirectionInput = 'E';
                                break;
                            case -1:
                                cPrimaryDirectionInput = 'S';
                                break;
                        }
                        break;
                    case 'S':
                        switch (nSecondaryDirectionInput)
                        {
                            case 1:
                                cPrimaryDirectionInput = 'E';
                                break;
                            case 0:
                                cPrimaryDirectionInput = 'S';
                                break;
                            case -1:
                                cPrimaryDirectionInput = 'W';
                                break;
                        }
                        break;
                    default:
                        switch (nSecondaryDirectionInput)
                        {
                            case 1:
                                cPrimaryDirectionInput = 'S';
                                break;
                            case 0:
                                cPrimaryDirectionInput = 'W';
                                break;
                            case -1:
                                cPrimaryDirectionInput = 'N';
                                break;
                        }
                        break;
                }

                #endregion SWITCH

                // </switch>

                // <ending>

                PrintTasknameAndSentence();
                AnsiConsole.Write(new Markup("\n\n\t" + "[green]" + "Result: [/]" + "[underline]" + cPrimaryDirectionInput + "[/]"));

                // </ending>

            }

        }

    }

}