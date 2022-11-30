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
        private ObservableCollection<UserModel> users;

        /// <inheritdoc cref="TheContactsString"/>
        private string theContactsString;

        private MessengerChat activeChat;

        /// <inheritdoc cref="CurrentUser"/>
        private UserModel currentUser;


        private UserModel selectedContact;


        private ObservableCollection<MessengerChat> chatList;


        /// <inheritdoc cref="WindowHeaderString"/>
        private string windowHeaderString;


        /// <inheritdoc cref="Message"/>
        private string message;


        /// <inheritdoc cref="UserFile"/>
        private FileInfo userFile;

        /// <inheritdoc cref="Server"/>
        private ClientReciever server;


        /// <summary>
        /// The service of the file selection dialog.
        /// <br />
        /// Сервис диалога выбора файла.
        /// </summary>
        private IDialogService _dialogService;


        private ClientLoginWindow _loginWindowReference;


        private ReversedClientWindow _chatWindowReference;




        /// <summary>
        /// The Users observable collection.
        /// <br />
        /// Обозреваемая коллекция пользователей.
        /// </summary>
        public ObservableCollection<UserModel> Users 
        {
            get { return users; }
            set
            {
                users = value;

                OnPropertyChanged(nameof(Users));
            }
        }


        /// <summary>
        /// The 'Contacts' string.
        /// <br />
        /// Надпись "Контакты".
        /// </summary>
        public string TheContactsString
        {
            get { return theContactsString; }
            set
            {
                theContactsString = value;
                OnPropertyChanged(nameof(TheContactsString));
            }
        }


        public MessengerChat ActiveChat
        {
            get { return activeChat; }
            set
            {
                activeChat = value;
                OnPropertyChanged(nameof(ActiveChat));
            }
        }




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


        public UserModel SelectedContact
        {
            get { return selectedContact; }
            set
            {
                selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));

                if (ChatList.First(c => c.Addressee == SelectedContact) is null) 
                    ChatList.Add(new(addresser: CurrentUser, addressee: SelectedContact));

                ActiveChat = ChatList.First(c => c.Addressee == SelectedContact);

                OnPropertyChanged(nameof(ActiveChat.MessageList));
            }
        }


        private ObservableCollection<MessengerChat> ChatList
        {
            get { return chatList; }
            set
            {
                chatList = value;
                OnPropertyChanged(nameof(ChatList));
            }
        }



        /// <summary>
        /// The header of the wpf window. Corrensponds to the user name.
        /// <br />
        /// Заголовок окна. Соответствует имени пользователя.
        /// </summary>
        public string WindowHeaderString
        {
            get { return windowHeaderString; }
            set
            {
                windowHeaderString = value;
                OnPropertyChanged(nameof(WindowHeaderString));
            }
        }





        /// <summary>
        /// Message input string.
        /// <br />
        /// Строка ввода сообщения.
        /// </summary>
        public string Message 
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }



        /// <summary>
        /// The file attached to the user message.
        /// <br />
        /// Файл, прикреплённый к пользовательскому сообщению.
        /// </summary>
        public FileInfo UserFile
        {
            get { return userFile; }
            set
            {
                userFile = value;
                OnPropertyChanged(nameof(UserFile));
            }
        }



        /// <summary>
        /// An instance of a 'ClientReciever';
        /// <br />
        /// Экземпляр класса "ClientReciever";
        /// </summary>
        public ClientReciever Server
        {
            get { return server; }
            set { server = value; }
        }





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
            userFile = null;
            _dialogService = new AttachFileDialogService();

            theContactsString = "contacts";

            currentUser = new();
            message = string.Empty;

            users = new ObservableCollection<UserModel>();
            server = new();

            server.connectedEvent += ConnectUser;                           // user connection;
            server.fileReceivedEvent += RecieveFile;                        // file receipt;
            server.msgReceivedEvent += RecieveMessage;                      // message receipt;
            server.otherUserDisconnectEvent += RemoveUser;                  // other user disconnection;
            //server.currentUserDisconnectEvent += DisconnectFromServer;      // current user disconnection;

            users.CollectionChanged += OnUsersCollectionChanged;

            // may be obsolete. tests needed;
            ConnectToServerCommand = new (ConnectToService);
            SendMessageCommand = new (SendMessage);
            SelectFileCommand = new (SelectFile);

            // we need to manage windows right after we connect;
            SignInButtonClickCommand = new(OnSignInButtonClick);

            server.SendOutput += ShowErrorMessage;
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
