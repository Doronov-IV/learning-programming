using ReversedClient.client_view;
using System.Windows;
using Debug.Net;
using ReversedClient.Model.Basics;
using ReversedClient.Properties;
using System.Windows.Interop;


namespace ReversedClient.ViewModel
{
    /// <summary>
    /// A view-model for the client window;
    /// <br />
    /// Вью-модель для окна клиента;
    /// </summary>
    public partial class ReversedClientWindowViewModel : INotifyPropertyChanged
    {



        #region PROPERTIES - Object State


        /// <inheritdoc cref="Users"/>
        private ObservableCollection<UserModel> _Users;

        /// <summary>
        /// The Users observable collection.
        /// <br />
        /// Обозреваемая коллекция пользователей.
        /// </summary>
        public ObservableCollection<UserModel> Users 
        {
            get { return _Users; }
            set
            {
                _Users = value;

                OnPropertyChanged(nameof(Users));
            }
        }




        /// <inheritdoc cref="TheMembersString"/>
        private string _TheMembersString;

        /// <summary>
        /// The 'Members' string.
        /// <br />
        /// Надпись "Участники".
        /// </summary>
        public string TheMembersString
        {
            get { return _TheMembersString; }
            set
            {
                _TheMembersString = value;
                OnPropertyChanged(nameof(TheMembersString));
            }
        }





        /// <inheritdoc cref="Messages"/>
        private ObservableCollection<string> _Messages;


        /// <summary>
        /// Message history observable collection.
        /// <br />
        /// Обозреваемая коллекция сообщений.
        /// </summary>
        public ObservableCollection<string> Messages 
        {
            get { return _Messages; }
            set
            {
                _Messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }





        /// <inheritdoc cref="UserName"/>
        private string _userName;


        /// <summary>
        /// The name of the user to connect;
        /// <br />
        /// Имя пользователя, для подключения;
        /// </summary>
        public string UserName 
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }


        /// <inheritdoc cref="WindowHeaderString"/>
        private string _windowHeaderString;

        /// <summary>
        /// The header of the wpf window. Corrensponds to the user name.
        /// <br />
        /// Заголовок окна. Соответствует имени пользователя.
        /// </summary>
        public string WindowHeaderString
        {
            get { return _windowHeaderString; }
            set
            {
                _windowHeaderString = value;
                OnPropertyChanged(nameof(WindowHeaderString));
            }
        }



        /// <inheritdoc cref="Message"/>
        private string _message;

        /// <summary>
        /// Message input string.
        /// <br />
        /// Строка ввода сообщения.
        /// </summary>
        public string Message 
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        /// <inheritdoc cref="UserFile"/>
        private FileInfo _userFile;

        /// <summary>
        /// The file attached to the user message.
        /// <br />
        /// Файл, прикреплённый к пользовательскому сообщению.
        /// </summary>
        public FileInfo UserFile
        {
            get { return _userFile; }
            set
            {
                _userFile = value;
                OnPropertyChanged(nameof(UserFile));
            }
        }


        /// <inheritdoc cref="Server"/>
        private ReversedService _server;

        /// <summary>
        /// An instance of a 'ReversedService';
        /// <br />
        /// Экземпляр класса "ReversedService";
        /// </summary>
        public ReversedService Server
        {
            get { return _server; }
            set { _server = value; }
        }



        /// <summary>
        /// The service of the file selection dialog.
        /// <br />
        /// Сервис диалога выбора файла.
        /// </summary>
        private IDialogService _DialogService;


        #endregion PROPERTIES - Object State




        #region COMMANDS - Prism Commands



        /// <summary>
        /// [?] To be revied, may be obsolete.
        /// </summary>
        public DelegateCommand ConnectToServerCommand { get; set; }


        /// <summary>
        /// A command to handle the 'Send' button click.
        /// <br />
        /// Команда для обработки нажатия кнопки "Отправить".
        /// </summary>
        public DelegateCommand SendMessageCommand { get; set; }


        /// <summary>
        /// A command to handle the sending file.
        /// <br />
        /// Команда для обработки отправки файла.
        /// </summary>
        public DelegateCommand SendFileCommand { get; set; }


        /// <summary>
        /// A command to handle the 'Sign In' button click.
        /// <br />
        /// Команда для обработки нажатия кнопки "Войти".
        /// </summary>
        public DelegateCommand SignInButtonClickCommand { get; }


        /// <summary>
        /// A command for the 'Attach file' button.
        /// <br />
        /// Команда для кнопки "Attach file".
        /// </summary>
        public DelegateCommand SelectFileCommand { get; }



        #endregion COMMANDS - Prism Commands




        #region CONSTRUCTION - Object Lifetime


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ReversedClientWindowViewModel()
        {
            _userFile = null;
            _DialogService = new AttachFileDialogService();

            _TheMembersString = "member";

            _userName = string.Empty;
            _message = string.Empty;

            _Users = new ObservableCollection<UserModel>();
            _Messages = new ObservableCollection<string>();
            _server = new();

            _server.connectedEvent += ConnectUser;           // user connection;
            _server.msgReceivedEvent += RecieveMessage;     // message receipt;
            _server.fileReceivedEvent += RecieveFile;      // file receipt;
            _server.otherUserDisconnectEvent += RemoveUser;    // user disconnection;
            _server.currentUserDisconnectEvent += DisconnectFromServer;

            _Users.CollectionChanged += OnUsersCollectionChanged;

            // may be obsolete. tests needed;
            ConnectToServerCommand = new (ConnectToService);

            SendMessageCommand = new (SendMessage);

            SelectFileCommand = new (SelectFile);

            // we need to manage windows right after we connect;
            SignInButtonClickCommand = new(OnSignInButtonClick);

            _server.SendOutput += ShowErrorMessage;
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
