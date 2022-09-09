using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.FileReader
{
    /// <summary>
    /// An entity, containing basic FileReader logic;
    /// <br />
    /// Сущность, которая содержит базовую логику FileReader'а;
    /// </summary>
    public abstract class AbstractFileReader
    {


        #region PROPERTIES


        /// <summary>
        /// A path to the source file;
        /// <br />
        /// Путь к файлу-источнику;
        /// </summary>
        public string Source
        {
            set;
            get;
        }


        #endregion PROPERTIES



        #region LOGIC


        /// <summary>
        /// Correct relative path to the projects folder;
        /// <br />
        /// Изменить указатель относительного пути на папку с проектами;
        /// </summary>
        protected void ValidatePath()
        {
            if (Source != null && !(Source.Contains(":\\") && Source.Contains(":/")))
            {
                // Change current folder from 'bin/something...' to the project's one;
                string sRes = "..\\..\\..\\";

                // Add it to our source;
                sRes += Source;

                Source = sRes;

                return;
            }
            else throw new NullReferenceException("You either haven't specified filepath, or passed \'null\'.");
            
        }


        /// <summary>
        /// Get file format;
        /// <br />
        /// Определить формат файла;
        /// </summary>
        /// <param name="sPath">Путь к файлу</param>
        /// <returns>Расширение в пути файла</returns>
        protected string GetFileFormat(string sPath)
        {
            string sResult = "Unknown";

            List<string> FilePathList = new List<string>();

            FilePathList.Add(".txt");
            FilePathList.Add(".json");

            foreach (var unit in FilePathList)
            {
                if (sPath.Contains(unit)) sResult = unit;
            }

            return sResult;
        }


        #endregion LOGIC


    }
}
