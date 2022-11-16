using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Streamlet.Service;

namespace Streamlet.Forms
{
    /// <summary>
    /// Text editor form;
    /// <br />
    /// Форма-редактор текстовых файлов;
    /// </summary>
    public partial class SecondaryForm : Form
    {




        #region PROPERTIES


        /// <summary>
        /// A reference to a current file;
        /// <br />
        /// Ссылка на текущий файл;
        /// </summary>
        private FileInfo? _CurrentFileInfo;

        /// <summary>
        /// @see 'private FileInfo _CurrentFileInfo';
        /// </summary>
        public FileInfo? CurrentFileInfo { get { return _CurrentFileInfo; } set { _CurrentFileInfo = value; } }


        #endregion PROPERTIES




        #region HANDLERS


        /// <summary>
        /// Handle Save button click event.
        /// <br />
        /// Обработать событие нажатия на кнопку "Save".
        /// </summary>
        private void OnSaveFileClick(object sender, EventArgs e)
        {
            File.WriteAllText(_CurrentFileInfo.FullName, MainRichTextBox.Text);
        }


        /// <summary>
        /// Handle Save As button click event.
        /// <br />
        /// Обработать нажатие на кнопку "Save As".
        /// </summary>
        private void OnSaveAsClick(object sender, EventArgs e)
        {
            if (SaveAsFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo createdFile;

                if (!SaveAsFileDialog.FileName.Equals(String.Empty))
                {
                    createdFile = new(SaveAsFileDialog.FileName);
                }
                else
                {
                    createdFile = new(SaveAsFileDialog.InitialDirectory + @"\Unnamed.txt");
                }

                if (!File.Exists(createdFile.FullName))
                {
                    using var stream = File.Create(createdFile.FullName);
                }
                _CurrentFileInfo = createdFile;
                OnSaveFileClick(sender, e);
            }
        }


        /// <summary>
        /// Handle Open button click event.
        /// <br />
        /// Обработать событие клика по кнопке "Open".
        /// </summary>
        private void OnOpenFileClick(object sender, EventArgs e)
        {
            if (MainOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                _CurrentFileInfo = new(MainOpenFileDialog.FileName);

                LoadFile();
            }
        }


        #endregion HANDLERS




        #region CONSTRUCTION


        /// <summary>
        /// Load current file contents to the screen.
        /// <br />
        /// Загрузить содержимое текущего файла на экран.
        /// </summary>
        private void LoadFile()
        {
            MainRichTextBox.Text = File.ReadAllText(_CurrentFileInfo.FullName);

            this.Text = _CurrentFileInfo.Name + "- editing";
            LowerStatusStrip.Items.Add(_CurrentFileInfo.FullName);
        }


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public SecondaryForm()
        {
            InitializeComponent();

            SaveAsFileDialog.Filter = ".txt|*.txt";
            MainOpenFileDialog.Filter = ".txt|*.txt";

            SaveAsFileDialog.FileName = String.Empty;
            MainOpenFileDialog.FileName = String.Empty;
        }


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public SecondaryForm(FileInfo fileInfo) : this()
        {
            _CurrentFileInfo = fileInfo;
            LoadFile();
        }


        /// <summary>
        /// Notebook form laod handler;
        /// <br />
        /// Хендлер загрузки формы текстового редактора;
        /// </summary>
        private void OnSecondaryFormLoad(object sender, EventArgs e)
        {
        }


        #endregion CONSTRUCTION


    }
}
