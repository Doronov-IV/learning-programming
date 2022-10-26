namespace Paint.NET.Core.Forms
{
    /// <summary>
    /// Main painting form.
    /// <br />
    /// �������� paint-�����.
    /// </summary>
    public partial class MainForm : Form
    {


        #region CONSTRUCTION


        /// <summary>
        /// Default constructor.
        /// <br />
        /// ����������� �� ���������.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Show();

            Graphics? graphics = CreateGraphics();

            graphics.DrawRectangle(new(Color.Black, 3), 50f,50f,100f,100f);

            graphics.Dispose();
        }


        #endregion CONSTRUCTION


    }
}