using NetworkingAuxiliaryLibrary.Net.Auxiliary.Processing;
using ReversedClient.ViewModel.ClientStartupWindow;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using NetworkingAuxiliaryLibrary.Objects.Common;
using ReversedClient.ViewModel.ClientChatWindow;
using NetworkingAuxiliaryLibrary.Processing;
using System.IO.Packaging;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;
using System.Windows;
using System.Net;
using ReversedClient.LocalService;

namespace Net.Transmition
{
    /// <summary>
    /// An instance that provides client with basic datalink operations such as: connectoin, data receipt, data sending;
    /// <br />
    /// Абстракция, которая предоставляет клиенту возможность проводить основные сетевые действия, такие как: подключение, приём данных, передача данных;
    /// </summary>
    public class ClientTransmitter
    {


        #region PROPERTIES - public & private Properties



        //
        // EndPoint
        //


        /// <summary>
        /// Localhost address;
        /// <br />
        /// Адрес локалхоста;
        /// </summary>
        private static IPAddress localHostIpAddress = IPAddress.Parse("127.0.0.1");


        /// <summary>
        /// A field you can insert your address into;
        /// <br />
        /// Поле, в которое вы можете вписать свой адрес;
        /// </summary>
        private static IPAddress otherHostIpAddress = IPAddress.Parse("127.0.0.1");


        /// <summary>
        /// Provides client connections fo the service;
        /// <br />
        /// Предоставляет клиенские подключения для сервиса;
        /// </summary>
        private TcpClient authorizationSocket;


        private TcpClient messengerSocket;


        /// <summary>
        /// An auxiliary object to make reading/writing messages easier;
        /// <br />
        /// Вспомогательный объект, который необходим, чтобы упростить чтение/запись сообщений;
        /// </summary>
        private PackageReader _authorizationPacketReader;






        /// <summary>
        /// .
        /// <br />
        /// .
        /// </summary>
        private PackageReader _messangerPacketReader;



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
        /// File receipt event.
        /// <br />
        /// Событие получения файла.
        /// </summary>
        public event Action fileReceivedEvent;

        /// <summary>
        /// Other user disconnection event;
        /// <br />
        /// Событие отключения другого пользователя;
        /// </summary>
        public event Action otherUserDisconnectEvent;

        /// <summary>
        /// Current user disconnection event.
        /// <br />
        /// Событие отключение текущего пользователя.
        /// </summary>
        public event Action currentUserDisconnectEvent;




        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();




        /// <summary>
        /// A delegate for transeffring output to other objects;
        /// <br />
        /// Делегат для передачи аутпута другим объектам;
        /// </summary>
        /// <param name="sOutputMessage">
        /// A _message that we want to see somewhere (в данном случае, в консоли сервера и в пользовательском клиенте);
        /// <br />
        /// Сообщение, которое мы хотим где-то увидеть (в данном случае, в консоли сервера и в пользовательском клиенте);
        /// </param>
        public delegate void PendOutputDelegate(string sOutputMessage);

        /// <inheritdoc cref="PendOutputDelegate"/>
        public event PendOutputDelegate SendOutput;


        public PackageReader AuthorizationPacketReader
        {
            get { return _authorizationPacketReader; }
            set
            {
                _authorizationPacketReader = value;
            }
        }


        public PackageReader MessangerPacketReader
        {
            get { return _messangerPacketReader; }
            set
            {
                _messangerPacketReader = value;
            }
        }




        #endregion PROPERTIES - public & private Properties





        #region API - public Behavior




        /// <summary>
        /// Conntect to the authorization service and proceed to authorization;
        /// <br />
        /// Подключиться к сервису авторизации и пройти процесс авторизации;
        /// </summary>
        /// <param name="user">
        /// User technical DTO containing their private data.
        /// <br />
        /// "Технический" DTO пользователя, содержащий его личную информацию.
        /// </param>
        /// <returns>
        /// 'True' - if authorization successful, otherwise 'false'.
        /// <br />
        /// "True" - есди авторизация успешна, иначе "false".
        /// </returns>
        public bool ConnectAndAuthorize(UserClientTechnicalDTO user)
        {
            //Если клиент не подключен
            if (!authorizationSocket.Connected)
            {
                /// 
                /// - Client connection [!]
                ///
                authorizationSocket.ConnectAsync(NetworkConfigurator.GetAuthorizerEndPoint());
            }
                _authorizationPacketReader = new(authorizationSocket.GetStream());

                var connectPacket = new PackageBuilder();

                connectPacket.WriteOpCode(1);

                connectPacket.WriteMessage(new TextMessagePackage($"{user.Login}", "Service", $"{user.Login}|{user.Password}"));

                authorizationSocket.Client.Send(connectPacket.GetPacketBytes());

                var result = _authorizationPacketReader.ReadMessage().Message as string;

                if (result.Equals("Denied")) return false;
                else return true;
        }



        /// <summary>
        /// Connect to the messenger service and send a query for current user data.
        /// <br />
        /// Подключиться к сервису сообщений и запросить данные текущего пользователя.
        /// </summary>
        /// <param name="user">
        /// User technical DTO containing their private data.
        /// <br />
        /// "Технический" DTO пользователя, содержащий его личную информацию.
        /// </param>
        public void ConnectAndSendLoginToService(UserClientTechnicalDTO user)
        {
            try
            {
                messengerSocket.Connect(NetworkConfigurator.GetMessengerEndPoint());
            }
            catch (Exception ex)
            {
                messengerSocket = new();
                messengerSocket.Connect(NetworkConfigurator.GetMessengerEndPoint());
            }

            if (messengerSocket.Connected)
            {
                MessangerPacketReader = new(messengerSocket.GetStream());

                var connectPacket = new PackageBuilder();

                connectPacket.WriteMessage(new TextMessagePackage($"{user.Login}", "Service", $"{user.Login}"));

                messengerSocket.Client.Send(connectPacket.GetPacketBytes());
            }
        }



