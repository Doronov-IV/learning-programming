using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamlet.Service
{
    /// <summary>
    /// A wrapper for the file explorer lists;
    /// <br />
    /// Обёртка для пунктов списка файл менеджера;
    /// </summary>
    public class StreamletListObject
    {

        #region PROPERTIES


        /// <summary>
        /// Name of file;
        /// <br />
        /// Имя файла;
        /// </summary>
        private string _Name;

        /// <summary>
        /// Extension of file;
        /// <br />
        /// Расширение файла;
        /// </summary>
        private string _Ext;

        /// <summary>
        /// Size of file (in bytes);
        /// <br />
        /// Размер файла (в байтах);
        /// </summary>
        private string _Size;

        /// <summary>
        /// Date of change of the file/directory;
        /// <br />
        /// Дата изменения файла/папки;
        /// </summary>
        private string _Date;



        /// <summary>
        /// @see private string _Name
        /// </summary>
        public string Name { get { return _Name; } set { _Name = value; } }

        /// <summary>
        /// @see private string _Ext
        /// </summary>
        public string Ext { get { return _Ext; } set { _Ext = value; } }

        /// <summary>
        /// @see private string _Size
        /// </summary>
        public string Size { get { return _Size; } set { _Size = value; } }

        /// <summary>
        /// @see private string _Date
        /// </summary>
        public string Date { get { return _Date; } set { _Date = value; } }


        #endregion PROPERTIES


        #region CONSTRUCTION


        /// <summary>
        /// Constructor with file info;
        /// <br />
        /// Конструктор с описанием файла;
        /// </summary>
        /// <param name="fileInfo">File info;<br />Описание файла;</param>
        public StreamletListObject(FileInfo fileInfo)
        {
            _Name = fileInfo.Name;
            _Ext = fileInfo.Extension;
            _Size = fileInfo.Length.ToString();
            _Date = fileInfo.LastWriteTime.ToShortDateString();
        }


        /// <summary>
        /// Constructor with directory info;
        /// <br />
        /// Конструктор с описанием папки;
        /// </summary>
        /// <param name="directoryInfo">Directory info;<br />Описание директории;</param>
        public StreamletListObject(DirectoryInfo directoryInfo)
        {
            _Name = directoryInfo.Name;
            _Ext = directoryInfo.Extension;
            _Size = "";
            _Date = directoryInfo.LastWriteTime.ToShortDateString();
        }




        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public StreamletListObject()
        {
            _Name = _Ext = _Size = _Date = "";
        }


        /// <summary>
        /// Some text string constructor;
        /// <br />
        /// Конструктор с какой-то строкой;
        /// </summary>
        /// <param name="someName">Some string we want to display in the name field;<br />Какая-то страка для поля Name;</param>
        public StreamletListObject(string someName) : this()
        {
            if (someName == "DEFAULT") BuildListBoxHeader();
            else _Name = someName;
        }


        /// <summary>
        /// Build default string for the list box header;
        /// <br />
        /// Построить строку по-умолчанию для заголовка списка;
        /// </summary>
        public void BuildListBoxHeader()
        {
            _Name = "Name";
            _Ext = "Ext";
            _Size = "Size";
            _Date = "Date";
        }


        #endregion CONSTRUCTION


        #region OVERRIDES


        /// <summary>
        /// ToString method override;
        /// <br />
        /// Перегрузка метода ToString;
        /// </summary>
        /// <returns>Name + ext + size + date;<br />Имя + расширение + размер + дата;</returns>
        public override string ToString()
        {
            string returnString = "";

            string shortenedName = "";
            if (_Name.Length > 20)
            {
                shortenedName = _Name.Substring(0, 17);
                shortenedName += "...";
            }
            else shortenedName = _Name;


            string shortenedExt = "";
            if (_Ext.Length > 10)
            {
                shortenedExt = _Ext.Substring(0, 7);
                shortenedExt += "...";
            }
            else shortenedExt = _Ext;

            returnString += TempToolbox.TabWidth(shortenedName, 20);
            returnString += TempToolbox.TabWidth(shortenedExt, 10);
            returnString += TempToolbox.TabWidth(_Size, 20);
            returnString += TempToolbox.TabWidth(_Date, 15);
            

            return returnString;
        }


        #endregion OVERRIDES

    }
}
