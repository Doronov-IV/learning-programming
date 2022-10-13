using System.Collections.ObjectModel;
using System.Windows;

using MainNetworkingProject.Model.Basics;

namespace MainNetworkingProject.ViewModel
{
    public class ReversedClientWindowViewModel
    {


        /// <summary>
        /// Обозреваемая коллекция из моделей пользователя
        /// </summary>
        public ObservableCollection<UserModel> Users { get; set; }

        /// <summary>
        /// Обозреваемая коллекция из сообщений
        /// </summary>
        public ObservableCollection<string> Messages { get; set; }

        /// <summary>
        /// Команда для подключения к серверу
        /// </summary>
        public RelayCommand ConnectToServerCommand { get; set; }

        /// <summary>
        /// Команда для отправки сообщения
        /// </summary>
        public RelayCommand SendMessageCommand { get; set; }

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

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ReversedClientWindowViewModel()
        {
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();
            _server = new();

            _server.connectedEvent += ConnectUser;//подключение нового пользователя
            _server.msgReceivedEvent += RecieveMessage;//получение сообщения
            _server.userDisconnectEvent += RemoveUser;//отключение пользователя

            //Команда подключения к серверу. Если имя пользователя не будет введено в текстовое поле, то команда не выполниться.
            ConnectToServerCommand = new RelayCommand(o => _server.ConnectToServer(UserName), o => !string.IsNullOrEmpty(UserName));

            //Команда для отправки сообщения. Если сообщение не будет введено в текстовое поле, то команда не выполниться.
            SendMessageCommand = new RelayCommand(o => _server.SendMessageToServer(Message), o => !string.IsNullOrEmpty(Message));
        }

        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();
            user = null!;
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));//удаление отключившегося пользователя из обозреваемой коллекции пользователей
        }

        /// <summary>
        /// считывание присланных данных
        /// </summary>
        private void RecieveMessage()
        {
            var msg = _server.PacketReader.ReadMessage();//считываем сообщение
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));//добавляем в обозреваемую коллекцию сообщений новое сообщение
        }

        //При подключении нового пользователя
        private void ConnectUser()
        {
            //создается новый экземпляр пользователя из модели пользователя с именем и идентификатором
            var user = new UserModel
            {
                UserName = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage(),
            };

            /*
             
              Если в коллекции пользователей нет ни одного пользователя у которого есть такой идентификатор,
            то создаем нового пользователя
            
             */
            if (!Users.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }


    }
}
