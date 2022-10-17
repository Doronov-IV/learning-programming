using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spectre.Console;

using Tools.Formatting;

using LocalizationDictionary.Service;
using LocalizationDictionary.Visual;


namespace LocalizationDictionary.TopLevel
{

    /// <summary>
    /// Localization dictionary simulator;
    /// <br />
    /// Симулятор словаря локализации;
    /// </summary>
    public class LocalizationDictionarySimulator
    {


        #region PROPERTIES - Values that form the State of this class



        #region PRIVATE


        /// <summary>
        /// An instance of a dictionary wrapped in decorator;
        /// <br />
        /// Образец словаря, в виде декоратора;
        /// </summary>
        private LocalizationDictionaryDecorator DemoDecorator;


        /// <summary>
        /// A number of a language chosen by user;
        /// <br />
        /// Номер языка, выбранного пользователем;
        /// </summary>
        private int FocusedLanguageNumber { get; set; }


        #endregion PRIVATE



        #endregion PROPERTIES



        #region API - Contract


        /// <summary>
        /// Run the simulation;
        /// <br />
        /// Запустить симуляцию;
        /// </summary>
        public void Run()
        {
            LocalizationDictionary tempExample = LocalizationDictionary.GetInstance();

            string sUserInput = Toolbox.GetUserInputUponMenu("All", "Search by [underline]Id[/]", "Search by [underline]Word[/]",
                "Add new word", "Choose the language", "Sort", "Exit");

            Console.WriteLine("\n\n\n\n\n");
            switch (sUserInput)
            {
                case "All":
                    DemoDecorator.PrintAll();
                    Console.SetCursorPosition(0, 0);
                    Run();
                    break;
                case "Search by [underline]Id[/]":
                    Console.WriteLine("\n\n\n\n\n");
                    DemoDecorator.SeekWithKey(GetIdInput());
                    Console.SetCursorPosition(0, 0);
                    Run();
                    break;
                case "Search by [underline]Word[/]":
                    Console.WriteLine("\n\n\n\n\n");
                    DemoDecorator.SeekWithSubstring(LanguageAdapter.NumberToLanguage(FocusedLanguageNumber), GetSubstringInput("\tPlease, enter the [underline]word[/].\n\n\tYou: "));
                    Console.SetCursorPosition(0, 0);
                    Run();
                    break;
                case "Add new word":
                    AddNewWordMenu();
                    Console.SetCursorPosition(0, 0);
                    Run();
                    break;
                case "Choose the language":
                    Console.SetCursorPosition(0, 0);
                    FocusedLanguageNumber = GetLanguageInput();
                    TryPrint();
                    Console.SetCursorPosition(0, 0);
                    Run();
                    break;
                case "Sort":
                    TryImplementSort();
                    TryPrint();
                    Console.SetCursorPosition(0, 0);
                    Run();
                    break;
                case "Exit":
                    break;
                default:
                    Console.WriteLine("\nDEFAULT\n");
                    break;
            }

        }


        #endregion API



        #region LOGIC - Mostly private encapsulated auxiliary methods


        /// <summary>
        /// Get user 'Id' input;
        /// <br />
        /// Получить 'Id', выбранный пользователем;
        /// </summary>
        /// <returns>The Id of a word;<br />Id слова;</returns>
        private int GetIdInput()
        {
            Console.Clear();

            int UserKeyInput = 0;
            Console.WriteLine("\n");

            try
            {
                AnsiConsole.Write(new Markup("\tPlease, enter the [underline]Id[/].\n\n\tYou: "));
                UserKeyInput = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                AnsiConsole.Write(new Markup("\n\t[red]Please, enter a digital number.[/]\n"));
                Thread.Sleep(2000);
                Console.Clear();
            }

            Console.Clear();

            Console.WriteLine("\n\n\n\n\n");

            return UserKeyInput;
        }


        /// <summary>
        /// Get user language input;
        /// <br />
        /// Получить номер языка, выбранного пользователем;
        /// </summary>
        /// <returns>The language bumber;<br />Номер языка;</returns>
        private int GetLanguageInput()
        {
            Console.WriteLine("\n");

            int nLanguageInput = 1;

            string sUserInput = Toolbox.GetUserInputUponMenu("English", "Russian", "French");

            nLanguageInput = LanguageAdapter.LanguageToNumber(sUserInput);

            Console.Clear();

            Console.WriteLine("\n\n\n\n\n");

            return nLanguageInput;
        }


