using NetworkingAuxiliaryLibrary.Objects.Common;
using ReversedClient.LocalService;
using ReversedClient.client_view;
using ReversedClient.Properties;
using System.Windows.Interop;
using ReversedClient.Model;
using Net.Transmition;
using System.Windows;

namespace ReversedClient.ViewModel.ClientChatWindow
{
    /// <summary>
    /// A view-model for the client window;
    /// <br />
    /// Вью-модель для окна клиента;
    /// </summary>
    public partial class ClientMessengerWindowViewModel : INotifyPropertyChanged
    {



        #region PROPERTIES - Object State




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                               ↓   FIELDS   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <inheritdoc cref="CurrentUserTechnicalDTO"/>
        private UserClientTechnicalDTO _currentUserTechnicalDTO;


        /// <inheritdoc cref="OnlineMembers"/>
        private ObservableCollection<UserClientPublicDTO> _onlineMembers;


        /// <inheritdoc cref="ActiveChat"/>
        private MessengerChat _activeChat;


        /// <inheritdoc cref="SelectedOnlineMember"/>
        private UserClientPublicDTO _selectedOnlineMember;


        /// <inheritdoc cref="CurrentUserModel"/>
        private UserClientPublicDTO _currentUserModel;


        /// <inheritdoc cref="ChatList"/>
        private ObservableCollection<MessengerChat> _chatList;


        /// <inheritdoc cref="WindowHeaderString"/>
        private string _windowHeaderString;


        /// <inheritdoc cref="Message"/>
        private string _message;


        /// <inheritdoc cref="UserFile"/>
        private FileInfo? _userFile;


        /// <inheritdoc cref="ServiceTransmitter"/>
        private ClientTransmitter _serviceTransmitter;


        private UserServerSideDTO acceptedUserData;


        private string _selectedMessage;


        /// <summary>
        /// The service of the file selection dialog.
        /// <br />
        /// Сервис диалога выбора файла.
        /// </summary>
        private IDialogService dialogService;




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   PROPERTIES   ↓                           ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 




        public UserClientTechnicalDTO CurrentUserTechnicalDTO
        {
            get { return _currentUserTechnicalDTO; }
            set
            {
                _currentUserTechnicalDTO = value;
                OnPropertyChanged(nameof(CurrentUserTechnicalDTO));
            }
        }


        /// <summary>
        /// The observable collection of known online users.
        /// <br />
        /// Обозреваемая коллекция пользователей в сети.
        /// </summary>
        public ObservableCollection<UserClientPublicDTO> OnlineMembers 
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
        /// The menu item representing selected online user from the 'Online' tab.
        /// <br />
        /// Пункт меню, который представляет собой выбранного пользователя "в сети", из вкладки "Online".
        /// </summary>
        public UserClientPublicDTO SelectedOnlineMember
        {
            get { return _selectedOnlineMember; }
            set
            {
                _selectedOnlineMember = value;
                OnPropertyChanged(nameof(SelectedOnlineMember));

                var someExistingChat = ChatList.FirstOrDefault(c => c.Addressee.PublicId.Equals(_selectedOnlineMember.UserName));
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
        public UserClientPublicDTO CurrentUserModel
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
            get { return _chatList; }
            set
            {
                _chatList = value;
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
            get { return _windowHeaderString; }
            set
            {
                _windowHeaderString = value;
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
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }


        /// <summary>
        /// The file attached to the user _message.
        /// <br />
        /// Файл, прикреплённый к пользовательскому сообщению.
        /// </summary>
        public FileInfo? UserFile
        {
            get { return _userFile; }
            set
            {
                _userFile = value;
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
            get { return _serviceTransmitter; }
            set { _serviceTransmitter = value; }
        }


        public string SelectedMessage
        {
            get
            {
                return _selectedMessage;
            }
            set
            {
                _selectedMessage = value;
                OnPropertyChanged(nameof(SelectedMessage));
            }
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


        public DelegateCommand DeleteMessageCommand { get; }



        #endregion COMMANDS - Prism Commands




        #region CONSTRUCTION - Object Lifetime


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ClientMessengerWindowViewModel()
        {
        }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ClientMessengerWindowViewModel(UserServerSideDTO userData, ClientTransmitter clientSocket)
        {
            acceptedUserData = userData;
            if (_chatList is null) _chatList = new();
            _currentUserModel = new(userName: userData.CurrentNickname, publicId: userData.CurrentPublicId);

            ChatParser.FillChats(userData, ref _chatList);
            OnPropertyChanged(nameof(ChatList));

            _windowHeaderString = userData.CurrentNickname;

            _userFile = null;
            dialogService = new AttachFileDialogService();

            _message = string.Empty;

            _onlineMembers = new ObservableCollection<UserClientPublicDTO>();

            _serviceTransmitter = clientSocket;
            _serviceTransmitter.connectedEvent += ConnectUser;                           // user connection;
            _serviceTransmitter.messageDeletionEvent += DeleteMessageAfterServiceRespond;                        // file receipt;
            _serviceTransmitter.msgReceivedEvent += RecieveMessage;                      // _message receipt;
            _serviceTransmitter.otherUserDisconnectEvent += RemoveUser;                  // other user disconnection;
            _serviceTransmitter.currentUserDisconnectEvent += DisconnectFromService;      // current user disconnection;
            ServiceTransmitter.ReadPacketsAsync();

            // may be obsolete. tests needed;
            ConnectToServerCommand = new(ConnectToService);
            SendMessageCommand = new(SendMessage);
            SelectFileCommand = new(SelectFile);
            DeleteMessageCommand = new(InitiateMessageDeletion);

            _serviceTransmitter.SendOutput += ShowErrorMessage;
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
