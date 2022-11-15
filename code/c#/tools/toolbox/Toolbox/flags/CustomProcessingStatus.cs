using System.ComponentModel;

namespace Toolbox.Flags
{
    /// <summary>
    /// Represents a manual set of flags for denoting application gloabal processing status.
    /// <br />
    /// Представляет собой кастомный набор флагов для определения статуса работы приложения.
    /// </summary>
    public class CustomProcessingStatus : INotifyPropertyChanged
    {


        #region STATE



        /// <inheritdoc cref="IsRunning"/>
        private bool _isRunning;


        /// <inheritdoc cref="IsNotRunning"/>
		private bool _isNotRunning;


        /// <inheritdoc cref="HasStarted"/>
        private bool _hasStarted;


        /// <inheritdoc cref="HasNotStarted"/>
        private bool _hasNotStarted;


        /// <inheritdoc cref="HasFinished"/>
        private bool _hasFinished;


        /// <inheritdoc cref="HasNotFinished"/>
        private bool _hasNotFinished;



        /// <summary>
        /// True - if the process is running at the moment, otherwise false.
        /// <br />
        /// "True", если процесс работает в данный момент, иначе "false".
        /// </summary>
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }


        /// <summary>
        /// True - if the process is NOT running at the moment (i.e. 'paused'), otherwise false.
        /// <br />
        /// "True", если процесс НЕ работает в данный момент (т.е. "пауза"), иначе "false".
        /// </summary>
        public bool IsNotRunning
        {
            get { return _isNotRunning; }
            set
            {
                _isNotRunning = value;
                OnPropertyChanged(nameof(IsNotRunning));
            }
        }


        /// <summary>
        /// True - if process has already started, otherwise false.
        /// <br />
        /// "True" - если процесс уже запустился, иначе "false".
        /// </summary>
        public bool HasStarted
        {
            get { return _hasStarted; }
            set
            {
                _hasStarted = value;
                OnPropertyChanged(nameof(HasStarted));
            }
        }



        /// <summary>
        /// True - if process has NOT started yet, otherwise false.
        /// <br />
        /// "True" - если процесс ещё НЕ запустился, иначе "false".
        /// </summary>
        public bool HasNotStarted
        {
            get { return _hasNotStarted; }
            set
            {
                _hasNotStarted = value;
                OnPropertyChanged(nameof(HasNotStarted));
            }
        }


        /// <summary>
        /// True - if process has already finished, otherwise false.
        /// <br />
        /// "True" - если процесс уже завершился, иначе "false".
        /// </summary>
        public bool HasFinished
        {
            get { return _hasFinished; }
            set
            {
                _hasFinished = value;
                OnPropertyChanged(nameof(HasFinished));
            }
        }


        /// <summary>
        /// True - if process has NOT finished yet, otherwise false.
        /// <br />
        /// "True" - если процесс ещё НЕ завершился, иначе "false".
        /// </summary>
        public bool HasNotFinished
        {
            get { return _hasNotFinished; }
            set
            {
                _hasNotFinished = value;
                OnPropertyChanged(nameof(HasNotFinished));
            }
        }



        #endregion STATE





        #region API



        /// <summary>
        /// ToggleProcessing flags, changing their values.
        /// <br />
        /// Переключить флаги, заменив их значения.
        /// </summary>
        public void ToggleProcessing()
        {
            bool bTemp = IsRunning;
            IsRunning = IsNotRunning;
            IsNotRunning = bTemp;
        }



        /// <summary>
        /// Toggle completion flags changing their value accordingly.
        /// <br />
        /// Переключить флаги готовности результата, изменив их значения соответствующим образом.
        /// </summary>
        public void Toggle()
        {
            ToggleProcessing();
            HasFinished = IsNotRunning;
            HasStarted = IsRunning || HasFinished;
        }



        #endregion API





        #region CONSTRUCTION




        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public CustomProcessingStatus()
        {
            _hasStarted = _isRunning = false;
            _hasFinished = _isNotRunning = true;
        }


        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="isAlreadyRunning">
        /// True - if the application is already running, otherwise false.
        /// <br />
        /// "True", если приложение уже запущено, иначе "false".
        /// </param>
        public CustomProcessingStatus(bool isAlreadyRunning) : this()
        {
            _hasStarted = _isRunning = isAlreadyRunning;
            _hasFinished = _isNotRunning = !isAlreadyRunning;
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
