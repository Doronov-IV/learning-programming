using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Paint.NET.Core.Forms
{
    /// <summary>
    /// Main painting form.
    /// <br />
    /// Основная paint-форма.
    /// </summary>
    public partial class MainForm : Form, INotifyPropertyChanged
    {


        #region MODULES






        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                              ↓   ACTIONS   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 





        #region Module: Action handling




        /// <summary>
        /// Add an action to the main '_onPaint' delegate and to the stack of actions.
        /// <br />
        /// Добавить действие в основной делегат "_onPaint" и в стек действий.
        /// </summary>
        private void AddAction(Action<Graphics> action)
        {
            // if we are painting, i.e. clicked with LMB and aiming the figure; 
            if (_isPainting && !_isDoodling)
            {
                _onPaintPreview = null;
                _onPaintPreview += action;
            }
            // if we have already done that and we need to draw whatever we previewed;
            else
            {
                if (_isDoodling) _doodleAction += action;
                _onPaint += action;
                PerformedActionsStack.Push(action);

                //CheckStacksSize();
            }
        }



        /// <summary>
        /// Cancell last action and handle it from 'performed' stack to the 'cancelled' ones.
        /// <br />
        /// Отменить последнее действие, и переложить его из стека "выполненых" в "отменённые".
        /// </summary>
        private void CancellLastAction()
        {
            var lastAction = PerformedActionsStack.Pop();

            _onPaint -= lastAction;

            CancelledActionsStack.Push(lastAction);

            //CheckStacksSize();
        }



        /// <summary>
        /// Repeat last cancelled action and handle it from 'cancelled' stack to the 'performed' ones.
        /// <br />
        /// Повторить последнее отменённое действие, и переложить его из стека "отменённых" в "выполненные".
        /// </summary>
        private void RepeatLastCancelledAction()
        {
            var lastAction = CancelledActionsStack.Pop();

            _onPaint += lastAction;

            PerformedActionsStack.Push(lastAction);

            //CheckStacksSize();
        }



        /// <summary>
        /// Check what stack has zero size.
        /// <br />
        /// .
        /// </summary>
        private void CheckStacksSize()
        {
            if (CancelledActionsStack.Count > 0) RepeatActionButton.Enabled = true;
            else RepeatActionButton.Enabled = false;

            if (PerformedActionsStack.Count > 0) CancellActionButton.Enabled = true;
            else CancellActionButton.Enabled = false;
        }




        #endregion Module: delegate handling






        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                              ↓   DRAWING   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 





        #region Module: Figures




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

            AddAction(newAction);

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

            AddAction(newAction);

            MainPictureBox.Invalidate();
        }



        /// <summary>
        /// Draw filled rectangle.
        /// <br />
        /// Нарисовать закрашенный прямоугольник.
        /// </summary>
        private void OnDrawFilledRectangle()
        {
            Brush currentBrushClone = _currentBrush.Clone() as Brush;

            var rekt = GetNewRectangleFromCoursorPosition();

            Action<Graphics> newAction = (graphics) => { graphics.FillRectangle(currentBrushClone, rekt); };

            _onPaintPreview = null;

            AddAction(newAction);

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

            AddAction(newAction);

            MainPictureBox.Invalidate();
        }



        /// <summary>
        /// Draw filled ellipse.
        /// <br />
        /// Нарисовать закрашенный эллипс.
        /// </summary>
        private void OnDrawFilledEllipse()
        {
            Brush currentBrushClone = _currentBrush.Clone() as Brush;

            var rekt = GetNewRectangleFromCoursorPosition();

            Action<Graphics> newAction = (graphics) => { graphics.FillEllipse(currentBrushClone, rekt); };

            _onPaintPreview = null;

            AddAction(newAction);

            MainPictureBox.Invalidate();
        }






        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   AUXILIARY   ↓                            ↓    ///
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



        #endregion Module: Figures






        #region Module: Stylo



        /// <summary>
        /// Draw something with 'pencil'. Width is set to 1.
        /// <br />
        /// Нарисовать что-то "карандашом". Ширина выставлена на 1.
        /// </summary>
        private void OnDrawWithPencil()
        {
            _currentPen = new(_currentColor.Value, 1);
            Pen currentPenClone = _currentPen.Clone() as Pen;

            DrawWithSomething(currentPenClone);
        }



        /// <summary>
        /// Draw something with "pen". Width is set to 2.
        /// <br />
        /// Нарисовать что-то "ручкой". Ширина выставлена на 2.
        /// </summary>
        private void OnDrawWithPen()
        {
            _currentPen = new(_currentColor.Value, 2);
            Pen currentPenClone = _currentPen.Clone() as Pen;

            DrawWithSomething(currentPenClone);
        }



        /// <summary>
        /// Draw something with "brush". Width is set to 5.
        /// <br />
        /// Нарисовать что-то "кистью". Ширина выставлена на 5.
        /// </summary>
        private void OnDrawWithBrush()
        {
            _currentPen = new(_currentColor.Value, 5);
            Pen currentPenClone = _currentPen.Clone() as Pen;

            DrawWithSomething(currentPenClone);
        }



        /// <summary>
        /// Draw with white 20px-width brush.
        /// <br />
        /// Нарисовать при помощи белой кисти, толщиной в 20 п.
        /// </summary>
        private void OnDrawWithEraser()
        {
            Pen penCopy = new(Color.White, 20);

            DrawWithSomething(penCopy);
        }

        

        /// <summary>
        /// Draw a doodle-picture with current controler.
        /// <br />
        /// Нарисовать свой простой рисунок при помощи текущего контроллера.
        /// </summary>
        /// <param name="penCopy">
        /// A copy of the current pen instance.
        /// <br />
        /// Копия экземпляра "current pen".
        /// </param>
        private void DrawWithSomething(Pen penCopy)
        {
            _isDoodling = true;

            Point? _mouseStartingPositionCopy = new Point(_mousePreviousPosition.X, _mousePreviousPosition.Y);
            Point? _mouseEndingPositionCopy = new Point(_mouseCurrentEndingPosition.X, _mouseCurrentEndingPosition.Y);

            Action<Graphics> newAction = (graphics) => { graphics.DrawLine(penCopy, _mouseStartingPositionCopy.Value, _mouseEndingPositionCopy.Value); };

            AddAction(newAction);

            MainPictureBox.Invalidate();
        }



        #endregion Module: Stylo






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




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                       ↓   MOUSE EVENT HANDLERS   ↓                       ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 




        /// <summary>
        /// Secure the state of the mouse buttons to depict either preview or the painted picture itself.
        /// <br />
        /// Зафиксировать состояние кнопок мыши, чтобы правильно отобразить превью фигуры или сам нарисованный объект.
        /// </summary>
        private void OnImageBoxMouseDown(object sender, MouseEventArgs e)
        {
            _mousePreviousPosition = _mouseCurrentStartingPosition = e.Location;

            _isPainting = true;
        }


        /// <inheritdoc cref="OnImageBoxMouseDown(object, MouseEventArgs)"/>
        private void OnImageBoxMouseUp(object sender, MouseEventArgs e)
        {
            _isPainting = false;

            if (_isDoodling) _isDoodling = false;

            _currentAction.Invoke();
        }


        /// <inheritdoc cref="OnImageBoxMouseDown(object, MouseEventArgs)"/>
        private void OnImageBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (_isPainting)
            {
                _mousePreviousPosition = _mouseCurrentEndingPosition;

                _mouseCurrentEndingPosition = e.Location;

                _currentAction.Invoke();
            }
            else
            {
                _mousePreviousPosition = _mouseCurrentEndingPosition = e.Location;
            }
        }



        #endregion Module: Image Box 






        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                            ↓   UI CONTROLS   ↓                           ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 





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
                _currentColor = MainColorDialog.Color;
                _currentPen.Color = _currentColor.Value;
                _currentBrush = new SolidBrush(_currentColor.Value);

            }

            MainPictureBox.Invalidate();
        }



        /// <summary>
        /// Handle selected figure changed.
        /// <br />
        /// Обработать изменение выбора фигуры.
        /// </summary>
        private void OnFiguresListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            if (FigureListView.SelectedItems.Count > 0)
            {
                StyloListView.SelectedItems.Clear();

                switch (FigureListView.SelectedItems[0].ToolTipText)
                {
                    case "line":
                        _currentAction = OnDrawLine;
                        break;
                    case "rectangle":
                        _currentAction = OnDrawRectangle;
                        break;
                    case "filled rectangle":
                        _currentAction = OnDrawFilledRectangle;
                        break;
                    case "ellipse":
                        _currentAction = OnDrawEllipse;
                        break;
                    case "filled ellipse":
                        _currentAction = OnDrawFilledEllipse;
                        break;
                    default:
                        break;
                }
            }
        }



        /// <summary>
        /// Handle selected stylo changed.
        /// <br />
        /// Обработать изменение выбора пера.
        /// </summary>
        private void OnStyloListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            if (StyloListView.SelectedItems.Count > 0)
            {
                FigureListView.SelectedItems.Clear();

                switch (StyloListView.SelectedItems[0].ToolTipText)
                {
                    case "pencil":
                        _currentAction = OnDrawWithPencil;
                        break;
                    case "pen":
                        _currentAction = OnDrawWithPen;
                        break;
                    case "brush":
                        _currentAction = OnDrawWithBrush;
                        break;
                    case "eraser":
                        _currentAction = OnDrawWithEraser;
                        break;
                    default:
                        break;
                }
            }
        }



        /// <summary>
        /// Handle cancell last action button click event.
        /// <br />
        /// Обработать событие клика по кнопке отмены последнего действия.
        /// </summary>
        private void OnCancellActionButtonClick(object sender, EventArgs e)
        { 
            if (_performedActionsStack.Count > 0)
            {
                CancellLastAction();

                if (_performedActionsStack.Count == 0) CancellActionButton.Enabled = false;
            }

            MainPictureBox.Refresh();
        }


        /// <summary>
        /// Handle repeat cancelled action button click event.
        /// <br />
        /// Обработать событие клика по кнопке повтора отменённого действия.
        /// </summary>
        private void OnRepeatActionButtonClick(object sender, EventArgs e)
        {
            if (_cancelledActionsStack.Count > 0)
            {
                RepeatLastCancelledAction();

                if (_cancelledActionsStack.Count == 0) RepeatActionButton.Enabled = false;
            }

            MainPictureBox.Refresh();
        }



        #endregion Module: Main Controls







        #endregion MODULES






        #region STATE




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                            ↓   PAINTING   ↓                              ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 




        /// <summary>
        /// A reference to the current bitmap instance.
        /// <br />
        /// Ссылка на экземпляр текущего Bitmap.
        /// </summary>
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


        private bool _isDoodling;


        /// <summary>
        /// A reference to the current pen.
        /// <br />
        /// Ссылка на текущий карандаш.
        /// </summary>
        private static Pen? _currentPen;


        /// <summary>
        /// An eraser pen, writing in white color.
        /// <br />
        /// Стирательная резинка, окрашивающая в белый цвет.
        /// </summary>
        private static Pen? _eraser;


        /// <summary>
        /// A reference to the current brush.
        /// <br />
        /// Ссылка на текущую кисть.
        /// </summary>
        private static Brush? _currentBrush;


        /// <summary>
        /// Current color.
        /// <br />
        /// Текущий цвет.
        /// </summary>
        private static Color? _currentColor;


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


        /// <summary>
        /// The previous position of the mouse coursor.
        /// <br />
        /// Предыдущая позиция курсора мыши.
        /// </summary>
        private Point _mousePreviousPosition;




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                              ↓   ACTIONS   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 




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


        /// <inheritdoc cref="PerformedActionsStack"/>
        private Stack<Action<Graphics>> _performedActionsStack;


        /// <inheritdoc cref="CancelledActionsStack"/>
        private Stack<Action<Graphics>> _cancelledActionsStack;


        /// <summary>
        /// The stack of all performed actions.
        /// <br />
        /// Стек всех совершённых действий.
        /// </summary>
        public Stack<Action<Graphics>> PerformedActionsStack
        {
            get { return _performedActionsStack; }
            set
            {
                _performedActionsStack = value;
                CheckStacksSize();
            }
        }


        /// <summary>
        /// The stack of all cancelled actoins.
        /// <br />
        /// Стек всех отменённых действий.
        /// </summary>
        public Stack<Action<Graphics>> CancelledActionsStack
        {
            get { return _cancelledActionsStack; }
            set
            {
                _cancelledActionsStack = value;
                CheckStacksSize();
            }
        }


        /// <summary>
        /// An action that has all those actions that pile when user paints with stylos.
        /// <br />
        /// Action, который хранит в себе все действия, которые накапливаются, когда позователь пишет пером.
        /// </summary>
        public Action<Graphics> _doodleAction;



        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                              ↓   OTHER   ↓                               ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 




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
            imageList.ImageSize = new Size(20,20);
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/line20.png"));
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/rectangle20.png"));
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/filled-square.png"));
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/circle.png"));
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/filled-circle.png"));

            ListViewItem lineItem = new();
            lineItem.ToolTipText = "line";
            lineItem.ImageIndex = 0;


            ListViewItem squareItem = new();
            squareItem.ToolTipText = "rectangle";
            squareItem.ImageIndex = 1;


            ListViewItem filledSquareItem = new();
            filledSquareItem.ToolTipText = "filled rectangle";
            filledSquareItem.ImageIndex = 2;


            ListViewItem ellipseItem = new();
            ellipseItem.ToolTipText = "ellipse";
            ellipseItem.ImageIndex = 3;


            ListViewItem filledEllipseItem = new();
            filledEllipseItem.ToolTipText = "filled ellipse";
            filledEllipseItem.ImageIndex = 4;


            FigureListView.LargeImageList = imageList;
            
            //ColumnHeaderAutoResizeStyle.ColumnContent;
            FigureListView.Items.Clear();
            FigureListView.Items.Add(lineItem);
            FigureListView.Items.Add(squareItem);
            FigureListView.Items.Add(filledSquareItem);
            FigureListView.Items.Add(ellipseItem);
            FigureListView.Items.Add(filledEllipseItem); 
        }




        /// <summary>
        /// Fill the StyloListView with data.
        /// <br />
        /// Заполнить список рисовалок.
        /// </summary>
        private void FillStyloListView()
        {
            ImageList imageList = new();
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/pencil.png"));
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/pen.png"));
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/brush.png"));
            imageList.Images.Add(Image.FromFile("../../../.resources/icons/eraser.png"));

            ListViewItem pencilItem = new();
            pencilItem.ToolTipText = "pencil";
            pencilItem.ImageIndex = 0;


            ListViewItem penItem = new();
            penItem.ToolTipText = "pen";
            penItem.ImageIndex = 1;


            ListViewItem brushItem = new();
            brushItem.ToolTipText = "brush";
            brushItem.ImageIndex = 2;


            ListViewItem eraserItem = new();
            eraserItem.ToolTipText = "eraser";
            eraserItem.ImageIndex = 3;


            StyloListView.LargeImageList = imageList;
            StyloListView.Items.Add(pencilItem);
            StyloListView.Items.Add(penItem);
            StyloListView.Items.Add(brushItem);
            StyloListView.Items.Add(eraserItem);
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
        /// Fill the list-view's with data.
        /// <br />
        /// Заполнить все list-view данными.
        /// </summary>
        private void InitializeLists()
        {
            FillFiguresListView();

            FillStyloListView();
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

            InitializeLists();

            CancellActionButton.Enabled = false;
            RepeatActionButton.Enabled = false;

            _performedActionsStack = new();
            _cancelledActionsStack = new();

            _currentColor = Color.Black;

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

            // DEFAULT ACTION;
            _currentAction = OnDrawWithPencil;
        }




        #region Property changed


        /// <summary>
        /// Propery changed event handler;
        /// <br />
        /// Делегат-обработчик события 'property changed';
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// Handler-method of the 'property changed' delegate;
        /// <br />
        /// Метод-обработчик делегата 'property changed';
        /// </summary>
        /// <param name="propName">The name of the property;<br />Имя свойства;</param>
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        #endregion Property changed




        #endregion CONSTRUCTION


    }
}