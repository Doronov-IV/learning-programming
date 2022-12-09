using ReversedService.LocalService;
using ReversedService.Model.Context;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using System.Linq;
using Toolbox.Flags;
using Tools.Flags;

namespace ReversedService.ViewModel.ServiceWindow
{
    /// <summary>
    /// The _service window view-model;
    /// <br />
    /// Вью-модель окна сервиса;
    /// </summary>
    public partial class ServiceWindowViewModel : INotifyPropertyChanged
    {



        #region STATE - Fields and Properties



        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                               ↓   FIELDS   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <inheritdoc cref="Service"/>
        private ServiceController _service;


        /// <inheritdoc cref="CustomTerminalManager"/>
        private TerminalManager _customTerminalManager;


        /// <inheritdoc cref="ServiceTrigger"/>
        private bool _serviceTrigger;


        /// <inheritdoc cref="ProcessingStatus"/>
        private CustomProcessingStatus _processingStatus;


        /// <summary>
        /// The main CTS to be able to stop service work w/o closing the application.
        /// <br />
        /// Основной CTS для того, чтобы можно было останавливать работу сервиса не закрывая приложения.
        /// </summary>
        public static CancellationTokenSource cancellationTokenSource = new();




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   PROPERTIES   ↓                           ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// An instance of the _service-wrapper class;
        /// <br />
        /// Экземпляр класса-обёртки сервиса;
        /// </summary>
        public ServiceController Service
        {
            get { return _service; }
            set
            {
                _service = value;
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
            get { return _customTerminalManager; }
            set
            {
                _customTerminalManager = value;
                OnPropertyChanged(nameof(CustomTerminalManager));
            }
        }


        /// <summary>
        /// The status of _service work.
        /// <br />
        /// Статус работы сервиса.
        /// </summary>
        public CustomProcessingStatus ProcessingStatus
        {
            get { return _processingStatus; }
            set
            {
                _processingStatus = value;
                OnPropertyChanged(nameof(ProcessingStatus));
            }
        }


        /// <summary>
        /// 'True' - if _service is processing at the moment, otherwise 'false'.
        /// <br />
        /// "True", если сервис работает в данный момент, иначе "false".
        /// </summary>
        public bool ServiceTrigger
        {
            get { return _serviceTrigger; }
            set
            {
                _serviceTrigger = value;
                
                OnPropertyChanged(nameof(ServiceTrigger));

                if (_serviceTrigger && ProcessingStatus.IsNotRunning) 
                    OnRunClick();
                else OnShutdownClick();
            }
        }



        #endregion STATE - Fields and Properties






        #region COMMANDS



        /// <summary>
        /// Binding checkbox with the 'ListenClientsAsync' method;
        /// <br />
        /// Привязка чекбокса с методом "ListenClientsAsync";
        /// </summary>
        public DelegateCommand RunServiceCommand { get; }


        /// <summary>
        /// Binding checkbox with the 'Stop' method;
        /// <br />
        /// Привязка чекбокса с методом "Stop";
        /// </summary>
        public DelegateCommand StopServiceCommand { get; }


        /// <summary>
        /// Clear service message log command.
        /// <br />
        /// Команда очистки лога сообщений сервиса.
        /// </summary>
        public DelegateCommand ClearLogCommand { get; }



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
            _customTerminalManager = new TerminalManager();

            ProcessingStatus = new();
            _serviceTrigger = false;
            ClearLogCommand = new(CustomTerminalManager.ClearLog);
            Service.SendServiceOutput += CustomTerminalManager.AddMessage;
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
