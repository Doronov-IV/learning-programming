﻿using NetworkingAuxiliaryLibrary.Objects.Entities;
using NetworkingAuxiliaryLibrary.Objects;
using NetworkingAuxiliaryLibrary.Style.Messenger;
using NetworkingAuxiliaryLibrary.Net.Auxiliary.Processing;
using NetworkingAuxiliaryLibrary.Net.Config;
using NetworkingAuxiliaryLibrary.Objects;
using NetworkingAuxiliaryLibrary.Style.Common;
using MessengerService.Model.Context;
using Tools.Flags;
using Newtonsoft.Json;
using System.Linq;
using Toolbox.Flags;
using Spectre.Console;
using System.IdentityModel.Tokens.Jwt;
using NetworkingAuxiliaryLibrary.Objects.Common;

namespace MessengerService.Datalink
{
    /// <summary>
    /// A controller for ServiceReciever instance to broadcast recieved data to every users.
    /// <br />
    /// Контроллер экземпляра "ServiceReciever" для рассылки полученных данных всем пользователям
    /// </summary>
    public class ServiceController
    {


        #region STATE - Fields and Properties



        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                               ↓   FIELDS   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        private ServiceBroadcaster broadcaster;


        /// <summary>
        /// The main TCP userListenner;
        /// <br />
        /// Основной слушатель;
        /// </summary>
        private TcpListener userListenner = null!;


        /// <summary>
        /// Authorizer _service reference.
        /// <br />
        /// Ссылка на сервис авторизации.
        /// </summary>
        private ServiceReciever authorizer;


        /// <summary>
        /// The main TCP userListenner;
        /// <br />
        /// Основной слушатель;
        /// </summary>
        private TcpListener authorizationServiceListenner = null!;


        /// <inheritdoc cref="Status"/>
        private CustomProcessingStatus _status;


        /// <inheritdoc cref="UserList"/>
        private List<ServiceReciever> _userList = null!;




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   PROPERTIES   ↓                           ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 



        /// <summary>
        /// A list of current users;
        /// <br />
        /// Актуальный список пользователей;
        /// </summary>
        public List<ServiceReciever> UserList
        {
            get 
            { 
                return _userList; 
            }
            set 
            {
                _userList = value; 
            }
        }


        /// <summary>
        /// Is _service running;
        /// <br />
        /// Работает ли сервис;
        /// </summary>
        public CustomProcessingStatus Status
        {
            get 
            { 
                return _status; 
            }
            set
            {
                _status = value;
            }
        }



        #endregion STATE - Fields and Properties






        #region API - public Behavior



        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   LISTENNING   ↓                           ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// Listen to clients in a loop async;
        /// <br />
        /// Асинхронно слушать клиентов в цикле;
        /// </summary>
        public async Task ListenClientsAsync()
        {
            // create and start listenner
            userListenner = new TcpListener(IPAddress.Any, NetworkConfigurator.ClientMessengerPort);
            userListenner.Start();

            // create basic references for reading clients
            PackageReader reader;
            ServiceReciever client = null;
            JsonMessagePackage msg = null;

            Status.ToggleCompletion();

            while (true)
            {
                client = null;

                await Task.Run(() => client = new ServiceReciever(userListenner.AcceptTcpClient()));

                if (client is not null)
                {
                    client.ProcessTextMessageEvent += AddNewMessageToTheDb;
                    client.ProcessTextMessageEvent += broadcaster.BroadcastMessage;

                    client.UserDisconnected += broadcaster.BroadcastDisconnect;

                    client.MessageDeletedEvent += DeleteMessageFromDb;
                    client.MessageDeletedEvent += broadcaster.BroadcastMessageDeletion;

                    reader = new(client.ClientSocket.GetStream());

                    await Task.Run(() => msg = JsonMessageFactory.GetUnserializedPackage(reader.ReadJsonMessage()));

                    AnsiConsole.Write(new Markup(ConsoleServiceStyleCommon.GetUserConnection(msg.Message as string)));

                    if (msg is not null)
                    {
                        _userList.Add(client);

                        var user = GetUserFromDatabaseByLogin(msg.Message as string);

                        client.CurrentUser = user;

                        SendUserInfo(client, user);

                        SendMembersList(client);

                        broadcaster.BroadcastConnection();

                        client.ProcessAsync();
                    }
                }
            }

            Status.ToggleProcessing();
        }


