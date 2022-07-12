using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


using Spectre.Console;


namespace Tools.FileReader
{
    /// <summary>
    /// @see Common/FileReader.cs
    /// </summary>
    public partial class FileReader
    {

        /// <summary>
        /// A auxiliary class for reading text files;
        /// <br />
        /// Вспомогательный класс для чтения текстовых файлов;
        /// </summary>
        private class TextFileReader : AbstractFileReader
        {


            #region LOGIC


            /// <summary>
            /// Read a file and get contents as a single string;
            /// <br />
            /// Прочитать файл, получив в результате одну строку с содержимым;
            /// </summary>
            /// <returns>A string with '\n' symbols;<br />Строка с символами '\n';</returns>
            public string Read()
            {
                string sRes = null;

                sRes = File.ReadAllText(Source);

                return sRes;
            }


            public List<string> ReadList()
            {
                StreamReader fs = null;

                List<string> sRes = null;

                bool isFileOpen = false;

                try
                {
                    fs = new StreamReader(Source);

                    isFileOpen = true;

                    int nLineCounter = 0;

                    List<string> lCharArray = new List<string>();
                    string sLine = "";

                    while (sLine != null)
                    {
                        sLine = fs.ReadLine();
                        if (sLine != null)
                        {
                            lCharArray.Add(sLine);
                            nLineCounter += 1;
                        }
                    }

                    sRes = lCharArray;
                }

                catch (FileNotFoundException ex)
                {
                    AnsiConsole.Write(new Markup("[red]Current filepath is invalid.[/]"));
                }

                catch (NullReferenceException ex)
                {
                    AnsiConsole.Write(new Markup("[red]Filepath is not specified.[/]"));
                }

                finally
                {
                    if (null != fs && true == isFileOpen) fs.Close();
                }

                return sRes;
            }


            #endregion LOGIC



            #region CONSTRUCTION


            /// <summary>
            /// Default constructor;
            /// <br />
            /// Конструктор по-умолчанию;
            /// </summary>
            public TextFileReader()
            {

            }


            #endregion CONSTRUCTION


        }
    }
}
