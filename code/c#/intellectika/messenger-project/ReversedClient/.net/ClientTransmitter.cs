using Hyper;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using NetworkingAuxiliaryLibrary.Processing;
using System.IO.Packaging;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

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
        /// Current service EndPoint;
        /// <br />
        /// Текущий эндпоинт сервиса;
        /// </summary>
        private IPEndPoint messangerServiceEndPoint = new(localHostIpAddress, 7333);


        private IPEndPoint authorizationServiceEndPoint = new(localHostIpAddress, 7222);


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
        /// A message that we want to see somewhere (в данном случае, в консоли сервера и в пользовательском клиенте);
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
        /// Connect user to the service;
        /// <br />
        /// Подключить пользователя к сервису;
        /// </summary>
        /// <param name="user">
        /// .
        /// <br />
        /// .
        /// </param>
        /// <returns>
        /// .
        /// <br />
        /// .
        /// </returns>
        public bool ConnectAndAuthorize(UserDTO user)
        {
            try
            {
                //Если клиент не подключен
                if (!authorizationSocket.Connected)
                {
                    /// 
                    /// - Client connection [!]
                    ///
                    authorizationSocket.ConnectAsync(authorizationServiceEndPoint);
                }
                    _authorizationPacketReader = new(authorizationSocket.GetStream());

                    if (cancellationTokenSource.IsCancellationRequested)
                        cancellationTokenSource = new();


                    var connectPacket = new PackageBuilder();

                    connectPacket.WriteOpCode(1);

                    connectPacket.WriteMessage(new TextMessagePackage($"{user.Login}", "Service", $"{user.Login}|{user.Password}"));

                    authorizationSocket.Client.Send(connectPacket.GetPacketBytes());

                    var result = _authorizationPacketReader.ReadMessage().Message as string;

                    if (result.Equals("Denied")) return false;
                    else return true;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("The service is down.", "Unable to connect", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                return false;
            }
        }


        public void ConnectAndSendLoginToService(UserDTO user)
        {
            try
            {
                messengerSocket.Connect(messangerServiceEndPoint);
            }
            catch (Exception ex)
            {
                messengerSocket = new();
                messengerSocket.Connect(messangerServiceEndPoint);
            }

            if (messengerSocket.Connected)
            {
                _messangerPacketReader = new(messengerSocket.GetStream());

                if (cancellationTokenSource.IsCancellationRequested)
                    cancellationTokenSource = new();


                var connectPacket = new PackageBuilder();

                connectPacket.WriteMessage(new TextMessagePackage($"{user.Login}", "Service", $"{user.Login}"));

                messengerSocket.Client.Send(connectPacket.GetPacketBytes());
            }
        }


        /// <summary>
        /// Disconnect client from service;
        /// <br />
        /// Отключить клиент от сервиса;
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
        /// Send user's message to the service;
        /// <br />
        /// Отправить сообщение пользователя на сервис;
        /// </summary>
        /// <param name="message">
        /// User's message;
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
                authorizationSocket.Client.Send(messagePacket.GetPacketBytes());
            }
            catch (Exception ex)
            {
                SendOutput.Invoke($"You haven't connected yet.\n\nException: {ex.Message}");
            }
        }


        /// <summary>
        /// Send file to the service.
        /// <br />
        /// Отправить файл на сервис.
        /// </summary>
        /// <param name="info">
        /// Attached file.
        /// <br />
        /// Прикреплённый файл.
        /// </param>
        public async void SendFileToServerAsync(FileMessagePackage filePackage)
        {
            PackageBuilder messagePacket = new();
            messagePacket.WriteOpCode(6);
            messagePacket.WriteMessage(filePackage);

            var bytes = messagePacket.GetPacketBytes();
            const int bufferSize = 4096;
            byte[] buffer;
            try
            {
                await Task.Run(() =>
                {
                    if (bytes.Length > bufferSize)
                    {
                        using (MemoryStream stream = new(bytes))
                        {
                            while (stream.Position != stream.Length)
                            {
                                buffer = new byte[bufferSize];
                                stream.Read(buffer, 0, bufferSize);
                                authorizationSocket.Client.Send(buffer, SocketFlags.Partial);
                            }
                        }
                    }
                else authorizationSocket.Client.Send(messagePacket.GetPacketBytes(), SocketFlags.Partial);
                });
            }
            catch (Exception ex)
            {
                SendOutput.Invoke($"You haven't connected yet.\n\nException: {ex.Message}");
            }
            finally
            {
                messagePacket = null;
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
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
        public void SendNewClientData(UserDTO userData)
        {
            var messagePacket = new PackageBuilder();
            var signUpMessage = new TextMessagePackage(sender: userData.Login, reciever: "Service", message: $"{userData.Password}");
            messagePacket.WriteOpCode(1); // write another code, since code '1' is for sign in
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


        public User GetResponseData()
        {
            User res = null;
            var code = _messangerPacketReader.ReadByte();
            if (code == 12)
            {
                var bufferLength = _messangerPacketReader.ReadInt32();

                byte[] buffer = new byte[bufferLength];

                _messangerPacketReader.Read(buffer, 0, bufferLength);

                Span<byte> data = new(buffer);

                res = HyperSerializer<User>.Deserialize(data);
            }
            return res;
        }


        #endregion API - public Behavior





        #region LOGIC - internal Behavior



        /// <summary>
        /// Read the incomming packet. A packet is a specific message, sent by ServiceHub to handle different actions;
        /// <br />
        /// Прочитать входящий пакет. Пакет - это специальное сообщение, отправленное объектом "ServiceHub", чтобы структурировать обработку разных событий;
        /// </summary>
        public async Task ReadPacketsAsync()
        {
            await Task.Run(() =>
            {
                byte opCode = 0;
                while (true)
                {

                    if (cancellationTokenSource.IsCancellationRequested) break;

                    try
                    {
                        opCode = _messangerPacketReader.ReadByte();
                    }
                    catch (Exception e)
                    {
                        Disconnect();
                    }


                    switch (opCode)
                    {
                        case 0:
                            break;

                        case 1:
                            connectedEvent?.Invoke(); // client connection;
                            break;

                        case 5:
                            msgReceivedEvent?.Invoke(); // message recieved;
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
                            SendOutput.Invoke("Operation code out of [1,5,6,10]. This is a debug message.\nproject: ReversedClient, class: ClientTransmitter, method: ReadPacketsAsync.");
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
        public ClientTransmitter()
        {
            authorizationSocket = new TcpClient();
            messengerSocket = new TcpClient();
        }


        #endregion CONSTRUCTION - Object Lifetime



    }
}
