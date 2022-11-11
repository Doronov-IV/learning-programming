using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;

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
        private static object _locker;



        /// <summary>
        /// An auto reset event instance for lockers demo.
        /// <br />
        /// Экземпляр AutoResetEvent для демки локеров.
        /// </summary>
        private AutoResetEvent _autoResetHandler;



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



        private static Stopwatch _stopwatch;

        private string _elapsedTime;

        public string elapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                _elapsedTime = value;
                OnPropertyChanged(nameof(elapsedTime));
            }
        }


        //public List<List<Point>> DotsList;



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





        private double _maxAmountOfDots;


        public double MaxAmountOfDots
        {
            get { return _maxAmountOfDots; }
            set
            {
                _maxAmountOfDots = value;
                OnPropertyChanged(nameof(MaxAmountOfDots));
            }
        }




        private double _maxCircleRadious;


        public double MaxCircleRadious
        {
            get { return _maxCircleRadious; }
            set
            {
                _maxCircleRadious = value;
                OnPropertyChanged(nameof(MaxCircleRadious));
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



        private double _startingRadius;


        public double StartingRadius
        {
            get { return _startingRadius; }
            set
            {
                _startingRadius = value;
                OnPropertyChanged(nameof(StartingRadius));
            }
        }



        private string _resultPiNumber;
        private int _threadCount;

        public string resultPiNumber
        {
            get { return _resultPiNumber; }
            set
            {
                _resultPiNumber = value;
                OnPropertyChanged(nameof(resultPiNumber));
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
            _largeMessage = string.Empty;
            _locker = new();
            _autoResetHandler = new(initialState: true);
            _mutex = new();

            _outputCollection = new();
            //OutputCollection.CollectionChanged += OnObservableCollectionChanged;

            double x =  1e213;
            int y = 0x9C4000;

            _maxAmountOfDots = 2048e5;      // limit_Nmax  = 1e7;
            _maxCircleRadious = 1024e4;     // limit_a = 1e6;
            StartingRadius = 100;           // min_a = 100;
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
