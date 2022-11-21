using Toolbox.Flags;
using Tools.Flags;

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



        /// <inheritdoc cref="Service"/>
        private ServiceController service;

        /// <summary>
        /// An instance of the service-wrapper class;
        /// <br />
        /// Экземпляр класса-обёртки сервиса;
        /// </summary>
        public ServiceController Service
        {
            get { return service; }
            set
            {
                service = value;
                OnPropertyChanged(nameof(Service));
            }
        }


        public static CancellationTokenSource cancellationTokenSource = new();



        //
        // The chat messages in form of a list
        //

        /// <inheritdoc cref="ServiceLog"/>
        private AsyncObservableCollection<string> serviceLog;

        /// <summary>
        /// A list of chat messages;
        /// <br />
        /// Список сообщений в чате;
        /// </summary>
        public AsyncObservableCollection<string> ServiceLog
        {
            get { return serviceLog; }
            set
            {
                serviceLog = value;
                OnPropertyChanged(nameof(ServiceLog));
            }
        }



        //
        // Visibility binding properties
        //


        private CustomProcessingStatus processingStatus;

        public CustomProcessingStatus ProcessingStatus
        {
            get { return processingStatus; }
            set
            {
                processingStatus = value;
                OnPropertyChanged(nameof(ProcessingStatus));
            }
        }



        private bool serviceTrigger;

        public bool ServiceTrigger
        {
            get { return serviceTrigger; }
            set
            {
                serviceTrigger = value;
                
                OnPropertyChanged(nameof(ServiceTrigger));

                if (serviceTrigger && ProcessingStatus.IsNotRunning)
                {
                    OnRunClick();
                }
                else OnShutdownClick();
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


        public DelegateCommand StopServiceCommand { get; }


        #endregion COMMANDS




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ServiceWindowViewModel()
        {
            Service = new();
            ServiceLog = new();

            ProcessingStatus = new();

            Service.SendServiceOutput += OnServiceOutput;

            //RunServiceCommand = new(OnRunClick);
            //StopServiceCommand = new(OnShutdownClick);

            serviceTrigger = false;
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
