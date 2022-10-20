using ReversedClient.client_view;
using System.Windows;

using ReversedClient.Model.Basics;
using ReversedClient.Net.Main;
using ReversedClient.Net.Auxiliary;
using ReversedClient.ViewModel.ReversedClientWindow;

namespace ReversedClient.ViewModel
{
    /// <summary>
    /// A view-model for the client window;
    /// <br />
    /// Вью-модель для окна клиента;
    /// </summary>
    public class ReversedClientWindowViewModel : INotifyPropertyChanged
    {



        #region PROPERTIES - Object State



        private ReversedClientWindowViewModelHandler _Handler;


        /// <summary>
        /// @see public ObservableCollection<UserModel> Users;
        /// </summary>
        private ObservableCollection<UserModel> _Users;


        /// <summary>
        /// Обозреваемая коллекция из моделей пользователя
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


        private string _TheMembersString;

        public string TheMembersString
        {
            get { return _TheMembersString; }
            set
            {
                _TheMembersString = value;
                OnPropertyChanged(nameof(TheMembersString));
            }
        }


        /// <summary>
        /// @see public ObservableCollection<string> Messages;
        /// </summary>
        private ObservableCollection<string> _Messages;


        /// <summary>
        /// Обозреваемая коллекция из сообщений
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


        /// <summary>
        /// @see public string UserName;
        /// </summary>
        private string _UserName;


        /// <summary>
        /// The name of the user to connect;
        /// <br />
        /// Имя пользователя, для подключения;
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


        private string _WindowHeaderString;

        public string WindowHeaderString
        {
            get { return _WindowHeaderString; }
            set
            {
                _WindowHeaderString = value;
                OnPropertyChanged(nameof(WindowHeaderString));
            }
        }



        private string _Message;


        /// <summary>
        /// Свойство: Сообщение
        /// </summary>
        public string Message 
        {
            get { return _Message; }
            set
            {
                _Message = value;
                OnPropertyChanged(nameof(Message));
            }
        }


        private FileInfo _UserFile;

        public FileInfo UserFile
        {
            get { return _UserFile; }
            set
            {
                _UserFile = value;
                OnPropertyChanged(nameof(UserFile));
            }
        }


        /// <summary>
        /// @see public ReversedService Server;
        /// </summary>
        private ReversedClient.Net.Main.ReversedService _server;


        /// <summary>
        /// An instance of a 'ReversedService';
        /// <br />
        /// Экземпляр класса "ReversedService";
        /// </summary>
        public ReversedClient.Net.Main.ReversedService Server
        {
            get { return _server; }
            set { _server = value; }
        }


        private IDialogService _DialogService;


        #endregion PROPERTIES - Object State




        #region COMMANDS - Prism Commands



        /// <summary>
        /// [?] To be revied, may be obsolete;
        /// </summary>
        public RelayCommand ConnectToServerCommand { get; set; }

        /// <summary>
        /// A command to handle the 'Send' button click;
        /// <br />
        /// Команда для обработки нажатия кнопки "Отправить";
        /// </summary>
        public RelayCommand SendMessageCommand { get; set; }

        public RelayCommand SendFileCommand { get; set; }

        /// <summary>
        /// A command to handle the 'Sign In' button click;
        /// <br />
        /// Команда для обработки нажатия кнопки "Войти";
        /// </summary>
        public RelayCommand SignInButtonClickCommand { get; }


        public RelayCommand SelectFileCommand { get; }



        #endregion COMMANDS - Prism Commands




        #region LOGIC - internal behavior



        /// <summary>
        /// Remove a user from the client list;
        /// <br />
        /// Удалить пользователя из списка клиентов;
        /// </summary>
        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();

            //foreach (var user in )
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user)); // removing disconnected user;
        }



        /// <summary>
        /// Recieve user message;
        /// <br />
        /// Получить сообщение от пользователя;
        /// </summary>
        private void RecieveMessage()
        {
            var msg = _server.PacketReader.ReadMessage();                   // reading new message via our packet reader;
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg)); // adding it to the observable collection;
        }


        private void RecieveFile()
        {
            var file = _server.PacketReader.ReadFile(UserName);
            Application.Current.Dispatcher.Invoke(() => Messages.Add("File recieved."));
        }



        /// <summary>
        /// Connect new user;
        /// <br />
        /// Подключить нового пользователя;
        /// </summary>
        public void ConnectUser()
        {
            // create new user instance;
            var user = new UserModel
            {
                UserName = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage(),
            };

            /*
             
           [!] In case there's no such user in collection we add them manualy;
            To prevent data duplication;
            
             */

            if (!Users.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }


        public void SelectFile()
        {
            try
            {
                if (_DialogService.OpenFileDialog())
                {
                    UserFile = new(_DialogService.FilePath);
                }
            }
            catch (Exception ex)
            {
                _DialogService.ShowMessage(ex.Message);
            }
        }



        /// <summary>
        /// Send a message to the service;
        /// <br />
        /// Is needed to nullify the chat message field after sending;
        /// <br />
        /// <br />
        /// Отправить сообщение на сервис;
        /// <br />
        /// Необходимо, чтобы стереть сообщение после отправкиж
        /// </summary>
        private void SendMessage()
        {
            _server.SendMessageToServer(Message);
            Message = "";
        }

        private void SendFile()
        {
            if (UserFile.FullName != "")
            _server.SendFileToServer(UserFile);
        }



        /// <summary>
        /// [?] To be revued;
        /// </summary>
        public void CommenceDisconnect(object? sender, CancelEventArgs args)
        {
            _server.Disconnect();
        }



        #endregion LOGIC - internal behavior




        #region CONSTRUCTION - Object Lifetime


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ReversedClientWindowViewModel()
        {
            _DialogService = new AttachFileDialogService();

            _TheMembersString = "member";

            _UserName = string.Empty;
            _Message = string.Empty;

            _Users = new ObservableCollection<UserModel>();
            _Messages = new ObservableCollection<string>();
            _server = new();

            _server.connectedEvent += ConnectUser;           // user connection;
            _server.msgReceivedEvent += RecieveMessage;     // message receipt;
            _server.fileReceivedEvent += RecieveFile;      // file receipt;
            _server.userDisconnectEvent += RemoveUser;    // user disconnection;

            _Handler = new(this);

            _Users.CollectionChanged += _Handler.OnUsersCollectionChanged;

            // may be obsolete. tests needed;
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(UserName), o => 1 == 1);

            SendMessageCommand = new RelayCommand(o => SendMessage(), o => 1 == 1);

            SendFileCommand = new RelayCommand(o => SendFile(), o => 1 == 1);

            SelectFileCommand = new RelayCommand(o => SelectFile(), o => 1 == 1);

            // we need to manage windows right after we connect;
            SignInButtonClickCommand = new(o => _Handler.OnSignInButtonClick(), o => 1 == 1);
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
