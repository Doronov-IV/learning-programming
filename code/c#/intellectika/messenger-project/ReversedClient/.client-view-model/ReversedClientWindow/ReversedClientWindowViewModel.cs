using ReversedClient.client_view;
using System.Windows;
using Debug.Net;
using ReversedClient.Model.Basics;
using ReversedClient.ViewModel.Chatting;
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




        /// <inheritdoc cref="TheContactsString"/>
        private string _TheMembersString;

        /// <summary>
        /// The 'Members' string.
        /// <br />
        /// Надпись "Участники".
        /// </summary>
        public string TheContactsString
        {
            get { return _TheMembersString; }
            set
            {
                _TheMembersString = value;
                OnPropertyChanged(nameof(TheContactsString));
            }
        }


        private MessengerChat activeChat;

        public MessengerChat ActiveChat
        {
            get { return activeChat; }
            set
            {
                activeChat = value;
                OnPropertyChanged(nameof(ActiveChat));
            }
        }





        /// <inheritdoc cref="CurrentUser"/>
        private UserModel currentUser;


        /// <summary>
        /// The name of the user to connect;
        /// <br />
        /// Имя пользователя, для подключения;
        /// </summary>
        public UserModel CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }


        private UserModel _selectedContact;


        public UserModel SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));

                if (ChatList.First(c => c.Addressee == SelectedContact) is null) 
                    ChatList.Add(new(addresser: CurrentUser, addressee: SelectedContact));
                else 
                    ActiveChat = ChatList.First(c => c.Addressee == SelectedContact);
                OnPropertyChanged(nameof(ActiveChat.MessageList));
            }
        }


        private ObservableCollection<MessengerChat> chatList;


        private ObservableCollection<MessengerChat> ChatList
        {
            get { return chatList; }
            set
            {
                chatList = value;
                OnPropertyChanged(nameof(ChatList));
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
        private ClientReciever _server;

        /// <summary>
        /// An instance of a 'ClientReciever';
        /// <br />
        /// Экземпляр класса "ClientReciever";
        /// </summary>
        public ClientReciever Server
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



        private ClientLoginWindow loginWindowReference;


        private ReversedClientWindow chatWindowReference;





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
            chatList = new();
            _userFile = null;
            _DialogService = new AttachFileDialogService();

            _TheMembersString = "contacts";

            currentUser = new();
            _message = string.Empty;

            _Users = new ObservableCollection<UserModel>();
            _server = new();

            _server.connectedEvent += ConnectUser;                           // user connection;
            _server.fileReceivedEvent += RecieveFile;                        // file receipt;
            _server.msgReceivedEvent += RecieveMessage;                      // message receipt;
            _server.otherUserDisconnectEvent += RemoveUser;                  // other user disconnection;
            _server.currentUserDisconnectEvent += DisconnectFromServer;      // current user disconnection;

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
