using System.Collections.ObjectModel;
using System.Windows;
using ReversedClient.client_view;

using ReversedClient.Model.Basics;

namespace ReversedClient.ViewModel
{
    public class ReversedClientWindowViewModel : INotifyPropertyChanged
    {


        #region PROPERTIES - Object State



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


        private string _UserName;

        /// <summary>
        /// Свойство: Имя пользователя
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

        /// <summary>
        /// Экземпляр класса Сервер
        /// </summary>
        private ReversedService _server;



        private ReversedClientWindowViewModel _ReversedViewModelReference;


        #endregion PROPERTIES - Object State




        #region COMMANDS - Prism Commands



        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        public RelayCommand ConnectToServerCommand { get; set; }


        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        public RelayCommand SendMessageCommand { get; set; }

        public RelayCommand SignInButtonClickCommand { get; }


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



        private void SendMessage()
        {
            _server.SendMessageToServer(Message);
            Message = "";
        }



        public void CommenceDisconnect(object? sender, CancelEventArgs args)
        {
            _server.Disconnect();
        }


        public void OnSignInButtonClick()
        {
            _server.ConnectToServer(UserName);

            ReversedClientWindow clientChatWindow = new();
            clientChatWindow.Show();

            ClientLoginWindow clientLoginWindow = Application.Current.MainWindow as ClientLoginWindow;
            clientLoginWindow.Close();
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
            _Users = new ObservableCollection<UserModel>();
            _Messages = new ObservableCollection<string>();
            _server = new();

            _server.connectedEvent += ConnectUser;
            _server.msgReceivedEvent += RecieveMessage;//получение сообщения
            _server.userDisconnectEvent += RemoveUser;//отключение пользователя


            //Команда подключения к серверу. Если имя пользователя не будет введено в текстовое поле, то команда не выполнится.
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(UserName), o => 1 == 1);

            //Команда для отправки сообщения. Если сообщение не будет введено в текстовое поле, то команда не выполнится.
            SendMessageCommand = new RelayCommand(o => SendMessage(), o => 1 == 1);


            SignInButtonClickCommand = new(o => OnSignInButtonClick(), o => 1 == 1);


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
