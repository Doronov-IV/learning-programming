using ReversedClient.client_view;
using System.Windows;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using Net.Transmition;
using ReversedClient.Model.Basics;
using ReversedClient.ViewModel.Misc;
using ReversedClient.Properties;
using System.Windows.Interop;


namespace ReversedClient.ViewModel.ClientChatWindow
{
    /// <summary>
    /// A view-model for the client window;
    /// <br />
    /// Вью-модель для окна клиента;
    /// </summary>
    public partial class ReversedClientWindowViewModel : INotifyPropertyChanged
    {



        #region PROPERTIES - Object State




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                               ↓   FIELDS   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        private UserDTO _currentUserDTO;


        private User currentServiceSiteUser;


        /// <inheritdoc cref="OnlineMembers"/>
        private ObservableCollection<UserModel> _onlineMembers;


        /// <inheritdoc cref="ActiveChat"/>
        private MessengerChat _activeChat;


        /// <inheritdoc cref="SelectedOnlineMember"/>
        private UserModel _selectedOnlineMember;


        /// <inheritdoc cref="CurrentUserModel"/>
        private UserModel _currentUserModel;


        /// <inheritdoc cref="ChatList"/>
        private ObservableCollection<MessengerChat> chatList;


        /// <inheritdoc cref="WindowHeaderString"/>
        private string windowHeaderString;


        /// <inheritdoc cref="Message"/>
        private string message;


        /// <inheritdoc cref="UserFile"/>
        private FileInfo? userFile;


        /// <inheritdoc cref="ServiceTransmitter"/>
        private ClientTransmitter serviceTransmitter;


        /// <summary>
        /// The service of the file selection dialog.
        /// <br />
        /// Сервис диалога выбора файла.
        /// </summary>
        private IDialogService dialogService;




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   PROPERTIES   ↓                           ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 
        



        public UserDTO CurrentUserDTO
        {
            get { return _currentUserDTO; }
            set
            {
                _currentUserDTO= value;
                OnPropertyChanged(nameof(CurrentUserDTO));
            }
        }


        /// <summary>
        /// The observable collection of known online users.
        /// <br />
        /// Обозреваемая коллекция пользователей в сети.
        /// </summary>
        public ObservableCollection<UserModel> OnlineMembers 
        {
            get { return _onlineMembers; }
            set
            {
                _onlineMembers = value;

                OnPropertyChanged(nameof(OnlineMembers));
            }
        }


        /// <summary>
        /// A chat that user has clicked upon. 'Null' by default.
        /// <br />
        /// Чат, на который кликнул пользователь. "Null" по умолчанию.
        /// </summary>
        public MessengerChat ActiveChat
        {
            get { return _activeChat; }
            set
            {
                _activeChat = value;
                OnPropertyChanged(nameof(ActiveChat));
            }
        }


        /// <summary>
        /// .
        /// <br />
        /// .
        /// </summary>
        public UserModel SelectedOnlineMember
        {
            get { return _selectedOnlineMember; }
            set
            {
                _selectedOnlineMember = value;
                OnPropertyChanged(nameof(SelectedOnlineMember));

                var someExistingChat = ChatList.FirstOrDefault(c => c.Addressee.UserName.Equals(_selectedOnlineMember.UserName));
                if (someExistingChat is null)
                {
                    someExistingChat = new(addresser: CurrentUserModel, addressee: SelectedOnlineMember);
                }
                ActiveChat= someExistingChat;
            }
        }


        /// <summary>
        /// The instance representing current client info;
        /// <br />
        /// Объект, который содержит в себе данные текущего клиента;
        /// </summary>
        public UserModel CurrentUserModel
        {
            get { return _currentUserModel; }
            set
            {
                _currentUserModel = value;
                OnPropertyChanged(nameof(CurrentUserModel));
            }
        }


        /// <summary>
        /// A list of chats.
        /// <br />
        /// Список чатов.
        /// </summary>
        public ObservableCollection<MessengerChat> ChatList
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
        public FileInfo? UserFile
        {
            get { return userFile; }
            set
            {
                userFile = value;
                OnPropertyChanged(nameof(UserFile));
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

        public event WindowSwapDelegate Disconnection;


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
        public ReversedClientWindowViewModel() : this(new(),new())
        {
        }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ReversedClientWindowViewModel(User userData, ClientTransmitter clientRadio)
        {
            FillChats(chatList, userData);
            currentServiceSiteUser = userData;
            if (chatList is null) chatList = new();

            windowHeaderString = userData.CurrentNickname;

            userFile = null;
            dialogService = new AttachFileDialogService();

            _currentUserModel = new(userName: userData.CurrentNickname, publicId: userData.PublicId);
            message = string.Empty;

            _onlineMembers = new ObservableCollection<UserModel>();

            serviceTransmitter = clientRadio;
            serviceTransmitter.connectedEvent += ConnectUser;                           // user connection;
            serviceTransmitter.fileReceivedEvent += RecieveFile;                        // file receipt;
            serviceTransmitter.msgReceivedEvent += RecieveMessage;                      // message receipt;
            serviceTransmitter.otherUserDisconnectEvent += RemoveUser;                  // other user disconnection;
            serviceTransmitter.currentUserDisconnectEvent += DisconnectFromService;      // current user disconnection;
            ServiceTransmitter.ReadPacketsAsync();

            // may be obsolete. tests needed;
            ConnectToServerCommand = new(ConnectToService);
            SendMessageCommand = new(SendMessage);
            SelectFileCommand = new(SelectFile);

            serviceTransmitter.SendOutput += ShowErrorMessage;
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
