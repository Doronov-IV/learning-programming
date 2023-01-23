using NetworkingAuxiliaryLibrary.Objects;
using NetworkingAuxiliaryLibrary.Style.Messenger;
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
using MessengerService.Model.Enums;

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



        #endregion STATE - Fields and Properties






        #region API - public Contract



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
            ServiceReciever client;
            JsonMessagePackage msg = null;

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
                    authorizer = new(authorizationServiceListenner.AcceptTcpClient());
                    reader = new(authorizer.ClientSocket.GetStream());
                });
            }

            JsonMessagePackage? message;

            try
            {
                while (true)
                {
                    message = default;
                    try
                    {
                        if (authorizer != null && authorizer.ClientSocket.Connected)
                            if (reader is not null) // it is null
                                await Task.Run(() => message = JsonMessageFactory.GetUnserializedPackage(reader.ReadJsonMessage()));
                    }
                    catch { /* Notofication exception */}
                    if (message is not null)
                    {
                        CheckIncommingRegistrationData(message);

                        var userData = SubstractUserData(message);

                        AnsiConsole.Write(new Markup(ConsoleServiceStyle.GetLoginReceiptStyle(userData.PublicId)));
                    }
                }
            }
            catch { }
        }




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                        ↓  API DB COMMUNICATION   ↓                       ↓    ///
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
                // creating and initializing it with content text, date and time;
                Message newMessage = new();
                newMessage.Contents = (string)package.GetMessage();
                newMessage.Date = package.GetDate();
                newMessage.Time = package.GetTime();
                using (MessengerDatabaseContext context = new())
                {

                    // retrieving a sender;
                    var messageSender = TryAddUser(package, context, UserRoles.Sender) ?? throw new InvalidDataException("[Manual] Server failed to substract a sender from the message package. Please, inspect the user processing method.");
                    newMessage.Author = messageSender;

                    // processing the chat;
                    var messageChat = TryAddChat(package, messageSender, context) ?? throw new InvalidDataException("[Manual] Server failed to process the chat for the new message. Please, inspect the chat processing method.");
                    messageChat.MessageList?.Add(newMessage);
                    newMessage.Chat = messageChat;

                    // adding message to the db

                    context.Messages.Add(newMessage);
                    context.SaveChanges();
                }
            }
        }



        /// <summary>
        /// Get User reference by searching with their login.
        /// <br />
        /// Получить ссылку на пользователя в результате поиска по его логину.
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



        /// <summary>
        /// Delete the recieved message from the Db.
        /// <br />
        /// Удалить полученное сообщение из б/д.
        /// </summary>
        public void DeleteMessageFromDb(IMessage message)
        {
            using (MessengerDatabaseContext context = new())
            {
                int deletedMessageAuthorId = default;
                bool breakFlag = default;
                Message? messageToDelete = default;

                var user = context.Users.Include(u => u.MessageList).FirstOrDefault(u => u.PublicId.Equals(message.GetSender()));
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

                if (messageToDelete is not null)
                {
                    Chat? chatContainingMessageToDelete = context.Chats.Select(c => c).Where(c => c.MessageList.Contains(messageToDelete)).FirstOrDefault();
                    chatContainingMessageToDelete.MessageList.Remove(messageToDelete);

                    var userThatHasThatMessage = context.Users.Where(u => u.MessageList.Contains(messageToDelete)).FirstOrDefault();
                    userThatHasThatMessage.MessageList.Remove(messageToDelete);

                    context.Messages.Remove(messageToDelete);
                    context.SaveChanges();
                }
                else throw new InvalidDataException("[Custom] Message queried for deletion was not found on the messenger database.");
            }
            
        }


        #endregion API - public Contract







        #region LOGIC - private Behavior



        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                       ↓  LOGIC DB COMMUNICATION   ↓                      ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// Get the user and add them to the db if they are not already present there.
        /// <br />
        /// Получить пользователя и добавить его в б/д, если он там ешё не числится.
        /// </summary>
        private User? TryAddUser(IMessage package, MessengerDatabaseContext context, UserRoles role)
        {
            User? newSender = null;
            var existingSender = context.Users.AsNoTracking().FirstOrDefault(u => u.PublicId.Equals(package.GetSender()));
            if (existingSender is null)
            {
                newSender = new(package, UserRoles.Sender);
                context.Users.Add(newSender);
                context.SaveChanges();
            }
            else newSender = existingSender;
            return newSender;
        }


        /// <summary>
        /// Get the chat of the message or create it, if it is not present at the db.
        /// <br />
        /// Получить чат сообщения, или создать его, если его ещё нет в б/д.
        /// </summary>
        private Chat? TryAddChat(IMessage package, User messageSender, MessengerDatabaseContext context)
        {
            Chat? messageChat = null;
            var messageReciever = TryAddUser(package, context ,UserRoles.Reciever);

            var existingChat = context.Chats.Include(c => c.UserList).FirstOrDefault(c => c.UserList.Contains(messageSender) && c.UserList.Contains(messageReciever));
            if (existingChat is null)
            {
                messageChat.UserList.Add(messageSender);
                messageChat.UserList.Add(messageReciever);
                context.Chats.Add(messageChat);
                context.SaveChanges();
            }
            else messageChat = existingChat;

            return messageChat;
        }



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



        /// <summary>
        /// Send the list of the members from the db to reciever.
        /// <br />
        /// Отправить список участников из б/д в клиент "reciever".
        /// </summary>
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



        /// <summary>
        /// Check the data of the new registered user sent from authorizer.
        /// <br />
        /// Проверить данные нового зарегистрированного пользователя от авторизатора.
        /// </summary>
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



        /// <summary>
        /// Split the data sent from client to retrieve login and password.
        /// <br />
        /// Разделить данные, отправленные клиентом, чтобы получить логин и пароль.
        /// </summary>
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
            broadcaster = new(this);
        }



        #endregion CONSTRUCTION - Object Lifetime


    }
}