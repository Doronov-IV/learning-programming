﻿using NetworkingAuxiliaryLibrary.Net.Auxiliary.Processing;
using ReversedClient.ViewModel.ClientStartupWindow;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using NetworkingAuxiliaryLibrary.Objects.Common;
using ReversedClient.ViewModel.ClientChatWindow;
using NetworkingAuxiliaryLibrary.Net.Config;
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



        /// <summary>
        /// An instance that serves as a flag for client windows changing.
        /// <br />
        /// Объект, который служит для перемещения между окнами клиента.
        /// </summary>
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();



        /// <summary>
        /// Provides client connections fo the authorization service;
        /// <br />
        /// Предоставляет клиенские подключения для сервиса авторизации;
        /// </summary>
        private TcpClient authorizationSocket;


        /// <summary>
        /// Provides client connections fo the messenger service;
        /// <br />
        /// Предоставляет клиенские подключения для сервиса месенжера;
        /// </summary>
        private TcpClient messengerSocket;



        ///<inheritdoc cref="AuthorizationPacketReader"/>
        private PackageReader _authorizationPacketReader;


        ///<inheritdoc cref="MessengerPacketReader"/>
        private PackageReader _messengerPacketReader;



        /// <summary>
        /// An object that provides aid in reading authorizer networkstream data.
        /// <br />
        /// Объект, который помогает читать данные из сетевого стрима авторизатора.
        /// </summary>
        public PackageReader AuthorizationPacketReader
        {
            get { return _authorizationPacketReader; }
            set
            {
                _authorizationPacketReader = value;
            }
        }


        /// <summary>
        /// An object that provides aid in reading messenger networkstream data.
        /// <br />
        /// Объект, который помогает читать данные из сетевого стрима мессенжера.
        /// </summary>
        public PackageReader MessengerPacketReader
        {
            get { return _messengerPacketReader; }
            set
            {
                _messengerPacketReader = value;
            }
        }





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
        /// When some user decides to delete their message.
        /// <br />
        /// Когда некий пользователь решает удалить своё сообщение.
        /// </summary>
        public event Action messageDeletionEvent;

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
        public async Task<bool> ConnectAndAuthorize(UserClientTechnicalDTO user)
        {
            //Если клиент не подключен
            if (!authorizationSocket.Connected)
            {
                try
                {
                    await authorizationSocket.ConnectAsync(NetworkConfigurator.ClientAuthorizerEndPoint);
                }
                catch 
                {
                    SendOutput.Invoke("Service is down. Please, concider connecting later.");
                    return false;
                }
            }

            _authorizationPacketReader = new(authorizationSocket.GetStream());

            var connectPacket = new PackageBuilder();

            var message = new JsonMessagePackage();
            message.Sender = user.Login;
            message.Reciever = "Service";
            message.Date = message.Time = "dnm";
            // ok, I've had enough, i'm getiing builder;

            connectPacket.WriteOpCode(1);

            connectPacket.WriteMessage(new TextMessagePackage($"{user.Login}", "Service", "dnm", "dnm", $"{user.Login}|{user.Password}"));

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
        public bool ConnectAndSendLoginToService(UserClientTechnicalDTO user)
        {
            try
            {
                messengerSocket.Connect(NetworkConfigurator.ClientMessengerEndPoint);
            }
            catch (Exception exSocketObsolete)
            {
                try 
                { 
                    messengerSocket = new();
                    messengerSocket.Connect(NetworkConfigurator.ClientMessengerEndPoint);
                }
                catch (Exception exServiceDown)
                {
                    SendOutput("Server is down. Concider connecting later.");
                }
            }

            if (messengerSocket.Connected)
            {
                MessengerPacketReader = new(messengerSocket.GetStream());

                var connectPacket = new PackageBuilder();

                connectPacket.WriteMessage(new TextMessagePackage($"{user.Login}", "Service", "dnm", "dnm", $"{user.Login}"));

                messengerSocket.Client.Send(connectPacket.GetPacketBytes());

                return true;
            }

            return false;
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


        public void SendJsonMessageToServer(JsonMessagePackage package)
        {
            var builder = new PackageBuilder();

            builder.WriteOpCode(5);

            builder.WriteJsonMessage(JsonConvert.SerializeObject(package));

            try
            {
                messengerSocket.Client.Send(builder.GetPacketBytes());
            }
            catch (Exception ex)
            {
                SendOutput.Invoke($"You haven't connected yet.\n\nException: {ex.Message}");
            }
        }



        /// <summary>
        /// Sign up new user based on user technical dto.
        /// <br />
        /// Зарегистрировать нового пользователя, основанного на техническом объекте.
        /// </summary>
        public bool RegisterNewUser(UserClientTechnicalDTO userData)
        {
            if (!authorizationSocket.Connected) authorizationSocket.Connect(NetworkConfigurator.ClientAuthorizerEndPoint);

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
            var code = _messengerPacketReader.ReadByte();
            if (code == 12)
            {
                var msg = _messengerPacketReader.ReadMessage();

                res = JsonConvert.DeserializeObject(msg.Message as string, type: typeof(UserServerSideDTO)) as UserServerSideDTO;
            }
            return res;
        }


        public void SendMessageDeletionToServer(MessagePackage pack)
        {
            var messagePacket = new PackageBuilder();
            messagePacket.WriteOpCode(6);
            messagePacket.WriteMessage(pack);
            try
            {
                messengerSocket.Client.Send(messagePacket.GetPacketBytes());
            }
            catch (Exception ex)
            {
                SendOutput.Invoke($"You haven't connected yet.\n\nException: {ex.Message}");
            }
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
            if (_messengerPacketReader is null) _messengerPacketReader = new(messengerSocket.GetStream());

            byte opCode = 77;
            while (true)
            {
                try
                {
                    await Task.Run(() => opCode = _messengerPacketReader.ReadByte());
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
                                messageDeletionEvent?.Invoke(); // file  recieved;
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
            var signUpMessage = new TextMessagePackage(sender: userData.PublicId, reciever: "Service", "does not matter", "does not matter", message: $"{userData.Login}|{userData.Password}");
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
