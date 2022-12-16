using NetworkingAuxiliaryLibrary.Objects.Entities;
using NetworkingAuxiliaryLibrary.Objects;
using NetworkingAuxiliaryLibrary.Style.Messenger;
using NetworkingAuxiliaryLibrary.Net.Auxiliary.Processing;
using NetworkingAuxiliaryLibrary.Objects;
using NetworkingAuxiliaryLibrary.Style.Common;
using MessengerService.Model.Context;
using Tools.Flags;
using Newtonsoft.Json;
using System.Linq;
using Toolbox.Flags;
using Spectre.Console;
using System.IdentityModel.Tokens.Jwt;

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


        /// <summary>
        /// A list of current users;
        /// <br />
        /// Актуальный список пользователей;
        /// </summary>
        private List<ServiceReciever> userList = null!;


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




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   PROPERTIES   ↓                           ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// Is _service running;
        /// <br />
        /// Работает ли сервис;
        /// </summary>
        public CustomProcessingStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
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
            userListenner = new TcpListener(IPAddress.Parse("127.0.0.1"), 7333);
            userListenner.Start();

            // create basic references for reading clients
            PackageReader reader;
            ServiceReciever client = null;
            MessagePackage msg = null;

            Status.ToggleCompletion();

            while (true)
            {
                client = null;

                await Task.Run(() => client = new ServiceReciever(userListenner.AcceptTcpClient()));

                if (client is not null)
                {
                    client.ProcessTextMessageEvent += AddNewMessageToTheDb;
                    client.ProcessFileMessageEvent += BroadcastMessage;
                    client.UserDisconnected += BroadcastDisconnect;

                    reader = new(client.ClientSocket.GetStream());

                    await Task.Run(() => msg = reader.ReadMessage());

                    AnsiConsole.Write(new Markup(ConsoleServiceStyleCommon.GetUserConnection(msg.Message as string)));

                    if (msg is not null)
                    {
                        userList.Add(client);

                        CheckIncommingLogin(msg.Message as string);

                        var user = GetUserFromDatabaseByLogin(msg.Message as string);

                        client.CurrentUser = user;

                        SendUserInfo(client, user);

                        BroadcastConnection();

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

            authorizationServiceListenner = new(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7111));

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

            MessagePackage msg = null;

            try
            {
                while (true)
                {
                    msg = null;
                    try
                    {
                        if (authorizer != null && authorizer.ClientSocket.Connected)
                            if (reader is not null) // it is null
                            await Task.Run(() => msg = reader.ReadMessage());
                    }
                    catch { /* Notofication exception */}
                    if (msg != null)
                    {
                        CheckIncommingLogin(msg.Message as string);

                        AnsiConsole.Write(new Markup(ConsoleServiceStyle.GetLoginReceiptStyle(msg)));
                    }
                }
            }
            catch { }
        }


        /// <summary>
        /// Stop _service.
        /// <br />
        /// Остановить сервис.
        /// </summary>
        public void Stop()
        {
            userListenner?.Stop();
            authorizationServiceListenner?.Stop();

            userList.ForEach(u => u.ClientSocket?.Close());
            authorizationServiceListenner?.Stop();

            userList = new();
            Status.ToggleProcessing();
        }




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                           ↓   BROADCASTING   ↓                           ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 
        

        /// <summary>
        /// Broadcast a notification message to all users about the new user connection;
        /// <br />
        /// Транслировать уведомление для всех пользователей о подключении нового пользователя;
        /// </summary>
        public void BroadcastConnection()
        {
            var broadcastPacket = new PackageBuilder();
            foreach (var user in userList)
            {
                foreach (var usr in userList)
                {
                    var usrName = new TextMessagePackage(usr.CurrentUser.PublicId, "@All", usr.CurrentUser.CurrentNickname);
                    var usrUID = new TextMessagePackage(usr.CurrentUser.PublicId, "@All", usr.CurrentUser.PublicId);

                    broadcastPacket.WriteOpCode(1); // code '1' means new user has connected;
                    broadcastPacket.WriteMessage(usrName);
                    broadcastPacket.WriteMessage(usrUID);

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
        public void BroadcastMessage(MessagePackage package)
        {
            var msgPacket = new PackageBuilder();
            msgPacket.WriteOpCode(5);
            msgPacket.WritePackageLength(package);
            msgPacket.WriteMessage(package);
            foreach (var user in userList)
            {
                if (package.Reciever != "@All")
                {
                    if (user.CurrentUser.PublicId == package.Reciever || user.CurrentUser.PublicId == package.Sender)
                    {
                        user.ClientSocket.Client.Send(msgPacket.GetPacketBytes(), SocketFlags.Partial);
                    
                    }
                }
                else user.ClientSocket.Client.Send(msgPacket.GetPacketBytes(), SocketFlags.Partial);
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
        public void BroadcastDisconnect(User userData)
        {
            var disconnectedUser = userList.Where(x => x.CurrentUser.PublicId.Equals(userData.PublicId)).FirstOrDefault();
            userList.Remove(disconnectedUser);            // removing user;

            var broadcastPacket = new PackageBuilder();
            foreach (var user in userList)
            {
                broadcastPacket.WriteOpCode(10);    // on user disconnection, _service recieves the code-10 operation and broadcasts the "disconnect message";  
                broadcastPacket.WriteMessage(new TextMessagePackage(userData.PublicId, "@All", userData.PublicId)); // it also sends disconnected user id (not sure where that goes, mb viewmodel delegate) so we can pull it out from users
                user.ClientSocket.Client.Send(broadcastPacket.GetPacketBytes(), SocketFlags.Partial);
            }

            //BroadcastMessage(new TextMessagePackage(disconnectedUser.CurrentUID, "@All" ,$"{disconnectedUser.CurrentUserName} Disconnected!"));
        }


        /// <summary>
        /// Broadcast _service shutdown message to the users and authorizer _service.
        /// <br />
        /// Транслировать выключение сервиса пользователям и сервису авторизации.
        /// </summary>
        public void BroadcastShutdown()
        {
            PackageBuilder builder = new();
            builder.WriteOpCode(byte.MaxValue);
            foreach (var user in userList)
            {
                user.ClientSocket.Client.Send(builder.GetPacketBytes());
            }
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
        public void AddNewMessageToTheDb(MessagePackage package)
        {
            if (package is not null)
            {
                using (MessengerDatabaseContext context = new())
                {
                    Message newMessage = new();

                    newMessage.Contents = package.Message as string;
                    newMessage.Date = DateTime.Now.ToString("dd.MM.yy");
                    newMessage.Time = DateTime.Now.ToString("HH:mm:ss");

                    // check if db knows the sender
                    User newSender = new();
                    var existingSender = context.Users.FirstOrDefault(u => u.PublicId.Equals(package.Sender));
                    if (existingSender is null)
                    {
                        newSender.PublicId = package.Sender;
                        newSender.CurrentNickname = package.Sender;
                        newSender.MessageList = new();
                        newSender.ChatList = new();

                        context.Users.Add(newSender);
                    }
                    else newSender = existingSender;
                    newMessage.Author = newSender;

                    // check if db knows the reciever
                    User newReciever = new();
                    var existingReciever = context.Users.FirstOrDefault(u => u.PublicId.Equals(package.Reciever));
                    if (existingReciever is null)
                    {
                        newReciever.PublicId = package.Reciever;
                        newReciever.CurrentNickname = package.Reciever;
                        newReciever.MessageList = new();
                        newReciever.ChatList = new();

                        context.Users.Add(newReciever);
                    }
                    else newReciever = existingReciever;

                    // check if it isn't a new chat
                    Chat newChat = new();
                    if (package.Reciever != "@All")
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
                    newSender.ChatList.Add(newChat);
                    newReciever.ChatList.Add(newChat);
                    newChat.MessageList.Add(newMessage);
                    newMessage.Chat = newChat;

                    context.Messages.Add(newMessage);

                    context.SaveChanges();
                }
            }
        }


        /// <summary>
        /// Return user reference by searching with user login.
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


        #endregion API - public Behavior







        #region LOGIC - private Behavior



        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   DATA SYNC   ↓                            ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// Send user reference found to the provided reciever.
        /// <br />
        /// Выслать ссылку на пользователя указанному получателю.
        /// </summary>
        private void SendUserInfo(ServiceReciever reciever, User user)
        {
            string userJson = string.Empty;

            var userServerSideDto = UserParser.ParseToDTO(user);

            userJson = JsonConvert.SerializeObject(userServerSideDto);

            PackageBuilder builder = new();

            TextMessagePackage pack = new("Messenger", "Client", userJson);

            builder.WriteOpCode(12);

            builder.WriteMessage(pack);

            reciever.ClientSocket.Client.Send(builder.GetPacketBytes());
        }


        /// <summary>
        /// Check login provided in the table of users.
        /// <br />
        /// Проверить предоставленный логин в таблице пользователей.
        /// </summary>
        private void CheckIncommingLogin(string login)
        {
            bool isPresentFlag = false;
            using (MessengerDatabaseContext context = new())
            {
                foreach (var user in context.Users)
                {
                    if (user.Login.Equals(login))
                    {
                        isPresentFlag = true;
                        break;
                    }
                }

                if (!isPresentFlag)
                {
                    AddNewUserByLogin(login);
                }
            }
        }


        /// <summary>
        /// Add new instance to the table users with only a login.
        /// <br />
        /// Добавить новый экземпляр в таблицу пользователей, используя только логин.
        /// </summary>
        private void AddNewUserByLogin(string login)
        {
            using (MessengerDatabaseContext context = new())
            {
                User newUser = new();
                newUser.Id = context.Users.Count() + 1;
                newUser.Login = login;
                newUser.CurrentNickname = "User" + context.Users.Count();
                newUser.PublicId = "User" + context.Users.Count();

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
            userList = new List<ServiceReciever>();
            authorizer = null;
            Status = new();
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