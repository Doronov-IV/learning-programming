using System.Drawing.Drawing2D;

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





        #region Module: Objects




        /// <summary>
        /// Draw a line in the pictureBox.
        /// <br />
        /// Нарисовать линию в pictureBox.
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



        /// <summary>
        /// Draw rectangle.
        /// <br />
        /// Нарисовать прямоугольник.
        /// </summary>
        private void OnDrawRectangle()
        {
            Pen currentPenClone = _currentPen.Clone() as Pen;

            var rekt = GetNewRectangleFromCoursorPosition();

            Action<Graphics> newAction = (graphics) => { graphics.DrawRectangle(currentPenClone, rekt); };

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



        /// <summary>
        /// Draw ellipse.
        /// <br />
        /// Нарисовать эллипс.
        /// </summary>
        private void OnDrawEllipse()
        {
            Pen currentPenClone = _currentPen.Clone() as Pen;

            var rekt = GetNewRectangleFromCoursorPosition();

            Action<Graphics> newAction = (graphics) => { graphics.DrawEllipse(currentPenClone, rekt); };

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





        ///////////////////////////////////////////////////////////////////////////////////////
        ///                                  AUXILIARY                                      ///
        ///////////////////////////////////////////////////////////////////////////////////////




        /// <summary>
        /// Get a new rectangle from current mouse coursor position.
        /// <br />
        /// Получить новый прямоугольник из текущей позиции курсора мыши.
        /// </summary>
        private Rectangle GetNewRectangleFromCoursorPosition()
        {
            Point? _mouseStartingPositionCopy = new Point(_mouseCurrentStartingPosition.X, _mouseCurrentStartingPosition.Y);
            Point? _mouseEndingPositionCopy = new Point(_mouseCurrentEndingPosition.X, _mouseCurrentEndingPosition.Y);


            int l, r, t, b;                     /*
             
                      🠙
                      | T
                      🠛
             .__________________.
          L  |                  |    R
        <--->|                  |<---> 
             |                  |
             .__________________.
                      🠙
                    B | 
                      🠛

                                                */



            if (_mouseEndingPositionCopy.Value.X > _mouseStartingPositionCopy.Value.X)
            {
                l = _mouseStartingPositionCopy.Value.X;
                r = _mouseEndingPositionCopy.Value.X;
            }
            else
            {
                r = _mouseStartingPositionCopy.Value.X;
                l = _mouseEndingPositionCopy.Value.X;
            }

            if (_mouseEndingPositionCopy.Value.Y > _mouseStartingPositionCopy.Value.Y)
            {
                t = _mouseStartingPositionCopy.Value.Y;
                b = _mouseEndingPositionCopy.Value.Y;
            }
            else
            {
                b = _mouseStartingPositionCopy.Value.Y;
                t = _mouseEndingPositionCopy.Value.Y;
            }

            return Rectangle.FromLTRB(l, t, r, b);
        }



        #endregion Module: Objects







        #region Module: Image Box 



        /// <summary>
        /// Redraw main pircure box.
        /// <br />
        /// Перерисовать "MainPictureBox".
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
        /// Зафиксировать состояние кнопок мыши, чтобы правильно отобразить превью фигуры или саму фигуру.
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
        /// Обработать клик по кнопке "сохранить как".
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

            MainPictureBox.Invalidate();
        }


        /// <summary>
        /// Handle selected figure changed.
        /// <br />
        /// Обработать изменение выбранной фигуры.
        /// </summary>
        private void OnFiguresListViewClick(object sender, EventArgs e)
        {
            if (FigureListView.SelectedItems.Count > 0)
            {
                switch (FigureListView.SelectedItems[0].ToolTipText)
                {
                    case "line":
                        _currentAction = OnDrawLine;
                        break;
                    case "rectangle":
                        _currentAction = OnDrawRectangle;
                        break;
                    case "ellipse":
                        _currentAction = OnDrawEllipse;
                        break;
                    default:
                        break;
                }
            }
        }


        #endregion Module: Main Controls







        #endregion MODULES





        #region STATE


        private static Bitmap? _currentBitmap;


        /// <summary>
        /// A reference to the current opened file if any.
        /// <br />
        /// Ссылка на текущий открытый файл, если он есть.
        /// </summary>
        private static FileInfo? _currentFileInfo;


        /// <summary>
        /// True if the LMB was down but is not up, otherwise false.
        /// <br />
        /// "True", если ЛКМ была нажата, но ещё не поднята, иначе "false".
        /// </summary>
        private static bool _isPainting;


        /// <summary>
        /// A reference to the current pen.
        /// <br />
        /// Ссылка на текущий карандаш.
        /// </summary>
        private static Pen? _currentPen;


        /// <summary>
        /// A reference to the current brush.
        /// <br />
        /// Ссылка на текущую кисть.
        /// </summary>
        private static Brush? _currentBrush;


        /// <summary>
        /// A reference to the file that we take copy from to not lock the actual file.
        /// <br />
        /// Ссылка на файл, который мы копируем, чтобы не блокировать основной файл.
        /// </summary>
        private static string? _copyFileName;


        /// <summary>
        /// A reference to the temp directory that we reserve for the copied files.
        /// <br />
        /// Ссылка на временную директорию, которую мы резервируем для копированных файлов.
        /// </summary>
        private static DirectoryInfo? _tempDirectory;



        ////////////////////////////////////////////////////////////////////////



        /// <summary>
        /// To be performed when we press the LMB to take a look at the preview of a chosen effect.
        /// <br />
        /// Выполняется, когда мы нажимаем ЛКМ, чтобы взглянуть на превью выбранного эффекта.
        /// </summary>
        private Action<Graphics> _onPaintPreview;


        /// <summary>
        /// To be performed when we end the preview of the effect and actualy want to keep it on the canvas.
        /// <br />
        /// Выполнится когда мы заверишм превью эффекта и захотим оставить его на холсте.
        /// </summary>
        private Action<Graphics> _onPaint;


        /// <summary>
        /// A reference to the current action chosen.
        /// <br />
        /// Ссылка на выбраное действие.
        /// </summary>
        private Action _currentAction;


        /// <summary>
        /// Starting coordinates of the mouse current coursor position.
        /// <br />
        /// Текущие начальные координаты позиции курсора мыши.
        /// </summary>
        private Point _mouseCurrentStartingPosition;


        /// <summary>
        /// Ending coordinates of the mouse current coursor position.
        /// <br />
        /// Текущие конечные координаты позиции курсора мыши.
        /// </summary>
        private Point _mouseCurrentEndingPosition;



        #endregion STATE






        #region AUXILIARY



        /// <summary>
        /// Clear temp directory to dispose of copied files.
        /// <br />
        /// Очистить временную папку, удаляя копии файлов.
        /// </summary>
        private void ClearTempFolder()
        {
            _tempDirectory?.GetFiles().ToList().ForEach(file => File.Delete(file.FullName));
        }




        /// <summary>
        /// Fill the listview of the figures.
        /// <br />
        /// Заполнить список фигур.
        /// </summary>
        private void FillFiguresListView()
        {
            ImageList imageList = new();
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/line20.png"));
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/rectangle20.png"));
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/circle.png"));

            ListViewItem lineItem = new();
            lineItem.ToolTipText = "line";
            lineItem.ImageIndex = 0;


            ListViewItem squareItem = new();
            squareItem.ToolTipText = "rectangle";
            squareItem.ImageIndex = 1;


            ListViewItem ellipseItem = new();
            ellipseItem.ToolTipText = "ellipse";
            ellipseItem.ImageIndex = 2;

            FigureListView.SmallImageList = imageList;
            FigureListView.Items.Add(lineItem);
            FigureListView.Items.Add(squareItem);
            FigureListView.Items.Add(ellipseItem);
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
            MainPictureBox.Dispose();

            _currentBitmap?.Dispose();
        }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MainForm()
        {
            ClearTempFolder();

            InitializeComponent();
            
            FillFiguresListView();

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