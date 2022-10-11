using System.Security.Cryptography.X509Certificates;

namespace MainNetworkingProject.Model.Basics
{
    public class ReversedService
    {


        #region PROPERTIES - public & private Properties


        /// <summary>
        /// Предоставляет клиентские подключения для сетевых служб протокола TCP.
        /// </summary>
        private TcpClient _client;


        public PacketReader PacketReader;

        /// <summary>
        /// событие подключения
        /// </summary>
        public event Action connectedEvent;

        /// <summary>
        /// событие получения сообщения
        /// </summary>
        public event Action msgReceivedEvent;

        /// <summary>
        /// событие отключения пользователя от сервера
        /// </summary>
        public event Action userDisconnectEvent;


        #endregion PROPERTIES - public & private Properties





        #region API - public Behavior


        /// <summary>
        /// Подключение к серверу
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        public void ConnectToServer(string userName)
        {
            try
            {
                //Если клиент не подключен
                if (!_client.Connected)
                {
                    //Подключение клиента к IP адресу (Localhost 127.0.0.1) и порту
                    _client.Connect("127.0.0.1", 7891);
                    //GetStream(): Возвращает объект NetworkStream, используемый для отправки и получения данных.
                    PacketReader = new(_client.GetStream());

                    if (!string.IsNullOrEmpty(userName))
                    {
                        var connectPacket = new PacketBuilder();

                        connectPacket.WriteOpCode(0);


                        connectPacket.WriteMessage(userName);

                        _client.Client.Send(connectPacket.GetPacketBytes());
                    }

                    ReadPackets();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"It seems that the server is down for now.\nPlease, try connect later.\n\nException: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        /// <summary>
        /// Disconnect client from service;
        /// <br />
        /// Отключить клиент от сервиса;
        /// </summary>
        public void Disconnect()
        {
            if (_client.Connected)
            {
                _client.Close();
            }
        }


        /// <summary>
        /// Отправка сообщения на сервер
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void SendMessageToServer(string message)
        {
            try
            {
                var messagePacket = new PacketBuilder();
                messagePacket.WriteOpCode(5);//присваиваем написанию сообщения код операции равный 5
                messagePacket.WriteMessage(message);
                _client.Client.Send(messagePacket.GetPacketBytes());//отправляем массив байт из сообщения
            }
            catch (Exception ex)
            {
                MessageBox.Show($"You haven't connected yet.\n\nException: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion API - public Behavior





        #region LOGIC - internal Behavior



        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private void ReadPackets()
        {
            Task.Run(() =>
            {
                byte opCode = 0;
                while (true)
                {
                    //код операции;
                    try
                    {
                        opCode = PacketReader.ReadByte();
                    }
                    catch (Exception e)
                    {
                        userDisconnectEvent?.Invoke();
                    }
                    switch (opCode)
                    {

                        case 1:
                            connectedEvent?.Invoke();// подключение клиента;
                            break;

                        case 5:
                            msgReceivedEvent?.Invoke(); // получение сообщения;
                            break;

                        case 10:
                            userDisconnectEvent?.Invoke();// отключение клиента от сервера;
                            break;

                        default:
                            MessageBox.Show("Operation code out of [1,5,10]. This is a debug message.", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            });
        }


        #endregion LOGIC - internal Behavior





        #region CONSTRUCTION - Object Lifetime


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ReversedService()
        {
            _client = new TcpClient();
        }


        #endregion CONSTRUCTION - Object Lifetime


    }
}
