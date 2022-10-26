namespace Paint.NET.Core.Forms
{
    /// <summary>
    /// Main painting form.
    /// <br />
    /// Основная paint-форма.
    /// </summary>
    public partial class MainForm : Form
    {


        #region Module: Top Toolstrip Buttons


        /// <summary>
        /// Handle the instruments button click event.
        /// <br />
        /// Обработать событие клика по кнопке "Instruments".
        /// </summary>
        private void OnShowInstrumentsButtonClick(object sender, EventArgs e)
        {

        }


        #endregion Module: Top Toolstrip Buttons





        #region PROPERTIES


        //


        #endregion PROPERTIES




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Button button = new Button();
            InstrumentsListView.Items.Add(new ListViewItem);

            Show();

            Graphics? graphics = CreateGraphics();

            graphics.DrawRectangle(new(Color.Black, 3), 50f,50f,100f,100f);

            graphics.Dispose();

            
        }




        #endregion CONSTRUCTION
    }
}