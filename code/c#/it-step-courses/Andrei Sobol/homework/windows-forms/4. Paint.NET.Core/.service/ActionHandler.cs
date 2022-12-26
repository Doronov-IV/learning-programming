using System.ComponentModel;

namespace Paint.NET.Core.Service
{
    /// <summary>
    /// An abstraction that manages main form actions.
    /// <br />
    /// Абстракция, которая управляет action'ами "main form"'ы.
    /// </summary>
    public class ActionHandler : INotifyPropertyChanged
    {



        #region STATE




        ///////////////////////////////////////////////////////////////////////////////////////
        ///  ↓                             ↓   FIELDS   ↓                               ↓   ///
        /////////////////////////////////////////////////////////////////////////////////////// 




        /// <inheritdoc cref="OnPaintPreview"
        private Action<Graphics> _onPaintPreview;


        /// <inheritdoc cref="OnPaint"/>
        private Action<Graphics> _onPaint;


        /// <inheritdoc cref="CurrentAction"/>
        private Action _currentAction;


        /// <inheritdoc cref="PerformedActionStack"/>
        private Stack<Action<Graphics>> _performedActionStack;


        /// <inheritdoc cref="CancelledActionStack"/>
        private Stack<Action<Graphics>> _cancelledActionStack;


        /// <inheritdoc cref="PerformedNotEmpty"/>
        private bool _performedNotEmpty;


        /// <inheritdoc cref="CancelledNotEmpty"/>
        private bool _cancelledNotEmpty;




        ///////////////////////////////////////////////////////////////////////////////////////
        ///  ↓                            ↓   PROPERTIES   ↓                            ↓   ///
        /////////////////////////////////////////////////////////////////////////////////////// 




        /// <summary>
        /// To be performed when we press the LMB to take a look at the preview of a chosen effect.
        /// <br />
        /// Выполняется, когда мы нажимаем ЛКМ, чтобы взглянуть на превью выбранного эффекта.
        /// </summary>
        public Action<Graphics> OnPaintPreview
        {
            get { return _onPaintPreview; }
            set
            {
                _onPaintPreview = value;
                OnPropertyChanged(nameof(OnPaintPreview));
            }
        }



        /// <summary>
        /// To be performed when we end the preview of the effect and actualy want to keep it on the canvas.
        /// <br />
        /// Выполнится когда мы заверишм превью эффекта и захотим оставить его на холсте.
        /// </summary>
        public Action<Graphics> OnPaint
        {
            get { return _onPaint; }
            set
            {
                _onPaint = value;
                OnPropertyChanged(nameof(OnPaint));
            }
        }



        /// <summary>
        /// A reference to the current action chosen.
        /// <br />
        /// Ссылка на выбраное действие.
        /// </summary>
        public Action CurrentAction
        {
            get { return _currentAction; }
            set
            {
                _currentAction = value;
                OnPropertyChanged(nameof(CurrentAction));
            }
        }



        /// <summary>
        /// The stack of all performed actions.
        /// <br />
        /// Стек всех совершённых действий.
        /// </summary>
        public Stack<Action<Graphics>> PerformedActionStack
        {
            get { return _performedActionStack; }
            set
            {
                _performedActionStack = value;
                OnPropertyChanged(nameof(PerformedActionStack));
            }
        }



        /// <summary>
        /// The stack of all cancelled actoins.
        /// <br />
        /// Стек всех отменённых действий.
        /// </summary>
        public Stack<Action<Graphics>> CancelledActionStack
        {
            get { return _cancelledActionStack; }
            set
            {
                _cancelledActionStack = value;
                OnPropertyChanged(nameof(CancelledActionStack));
            }
        }



        /// <summary>
        /// True - if 'PerformedActionStack' is NOT empty otherwise false.
        /// <br />
        /// "True" - если "PerformedActionStack" НЕ пуст, иначе "false".
        /// </summary>
        public bool PerformedNotEmpty
        {
            get { return _performedNotEmpty; }
            set
            {
                _performedNotEmpty = value;
                OnPropertyChanged(nameof(PerformedNotEmpty));
            }
        }



        /// <summary>
        /// True - if 'CancelledActionStack' is NOT empty otherwise false.
        /// <br />
        /// "True" - если "CancelledActionStack" НЕ пуст, иначе "false".
        /// </summary>
        public bool CancelledNotEmpty
        {
            get { return _cancelledNotEmpty; }
            set
            {
                _cancelledNotEmpty = value;
                OnPropertyChanged(nameof(CancelledNotEmpty));
            }
        }




