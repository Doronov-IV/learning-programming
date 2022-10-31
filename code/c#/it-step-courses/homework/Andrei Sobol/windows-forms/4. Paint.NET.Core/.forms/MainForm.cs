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


        #region MODULES





        #region Module: Objects




        /// <summary>
        /// Draw a line in the pictureBox.
        /// <br />
        /// ���������� ����� � pictureBox.
        /// </summary>
        private void OnDrawLine()
        {
            Pen currentPenClone = _currentPen.Clone() as Pen;
            Point? _mouseStartingPositionCopy = new Point(_mouseCurrentStartingPosition.X, _mouseCurrentStartingPosition.Y);
            Point? _mouseEndingPositionCopy = new Point(_mouseCurrentEndingPosition.X, _mouseCurrentEndingPosition.Y);

            Action<Graphics> newAction = (graphics) => { graphics.DrawLine(currentPenClone, _mouseStartingPositionCopy.Value, _mouseEndingPositionCopy.Value); };

            _onPaintPreview = null;

            // if we are painting, i.e. clicked with LMB and aiming the figure; 
            if (_isPainting)
            {
                _onPaintPreview = null;
                _onPaintPreview += newAction;
            }
            // if we have already done that and we need to draw whatever we previewed;
            else
            {
                _onPaint += newAction;
            }

            MainPictureBox.Invalidate();
        }



        #endregion Module: Objects




        #region Module: Image Box 



        /// <summary>
        /// Redraw main pircure box.
        /// <br />
        /// ������������ "MainPictureBox".
        /// </summary>
        private void OnMainPictureBoxPaint(object sender, PaintEventArgs e)
        {
            _onPaint?.Invoke(e.Graphics);
            _onPaintPreview?.Invoke(e.Graphics);
        }




        ////////////////////////////////////////////////////////////////////////////////////////
        ///                             MOUSE EVENT HANDLERS                                 ///
        ////////////////////////////////////////////////////////////////////////////////////////




        /// <summary>
        /// Secure the state of the mouse buttons to depict either preview or the painted figure itself.
        /// <br />
        /// ������������� ��������� ������ ����, ����� ��������� ���������� ������ ������ ��� ���� ������.
        /// </summary>
        private void OnImageBoxMouseDown(object sender, MouseEventArgs e)
        {
            _mouseCurrentStartingPosition = e.Location;

            _isPainting = true;
        }


        /// <inheritdoc cref="OnImageBoxMouseDown(object, MouseEventArgs)"/>
        private void OnImageBoxMouseUp(object sender, MouseEventArgs e)
        {
            _isPainting = false;

            _currentAction.Invoke();
        }


        /// <inheritdoc cref="OnImageBoxMouseDown(object, MouseEventArgs)"/>
        private void OnImageBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (_isPainting)
            {
                _mouseCurrentEndingPosition = e.Location;

                _currentAction.Invoke();
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
                using (Bitmap bitmap = new Bitmap(MainPictureBox.Width, MainPictureBox.Height))
                {
                    MainPictureBox.DrawToBitmap(bitmap, new Rectangle(0,0, bitmap.Width, bitmap.Height));
                    if (File.Exists(_currentFileInfo.FullName)) File.Delete(_currentFileInfo.FullName);
                    bitmap.Save(_currentFileInfo.FullName, bitmap.RawFormat);
                }
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
                using (Bitmap bitmap = new Bitmap(MainPictureBox.Width, MainPictureBox.Height))
                {
                    MainPictureBox.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                    if (File.Exists(MainSaveFileDialog.FileName)) File.Delete(MainSaveFileDialog.FileName);
                    bitmap.Save(MainSaveFileDialog.FileName, bitmap.RawFormat);
                }
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





        #region STATE


        private static Bitmap? _currentBitmap;


        /// <summary>
        /// A reference to the current opened file if any.
        /// <br />
        /// ������ �� ������� �������� ����, ���� �� ����.
        /// </summary>
        private static FileInfo? _currentFileInfo;


        /// <summary>
        /// True if the LMB was down but is not up, otherwise false.
        /// <br />
        /// "True", ���� ��� ���� ������, �� ��� �� �������, ����� "false".
        /// </summary>
        private static bool _isPainting;


        /// <summary>
        /// A reference to the current pen.
        /// <br />
        /// ������ �� ������� ��������.
        /// </summary>
        private static Pen? _currentPen;


        /// <summary>
        /// A reference to the current brush.
        /// <br />
        /// ������ �� ������� �����.
        /// </summary>
        private static Brush? _currentBrush;


        /// <summary>
        /// A reference to the file that we take copy from to not lock the actual file.
        /// <br />
        /// ������ �� ����, ������� �� ��������, ����� �� ����������� �������� ����.
        /// </summary>
        private static string? _copyFileName;


        /// <summary>
        /// A reference to the temp directory that we reserve for the copied files.
        /// <br />
        /// ������ �� ��������� ����������, ������� �� ����������� ��� ������������ ������.
        /// </summary>
        private static DirectoryInfo? _tempDirectory;



        ////////////////////////////////////////////////////////////////////////



        /// <summary>
        /// To be performed when we press the LMB to take a look at the preview of a chosen effect.
        /// <br />
        /// �����������, ����� �� �������� ���, ����� ��������� �� ������ ���������� �������.
        /// </summary>
        private Action<Graphics> _onPaintPreview;


        /// <summary>
        /// To be performed when we end the preview of the effect and actualy want to keep it on the canvas.
        /// <br />
        /// ���������� ����� �� �������� ������ ������� � ������� �������� ��� �� ������.
        /// </summary>
        private Action<Graphics> _onPaint;


        /// <summary>
        /// A reference to the current action chosen.
        /// <br />
        /// ������ �� �������� ��������.
        /// </summary>
        private Action _currentAction;


        /// <summary>
        /// Starting coordinates of the mouse current coursor position.
        /// <br />
        /// ������� ��������� ���������� ������� ������� ����.
        /// </summary>
        private Point _mouseCurrentStartingPosition;


        /// <summary>
        /// Ending coordinates of the mouse current coursor position.
        /// <br />
        /// ������� �������� ���������� ������� ������� ����.
        /// </summary>
        private Point _mouseCurrentEndingPosition;



        #endregion STATE






        #region AUXILIARY



        /// <summary>
        /// Clear temp directory to dispose of copied files.
        /// <br />
        /// �������� ��������� �����, ������ ����� ������.
        /// </summary>
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
            MainPictureBox.Dispose();

            _currentBitmap?.Dispose();
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

            _tempDirectory = new DirectoryInfo("../../../.temp");
            if (!Directory.Exists(_tempDirectory.FullName)) _tempDirectory.Create();

            // CAUTION;
            _currentAction = OnDrawLine;
        }








        #endregion CONSTRUCTION


    }
}