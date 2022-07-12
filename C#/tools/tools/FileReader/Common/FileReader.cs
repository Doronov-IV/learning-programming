using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




using Spectre.Console;


namespace Tools.FileReader
{
    /// <summary>
    /// One of them tools* for reading and parsing data into projects;
    /// <br />
    /// Один из иструментов для чтения и парсинга данных в проект;
    /// </summary>
    /// <typeparam name="T">Type of the parsed object; <br /> Тип считываемого объкта;</typeparam>
    public partial class FileReader : AbstractFileReader
    { 


        #region API


        /// <summary>
        /// 
        /// <br />
        /// 
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        public string Read(string sPath)
        {
            string sRes = "This string is Empty.";
            Source = sPath;
            ValidatePath();

            TextFileReader TextReader = new TextFileReader();
            TextReader.Source = Source;
            sRes = TextReader.Read();

            return sRes;
        }


        /// <summary>
        /// Read file as a list of strings;
        /// <br />
        /// Прочитать файл как массив строк;
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        public List<string> ReadList(string sPath)
        {
            List<string> sRes = new List<string>();

            Source = sPath;

            ValidatePath();

            if (GetFileFormat(sPath) == ".txt")
            {
                TextFileReader TextReader = new TextFileReader();
                TextReader.Source = Source;
                sRes = TextReader.ReadList();
            }

            return sRes;
        }


        #endregion API



        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по-умолчанию;
        /// </summary>
        public FileReader()
        {

        }


        #endregion CONSTRUCTION


    }
}
