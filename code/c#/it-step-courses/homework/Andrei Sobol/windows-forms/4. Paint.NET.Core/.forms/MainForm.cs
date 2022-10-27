namespace Paint.NET.Core.Forms
{
    /// <summary>
    /// Main painting form.
    /// <br />
    /// Основная paint-форма.
    /// </summary>
    public partial class MainForm : Form
    {





        #region PROPERTIES


        private static Image? _image;


        #endregion PROPERTIES




        #region CONSTRUCTION


        /// <summary>
        /// Handle MainForm closing event.
        /// <br />
        /// Обработать событие закрытия формы "MainForm".
        /// </summary>
        private void OnMainFormFormClosing(object sender, FormClosingEventArgs e)
        {

        }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            //_image = new();

            MainPictureBox.CreateGraphics();


            MainOpenFileDialog.Filter = @"Image Files (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            MainOpenFileDialog.InitialDirectory = @"C:\";
        }







        #endregion CONSTRUCTION


    }
}