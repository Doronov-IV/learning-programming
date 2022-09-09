using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;


namespace Tools.FileReader
{
    /// <summary>
    /// @see Generic/FileReader.cs
    /// </summary>
    /// <typeparam name="T">Generic type;<br />Обобщённый тип;</typeparam>
    public partial class FileReader<T>
    {
        /// <summary>
        /// A space for parsing objects;
        /// <br />
        /// Пространство для парсинга объектов;
        /// </summary>
        private class JsonParser<T> : AbstractFileReader
        {


            #region LOGIC


            /// <summary>
            /// Read file;
            /// <br />
            /// Прочитать файл;
            /// </summary>
            /// <returns>Object read;<br />Прочитанный объект;</returns>
            public T Read()
            {
                T tRes = default(T);

                if (File.Exists(Source))
                {
                    tRes = JsonConvert.DeserializeObject<T>(File.ReadAllText(Source));
                }

                return tRes;
            }


            #endregion LOGIC



            #region CONSTRUCTION


            /// <summary>
            /// 
            /// <br />
            /// 
            /// </summary>
            /// <param name="sPath"></param>
            public JsonParser()
            {

            }


            #endregion CONSTRUCTION


        }
    }
}
