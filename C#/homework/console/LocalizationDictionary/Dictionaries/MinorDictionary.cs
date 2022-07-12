using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationDictionary.Dictionaries
{
    /// <summary>
    /// A wrapper for system dictionary;
    /// <br />
    /// Обёртка для системного словаря;
    /// </summary>
    public class MinorDictionary
    {


        #region PROPERTIES - Values that form the State of this class


        /// <summary>
        /// Dictionary's language;
        /// <br />
        /// Язык словаря;
        /// </summary>
        public string Language { get; set; }


        /// <summary>
        /// A system dictionary, containing all the words;
        /// <br />
        /// Системный словарь, в котором хранятся все слова;
        /// </summary>
        private Dictionary<int, string> _SystemDictionary = null;


        /// <summary>
        /// @see _SystemDictionary
        /// </summary>
        public Dictionary<int, string> SystemDictionary 
        { 
            get
            {
                return _SystemDictionary;
            }
            set
            {
                _SystemDictionary = value;
            }
        }


        #endregion PROPERTIES



        #region API - Contract [EMPTY]


        //


        #endregion API



        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MinorDictionary()
        {
            Language = "Unknown";
            _SystemDictionary = new Dictionary<int, string>();
        }


        /// <summary>
        /// 'Language' constructor;
        /// <br />
        /// Конструктор с языком;
        /// </summary>
        /// <param name="sLanguage">The name of the language;<br />Имя языка;</param>
        public MinorDictionary(string sLanguage) : this()
        {
            Language = sLanguage;
        }


        #endregion CONSTRUCTION


    }
}
