using Net.Transmition;
using NetworkingAuxiliaryLibrary.Objects.Entities;

namespace ReversedClient.ViewModel.ClientStartupWindow
{
    public partial class ClientLoginWindowViewModel : INotifyPropertyChanged
    {


        #region STATE


        /// <inheritdoc cref="_userDTOdata"/>
        private UserDTO _userDTOdata;

        private User _fullUserData;


        /// <inheritdoc cref="ServiceTransmitter"/>
        private ClientTransmitter serviceTransmitter;



        /// <summary>
        /// An instance of an object to encapsulate user data transfered to another windows.
        /// <br />
        /// Экземпляр объекта для инкапсуляции пользовательских данных для передачи другим окнам.
        /// </summary>
        public UserDTO UserDTOdata
        {
            get { return _userDTOdata; }
            set
            {
                _userDTOdata = value;
                OnPropertyChanged(nameof(UserDTOdata));
            }
        }


        public User FullUserData
        {
            get { return _fullUserData; }
            set
            {
                _fullUserData = value;
                OnPropertyChanged(nameof(FullUserData));
            }
        }


        /// <summary>
        /// An instance of a 'ClientTransmitter' to communicate with the service;
        /// <br />
        /// Экземпляр класса "ClientTransmitter" для коммуникации с сервисом;
        /// </summary>
        public ClientTransmitter ServiceTransmitter
        {
            get { return serviceTransmitter; }
            set { serviceTransmitter = value; }
        }





        public delegate void WindowSwapDelegate();

        public event WindowSwapDelegate SuccessfulClientAuthorization;



        #endregion STATE




        #region COMMANDS


        public DelegateCommand SignInButtonClickCommand { get; }


        #endregion COMMANDS




        #region CONSTRUCTION




        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ClientLoginWindowViewModel()
        {
            _userDTOdata = new UserDTO();

            serviceTransmitter = new();
            serviceTransmitter.SendOutput += ShowErrorMessage;

            SignInButtonClickCommand = new(OnSignInButtonClick);
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
