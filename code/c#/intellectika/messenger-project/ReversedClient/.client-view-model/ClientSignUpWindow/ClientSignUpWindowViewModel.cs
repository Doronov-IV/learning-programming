using NetworkingAuxiliaryLibrary.Objects.Common;
using ReversedClient.Model;
using Net.Transmition;

namespace ReversedClient.ViewModel.ClientSignUpWindow
{
    public partial class ClientSignUpWindowViewModel : INotifyPropertyChanged
    {


        #region STATE



        /// <inheritdoc cref="UserData"/>
        private UserClientTechnicalDTO _userData;


        /// <inheritdoc cref="ServiceTransmitter"/>
        private ClientTransmitter _serviceTransmitter;



        /// <summary>
        /// An instance of an object to encapsulate user data transfered to another windows.
        /// <br />
        /// Экземпляр объекта для инкапсуляции пользовательских данных для передачи другим окнам.
        /// </summary>
        public UserClientTechnicalDTO UserData
        {
            get { return _userData; }
            set
            {
                _userData = value;
                OnPropertyChanged(nameof(UserData));
            }
        }


        /// <summary>
        /// An instance of a 'ClientTransmitter' to communicate with the service;
        /// <br />
        /// Экземпляр класса "ClientTransmitter" для коммуникации с сервисом;
        /// </summary>
        public ClientTransmitter ServiceTransmitter
        {
            get { return _serviceTransmitter; }
            set { _serviceTransmitter = value; }
        }



        #endregion STATE




        #region COMMANDS


        /// <summary>
        /// A Prism command to handle 'Register' button click.
        /// <br />
        /// Команда Prism, для обработки клика по кнопке "Register".
        /// </summary>
        public DelegateCommand RegisterCommand { get; }


        #endregion COMMANDS




        #region CONSTRUCTION




        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ClientSignUpWindowViewModel(ClientTransmitter clientRadio)
        {
            RegisterCommand = new(OnRegisterButtonClick);
            _userData = new();
        }




        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public ClientSignUpWindowViewModel(ClientTransmitter clientRadio, string login) : this(clientRadio)
        {
            _userData.Login = login;
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
