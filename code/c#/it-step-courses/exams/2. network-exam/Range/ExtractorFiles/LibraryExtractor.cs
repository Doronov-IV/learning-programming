using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryExtractor
{
    /// <summary>
    /// A mechanism that automates dll files transfer.
    /// <br />
    /// Механизм, который автоматизирует перенос "dll" файлов.
    /// </summary>
    public class LibraryExtractor
    {


        #region PROPERTIES


        /// <inheritdoc cref="Target"/>
        private FileInfo? _target;

        /// <inheritdoc cref="Destinations"/>
        private List<DirectoryInfo>? _destinations;


        /// <summary>
        /// The file of the library to copy.
        /// <br />
        /// Файл копируемой библиотеки.
        /// </summary>
        public FileInfo? Target
        {
            get => _target;
            set => _target = value;
        }

        /// <summary>
        /// The list of destination folders for extraction.
        /// <br />
        /// Список целей для копирования файла библиотеки.
        /// </summary>
        public List<DirectoryInfo>? Destinations
        {
            get => _destinations;
            set => _destinations = value;
        }


        #endregion PROPERTIES





        #region API


        /// <summary>
        /// Copy library files from target directory to the destinations' ones.
        /// <br />
        /// Копировать файл библиотеки из директории "Target" в папки "Destinations".
        /// </summary>
        public void Extract()
        {
            // if target and destinatons are not null;
            if (_target != null && _destinations != null)
            {
                // if target file exists;
                if (File.Exists(Target?.FullName))
                {
                    // copy targer file to the destinatons;
                    foreach (var dir in Destinations)
                    {
                        if (dir.Exists)
                        {
                            File.Copy(Target?.FullName, dir.FullName + "/" + Target?.FullName);
                        }
                    }
                }
            }
        }


        #endregion API





        #region CONSTRUCTION


        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="Target">
        /// Copied library file.
        /// <br />
        /// Файл копируемой библиотеки.
        /// </param>
        /// <param name="Destinations">
        /// List of destinations folders.
        /// <br />
        /// Список папок назначения.
        /// </param>
        public LibraryExtractor(FileInfo Target, List<DirectoryInfo> Destinations)
        {
            this.Target = Target;
            this.Destinations = Destinations;
        }


        #endregion CONSTRUCTION


    }
}
