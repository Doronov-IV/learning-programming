using ReversedService.Net.Main;

namespace ReversedService.ViewModel.ServiceWindow
{
    /// <summary>
    /// The service window view-model;
    /// <br />
    /// Вью-модель окна сервиса;
    /// </summary>
    public partial class ServiceWindowViewModel : INotifyPropertyChanged
    {



        #region PROPERTIES


        //
        // Basic assosiations
        //

        /// <summary>
        /// @see public ServiceWindowViewModelHandler Handler;
        /// </summary>
        private ServiceWindowViewModelHandler _Handler;

        /// <summary>
        /// ViewModel's behavior class, required to decompose it;
        /// <br />
        /// Класс поведения вью-модели, необходимый, чтобы декомпозировать её;
        /// </summary>
        public ServiceWindowViewModelHandler Handler
        {
            get { return _Handler; }
            set
            {
                _Handler = value;
                OnPropertyChanged(nameof(Handler));
            }
        }


        /// <summary>
        /// @see public ServiceHub Service;
        /// </summary>
        private ServiceHub _Service;

        /// <summary>
        /// An instance of the service-wrapper class;
        /// <br />
        /// Экземпляр класса-обёртки сервиса;
        /// </summary>
        public ServiceHub Service
        {
            get { return _Service; }
            set
            {
                _Service = value;
                OnPropertyChanged(nameof(Service));
            }
        }



        //
        // The chat messages in form of a list
        //

        /// <summary>
        /// @see public AsyncObservableCollection<string> ServiceLog;
        /// </summary>
        private AsyncObservableCollection<string> _ServiceLog;

        /// <summary>
        /// A list of chat messages;
        /// <br />
        /// Список сообщений в чате;
        /// </summary>
        public AsyncObservableCollection<string> ServiceLog
        {
            get { return _ServiceLog; }
            set
            {
                _ServiceLog = value;
                OnPropertyChanged(nameof(ServiceLog));
            }
        }



        //
        // Visibility binding properties
        //


        /// <summary>
        /// @see public bool IsRunning;
        /// </summary>
        private bool _IsRunning;

        /// <summary>
        /// @see public bool IsNotRunning;
        /// </summary>
        private bool _IsNotRunning;

        /// <summary>
        /// True if the server is listenning, otherwise false;
        /// <br />
        /// True если сервис ведёт приём, иначе false;
        /// </summary>
        public bool IsRunning
        {
            get { return _IsRunning; }
            set
            {
                _IsRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        /// <summary>
        /// True if the server is NOT listenning, otherwise false;
        /// <br />
        /// True если сервис НЕ ведёт приём, иначе false;
        /// </summary>
        public bool IsNotRunning
        {
            get { return _IsNotRunning; }
            set
            {
                _IsNotRunning = value;
                OnPropertyChanged(nameof(IsNotRunning));
            }
        }


        #endregion PROPERTIES




        #region COMMANDS


        /// <summary>
        /// A command that binds the 'Run' button with the corresponding method;
        /// <br />
        /// Комманда, которая связывает кнопку "Run" с соответствующим методом;
        /// </summary>
        public DelegateCommand RunServiceCommand { get; }


        /// <summary>
        /// A command that binds the 'Cancel Service' button with the corresponding method;
        /// <br />
        /// Команда, которая связывает кнопку "Cancel Service" с соответствующим методом;
        /// </summary>
        public DelegateCommand KillServiceCommand { get; }


        #endregion COMMANDS




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ServiceWindowViewModel()
        {
            _IsRunning = false;
            _IsNotRunning = true;

            _Service = new();
            _Handler = new(this);
            _ServiceLog = new();

            RunServiceCommand = new(Handler.OnRunButtonClick);
            KillServiceCommand = new(Handler.OnCancelServiceButtonClick);
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
