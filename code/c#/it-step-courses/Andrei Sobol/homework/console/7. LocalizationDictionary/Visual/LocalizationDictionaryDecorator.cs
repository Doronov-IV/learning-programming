using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools.Formatting;

using Spectre.Console;

using LocalizationDictionary.TopLevel;
using LocalizationDictionary.Service;


namespace LocalizationDictionary.Visual
{
    public class LocalizationDictionaryDecorator
    {


        #region PROPERTIES - Values that form the State of this class



        #region PUBLIC


        /// <summary>
        /// Visual settings of the decorator;
        /// <br />
        /// Визуальные настройки декоратора;
        /// </summary>
        public ViewProperties Props { set; get; }


        /// <summary>
        /// A code of the selected language;
        /// <br />
        /// Код выбранного языка;
        /// </summary>
        public int SelectedLanguage { set; get; }


        /// <summary>
        /// A list of pieces of data to print;
        /// <br />
        /// Список данных к выводу;
        /// </summary>
        public List<List<string>> CurrentView { set; get; }


        /// <summary>
        /// A reference of the top-level dictionary;
        /// <br />
        /// Ссылка на высокий словарь;
        /// </summary>
        public LocalizationDictionary.TopLevel.LocalizationDictionary DemoDictionaryInstance = LocalizationDictionary.TopLevel.LocalizationDictionary.GetInstance();


        #endregion PUBLIC



        #endregion PROPERTIES



        #region API - Contract


        /// <summary>
        /// Search with a substring;
        /// <br />
        /// Поиск по подстроке;
        /// </summary>
        /// <param name="sLanguage">A name of the language;<br />Имя языка;</param>
        /// <param name="sSubstring">The substring;<br />Подстрока;</param>
        public void SeekWithSubstring(string sLanguage, string sSubstring)
        {
            List<List<string>> ViewForPrint = new List<List<string>>();
            List<string> TempListForView = new List<string>();

            foreach (var word in DemoDictionaryInstance.Dictionaries[sLanguage].SystemDictionary)
            {
                if (word.Value.StartsWith(sSubstring))
                {
                    TempListForView = new List<string>();
                    TempListForView.Add(word.Key.ToString());
                    foreach (var language in DemoDictionaryInstance.Dictionaries)
                    {
                        TempListForView.Add(language.Value.SystemDictionary[word.Key]);
                    }
                    ViewForPrint.Add(TempListForView);
                }
            }

            CurrentView = ViewForPrint;
            Print();
        }


        /// <summary>
        /// Search with a key;
        /// <br />
        /// Поиск по ключу;
        /// </summary>
        /// <param name="nKey">Id слова;<br />Id of a word;</param>
        public void SeekWithKey(int nKey)
        {
            List<List<string>> ViewForPrint = new List<List<string>>();
            List<string> TempListForView = new List<string>();

            foreach (var word in DemoDictionaryInstance.Dictionaries["English"].SystemDictionary)
            {
                if (word.Key == nKey || word.Key.ToString().StartsWith(nKey.ToString()))
                {
                    TempListForView = new List<string>();
                    TempListForView.Add(word.Key.ToString());
                    foreach (var language in DemoDictionaryInstance.Dictionaries)
                    {
                        TempListForView.Add(language.Value.SystemDictionary[word.Key]);
                    }
                    ViewForPrint.Add(TempListForView);
                }
            }


            CurrentView = ViewForPrint;
            Print();
        }


        /// <summary>
        /// Print the full word list into system console;
        /// <br />
        /// Напечатать полный список слов в консоль;
        /// </summary>
        public void PrintAll()
        {
            List<List<string>> ViewForPrint = new List<List<string>>();
            List<string> TempListForView = new List<string>();


            foreach (var word in DemoDictionaryInstance.Dictionaries["English"].SystemDictionary)
            {
                TempListForView = new List<string>();
                TempListForView.Add(word.Key.ToString());
                foreach (var language in DemoDictionaryInstance.Dictionaries)
                {
                    TempListForView.Add(language.Value.SystemDictionary[word.Key]);
                }
                ViewForPrint.Add(TempListForView);
            }


            CurrentView = ViewForPrint;
            Print();

        }


        #endregion API



        #region LOGIC - Mostly private encapsulated auxiliary methods


        /// <summary>
        /// Sort the dictionary visual view;
        /// <br />
        /// Отсортировать таблицу представления;
        /// </summary>
        /// <param name="nLanguageMenuNumber">A code of the language;<br />Код языка;</param>
        public void SortTable(int nLanguageMenuNumber)
        {
            // We don't need it since the 0-th one is the list of Id's;
            // nLanguageMenuNumber += 1;
            //

            switch (Props.Order)
            {
                case OrderBy.Asc:
                    CurrentView.Sort(
                        delegate (List<string> word1, List<string> word2)
                        {
                            return string.Compare(word1[nLanguageMenuNumber], word2[nLanguageMenuNumber]);
                        });
                    CurrentView.Reverse();
                    Props.Order = OrderBy.Desc;
                    break;

                case OrderBy.Desc:
                    CurrentView?.Sort(
                        delegate (List<string> word1, List<string> word2)
                        {
                            return Int32.Parse(word1[0]).CompareTo(Int32.Parse(word2[0]));
                        });
                    Props.Order = OrderBy.Unsorted;
                    break;

                case OrderBy.Unsorted:
                    CurrentView?.Sort(
                        delegate (List<string> word1, List<string> word2)
                        {
                            return string.Compare(word1[nLanguageMenuNumber], word2[nLanguageMenuNumber]);
                        });
                    Props.Order = OrderBy.Asc;
                    break;

                default:
                    break;
            }
        }


        /// <summary>
        /// Print the specific word list into system console;
        /// <br />
        /// Напечатать частичный список слов в консоль;
        /// </summary>
        public void Print()
        {
            if (CurrentView != null)
            {
                var table = new Table();

                // <table header>
                table.AddColumn("Id");
                foreach (var unit in DemoDictionaryInstance.Dictionaries)
                {
                    table.AddColumn(unit.Key);
                }
                // <table header />


                foreach (var row in CurrentView)
                {
                    table.AddRow(row.ToArray());
                }

                AnsiConsole.Write("\n");
                AnsiConsole.Write(table);
                AnsiConsole.Write("\n");
            }
        }


        /// <summary>
        /// Build the dictionary;
        /// <br />
        /// Собрать словарь;
        /// </summary>
        private void BuildDictionary()
        {
            LocalizationDictionaryBuilder.Build(ref DemoDictionaryInstance);
        }


        #endregion LOGIC



        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// An instance of the class;
        /// <br />
        /// Экземпляр класса;
        /// </summary>
        private static LocalizationDictionaryDecorator instance;


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        private LocalizationDictionaryDecorator()
        {
            BuildDictionary();
            Props = ViewProperties.GetInstance();
        }


        /// <summary>
        /// Get the instance;
        /// <br />
        /// Получить экземпляра класса;
        /// </summary>
        /// <returns>A pointer to the instance;<br />Указатель на экземпляр;</returns>
        public static LocalizationDictionaryDecorator GetInstance()
        {
            if (instance == null)
            {
                instance = new LocalizationDictionaryDecorator();
            }
            return instance;
        }


        #endregion CONSTRUCTION


    }
}
