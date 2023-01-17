using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.ClassExtensions
{
    /// <summary>
    /// Extension methods I needed for some rather non-trivial tasks;
    /// <br />
    /// Методы расширения, которые понадобилиь мне для различных нетривиальных задач;
    /// </summary>
    public static class StringExtension
    {


        /// <summary>
        /// Compare one string to an array of strings;
        /// <br />
        /// Сравнить одну строку с массивом строк;
        /// <br />
        /// <see href = "https://improvingsoftware.com/2011/06/16/quick-tip-comparing-a-net-string-to-multiple-values/" >Source: </see>
        /// </summary>
        /// <param name="data">Source string;<br />Исходная строка;</param>
        /// <param name="compareType">Comparison type;<br />Тип сравнения;</param>
        /// <param name="compareValues">An array of strings to compare;<br />Массив строк к сравнению;</param>
        /// <returns>True if at least one is equal, othervise false;<br />True, если хотя бы одна строка совпадает, иначе false;</returns>
        public static bool CompareMultiple(string sourceString, StringComparison compareType, params string[] compareValues)
        {
            foreach (string s in compareValues)
            {
                if (sourceString.Equals(s, compareType))
                {
                    return true;
                }
            }
            return false;
        }


    }
}
