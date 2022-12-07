using ReversedService.ViewModel.ServiceWindow;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using ReversedService.Model.Context;
using NetworkingAuxiliaryLibrary.Objects;
using Tools.Flags;
using Hyper;
using System.Windows.Interop;
using System.Linq;
using Toolbox.Flags;

namespace NetworkingAuxiliaryLibrary.ClientService
{
    /// <summary>
    /// A controller for ServiceReciever instance to broadcast recieved data to every users.
    /// <br />
    /// Контроллер экземпляра "ServiceReciever" для рассылки полученных данных всем пользователям
    /// </summary>
    public class ServiceController : INotifyPropertyChanged
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




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                               ↓   OUTPUT   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// A delegate for transeffring output to other objects;
        /// <br />
        /// Делегат для передачи аутпута другим объектам;
        /// </summary>
        /// <param name="sOutputMessage">
        /// A message that we want to see somewhere (In this case, in a server console);
        /// <br />
        /// Сообщение, которое мы хотим где-то увидеть (в данном случае, в консоли сервера);
        /// </param>
        public delegate void ServiceOutputDelegate(string sOutputMessage);


        /// <inheritdoc cref="ServiceOutputDelegate"/>
        public event ServiceOutputDelegate SendServiceOutput;



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

            // check for cancellation token state
            if (ServiceWindowViewModel.cancellationTokenSource.IsCancellationRequested)
                ServiceWindowViewModel.cancellationTokenSource = new();

            // create basic references for reading clients
            PackageReader reader;
            ServiceReciever client = null;
            MessagePackage msg = null;

            Status.ToggleCompletion();

            while (!ServiceWindowViewModel.cancellationTokenSource.IsCancellationRequested)
            {
                client = null;

                await Task.Run(() => 
                {
                    try
                    {
                        client = new ServiceReciever(userListenner.AcceptTcpClient(), this);
                    }
                    catch { /* Notification Exception */ }
                });

                if (client is not null)
                {
                    reader = new(client.ClientSocket.GetStream());

                    await Task.Run(() => { msg = reader.ReadMessage(); });

                    SendServiceOutput.Invoke($"Login \"{msg.Message as string}\" has connected.");

                    if (msg is not null)
                    {
                        userList.Add(client);

                        var user = GetUserFromDatabaseByLogin(msg.Message as string);

                        client.CurrentUserName = user.CurrentNickname;
                        client.CurrentUID = user.PublicId;

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
            authorizationServiceListenner = new(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7111));

            authorizationServiceListenner.Start();

            if (ServiceWindowViewModel.cancellationTokenSource.IsCancellationRequested)
                ServiceWindowViewModel.cancellationTokenSource = new();

            PackageReader reader = default;

            while (!ServiceWindowViewModel.cancellationTokenSource.IsCancellationRequested && authorizer is null)
            {
                await Task.Run(() =>
                {
                    try
                    {
                        authorizer = new(authorizationServiceListenner.AcceptTcpClient(), this);
                        reader = new(authorizer.ClientSocket.GetStream());
                    }
                    catch { /* Notification Exception */ }
                });
            }

            string msg = string.Empty;

            try
            {
                while (!ServiceWindowViewModel.cancellationTokenSource.IsCancellationRequested)
                {
                    msg = null;
                    try
                    {
                        if (authorizer != null && authorizer.ClientSocket.Connected)
                            if (reader is not null)
                            await Task.Run(() => msg = reader.ReadMessage().Message as string);
                    }
                    catch { /* Notofication exception */}
                    if (msg != null)
                    {
                        CheckIncommingLogin(msg);

                        SendServiceOutput.Invoke($"Login \"{msg}\" read.");
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
                    var usrName = new TextMessagePackage(usr.CurrentUID, "@All", usr.CurrentUserName);
                    var usrUID = new TextMessagePackage(usr.CurrentUID, "@All", usr.CurrentUID);

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
                    if (user.CurrentUID == package.Reciever || user.CurrentUID == package.Sender)
                    {
                        user.ClientSocket.Client.Send(msgPacket.GetPacketBytes(), SocketFlags.Partial);
                    
                    }
                }
                else user.ClientSocket.Client.Send(msgPacket.GetPacketBytes(), SocketFlags.Partial);
            }
        }


        // broadcasting file;
        /*
        /// <summary>
        /// Send file to all clients.
        /// <br />
        /// Разослать файл всем клиентам.
        /// </summary>
        /// <param name="info">
        /// Broadcasted file.
        /// <br />
        /// Транслируемый файл.
        /// </param>
        /// <param name="SenderId">
        /// The id of the newReciever-user.
        /// <br />
        /// Идентификатор отправителя.
        /// </param>
        public void BroadcastFileInParallel(FileMessagePackage filePackage)
        {
            var msgPacket = new PackageBuilder();
            msgPacket.WriteOpCode(6);
            msgPacket.WriteMessage(filePackage);

            var bytes = msgPacket.GetPacketBytes();
            const int bufferSize = 4096;
            byte[] buffer;
            Parallel.ForEach(userList, (user) =>
            {
                if (user.CurrentUID != filePackage.Sender && user.CurrentUID == filePackage.Reciever)
                {
                    if (bytes.Length > bufferSize)
                    {
                        using (MemoryStream stream = new(bytes))
                        {
                            while (stream.Position != stream.Length)
                            {
                                if (stream.Length - stream.Position >= bufferSize)
                                    buffer = new byte[bufferSize];
                                else
                                {
                                    buffer = new byte[stream.Length - stream.Position];
                                }
                                stream.Read(buffer, 0, buffer.Length);
                                user.ClientSocket.Client.Send(buffer, SocketFlags.Partial);
                                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
                            }
                        }
                    }
                    else user.ClientSocket.Client.Send(msgPacket.GetPacketBytes(), SocketFlags.Partial);
                }
            });
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
        }
        */


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
        public void BroadcastDisconnect(string uid)
        {
            var disconnectedUser = userList.Where(x => x.CurrentUID.ToString() == uid).FirstOrDefault();
            userList.Remove(disconnectedUser);            // removing user;

            var broadcastPacket = new PackageBuilder();
            foreach (var user in userList)
            {
                broadcastPacket.WriteOpCode(10);    // on user disconnection, _service recieves the code-10 operation and broadcasts the "disconnect message";  
                broadcastPacket.WriteMessage(new TextMessagePackage(uid, "@All", uid)); // it also sends disconnected user id (not sure where that goes, mb viewmodel delegate) so we can pull it out from users
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
                    newMessage.Date = DateTime.Now.Date.ToString();
                    newMessage.Time = DateTime.Now.TimeOfDay.ToString();

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
                foreach (var user in context.Users)
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




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                               ↓   OUTPUT   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 
        

        /// <summary>
        /// Pass the message out to another object that might have the ability to output this message.
        /// <br />
        /// Передать сообщение другому объекту, который может иметь возможность вывести его куда-нибудь.
        /// </summary>
        /// <param name="sMessage">
        /// Message text.
        /// <br />
        /// Текст сообщения.
        /// </param>
        public void PassOutputMessage(string sMessage)
        {
            SendServiceOutput.Invoke(sMessage);
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
            Span<byte> bytes = new();

            bytes = HyperSerializer<User>.Serialize(user);

            PackageBuilder builder = new();

            builder.WriteOpCode(12);

            builder.WriteByteSpan(bytes);

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