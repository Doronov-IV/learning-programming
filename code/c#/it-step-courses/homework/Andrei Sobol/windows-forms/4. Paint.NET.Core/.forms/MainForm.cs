namespace Paint.NET.Core.Forms
{
    /// <summary>
    /// Main painting form.
    /// <br />
    /// Основная paint-форма.
    /// </summary>
    public partial class MainForm : Form
    {


        #region CONSTRUCTION


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
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