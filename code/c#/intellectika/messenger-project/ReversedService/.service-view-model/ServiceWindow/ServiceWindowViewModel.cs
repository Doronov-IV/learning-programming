using ReversedService.LocalService;
using ReversedService.Model.Context;
using ReversedService.Model.Entities;
using System.Linq;
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

        /// <inheritdoc cref="CustomTerminalManager"/>
        private TerminalManager customTerminalManager;

        /// <inheritdoc cref="ServiceTrigger"/>
        private bool serviceTrigger;

        /// <inheritdoc cref="ProcessingStatus"/>
        private CustomProcessingStatus processingStatus;


        /// <summary>
        /// .
        /// <br />
        /// .
        /// </summary>
        public static CancellationTokenSource cancellationTokenSource = new();



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


        /// <summary>
        /// An object to decompose vm of terminal management.
        /// <br />
        /// Объект для декомпозиции вью-модели от управления терминалом.
        /// </summary>
        public TerminalManager CustomTerminalManager
        {
            get { return customTerminalManager; }
            set
            {
                customTerminalManager = value;
                OnPropertyChanged(nameof(CustomTerminalManager));
            }
        }


        public CustomProcessingStatus ProcessingStatus
        {
            get { return processingStatus; }
            set
            {
                processingStatus = value;
                OnPropertyChanged(nameof(ProcessingStatus));
            }
        }


        public bool ServiceTrigger
        {
            get { return serviceTrigger; }
            set
            {
                serviceTrigger = value;
                
                OnPropertyChanged(nameof(ServiceTrigger));

                if (serviceTrigger && ProcessingStatus.IsNotRunning) 
                    OnRunClick();
                else OnShutdownClick();
            }
        }


        #endregion PROPERTIES




        #region COMMANDS



        /// <summary>
        /// Binding checkbox with the 'Run' method;
        /// <br />
        /// Привязка чекбокса с методом "Run";
        /// </summary>
        public DelegateCommand RunServiceCommand { get; }


        /// <summary>
        /// Binding checkbox with the 'Stop' method;
        /// <br />
        /// Привязка чекбокса с методом "Stop";
        /// </summary>
        public DelegateCommand StopServiceCommand { get; }


        public DelegateCommand ClearLogCommand { get; }

        public DelegateCommand SendLineCommand { get; }



        #endregion COMMANDS




        #region LOGIC


        private void SeedAdmins()
        {
        }


        #endregion LOGIC




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ServiceWindowViewModel()
        {
            Service = new();
            customTerminalManager = new TerminalManager();

            ProcessingStatus = new();
            serviceTrigger = false;

            SendLineCommand = new(CustomTerminalManager.AddLine);
            ClearLogCommand = new(CustomTerminalManager.ClearLog);
            Service.SendServiceOutput += CustomTerminalManager.AddMessage;

            try
            {
                SeedAdmins();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Admins already seeded.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
