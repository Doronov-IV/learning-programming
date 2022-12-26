using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamlet.Service
{
    public class FileSystemPointer
    {

        #region PROPERTIES - Values that form the State of this class


        /// <summary>
        /// A reference for the current directory;
        /// <br />
        /// Ссылка на текущую папку;
        /// </summary>
        protected DirectoryInfo _CurrentDirectory;


        /// <summary>
        /// A reference for the previous directory;
        /// <br />
        /// Ссылка на предыдущую папку;
        /// </summary>
        protected DirectoryInfo _PreviousDirectory;



        /// <summary>
        /// @see _CurrentDirectory;
        /// </summary>
        public DirectoryInfo CurrentDirectory
        {
            get { return _CurrentDirectory; }
            protected set { _CurrentDirectory = value; }
        }


        /// <summary>
        /// @see _PreviousDirectory;
        /// </summary>
        public DirectoryInfo PreviousDirectory
        {
            get { return _PreviousDirectory; }
            protected set { _PreviousDirectory = value; }
        }


        #endregion PROPERTIES



        #region API - Contract


        /// <summary>
        /// Step into the next directory;
        /// <br/>
        /// Rewrites "previous directory";
        /// <br />
        /// Переместиться в следующую папку;
        /// <br />
        /// Изменяет поле "предыдущая папка";
        /// </summary>
        /// <param name="nextDirectory"></param>
        public void NextDirectory(DirectoryInfo nextDirectory)
        {
            // The 'null-check' is not needed and can ruin the debug process;
            // Проверка на null не требуется, и может услоднить процесс дебага;
            _PreviousDirectory = _CurrentDirectory;
            _CurrentDirectory = nextDirectory;
        }


        #endregion API



        #region LOGIC - EMPTY


        //


        #endregion LOGIC



        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public FileSystemPointer()
        {
            _CurrentDirectory = null;
            _PreviousDirectory = null;
        }


        #endregion CONSTRUCTION

    }
}
