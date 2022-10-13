using System.Security.Cryptography.X509Certificates;

namespace ReversedClient.Model.Basics
{
    /// <summary>
    /// A service that manages both connections and reading/writing data on lower level;
    /// <br />
    /// Сервис, который отвечает за подключения и чтение/запись данных на более низком уровне;
    /// </summary>
    public class ReversedService
    {


        #region PROPERTIES - public & private Properties



        /// <summary>
        /// Provides client connections fo the service;
        /// <br />
        /// Предоставляет клиенские подключения для сервиса;
        /// </summary>
        private TcpClient _client;



        /// <summary>
        /// An auxiliary object to make reading/writing messages easier;
        /// <br />
        /// Вспомогательный объект, который необходим, чтобы упростить чтение/запись сообщений;
        /// </summary>
        public PacketReader PacketReader;



        /// <summary>
        /// User connection Event;
        /// <br />
        /// Событие подключения пользователя;
        /// </summary>
        public event Action connectedEvent;



        /// <summary>
        /// Message recievment event;
        /// <br />
        /// Событие получения сообщения;
        /// </summary>
        public event Action msgReceivedEvent;



        /// <summary>
        /// User disconnection event;
        /// <br />
        /// Событие отключения пользователя;
        /// </summary>
        public event Action userDisconnectEvent;



        #endregion PROPERTIES - public & private Properties





        #region API - public Behavior



        /// <summary>
        /// Connect user to the service;
        /// <br />
        /// Подключить пользователя к сервису;
        /// </summary>
        /// <param name="userName">
        /// The nickname of the user chosen on login;
        /// <br />
        /// Никнейм, который пользователь выбрал на логине;
        /// </param>
        public void ConnectToServer(string userName)
        {
            try
            {
                //Если клиент не подключен
                if (!_client.Connected)
                {
                    /// 
                    /// - Client connection [!]
                    ///
                    _client.Connect("127.0.0.1", 7891);
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
        /// Send user's message to the service;
        /// <br />
        /// Отправить сообщение пользователя на сервис;
        /// </summary>
        /// <param name="message">
        /// User's message;
        /// <br />
        /// Сообщение пользователя;
        /// </param>
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
        /// Read the incomming packet. A packet is a specific message, sent by ServiceHub to handle different actions;
        /// <br />
        /// Прочитать входящий пакет. Пакет - это специальное сообщение, отправленное объектом "ServiceHub", чтобы структурировать обработку разных событий;
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
                            connectedEvent?.Invoke();// client connection;
                            break;

                        case 5:
                            msgReceivedEvent?.Invoke(); // message recieved;
                            break;

                        case 10:
                            userDisconnectEvent?.Invoke();// client disconnection;
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