        /// <summary>
        /// Disconnect client from messenger service;
        /// <br />
        /// Отключить клиент от сервиса мессенжера;
        /// </summary>
        public void Disconnect()
        {
            if (authorizationSocket.Connected)
            {
                cancellationTokenSource.Cancel();

                messengerSocket.Close();

                currentUserDisconnectEvent?.Invoke();
            }
        }



        /// <summary>
        /// Send user's _message to the service;
        /// <br />
        /// Отправить сообщение пользователя на сервис;
        /// </summary>
        /// <param name="message">
        /// User's _message;
        /// <br />
        /// Сообщение пользователя;
        /// </param>
        public void SendMessageToServer(TextMessagePackage package)
        {
            var messagePacket = new PackageBuilder();
            messagePacket.WriteOpCode(5);
            messagePacket.WriteMessage(package);
            try
            {
                messengerSocket.Client.Send(messagePacket.GetPacketBytes());
            }
            catch (Exception ex)
            {
                SendOutput.Invoke($"You haven't connected yet.\n\nException: {ex.Message}");
            }
        }



        public bool RegisterNewUser(UserClientTechnicalDTO userData)
        {
            if (!authorizationSocket.Connected) authorizationSocket.Connect(NetworkConfigurator.GetAuthorizerEndPoint());

            SendNewClientData(userData);

            PackageReader _authorizationPacketReader = new(authorizationSocket.GetStream());

            var result = _authorizationPacketReader.ReadMessage().Message as string;

            if (result.Equals("Denied")) return false;
            else return true;
        }




        /// <summary>
        /// Get the data sent by messenger in response for client data request after successful authorization.
        /// <br />
        /// Получить данные от мессенжера, в ответ на запрос пользователя после удачной авторизации.
        /// </summary>
        public UserServerSideDTO GetResponseData()
        {
            UserServerSideDTO res = null;
            var code = _messangerPacketReader.ReadByte();
            if (code == 12)
            {
                var msg = _messangerPacketReader.ReadMessage();

                res = JsonConvert.DeserializeObject(msg.Message as string, type: typeof(UserServerSideDTO)) as UserServerSideDTO;
            }
            return res;
        }


        #endregion API - public Behavior





        #region LOGIC - internal Behavior



        /// <summary>
        /// Read the incomming packet. A packet is a specific _message, sent by ServiceHub to handle different actions;
        /// <br />
        /// Прочитать входящий пакет. Пакет - это специальное сообщение, отправленное объектом "ServiceHub", чтобы структурировать обработку разных событий;
        /// </summary>
        public async Task ReadPacketsAsync()
        {
            if (_messangerPacketReader is null) _messangerPacketReader = new(messengerSocket.GetStream());

            byte opCode = 77;
            while (true)
            {
                try
                {
                    await Task.Run(() => opCode = _messangerPacketReader.ReadByte());
                }
                catch (Exception e)
                {
                }
                finally
                {
                    if (opCode != 77)
                    {
                        switch (opCode)
                        {
                            case 0:
                                break;

                            case 1:
                                connectedEvent?.Invoke(); // client connection;
                                break;

                            case 5:
                                msgReceivedEvent?.Invoke(); // _message recieved;
                                break;

                            case 6:
                                fileReceivedEvent?.Invoke(); // file  recieved;
                                break;

                            case 10:
                                otherUserDisconnectEvent?.Invoke(); // client disconnection;
                                break;

                            case byte.MaxValue:
                                Disconnect();
                                break;

                            default:
                                SendOutput.Invoke("Operation code out of [1,5,6,10]. This is a debug _message.\nproject: ReversedClient, class: ClientTransmitter, method: ReadPacketsAsync.");
                                break;
                        }
                    }
                }
            }
        }



        /// <summary>
        /// Send data of the client that wants to register.
        /// <br />
        /// Отправить даные клиента, который хочет зарегистрироваться.
        /// </summary>
        /// <param name="userData">
        /// New user data, packed in DTO.
        /// <br />
        /// Данные нового пользователя, упакованные в "DTO".
        /// </param>
        private void SendNewClientData(UserClientTechnicalDTO userData)
        {
            var messagePacket = new PackageBuilder();
            var signUpMessage = new TextMessagePackage(sender: userData.PublicId, reciever: "Service", message: $"{userData.Login}|{userData.Password}");
            messagePacket.WriteOpCode(0);
            messagePacket.WriteMessage(signUpMessage);
            try
            {
                authorizationSocket.Client.Send(messagePacket.GetPacketBytes());
            }
            catch (Exception ex)
            {
                SendOutput.Invoke($"You haven't connected yet.\n\nException: {ex.Message}");
            }
        }



        #endregion LOGIC - internal Behavior





        #region CONSTRUCTION - Object Lifetime


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ClientTransmitter()
        {
            
            authorizationSocket = new TcpClient();
            messengerSocket = new TcpClient();
        }


        #endregion CONSTRUCTION - Object Lifetime



    }
}
