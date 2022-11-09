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

        public string ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                _elapsedTime = value;
                OnPropertyChanged(nameof(ElapsedTime));
            }
        }


        public List<List<Point>> DotsList;



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





        private string _maxAmountOfDots;


        public string MaxAmountOfDots
        {
            get { return _maxAmountOfDots; }
            set
            {
                _maxAmountOfDots = value;
                OnPropertyChanged(nameof(MaxAmountOfDots));
            }
        }




        private string _maxCircleRadious;


        public string MaxCircleRadious
        {
            get { return _maxCircleRadious; }
            set
            {
                _maxCircleRadious = value;
                OnPropertyChanged(nameof(MaxCircleRadious));
            }
        }





        private string _startingRadius;


        public string StartingRadius
        {
            get { return _startingRadius; }
            set
            {
                _startingRadius = value;
                OnPropertyChanged(nameof(StartingRadius));
            }
        }



        private string _resultPiNumber;


        public string ResultPiNumber
        {
            get { return _resultPiNumber; }
            set
            {
                _resultPiNumber = value;
                OnPropertyChanged(nameof(ResultPiNumber));
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


            MaxAmountOfDots = "2048e5";
            MaxCircleRadious = "1024e4";
            StartingRadius = "100";
            _stopwatch = new();
            DotsList = new();
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
