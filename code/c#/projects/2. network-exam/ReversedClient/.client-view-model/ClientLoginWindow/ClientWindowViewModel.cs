using ReversedClient.Model.Basics;
using System.Collections.ObjectModel;
using System.Security.RightsManagement;




namespace ReversedClient.ViewModel
{
    public class ClientWindowViewModel : INotifyPropertyChanged
    {



        #region PROPERTIES



        private ClientWindowViewModelHandler _Handler;



        /// <summary>
        /// @see public string UserName;
        /// </summary>
        private string _UserName;


        /// <summary>
        /// @see public string UserPassword;
        /// </summary>
        private string _UserPassword;


        /// <summary>
        /// User login;
        /// <br />
        /// Имя пользователя;
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }


        /// <summary>
        /// User's password;
        /// <br />
        /// Пароль пользователя;
        /// </summary>
        public string UserPassword
        {
            get { return _UserPassword; }
            set
            {
                _UserPassword = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }


        /// <summary>
        /// Экземпляр класса Сервер
        /// </summary>
        private ReversedService _server;

        public ReversedService Server
        {
            get { return _server; }
            set { _server = value; OnPropertyChanged(nameof(Server)); }
        }


        #endregion PROPERTIES




        #region COMMANDS


        public RelayCommand SignInButtonClickCommand { get; }


        #endregion COMMANDS





        #region CONSTRUCTION - Object Lifetime




        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ClientWindowViewModel()
        {

            _server = new();
            _Handler = new(this);

            _server.connectedEvent += _Handler.ConnectUser;


            _UserName = string.Empty;
            _UserPassword = string.Empty;

            SignInButtonClickCommand = new(h => _Handler.OnSignInButtonClick(), c => 1==1);
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




        #endregion CONSTRUCTION - Object Lifetime


    }
}