        #endregion STATE







        #region API



        /// <summary>
        /// Add preview action for figure.
        /// <br />
        /// Preview action is an action that is visible only during user aiming with LMB to place their figure permanently.
        /// <br />
        /// <br />
        /// Добавить preview-действие для фигуры.
        /// <br />
        /// Preview-действие - это такое действие, которое видно только тогда, когда ползователь примеряет фигуру при помощи ЛКМ, чтобы разместить её.
        /// </summary>
        /// <param name="action">
        /// A new action to be added to the preview action.
        /// <br />
        /// Новое действие, для добавление в preview-действие.
        /// </param>
        public void AddFigurePreviewAction(Action<Graphics> action)
        {
            OnPaintPreview = null;
            OnPaintPreview += action;
        }



        /// <summary>
        /// Add a preview 'micro-action' for coursor painting.
        /// <br />
        /// Action - a whole curved line painted with coursor. Micro-action - a dot that is drawn when user moves coursor slightly with his LMB pressed.
        /// <br />
        /// <br />
        /// Добавить micro-действие для рисования курсором.
        /// <br />
        /// Action - целая линия, которая рисуется курсором. Micro-action - точка, которая рисуется когда пользователь передвигает курсор немного с зажатой ЛКМ.
        /// </summary>
        /// <param name="action">
        /// A new action to be added to the preview action.
        /// <br />
        /// Новое действие, для добавление в preview-действие.
        /// </param>
        public void AddDoodlePreviewMicroAction(Action<Graphics> action)
        {
            OnPaintPreview += action;
        }



        /// <summary>
        /// Add new action to the main one and to the actions stack.
        /// <br />
        /// Добавить очередное действие в основное и в стек действий.
        /// </summary>
        /// <param name="action">
        /// A new action to be added to the main action and acion stack.
        /// <br />
        /// Новое действие, для добавление в основное действие и стек действий.
        /// </param>
        public void AddFinilizedAction(Action<Graphics> action)
        {
            _onPaint += action;

            PerformedActionStack.Push(action);

            OnPaintPreview = null;

            CheckStacksSize();
        }



        /// <summary>
        /// Cancell last action and handle it from 'performed' stack to the 'cancelled' ones.
        /// <br />
        /// Отменить последнее действие, и переложить его из стека "выполненых" в "отменённые".
        /// </summary>
        public void TryCancellLastAction()
        {
            try
            {
                var lastAction = PerformedActionStack.Pop();

                _onPaint -= lastAction;

                CancelledActionStack.Push(lastAction);

                CheckStacksSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// Repeat last cancelled action and handle it from 'cancelled' stack to the 'performed' ones.
        /// <br />
        /// Повторить последнее отменённое действие, и переложить его из стека "отменённых" в "выполненные".
        /// </summary>
        public void TryRepeatLastCancelledAction()
        {
            try
            {
                var lastAction = CancelledActionStack.Pop();

                _onPaint += lastAction;

                PerformedActionStack.Push(lastAction);

                CheckStacksSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// Check stacks for elements amount. Used for buttons visibility.
        /// <br />
        /// Проверить стеки на наличие элементов. Используется для регулирования видимости кнопок. 
        /// </summary>
        /// <returns>
        /// A tuple where: 'PerformedNotEmpty' - true if respective stack is not empty, otherwise false. 'CancelledNotEmpty' - same.
        /// <br />
        /// Кортеж(?), где: "PerformedNotEmpty" - "true" если соответствующий стек не пуст, иначе "false". "CancelledNotEmpty" - так же.
        /// </returns>
        public void CheckStacksSize()
        {
            if (CancelledActionStack.Count > 0) CancelledNotEmpty = true;
            else CancelledNotEmpty = false;

            if (PerformedActionStack.Count > 0) PerformedNotEmpty = true;
            else PerformedNotEmpty = false;
        }




        #endregion API







        #region CONSTRUCTION






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
        /// <param name="propName">
        /// The name of the property;
        /// <br />
        /// Имя свойства;
        /// </param>
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        #endregion Property changed






        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ActionHandler()
        {
            _cancelledActionStack = new();
            _performedActionStack = new();

            _performedNotEmpty = false;
            _cancelledNotEmpty = false;
        }





        #endregion CONSTRUCTION



    }
}
