using MainConcurrencyProject.Model.Calculator;
using MainConcurrencyProject.Model.Divisors;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using Toolbox.Flags;

namespace MainConcurrencyProject.ViewModel
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {


        #region STATE



        /// <summary>
        /// A locker for concurrent shared resources management.
        /// <br />
        /// Локер для многопоточного использования разделяемых ресурсов.
        /// </summary>
        public static object locker = new();



        /// <summary>
        /// An auto reset event instance for lockers demo.
        /// <br />
        /// Экземпляр AutoResetEvent для демки локеров.
        /// </summary>
        public static AutoResetEvent _autoResetHandler = new(initialState: true);



        /// <summary>
        /// A mutex instance for concurrency examples.
        /// <br />
        /// Экземпляр мьютекса для примеров многопоточности.
        /// </summary>
        private Mutex _mutex;



        /// <summary>
        /// A semaphore instance for concurrency examples.
        /// <br />
        /// Эеземпляр семафора для экспериментов с многопотоком.
        /// </summary>
        private static Semaphore _semaphore = new(1,1);



        /// <summary>
        /// A string that accumulates output in a console-like way.
        /// <br />
        /// Строка, которая накапливает инпут по типу консоли.
        /// </summary>
        private string _largeMessage;



        private string _pauseContinueActionName;

        private string _elapsedTime;


        private long _progressBarMaximum;

        public long ProgressBarMaximum
        {
            get { return _progressBarMaximum; }
            set
            {
                _progressBarMaximum = value;
                OnPropertyChanged(nameof(ProgressBarMaximum));
            }
        }

        private long _progressValue;

        public long ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                OnPropertyChanged(nameof(ProgressValue));
            }
        }


        public string elapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                _elapsedTime = value;
                OnPropertyChanged(nameof(elapsedTime));
            }
        }


        



        /// <inheritdoc cref="OutputCollection"/>
        private ObservableCollection<string> _outputCollection;



        /// <summary>
        /// ListBox messages collections.
        /// <br />
        /// Список сообщений листбокса.
        /// </summary>
        public ObservableCollection<string> OutputCollection
        {
            get { return _outputCollection; }
            set
            {
                _outputCollection = value;
                OnPropertyChanged(nameof(OutputCollection));
            }
        }


        public int ThreadCount
        {
            get => _threadCount;

            set
            {
                _threadCount = value;
                OnPropertyChanged(nameof(ThreadCount));
            }
        }


        private int _threadCount;


        private string resultNumber;


        public string ResultNumber
        {
            get => resultNumber;
            set
            {
                resultNumber = value;
                OnPropertyChanged((nameof(ResultNumber)));
            }
        }



        public CustomProcessingStatus ProcessingStatus
        {
            get;
            set;
        }


        public string PauseContinueActionName
        {
            get { return _pauseContinueActionName; }
            set
            {
                _pauseContinueActionName = value;
                OnPropertyChanged(nameof(PauseContinueActionName));
            }
        }



        #endregion STATE







        #region COMMANDS



        /// <summary>
        /// 'Do Action' button click command.
        /// <br />
        /// Команда клика по кнопке "Do Action".
        /// </summary>
        public DelegateCommand DoActionClickCommand { get; }



        /// <summary>
        /// 'Pause calculations' command.
        /// <br />
        /// Комманда "приостановить вычисление".
        /// </summary>
        public DelegateCommand PressPauseCommand { get; }



        #endregion COMMANDS







        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MainWindowViewModel()
        {
            DoActionClickCommand = new(OnDoActionButtonClickAsync);
            PressPauseCommand = new(OnPauseKeyButtonPressed);
            _largeMessage = string.Empty;
            locker = new();
            //_autoResetHandler = new(initialState: true);
            _mutex = new();

            PauseContinueActionName = "Pause";

            _outputCollection = new();
            ProcessingStatus = new();
        }



        #region property changed



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



        #endregion property changed



        #endregion CONSTRUCTION


    }
}
