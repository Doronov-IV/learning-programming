using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Spectre.Console;


using LocalizationDictionary.Dictionaries;
using LocalizationDictionary.Service;


namespace LocalizationDictionary.TopLevel
{
    /// <summary>
    /// Localization Dictionary. A hub for all minor dictionaries;
    /// <br />
    /// Словарь локализации. Платформа между всеми малыми словарями;
    /// </summary>
    public class LocalizationDictionary
    {


        #region PROPERTIES - Values that form the State of this class


        /// <summary>
        /// A collection of all specific dictionaries, using the name of the dictionary as a key;
        /// <br />
        /// Коллекция всех конкретных словарей, использующая их имена в качестве ключей;
        /// </summary>
        private Dictionary<string, MinorDictionary> _Dictionaries = null;


        /// <summary>
        /// @see _Dictionaries
        /// </summary>
        public Dictionary<string, MinorDictionary> Dictionaries 
        { 
            get
            {
                return _Dictionaries;
            } 
            private set
            {
                _Dictionaries = value;
            } 
        }


        /// <summary>
        /// A static Id, unique for each word in the English dictionary, which is considered the default one;
        /// <br />
        /// (Read '#Readme.txt' for more)
        /// <br />
        /// Статический Id, уникальный для каждого слова в словаре английского языка, который считается словарём по умолчанию;
        /// <br />
        /// Прочитайте '#Readme.txt', чтобы узнать подробности;
        /// </summary>
        private static int Id;


        #endregion PROPERTIES



        #region API - Contract


        /// <summary>
        /// Add minor dictionary;
        /// <br />
        /// Добавить малый словарь;
        /// </summary>
        /// /// <param name="sLanguage">The language of the minor dict;<br />Язык малого словаря;</param>
        public void AddMinorDictionary(string sLanguage)
        {
            Dictionaries.Add(sLanguage, new MinorDictionary(sLanguage));
        }


        /// <summary>
        /// Add new word (!) If it already exists in the English dictionary;
        /// <br />
        /// Добавить новое слово (!) Если оно уже существует в словаре английского языка;
        /// </summary>
        /// /// <param name="sLanguage">The language of the dict;<br />Язык словаря;</param>
        /// <param name="nId">English dictionary word Id;<br />Id слова в английском словаре;</param>
        /// <param name="sWord"></param>
        public void AddExistingWord(string sLanguage, int nId, string sWord)
        {
            // Check if this dictionary exists and it contains that specific id;
            if (Dictionaries.ContainsKey(sLanguage) && Dictionaries[sLanguage].SystemDictionary.ContainsKey(nId))
                Dictionaries[sLanguage].SystemDictionary[nId] = sWord;
        }


        /// <summary>
        /// An overload for the 'AddExistingWord(string sLanguage, int nId, string sWord)' in case we know the word but don't know the Id;
        /// <br />
        /// Перегрузка метода 'AddExistingWord(string sLanguage, int nId, string sWord)' на случай, если мы не помним Id, но помним само слово;
        /// </summary>
        /// <param name="sFilledLanguage">The language of the new word;<br />Язык нового языка;</param>
        /// <param name="sExistingWord">The English word;<br />Существующее английское слово;</param>
        /// <param name="sNewWord">The new word;<br />Новое слово;</param>
        public void AddExistingWord(string sFilledLanguage, string sExistingWord, string sNewWordsLanguage, string sNewWord)
        {
            // Check if this dictionary exists;
            if (Dictionaries.ContainsKey(sFilledLanguage))
            {
                // Check if that word already exists in that dict;
                //
                // If that word in English do exist;
                if (Dictionaries[sFilledLanguage].SystemDictionary.ContainsValue(sExistingWord))
                {
                    int actualId = 0;

                    // Get the actual Id;
                    foreach (var unit in Dictionaries[sFilledLanguage].SystemDictionary)
                    {
                        if (sExistingWord == unit.Value) actualId = unit.Key;
                    }

                    // Use previous method to add the word by Id;
                    AddExistingWord(sNewWordsLanguage, actualId, sNewWord);
                }
                // If that word doesn't exist;
                else
                {
                    AddNewWord(sNewWordsLanguage, sNewWord);
                }
            }
            else throw new ArgumentException("Unknown language. Please, add new language with AddMinorDictionary() method.");
        }


        /// <summary>
        /// Add new word;
        /// <br />
        /// Добавить новое слово;
        /// </summary>
        /// <param name="sLanguage">Dictionary's language;<br />Язык словаря;</param>
        /// <param name="sWord">The word itself;<br />Само слово;</param>
        /// <exception cref="ArgumentException">In case there's no such language yet;<br />В случае если такого языка ещё нет;</exception>
        public void AddNewWord(string sLanguage, string sWord)
        {
            // Check if this dictionary exists;
            if (Dictionaries.ContainsKey(sLanguage))
            {
                // Check if that word already exists in that dict;
                if (!Dictionaries[sLanguage].SystemDictionary.ContainsValue(sWord))
                {
                    // Add the new word into Language specified dict;
                    Dictionaries[sLanguage].SystemDictionary.TryAdd(Id, sWord);

                    // Select all others;
                    var otherDictionaries = Dictionaries.Select(el => el.Value).Where(el => el.Language != sLanguage).ToList();

                    // Add null value to them;
                    otherDictionaries.ForEach(el => el.SystemDictionary.Add(Id, "NULL"));

                    // The last move;
                    Id += 1;



                }
                else throw new ArgumentException("You've already added that word.");
            }
            else throw new ArgumentException("Unknown language. Please, add new language with AddMinorDictionary() method.");
        }


        #endregion API



        #region LOGIC - Mostly private encapsulated auxiliary methods [EMPTY]



        #endregion LOGIC 



        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// An instance of the class;
        /// <br />
        /// Экземпляр класса;
        /// </summary>
        private static LocalizationDictionary instance;


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        private LocalizationDictionary()
        {
            Id = 0;
            _Dictionaries = new Dictionary<string, MinorDictionary>();
        }


        /// <summary>
        /// Get the instance;
        /// <br />
        /// Получить экземпляра класса;
        /// </summary>
        /// <returns>A pointer to the instance;<br />Указатель на экземпляр;</returns>
        public static LocalizationDictionary GetInstance()
        {
            if (instance == null)
            {
                instance = new LocalizationDictionary();
            }
            return instance;
        }


        #endregion CONSTRUCTION


    }
}
