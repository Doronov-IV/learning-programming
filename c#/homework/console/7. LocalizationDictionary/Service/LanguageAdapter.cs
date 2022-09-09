using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationDictionary.Service
{
    /// <summary>
    /// Some parts of program use numeric codes to work with languages;
    /// <br />
    /// Некоторые части программы используют числовые коды, чтобы работать с языками;
    /// </summary>
    public static class LanguageAdapter
    {

        /// <summary>
        /// Parse language into int code;
        /// <br />
        /// Перевести язык в числовой код;
        /// </summary>
        /// <param name="sLanguage">Language name;<br />Имя языка;</param>
        /// <returns>Language code;<br />Код языка;</returns>
        public static int LanguageToNumber(string sLanguage)
        {
            int nRes = 1;

            switch (sLanguage)
            {
                case "English":
                    // nRes = 2; // unnecessary;
                    break;
                case "Russian":
                    nRes = 2;
                    break;
                case "French":
                    nRes = 3;
                    break;

                default:
                    break;
            }

            return nRes;
        }


        /// <summary>
        /// Parse int code into language;
        /// <br />
        /// Перевести числовой код в язык;
        /// </summary>
        /// <param name="nLanguageNumber">Language code;<br />Код языка;</param>
        /// <returns>Language name;<br />Имя языка;</returns>
        public static string NumberToLanguage(int nLanguageNumber)
        {
            string sRes = "English";

            switch (nLanguageNumber)
            {
                case 1:
                    // sLanguage = "English"; // unnecessary;
                    break;
                case 2:
                    sRes = "Russian";
                    break;
                case 3:
                    sRes = "French";
                    break;

                default:
                    break;
            }

            return sRes;
        }

    }
}
