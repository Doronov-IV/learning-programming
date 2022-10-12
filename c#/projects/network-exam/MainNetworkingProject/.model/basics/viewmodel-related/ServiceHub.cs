using MainNetworkingProject.Model.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainNetworkingProject.Model.Basics
{
    /// <summary>
    /// A wrapper on 'ReversedService' object. Unlike one, it's responsible for user notification, rather than connections;
    /// <br />
    /// Обёртка для объекта "ReversedService". В отличие от последнего, образец данного класса отвечает за рассылку уведомлений пользователям, а не за подключения;
    /// </summary>
    public class ServiceHub : INotifyPropertyChanged
    {


        #region PROPERTIES - State of an Object



        /// <summary>
        /// A list of current users;
        /// <br />
        /// Актуальный список пользователей;
        /// </summary>
        private List<ReversedClient> _UserList = null!;


        /// <summary>
        /// The main TCP listener;
        /// <br />
        /// Основной слушатель;
        /// </summary>
        private TcpListener _Listener = null!;


        /// <summary>
        /// @see public bool IsRunning;
        /// </summary>
        private bool _isRunning;


        /// <summary>
        /// Is service running;
        /// <br />
        /// Работает ли сервис;
        /// </summary>
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }





        /// <summary>
        /// A delegate for transeffring output to other objects;
        /// <br />
        /// Делегат для передачи аутпута другим объектам;
        /// </summary>
        /// <param name="sOutputMessage">
        /// A message that we want to see somewhere (In this case, in a server console);
        /// <br />
        /// Сообщение, которое мы хотим где-то увидеть (в данном случае, в консоли сервера);
        /// </param>
        public delegate void ServiceOutputDelegate(string sOutputMessage);


        /// <summary>
        /// @see public delegate void ServiceOutputDelegate(string sOutputMessage);
        /// </summary>
        public event ServiceOutputDelegate SendServiceOutput;



        #endregion PROPERTIES - State of an Object




        #region API - public Behavior



        /// <summary>
        /// Run service;
        /// <br />
        /// Запустить сервис;
        /// </summary>
        public void Run()
        {
            _UserList = new List<ReversedClient>();

            _Listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);

            _Listener.Start();

            while (true)
            {
                var client = new ReversedClient(_Listener.AcceptTcpClient(), this);//AcceptTcpClient(): Принимает ожидающий запрос на подключение.
                _UserList.Add(client);//Добавление ного клиента в список пользователей
                BroadcastConnection();/*Broadcast the connextion to everyone on the server: Раздать соединение всем на сервере*/
            }
        }



        /// <summary>
        /// Broadcast a notification message to all users about the new user connection;
        /// <br />
        /// Транслировать уведомление для всех пользователей о подключении нового пользователя;
        /// </summary>
        public void BroadcastConnection()
        {
            foreach (var user in _UserList)
            {
                foreach (var usr in _UserList)
                {

                    var broadcastPacket = new PacketBuilder();
                    broadcastPacket.WriteOpCode(1); // code '1' means 'new user have connected';
                    broadcastPacket.WriteMessage(usr.UserName);
                    broadcastPacket.WriteMessage(usr.UID.ToString());
                    user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
                }
            }
        }



        /// <summary>
        /// Send message to all users. Mostly use to deliver one user's message to all other ones;
        /// <br />
        /// Отправить сообщение всем пользователям. В основном, используется, чтобы переслать сообщение одного пользователя всем остальным;
        /// </summary>
        /// <param name="message"></param>
        public void BroadcastMessage(string message)
        {
            //рассылка отправленного сообщения всем пользователям
            foreach (var user in _UserList)
            {
                var msgPacket = new PacketBuilder();
                msgPacket.WriteOpCode(5);
                msgPacket.WriteMessage(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }



        /// <summary>
        /// Broadcast a notification message to all users about some user disconnection;
        /// <br />
        /// Транслировать уведомление для всех пользователей о том, что один из пользователей отключился;
        /// </summary>
        /// <param name="uid">
        /// id of the user in order to find and delete him from the user list;
        /// <br />
        /// id пользователя, чтобы найти его и удалить из списка пользователей;
        /// </param>
        public void BroadcastDisconnect(string uid)
        {
            var disconnectedUser = _UserList.Where(x => x.UID.ToString() == uid).FirstOrDefault();
            _UserList.Remove(disconnectedUser);            // removing user;
            foreach (var user in _UserList)
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.WriteOpCode(10);    // on user disconnection, service recieves the code-10 operation and broadcasts the "disconnect message";  
                broadcastPacket.WriteMessage(uid); // it also passes disconnected user id (not sure where that goes, mb viewmodel delegate) so we can pull it out from users list;
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
            }

            BroadcastMessage($"{disconnectedUser.UserName} Disconnected!");
        }



        /// <summary>
        /// Pass the message out to another object that might have the ability to output this message;
        /// <br />
        /// Передать сообщение другому объекту, который может иметь возможность вывести его куда-нибудь;
        /// </summary>
        /// <param name="sMessage"></param>
        public void PassOutputMessage(string sMessage)
        {
            SendServiceOutput.Invoke(sMessage);
        }



        #endregion API - public Behavior




        #region CONSTRUCTION - Object Lifetime



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ServiceHub()
        {
            _UserList = new List<ReversedClient>();
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
