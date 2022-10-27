namespace Paint.NET.Core.Forms
{
    /// <summary>
    /// Main painting form.
    /// <br />
    /// Основная paint-форма.
    /// </summary>
    public partial class MainForm : Form
    {



        #region MODULES




        #region Module: Image Box 


        private void OnMainPictureBoxPaint(object sender, PaintEventArgs e)
        {
            
            _currentGraphics = e.Graphics;
        }



        private void OnImageBoxMouseDown(object sender, MouseEventArgs e)
        {
            DrawObject.Invoke(_currentPen, e.X, e.Y, 50, 50);
            MainPictureBox.Image = _currentBitmap;
        }


        private void OnImageBoxMouseUp(object sender, MouseEventArgs e)
        {
            //
        }



        #endregion Module: Image Box 






        #region Module: Top Text Menu Strip



        /// <summary>
        /// Handle 'Open' button click.
        /// <br />
        /// Обработать клик по кнопке "открыть".
        /// </summary>
        private void OnOpenFileButtonClick(object sender, EventArgs e)
        {
            if (MainOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                _currentFileInfo = new(MainOpenFileDialog.FileName);
                _copyFileName = $"../../../.temp/{_currentFileInfo.Name}";
                if (File.Exists(_copyFileName)) File.Delete(_copyFileName);
                File.Copy(_currentFileInfo.FullName, _copyFileName);
                
                _currentBitmap = new(_copyFileName);
                MainPictureBox.Image = _currentBitmap;
                RefreshGraphics();
            }
        }



        /// <summary>
        /// Handle 'Save' button click.
        /// <br />
        /// Обработать клик по кнопке "сохранить".
        /// </summary>
        private void OnSaveButtonClick(object sender, EventArgs e)
        {
            if (_currentFileInfo != null)
            {
                DisposeGraphics();

                using (Bitmap bitmap = (Bitmap)MainPictureBox.Image.Clone())
                {
                    if (File.Exists(_currentFileInfo.FullName)) File.Delete(_currentFileInfo.FullName);
                    bitmap.Save(_currentFileInfo.FullName, bitmap.RawFormat);
                }

                ReassignGraphics();
            }
            else OnSaveAsFileButtonClick(sender, e);
        }



        /// <summary>
        /// Handle 'Save as...' button click.
        /// <br />
        /// Обработать клик по кнопке "сохранить как".
        /// </summary>
        private void OnSaveAsFileButtonClick(object sender, EventArgs e)
        {
            if (MainSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DisposeGraphics();

                using (Bitmap bitmap = (Bitmap)MainPictureBox.Image.Clone())
                {
                    if (File.Exists(MainSaveFileDialog.FileName)) File.Delete(MainSaveFileDialog.FileName);
                    bitmap.Save(MainSaveFileDialog.FileName, bitmap.RawFormat);
                }

                ReassignGraphics();
            }
        }



        #endregion Module: Top Text Menu Strip




         //********************************************************//
        ////////////////////////////////////////////////////////////




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




        //********************************************************//
       ////////////////////////////////////////////////////////////



        #region Module: Main Controls



        /// <summary>
        /// Handle color button click.
        /// <br />
        /// Обработать клик кнопки выбора цвета.
        /// </summary>
        private void OnColorButtonClick(object sender, EventArgs e)
        {
            if (MainColorDialog.ShowDialog() == DialogResult.OK)
            {
                _currentPen.Color = MainColorDialog.Color;
            }

        }



        #endregion Module: Main Controls







        #endregion MODULES





        #region PROPERTIES


        private static FileInfo? _currentFileInfo;

        private static Graphics? _currentGraphics;

        private static Bitmap? _currentBitmap;

        private static bool _isPainting;

        private static Pen? _currentPen;

        private static Brush? _currentBrush;


        private static string? _copyFileName;

        private static DirectoryInfo? _tempDirectory;



        private static Point? _mouseStartPoint;
        

        private static Point? _mouseEndPoint;


        public delegate void DrawObjectDelegate(Pen pen, float x, float y, float width, float height);


        public event DrawObjectDelegate DrawObject;


        #endregion PROPERTIES






        #region AUXILIARY



        /// <summary>
        /// Dispose and create graphics.
        /// <br />
        /// Удалить и создать объект graphics.
        /// </summary>
        private void RefreshGraphics()
        {
            DisposeGraphics();

            ReassignGraphics();
        }


        /// <summary>
        /// Attach current graphics to the current bitmap.
        /// <br />
        /// Прикрепить currentGraphics к currentBitmap.
        /// </summary>
        private void ReassignGraphics()
        {
            _currentGraphics = Graphics.FromImage(_currentBitmap);

            DrawObject += _currentGraphics.DrawRectangle;
        }


        /// <summary>
        /// Dispose graphics instance and unsubscribe it from the DrawObject.
        /// <br />
        /// Задиспозить currentGraphics и отписать его от DrawObject.
        /// </summary>
        private void DisposeGraphics()
        {
            DrawObject -= _currentGraphics.DrawRectangle;

            _currentGraphics.Dispose();
        }


        private void ClearTempFolder()
        {
            _tempDirectory?.GetFiles().ToList().ForEach(file => File.Delete(file.FullName));
        }



        #endregion AUXILIARY






        #region CONSTRUCTION



        /// <summary>
        /// Handle MainForm closing event.
        /// <br />
        /// Обработать событие закрытия формы "MainForm".
        /// </summary>
        private void OnMainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            _currentGraphics.Dispose();

            MainPictureBox.Dispose();

            _currentBitmap.Dispose();

            ClearTempFolder();
        }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _currentBrush = new SolidBrush(Color.Black);
            _currentPen = new(Color.Black, 1);
            _isPainting = false;

            string imageFilters = @"Image Files (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";

            MainSaveFileDialog.Filter = imageFilters;
            MainOpenFileDialog.InitialDirectory = @"C:\";

            MainOpenFileDialog.Filter = imageFilters;
            MainOpenFileDialog.InitialDirectory = @"C:\";

            MainPictureBox.Image = _currentBitmap;

            _currentBitmap = new (MainPictureBox.Width, MainPictureBox.Height);
            _currentGraphics = Graphics.FromImage(_currentBitmap);
            _currentGraphics.Clear(Color.White);
            DrawObject += _currentGraphics.DrawRectangle;

            _tempDirectory = new DirectoryInfo("../../../.temp");
            if (!Directory.Exists(_tempDirectory.FullName)) _tempDirectory.Create();
        }








        #endregion CONSTRUCTION

        private void MainPictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}