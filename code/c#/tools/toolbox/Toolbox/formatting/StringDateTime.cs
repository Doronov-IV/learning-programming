using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Formatting
{
    public static class StringDateTime
    {

        /// <summary>
        /// Removes specific chars from the string (e.g. '15.15.15' => '151515' etc.)
        /// <br />
        /// Искореняет определённые символы из строки (например: "15.15.15" => "151515" и тд.).
        /// </summary>
        public static string RemoveSeparation(string dateOrTimeAsset)
        {
            string separationChar = string.Empty;
            separationChar += dateOrTimeAsset.First(c => Char.IsPunctuation(c));
            return dateOrTimeAsset.Replace(separationChar, "");
        }

        public static string FromThreeToTwoSections(string time)
        {
            string sRes = string.Empty;
            for (int i = 0, iSize = time.Length; i < iSize; i++)
            {
                if (i < 5) sRes += time[i];
                else break;
            }
            return sRes;
        }

    }
}
