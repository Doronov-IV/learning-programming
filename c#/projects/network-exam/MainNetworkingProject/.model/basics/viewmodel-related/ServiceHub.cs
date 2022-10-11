using MainNetworkingProject.Model.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainNetworkingProject.Model.Basics
{
    public class ServiceHub : INotifyPropertyChanged
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        private List<ReversedClient> _users = null!;

        /// <summary>
        /// Прослушивает подключения от TCP-клиентов сети.
        /// </summary>
        private TcpListener _listener = null!;


        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }



        public delegate void ServiceOutputDelegate(string sOutputMessage);

        public event ServiceOutputDelegate SendServiceOutput;



        public void Run()
        {
            _users = new List<ReversedClient>();

            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);

            _listener.Start();

            while (true)
            {
                var client = new ReversedClient(_listener.AcceptTcpClient(), this);//AcceptTcpClient(): Принимает ожидающий запрос на подключение.
                _users.Add(client);//Добавление ного клиента в список пользователей
                BroadcastConnection();/*Broadcast the connextion to everyone on the server: Раздать соединение всем на сервере*/
            }
        }

        public void BroadcastConnection()
        {
            foreach (var user in _users)
            {
                foreach (var usr in _users)
                {

                    var broadcastPacket = new PacketBuilder();//создаем новый пакет
                    broadcastPacket.WriteOpCode(1);//присваиваем пакету код операции равный 1
                    broadcastPacket.WriteMessage(usr.UserName);//передаем текущее имя пользователя
                    broadcastPacket.WriteMessage(usr.UID.ToString());//так же передаем User ID в виде строки
                    user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());//теперь отправляем пакет байтов
                }
            }
        }
        public void BroadcastMessage(string message)
        {
            //рассылка отправленного сообщения всем пользователям
            foreach (var user in _users)
            {
                var msgPacket = new PacketBuilder();
                msgPacket.WriteOpCode(5);
                msgPacket.WriteMessage(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketBytes());
            }
        }

        public void BroadcastDisconnect(string uid)
        {
            var disconnectedUser = _users.Where(x => x.UID.ToString() == uid).FirstOrDefault();
            _users.Remove(disconnectedUser);//Удаление отключенного клиента из списка пользователей
            foreach (var user in _users)
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.WriteOpCode(10);//код операции 10 присваивается пользователю который был отключен
                broadcastPacket.WriteMessage(uid);
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
            }

            BroadcastMessage($"{disconnectedUser.UserName} Disconnected!");//отправка всем сообщения с информацией о том что пользователь отключился
        }

        public void PassOutputMessage(string sMessage)
        {
            SendServiceOutput.Invoke(sMessage);
        }



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ServiceHub()
        {
            _users = new List<ReversedClient>();
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


    }
}
