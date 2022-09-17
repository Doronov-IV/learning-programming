using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private FileInfo _CurrentFileInfo;

        /// <summary>
        /// @see 'private FileInfo _CurrentFileInfo';
        /// </summary>
        public FileInfo CurrentFileInfo { get { return _CurrentFileInfo; } set { _CurrentFileInfo = value; } }


        #endregion PROPERTIES




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public SecondaryForm()
        {
            InitializeComponent();
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





        private void видToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