        /// <summary>
        /// Try sort the list of words;
        /// <br />
        /// Попробовать отсортировать список слов;
        /// </summary>
        private void TryImplementSort()
        {
            try
            {
                DemoDecorator.SortTable(FocusedLanguageNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine("You haven't chosen any table properties yet.");
            }
        }


        /// <summary>
        /// Get user substring input;
        /// <br />
        /// Получить введённую пользователем построку;
        /// </summary>
        /// <returns>A substring;<br />Подстрока;</returns>
        public string GetSubstringInput(string sMessage)
        {
            Console.Clear();

            string UserStringInput = " ";
            Console.WriteLine("\n");

            try
            {
                AnsiConsole.Write(new Markup(sMessage));
                UserStringInput = Console.ReadLine();
            }
            catch (Exception e)
            {
                AnsiConsole.Write(new Markup("\t[red]" + e.Message + "[/]"));
                Thread.Sleep(2000);
                Console.Clear();
            }

            Console.Clear();

            Console.WriteLine("\n\n\n\n\n");

            return UserStringInput;
        }


        /// <summary>
        /// Try print current decorator buffer table;
        /// <br />
        /// Попробовать вывести текущий буфер декоратора;
        /// </summary>
        private void TryPrint()
        {
            try
            {
                DemoDecorator.Print();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("You haven't chosen any table properties yet.");
            }
        }


        /// <summary>
        /// Dispatch of adding new word operation;
        /// <br />
        /// 'Dispatch' для меню создания нового слова;
        /// </summary>
        private void AddNewWordMenu()
        {
            Console.Clear();

            Console.WriteLine("\n");

            int nLanguageInput = 1;

            string sUserInput = Toolbox.GetUserInputUponMenu("New word", "Existing word");

            switch (sUserInput)
            {
                case "New word":
                    RunAddNewWord();
                    break;
                case "Existing word":
                    RunExistingWord();
                    break;
            }

            Console.Clear();
        }


        /// <summary>
        /// Add new word;
        /// <br />
        /// Добавить новое слово;
        /// </summary>
        private void RunAddNewWord()
        {
            string sLanguage = GetSubstringInput("\t\tPlease, enter the name of the language of the word.\n\n\t\tYou: ");

            string sWord = GetSubstringInput("\t\tPlease, enter the word itself.\n\n\t\tYou: ");

            DemoDecorator.DemoDictionaryInstance.AddNewWord(sLanguage, sWord);
        }


        /// <summary>
        /// Изменить значение уже существующего;
        /// <br />
        /// Change the value of already present word;
        /// </summary>
        private void RunExistingWord()
        {
            string sExistingLanguage = GetSubstringInput("\t\tPlease, enter the name of the language of the existing word.\n\n\t\tYou: ");

            string sExistingWord = GetSubstringInput("\t\tPlease, enter the existing word itself.\n\n\t\tYou: ");


            string sNewWordLanguage = GetSubstringInput("\t\tPlease, enter the name of the language of the existing word.\n\n\t\tYou: ");

            string sNewWord = GetSubstringInput("\t\tPlease, enter the new word itself.\n\n\t\tYou: ");


            DemoDecorator.DemoDictionaryInstance.AddExistingWord(sExistingLanguage, sExistingWord, sNewWordLanguage, sNewWord);
        }


        #endregion LOGIC



        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// An instance of the class;
        /// <br />
        /// Экземпляр класса;
        /// </summary>
        private static LocalizationDictionarySimulator instance;


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        private LocalizationDictionarySimulator()
        {
            DemoDecorator = LocalizationDictionaryDecorator.GetInstance();
            FocusedLanguageNumber = 1;
        }


        /// <summary>
        /// Get the instance;
        /// <br />
        /// Получить экземпляра класса;
        /// </summary>
        /// <returns>A pointer to the instance;<br />Указатель на экземпляр;</returns>
        public static LocalizationDictionarySimulator GetInstance()
        {
            if (instance == null)
            {
                instance = new LocalizationDictionarySimulator();
            }
            return instance;
        }


        #endregion CONSTRUCTION


    }
}