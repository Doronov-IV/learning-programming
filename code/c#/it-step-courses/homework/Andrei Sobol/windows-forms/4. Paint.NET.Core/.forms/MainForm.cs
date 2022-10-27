namespace Paint.NET.Core.Forms
{
    /// <summary>
    /// Main painting form.
    /// <br />
    /// �������� paint-�����.
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
        /// ���������� ������� �������� ����� "MainForm".
        /// </summary>
        private void OnMainFormFormClosing(object sender, FormClosingEventArgs e)
        {

        }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// ����������� �� ���������.
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