using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spectre.Console;


/// <summary>
/// Namespace for some semi-relevant stuff;
/// </summary>
namespace Tools.Formatting
{
    /// <summary>
    /// The main toolbox-class;
    /// </summary>
    public static class Toolbox
    {

        #region GLOBAL_CONSTANTS


        // Transition from nominal to ordinal counting;
        // Перевод из порядковой в количественную и обратно;
        public const byte NUMERATION_ISSUE = 1;

        // For adding new element;
        // Для добавления элемента в группу;
        public const byte NEW_ELEMENT = 1;


        #endregion GLOBAL_CONSTANTS



        #region METHODS

        /// <summary>
        /// Asks user whether they want to continue or not;
        /// </summary>
        public static void ShowContinue()
        {
            Table tContinue = new Table();

            tContinue.Border(TableBorder.Double);

            tContinue.AddColumn("[bold]Do you want to continue? (y/n)[/]");

            tContinue.Columns[0].PadLeft(3).PadRight(3);

            tContinue.Centered();

            AnsiConsole.Write(tContinue);
        }

        /// <summary>
        /// Draws starting menu;
        /// </summary>
        public static string GetUserInputUponMenu(params string[] aMenuOptions)
        {

            // using spectre prompt menu for displaying menu and getting user choice;
            string sUserInput = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .PageSize(10)
            .AddChoices(aMenuOptions));

            return sUserInput;
        }



        public static int GetSimpleUserInput(int nMinValue, int nMaxValue)
        { 
            int nResult = 0;

            bool bCondition = false;

            do
            {

                try
                {
                    nResult = int.Parse(Console.ReadLine());
                    bCondition = nMinValue <= nResult && nResult <= nMaxValue;
                }
                catch (Exception e)
                {
                    AnsiConsole.Write(new Markup("\n\n\t\t[red]Please, enter a proper one.[/]"));
                    Task.Delay(3000).Wait();
                    Console.Clear();
                }


            } while (!bCondition);
            return nResult;
        }


        public static void DrawFarewell()
        {
            Console.WriteLine("\n");
            AnsiConsole.Write(
             new FigletText("Have a")
            .Centered()
            .Color(Spectre.Console.Color.Green));
            Console.WriteLine("\n");
            AnsiConsole.Write(
             new FigletText("nice day!")
            .Centered()
            .Color(Spectre.Console.Color.Blue));
            Console.WriteLine("\n");
        }


        #endregion METHODS

    }
}
