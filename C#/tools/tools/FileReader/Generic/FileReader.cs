using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Spectre.Console;

/// <summary>
/// v 1.1.0
/// </summary>


namespace Tools.FileReader
{
    /// <summary>
    /// One of them tools* for reading and parsing data into projects;
    /// <br />
    /// Один из иструментов для чтения и парсинга данных в проект;
    /// </summary>
    /// <typeparam name="T">Type of the parsed object; <br /> Тип считываемого объкта;</typeparam>
    public partial class FileReader<T> : AbstractFileReader
    {


        #region API


        /// <summary>
        /// Read an object;
        /// <br />
        /// Прочитать объект;
        /// </summary>
        /// <param name="sPath">Filepath;<br />Путь файла;</param>
        /// <returns>Object read;<br />Прочитанный объект;</returns>
        public T Read(string sPath)
        {
            Source = sPath;
            ValidatePath();
            T oRes = default(T);

            JsonParser<T> JsonReader = new JsonParser<T>();
            JsonReader.Source = Source;
            oRes = JsonReader.Read();

            return oRes;
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
