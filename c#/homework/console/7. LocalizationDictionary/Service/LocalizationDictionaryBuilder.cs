using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using LocalizationDictionary.TopLevel;
using LocalizationDictionary.Dictionaries;


namespace LocalizationDictionary.Service
{
    /// <summary>
    /// A builder for "LocalizationDictionary" data hardcode;
    /// <br />
    /// Билдер для хардкода данных класса "LocalizationDictionary";
    /// </summary>
    public static class LocalizationDictionaryBuilder
    {


        /// <summary>
        /// Build the dictionary;
        /// <br />
        /// Построить Словарь;
        /// </summary>
        /// <param name="GrandDictionary">Built dictionary reference;<br />Ссылка на строимый(?) словарь;</param>
        public static void Build(ref LocalizationDictionary.TopLevel.LocalizationDictionary GrandDictionary) 
        {
            GrandDictionary.AddMinorDictionary("English");
            GrandDictionary.AddMinorDictionary("Russian");
            GrandDictionary.AddMinorDictionary("French");


            BuildEnglishTable(ref GrandDictionary);
            BuildRussianTable(ref GrandDictionary);
            BuildFrenchTable(ref GrandDictionary);
        }


        /// <summary>
        /// Build the English dictionary;
        /// <br />
        /// Построить английский Словарь;
        /// </summary>
        /// <param name="GrandDictionary">Built dictionary reference;<br />Ссылка на строимый(?) словарь;</param>
        private static void BuildEnglishTable(ref LocalizationDictionary.TopLevel.LocalizationDictionary GrandDictionary)
        {
            GrandDictionary.AddNewWord("English", "cable");
            GrandDictionary.AddNewWord("English", "cabin");
            GrandDictionary.AddNewWord("English", "cafe");
            GrandDictionary.AddNewWord("English", "cage");
            GrandDictionary.AddNewWord("English", "calendar");
            GrandDictionary.AddNewWord("English", "calculate");
            GrandDictionary.AddNewWord("English", "call");
            GrandDictionary.AddNewWord("English", "camp");
            GrandDictionary.AddNewWord("English", "can");
            GrandDictionary.AddNewWord("English", "candidate");
            GrandDictionary.AddNewWord("English", "cap");
            GrandDictionary.AddNewWord("English", "car");
            GrandDictionary.AddNewWord("English", "card");
            GrandDictionary.AddNewWord("English", "care");
            GrandDictionary.AddNewWord("English", "castle");
            GrandDictionary.AddNewWord("English", "cashier");
            GrandDictionary.AddNewWord("English", "cat");
            GrandDictionary.AddNewWord("English", "catalog");
            GrandDictionary.AddNewWord("English", "catch");
            GrandDictionary.AddNewWord("English", "chance"); // total of 20;

        }


        /// <summary>
        /// Build the Russian dictionary;
        /// <br />
        /// Построить русский Словарь;
        /// </summary>
        /// <param name="GrandDictionary">Built dictionary reference;<br />Ссылка на строимый(?) словарь;</param>
        private static void BuildRussianTable(ref LocalizationDictionary.TopLevel.LocalizationDictionary GrandDictionary)
        {
            GrandDictionary.AddExistingWord("English", "cable", "Russian", "кабель");
            GrandDictionary.AddExistingWord("English", "cabin", "Russian", "кабина");
            GrandDictionary.AddExistingWord("English", "cafe", "Russian", "кафе");
            GrandDictionary.AddExistingWord("English", "cage", "Russian", "клетка");
            GrandDictionary.AddExistingWord("English", "calendar", "Russian", "календарь");
            GrandDictionary.AddExistingWord("English", "calculate", "Russian", "вычислять");
            GrandDictionary.AddExistingWord("English", "call", "Russian", "звать");
            GrandDictionary.AddExistingWord("English", "camp", "Russian", "лагерь");
            GrandDictionary.AddExistingWord("English", "can", "Russian", "банка");
            GrandDictionary.AddExistingWord("English", "candidate", "Russian", "кандидат");
            GrandDictionary.AddExistingWord("English", "cap", "Russian", "кепка");
            GrandDictionary.AddExistingWord("English", "car", "Russian", "автомобиль");
            GrandDictionary.AddExistingWord("English", "card", "Russian", "карта");
            GrandDictionary.AddExistingWord("English", "care", "Russian", "забота");
            GrandDictionary.AddExistingWord("English", "castle", "Russian", "замок");
            GrandDictionary.AddExistingWord("English", "cashier", "Russian", "кассир");
            GrandDictionary.AddExistingWord("English", "cat", "Russian", "кот");
            GrandDictionary.AddExistingWord("English", "catalog", "Russian", "каталог");
            GrandDictionary.AddExistingWord("English", "catch", "Russian", "улов");
            GrandDictionary.AddExistingWord("English", "chance", "Russian", "шанс"); // total of 20;
        }


        /// <summary>
        /// Build the French dictionary;
        /// <br />
        /// Построить французский Словарь;
        /// </summary>
        /// <param name="GrandDictionary">Built dictionary reference;<br />Ссылка на строимый(?) словарь;</param>
        private static void BuildFrenchTable(ref LocalizationDictionary.TopLevel.LocalizationDictionary GrandDictionary)
        {
            GrandDictionary.AddExistingWord("English", "cable", "French", "le cable");
            GrandDictionary.AddExistingWord("English", "cabin", "French", "la cabine");
            GrandDictionary.AddExistingWord("English", "cafe", "French", "le cafe");
            GrandDictionary.AddExistingWord("English", "cage", "French", "le cage");
            GrandDictionary.AddExistingWord("English", "calendar", "French", "le calendrier");
            GrandDictionary.AddExistingWord("English", "calculate", "French", "calculer");
            GrandDictionary.AddExistingWord("English", "call", "French", "appeler");
            GrandDictionary.AddExistingWord("English", "camp", "French", "le camp");
            GrandDictionary.AddExistingWord("English", "can", "French", "le bocal");
            GrandDictionary.AddExistingWord("English", "candidate", "French", "le candidat");
            GrandDictionary.AddExistingWord("English", "cap", "French", "le chapeau");
            GrandDictionary.AddExistingWord("English", "car", "French", "la voiture");
            GrandDictionary.AddExistingWord("English", "card", "French", "la card");
            GrandDictionary.AddExistingWord("English", "care", "French", "le soin");
            GrandDictionary.AddExistingWord("English", "castle", "French", "le chateau");
            GrandDictionary.AddExistingWord("English", "cashier", "French", "le cassier");
            GrandDictionary.AddExistingWord("English", "cat", "French", "le chat");
            GrandDictionary.AddExistingWord("English", "catalog", "French", "le catalogue");
            GrandDictionary.AddExistingWord("English", "catch", "French", "le butin");
            GrandDictionary.AddExistingWord("English", "chance", "French", "le chance"); // total of 20;
        }


    }
}
