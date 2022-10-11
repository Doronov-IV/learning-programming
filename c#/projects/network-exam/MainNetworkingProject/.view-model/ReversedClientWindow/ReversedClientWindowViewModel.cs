using System.Collections.ObjectModel;
using System.Windows;

using MainNetworkingProject.Model.Basics;

namespace MainNetworkingProject.ViewModel
{
    public class ReversedClientWindowViewModel
    {


        #region PROPERTIES - Object State


        /// <summary>
        /// Observable list of users;
        /// <br />
        /// Обозреваемый список пользователей;
        /// </summary>
        public ObservableCollection<UserModel> UserList { get; set; }


        /// <summary>
        /// Observable list of messages;
        /// <br />
        /// Обозреваемый список сообщений;
        /// </summary>
        public ObservableCollection<string> MessageList { get; set; }


        /// <summary>
        /// Свойство: Имя пользователя
        /// </summary>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Свойство: Сообщение
        /// </summary>
        public string Message { get; set; } = null!;

        /// <summary>
        /// Экземпляр класса Сервер
        /// </summary>
        private ReversedService _server;


        #endregion PROPERTIES - Object State




        #region COMMANDS - Prism Commands


        /// <summary>
        /// Service connection;
        /// <br />
        /// Подключение к сервису;
        /// </summary>
        public RelayCommand ConnectToServerCommand { get; set; }


        /// <summary>
        /// Sending message;
        /// <br />
        /// Отправка сообщения;
        /// </summary>
        public RelayCommand SendMessageCommand { get; set; }


        #endregion COMMANDS - Prism Commands




        #region CONSTRUCTION - Object Lifetime


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ReversedClientWindowViewModel()
        {
            UserList = new ObservableCollection<UserModel>();
            MessageList = new ObservableCollection<string>();
            _server = new();

            _server.connectedEvent += ConnectUser;//подключение нового пользователя
            _server.msgReceivedEvent += RecieveMessage;//получение сообщения
            _server.userDisconnectEvent += RemoveUser;//отключение пользователя

            // check if the user name is present;
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(UserName), o => !string.IsNullOrEmpty(UserName));

            // check if the message is not empty
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
        }


        #endregion CONSTRUCTION - Object Lifetime




        #region LOGIC - internal behavior


        /// <summary>
        /// Remove a user from the client list;
        /// <br />
        /// Удалить пользователя из списка клиентов;
        /// </summary>
        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = UserList.Where(x => x.UID == uid).FirstOrDefault();
            user = null!;
            Application.Current.Dispatcher.Invoke(() => UserList.Remove(user)); // removing disconnected user;
        }


        /// <summary>
        /// Recieve user message;
        /// <br />
        /// Получить сообщение от пользователя;
        /// </summary>
        private void RecieveMessage()
        {
            var msg = _server.PacketReader.ReadMessage();                   // reading new message via our packet reader;
            Application.Current.Dispatcher.Invoke(() => MessageList.Add(msg)); // adding it to the observable collection;
        }


        /// <summary>
        /// Connect new user;
        /// <br />
        /// Подключить нового пользователя;
        /// </summary>
        private void ConnectUser()
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

            if (!UserList.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => UserList.Add(user));
            }
        }


        #endregion LOGIC - internal behavior


    }
}