        /// <summary>
        /// Listen to authorization _service in a loop async;
        /// <br />
        /// Асинхронно слушать сервис авторизации в цикле;
        /// </summary>
        public async Task ListenAuthorizerAsync()
        {
            authorizer = null;

            authorizationServiceListenner = new(new IPEndPoint(IPAddress.Any, NetworkConfigurator.AuthorizerMessengerPort));

            authorizationServiceListenner.Start();

            PackageReader reader = default;

            while (authorizer is null)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        authorizer = new(authorizationServiceListenner.AcceptTcpClient());
                        reader = new(authorizer.ClientSocket.GetStream());
                    }
                    catch { /* Notification Exception */ }
                });
            }

            JsonMessagePackage msg = null;

            try
            {
                while (true)
                {
                    msg = null;
                    try
                    {
                        if (authorizer != null && authorizer.ClientSocket.Connected)
                            if (reader is not null) // it is null
                            await Task.Run(() => msg = JsonMessageFactory.GetUnserializedPackage(reader.ReadJsonMessage()));
                    }
                    catch { /* Notofication exception */}
                    if (msg != null)
                    {
                        CheckIncommingRegistrationData(msg);

                        var userData = SubstractUserData(msg);

                        AnsiConsole.Write(new Markup(ConsoleServiceStyle.GetLoginReceiptStyle(userData.PublicId)));
                    }
                }
            }
            catch { }
        }




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                          ↓   DB COMMUNICATION   ↓                        ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// Send broadcasted message to the database.
        /// <br />
        /// Отправить рассылаемое сообщение в б/д.
        /// </summary>
        /// <param name="package">
        /// Message package to be sent.
        /// <br />
        /// Пакет сообщения для отправки.
        /// </param>
        public void AddNewMessageToTheDb(IMessage package)
        {
            if (package is not null)
            {
                using (MessengerDatabaseContext context = new())
                {
                    Message newMessage = new();

                    newMessage.Contents = package.GetMessage() as string;
                    newMessage.Date = DateTime.Now.ToString("dd.MM.yy");
                    newMessage.Time = DateTime.Now.ToString("HH:mm:ss");

                    // check if db knows the sender
                    User newSender = new();
                    var existingSender = context.Users.FirstOrDefault(u => u.PublicId.Equals(package.GetSender()));
                    if (existingSender is null)
                    {
                        newSender.PublicId = package.GetSender();
                        newSender.CurrentNickname = package.GetSender();
                        newSender.MessageList = new();
                        newSender.ChatList = new();

                        context.Users.Add(newSender);
                    }
                    else newSender = existingSender;
                    newMessage.Author = newSender;

                    // check if db knows the reciever
                    User newReciever = new();
                    var existingReciever = context.Users.FirstOrDefault(u => u.PublicId.Equals(package.GetReciever()));
                    if (existingReciever is null)
                    {
                        newReciever.PublicId = package.GetReciever();
                        newReciever.CurrentNickname = package.GetReciever();
                        newReciever.MessageList = new();
                        newReciever.ChatList = new();

                        context.Users.Add(newReciever);
                    }
                    else newReciever = existingReciever;

                    // check if it isn't a new chat
                    Chat newChat = new();
                    if (package.GetReciever() != "@All")
                    {
                        var existingChat = context.Chats.FirstOrDefault(c => c.UserList.Contains(newSender) && c.UserList.Contains(newReciever) && c.UserList.Count == 2);
                        if (existingChat is null)
                        {
                            newChat.UserList.Add(newSender);
                            newChat.UserList.Add(newReciever);
                            context.Chats.Add(newChat);
                        }
                        else newChat = existingChat;
                    }
                    //newSender.ChatList.Add(newChat);
                    //newReciever.ChatList.Add(newChat);
                    newChat.MessageList.Add(newMessage);
                    newMessage.Chat = newChat;

                    context.Messages.Add(newMessage);

                    context.SaveChanges();
                }
            }
        }


        /// <summary>
        /// Return userData reference by searching with userData login.
        /// <br />
        /// Вернуть ссылку на пользователя в результате поиска по логину пользователя.
        /// </summary>
        public User GetUserFromDatabaseByLogin(string login)
        {
            User res = null;

            using (MessengerDatabaseContext context = new())
            {
                List<User> debugList = context.Users.Include(u => u.ChatList).Include(u => u.MessageList).ToList();

                foreach (var user in debugList)
                {
                    if (user.Login.Equals(login))
                    {
                        res = user;
                        break;
                    }
                }
            }
            return res;
        }



        public void DeleteMessageFromDb(IMessage message)
        {
            using (MessengerDatabaseContext context = new())
            {

                int deletedMessageAuthorId = 0;
                bool breakFlag = false;
                Message messageToDelete = null;

                foreach (var user in context.Users.Include(u => u.MessageList))
                {
                    foreach (var messageIntem in user.MessageList)
                    {
                        if (MessageParser.IsMessageIdenticalToAnotherOne(messageIntem, message))
                        {
                            messageToDelete = messageIntem;
                            deletedMessageAuthorId = user.Id;
                            breakFlag = true;
                            break;
                        }
                    }

                    if (breakFlag) break;
                }

                

                if (messageToDelete is not null)
                {
                    var chatContainingMessageToDelete = context.Chats.Select(c => c).Where(c => c.MessageList.Contains(messageToDelete)).FirstOrDefault();
                    chatContainingMessageToDelete.MessageList.Remove(messageToDelete);

                    var userThatHasThatMessage = context.Users.Where(u => u.MessageList.Contains(messageToDelete)).FirstOrDefault();
                    userThatHasThatMessage.MessageList.Remove(messageToDelete);

                    context.Messages.Remove(messageToDelete);

                    context.SaveChanges();
                }
                else throw new InvalidDataException("[Custom] Message queried for deletion was not found on the messenger database.");
            }
            
        }


        #endregion API - public Behavior







        #region LOGIC - private Behavior



        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   DATA SYNC   ↓                            ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// Send userData reference found to the provided reciever.
        /// <br />
        /// Выслать ссылку на пользователя указанному получателю.
        /// </summary>
        private void SendUserInfo(ServiceReciever reciever, User user)
        {
            string userJson = string.Empty;

            var userServerSideDto = UserParser.ParseToDTO(user);

            userJson = JsonConvert.SerializeObject(userServerSideDto);

            PackageBuilder builder = new();

            builder.WriteOpCode(12);

            builder.WriteJsonMessage(JsonMessageFactory.GetJsonMessageSimplified("Messenger", "Client", userJson));

            var debugSize = builder.GetPacketBytes().Length;

            reciever.ClientSocket.Client.Send(builder.GetPacketBytes());
        }

        private void SendMembersList(ServiceReciever reciever)
        {
            List<UserClientPublicDTO> result = new();

            using (MessengerDatabaseContext context = new())
            {
                foreach(var user in context.Users)
                {
                    UserClientPublicDTO dto = new();
                    dto.UserName = user.CurrentNickname;
                    dto.PublicId = user.PublicId;
                    result.Add(dto);
                }
            }

            var jsonResult = JsonConvert.SerializeObject(result);

            PackageBuilder builder = new();

            var json = JsonMessageFactory.GetJsonMessageSimplified("Messenger", "Client", jsonResult);

            builder.WriteJsonMessage(json);

            reciever.ClientSocket.Client.Send(builder.GetPacketBytes());
        }


        private void CheckIncommingRegistrationData(IMessage pack)
        {
            var userData = SubstractUserData(pack);

            bool isPresentFlag = false;
            using (MessengerDatabaseContext context = new())
            {
                foreach (var user in context.Users)
                {
                    if (userData.Login.Equals(user.Login))
                    {
                        isPresentFlag = true;
                        break;
                    }
                }

                if (!isPresentFlag)
                {
                    AddNewUserByLogin(userData.Login, userData.PublicId);
                }
            }
        }


        private UserClientTechnicalDTO SubstractUserData(IMessage pack)
        {
            UserClientTechnicalDTO userData = new();
            var queue = pack.GetMessage() as string;
            var strings = queue.Split("|");
            if (strings.Length == 2)
            {
                userData.Login = strings[0];
                userData.PublicId = strings[1];
            }
            else if (strings.Length == 1)
                userData.Login = userData.PublicId = strings[0];

            return userData;
        }


        /// <summary>
        /// Add new instance to the table users with only a login.
        /// <br />
        /// Добавить новый экземпляр в таблицу пользователей, используя только логин.
        /// </summary>
        private void AddNewUserByLogin(string login, string publicId)
        {
            using (MessengerDatabaseContext context = new())
            {
                User newUser = new();
                newUser.Id = context.Users.Count() + 1;
                newUser.Login = login;
                newUser.CurrentNickname = publicId;
                newUser.PublicId = publicId;

                context.Users.Add(newUser);
                context.SaveChanges();
            }
        }



        #endregion LOGIC - private Behavior






        #region CONSTRUCTION - Object Lifetime



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ServiceController()
        {
            _userList = new List<ServiceReciever>();
            authorizer = null;
            Status = new();
            broadcaster = new(this);
        }



        #endregion CONSTRUCTION - Object Lifetime


    }
}