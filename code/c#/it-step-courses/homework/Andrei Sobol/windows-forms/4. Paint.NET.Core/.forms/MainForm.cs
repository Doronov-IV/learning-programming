using System.Drawing.Drawing2D;

namespace Paint.NET.Core.Forms
{
    /// <summary>
    /// Main painting form.
    /// <br />
    /// �������� paint-�����.
    /// </summary>
    public partial class MainForm : Form
    {
        private Action<Graphics> _onPaint;
        private Action<Graphics> _onPaintPreview;

        private Point _mouseCurrentStartingPosition;
        private Point _mouseCurrentEndingPosition;


        private Point _mouseLastStartingPosition;
        private Point _mouseLastEndingPosition;
        private Pen _lastPen;

        private Pen _pen;

        #region MODULES




        #region Module: Image Box 


        private void OnDrawLine()
        {


            Pen currentPenClone = _currentPen.Clone() as Pen;
            Point? _mouseStartingPositionCopy = new Point(_mouseCurrentStartingPosition.X, _mouseCurrentStartingPosition.Y);
            Point? _mouseEndingPositionCopy = new Point(_mouseCurrentEndingPosition.X, _mouseCurrentEndingPosition.Y);

            Action<Graphics> newAction = (graphics) => { graphics.DrawLine(currentPenClone, _mouseStartingPositionCopy.Value, _mouseEndingPositionCopy.Value); };

            _onPaint += newAction;

            MainPictureBox.Invalidate();
        }

        private void OnDrawLinePreview()
        {


            Pen currentPenClone = _currentPen.Clone() as Pen;
            Point? _mouseStartingPositionCopy = new Point(_mouseCurrentStartingPosition.X, _mouseCurrentStartingPosition.Y);
            Point? _mouseEndingPositionCopy = new Point(_mouseCurrentEndingPosition.X, _mouseCurrentEndingPosition.Y);

            Action<Graphics> newAction = (graphics) => { graphics.DrawLine(currentPenClone, _mouseStartingPositionCopy.Value, _mouseEndingPositionCopy.Value); };

            _onPaintPreview = null;
            _onPaintPreview += newAction;

            MainPictureBox.Invalidate();
        }

        private void OnDrawRectangle()
        {
            _onPaint += (graphics) => { graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black), 1), _mouseCurrentEndingPosition.X, _mouseCurrentEndingPosition.Y, 100, 100); };
        }

        private void OnMainPictureBoxPaint(object sender, PaintEventArgs e)
        {
            //c = e.Graphics;

            _onPaint?.Invoke(e.Graphics);
            _onPaintPreview?.Invoke(e.Graphics);

        }



        private void OnImageBoxMouseDown(object sender, MouseEventArgs e)
        {
            _mouseCurrentStartingPosition = e.Location;

            _isPainting = true;

        }


        private void OnImageBoxMouseUp(object sender, MouseEventArgs e)
        {
            OnDrawLine();

            

            _isPainting = false;
        }


        private void OnImageBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (_isPainting)
            {
                _mouseCurrentEndingPosition = e.Location;


                OnDrawLinePreview();

                MainPictureBox.Refresh();
            }
        }



        #endregion Module: Image Box 






        #region Module: Top Text Menu Strip



        /// <summary>
        /// Handle 'Open' button click.
        /// <br />
        /// ���������� ���� �� ������ "�������".
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
        /// ���������� ���� �� ������ "���������".
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
        /// ���������� ���� �� ������ "��������� ���".
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
        /// ���������� ������� ����� �� ������ "Instruments".
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
        /// ���������� ���� ������ ������ �����.
        /// </summary>
        private void OnColorButtonClick(object sender, EventArgs e)
        {
            if (MainColorDialog.ShowDialog() == DialogResult.OK)
            {
                _currentPen.Color = MainColorDialog.Color;
            }

            MainPictureBox.Invalidate();

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
        /// ������� � ������� ������ graphics.
        /// </summary>
        private void RefreshGraphics()
        {
            DisposeGraphics();

            ReassignGraphics();
        }


        /// <summary>
        /// Attach current graphics to the current bitmap.
        /// <br />
        /// ���������� currentGraphics � currentBitmap.
        /// </summary>
        private void ReassignGraphics()
        {
            _currentGraphics = Graphics.FromImage(_currentBitmap);

            DrawObject += _currentGraphics.DrawRectangle;
        }


        /// <summary>
        /// Dispose graphics instance and unsubscribe it from the DrawObject.
        /// <br />
        /// ����������� currentGraphics � �������� ��� �� DrawObject.
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
        /// ���������� ������� �������� ����� "MainForm".
        /// </summary>
        private void OnMainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            _currentGraphics.Dispose();

            MainPictureBox.Dispose();

            //_currentBitmap.Dispose();

            //ClearTempFolder();
        }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// ����������� �� ���������.
        /// </summary>
        public MainForm()
        {
            ClearTempFolder();

            InitializeComponent();

            _currentBrush = new SolidBrush(Color.Black);
            _currentPen = new(Color.Black, 3);
            _isPainting = false;

            string imageFilters = @"Image Files (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";

            MainSaveFileDialog.Filter = imageFilters;
            MainOpenFileDialog.InitialDirectory = @"C:\";

            MainOpenFileDialog.Filter = imageFilters;
            MainOpenFileDialog.InitialDirectory = @"C:\";

            

            _currentGraphics = MainPictureBox.CreateGraphics();
            _currentGraphics.SmoothingMode = SmoothingMode.HighQuality;
            _currentGraphics.Clear(Color.White);
            //DrawObject += _currentGraphics.DrawRectangle;

            _tempDirectory = new DirectoryInfo("../../../.temp");
            if (!Directory.Exists(_tempDirectory.FullName)) _tempDirectory.Create();

            
        }








        #endregion CONSTRUCTION

        private void MainPictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}