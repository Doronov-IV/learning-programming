using MainNetworkingProject.Model.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainNetworkingProject.Model.Basics
{
    public class ServiceHub
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        private static List<Client> _users = null!;

        /// <summary>
        /// Прослушивает подключения от TCP-клиентов сети.
        /// </summary>
        private static TcpListener _listener = null!;



        public delegate void ServiceOutputDelegate(string sOutputMessage);

        public static event ServiceOutputDelegate SendServiceOutput;



        public static void Run()
        {
            _users = new List<Client>();

            /*IP сервера (локальный IP адрес пк (Localhost 127.0.0.1)) и порт сервера
             * IPAddress: Предоставляет IP-адрес.*/
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);


            /*Метод Start инициализирует базовый Socketобъект, привязывает его к локальной конечной точке и ожидает входящих попыток подключения.
             *При получении Start запроса на подключение метод помещается в очередь и продолжает прослушивать дополнительные запросы до вызова Stop метода. 
             *Если TcpListener запрос на подключение будет получен после того, как он уже поставил в очередь максимальное количество подключений, 
             *он создаст SocketException запрос на подключение на клиенте.*/
            _listener.Start(); //Запускает ожидание входящих запросов на подключение.

            while (true)
            {
                var client = new Client(_listener.AcceptTcpClient());//AcceptTcpClient(): Принимает ожидающий запрос на подключение.
                client.SendServiceOutput += PassOutputMessage;
                _users.Add(client);//Добавление ного клиента в список пользователей
                BroadcastConnection();/*Broadcast the connextion to everyone on the server: Раздать соединение всем на сервере*/
            }
        }

        public static void BroadcastConnection()
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
        public static void BroadcastMessage(string message)
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

        public static void BroadcastDisconnect(string uid)
        {
            var disconnectedUser = _users.Where(x => x.UID.ToString() == uid).FirstOrDefault(); disconnectedUser = null!;
            _users.Remove(disconnectedUser);//Удаление отключенного клиента из списка пользователей
            foreach (var user in _users)
            {
                var broadcastPacket = new PacketBuilder();
                broadcastPacket.WriteOpCode(10);//код операции 10 присваивается пользователю который был отключен
                broadcastPacket.WriteMessage(uid);
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes());
            }

            BroadcastMessage($"[{disconnectedUser.UserName}] Disconnected!");//отправка всем сообщения с информацией о том что пользователь отключился
        }

        public static void PassOutputMessage(string sMessage)
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
            _users = new List<Client>(); 
        }
    }
}
