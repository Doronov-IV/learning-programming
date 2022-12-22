﻿using AuthorizationServiceProject.Net;
using Toolbox.Flags;
using AuthorizationServiceProject.Model.Context;
using AuthorizationServiceProject.Model.Entities;
using NetworkingAuxiliaryLibrary.Style.Common;
using NetworkingAuxiliaryLibrary.Style.Authorizer;
using NetworkingAuxiliaryLibrary.Objects.Common;
using NetworkingAuxiliaryLibrary.Net.Config;

namespace AuthorizationServiceProject.Net
{
    /// <summary>
    /// An instance that assosiates all reciever as well as talks to the db.
    /// <br />
    /// Объект, который хранит всех ресиверов и общается с б/д.
    /// </summary>
    public class ServiceController
    {


        #region STATE



        /// <summary>
        /// An instance to accept incomming connections.
        /// <br />
        /// Сущность для приёма входящих подключений.
        /// </summary>
        private TcpListener clientListener;


        /// <summary>
        /// A messenger service socket.
        /// <br />
        /// Сокет сервиса сообщений.
        /// </summary>
        private TcpClient messengerServiceSocket;



        /// <inheritdoc cref="UserList">
        private List<ServiceReciever> _userList;



        /// <summary>
        /// A list of users.
        /// <br />
        /// Лист пользователей.
        /// </summary>
        public List<ServiceReciever> UserList
        { 
            get { return _userList; } 
            set
            {
                _userList = value;
            }
        }


        #endregion STATE





        #region API



        /// <summary>
        /// Run client incomming connection listenning loop.
        /// <br />
        /// Запустить цикл прослушивания входящий подключений.
        /// </summary>
        public void ListenToClientConnections()
        {
            try
            {
                AnsiConsole.Write(new Markup(ConsoleServiceStyleCommon.GetAuthorizerGreeting()));

                ServiceReciever newClient;

                clientListener.Start();

                try
                {
                    if (!messengerServiceSocket.Connected) messengerServiceSocket.Connect(NetworkConfigurator.AuthorizerMessengerEndPoint);
                }
                catch (Exception ex)
                {
                    AnsiConsole.Write(new Markup($"{ConsoleServiceStyleCommon.GetCurrentTime()} [yellow]Messenger service is currently down.[/]\n"));
                }

                while (true)
                {
                    newClient = new(clientListener.AcceptTcpClient(), this);

                    UserList.Add(newClient);

                    newClient.ProcessAsync();
                }
            }
            catch (Exception ex) 
            {
                AnsiConsole.Write(new Markup("[red on white]Unexpected Exception on client listenning.[/] " + ex.Message + "\n"));
            }
        }






        /// <summary>
        /// Check user authorization data.
        /// <br />
        /// Проверить данные пользователя.
        /// </summary>
        public bool CheckAuthorizationData(UserClientTechnicalDTO pair)
        {
            bool bRes = false;
            using (AuthorizationDatabaseContext context = new())
            {
                foreach (var el in context.Users)
                {
                    if (el.Login.Equals(pair.Login))
                    {
                        if (el.PasswordHash.Equals(pair.Password))
                        {
                            bRes = true;
                            break;
                        }
                    }
                }
            }
            return bRes;
        }


        public void SendClientResponse(ServiceReciever client, bool checkResult)
        {
            PackageBuilder builder = new PackageBuilder();
            TextMessagePackage package;

            // 'true' - access granted. else 'false'
            if (checkResult) package = new("Authorizer", "Client" ,"dnm","dnm", "Granted");
            else package = new("Authorizer", "Client","dnm","dnm", "Denied");

            builder.WriteMessage(package);

            client.ClientSocket.Client.Send(builder.GetPacketBytes());
        }


        /// <summary>
        /// Try send user login to the messenger service.
        /// <br />
        /// Попытаться отправить логин пользователя на сервис сообщений.
        /// </summary>
        public bool TrySendLoginToService(ServiceReciever user)
        {
            try
            {
                if (!messengerServiceSocket.Connected) messengerServiceSocket.Connect(NetworkConfigurator.AuthorizerMessengerEndPoint);
            }
            catch (Exception ex)
            {
                AnsiConsole.Write(new Markup($"{ConsoleServiceStyleCommon.GetCurrentTime()} [yellow]Messenger service was down. Trying to renew connection...[/]\n"));
                try
                {
                    messengerServiceSocket = new();
                    messengerServiceSocket.Connect(NetworkConfigurator.AuthorizerMessengerEndPoint);
                    AnsiConsole.Write(new Markup($"{ConsoleServiceStyleCommon.GetCurrentTime()} [yellow on green]Connection renewed.[/]\n"));
                }
                catch (Exception inex)
                {
                    AnsiConsole.Write(new Markup($"{ConsoleServiceStyleCommon.GetCurrentTime()} [white on red]Messenger service is still down. Client data was not sent.[/]\n"));
                    //System.Environment.Exit(0); // console application manual shutdown
                    return false;
                }
            }

            if (messengerServiceSocket.Connected)
            {
                TextMessagePackage package = new TextMessagePackage("Authorizer", "Messenger", "dnm", "dnm", $"{user.CurrentUser.Login}|{user.CurrentUser.PublicId}");
                PackageBuilder builder = new();
                builder.WriteMessage(package);
                messengerServiceSocket.Client.Send(builder.GetPacketBytes());
                return true;
            }

            return false;
        }







        /// <summary>
        /// Try add new user to the database.
        /// <br />
        /// Попытаться добавить нового пользователя в б/д.
        /// </summary>
        /// <returns>
        /// 'True' - if operation was successful, if user already present at the db - 'false'.
        /// <br />
        /// "True" - если процесс прошёл успешно, если пользователь уже есть в базе -  "false".
        /// </returns>
        public bool TryAddNewUser(UserClientTechnicalDTO user)
        {
            bool doesContain = UserIsPresentInDatabase(user);
            using (AuthorizationDatabaseContext context = new())
            {
                if (!doesContain)
                {
                    UserModel newUser = new();
                    newUser.Login = user.Login;
                    newUser.PasswordHash = user.Password;
                    context.Users.Add(newUser);

                    context.SaveChanges();
                }
            }
            return !doesContain;
        }



        /// <summary>
        /// Check if passed user information is contained in database and is correct.
        /// <br />
        /// Проверить, если переданная информация о пользователе лежит в б/д и записана корректно.
        /// </summary>
        /// <returns>
        /// 'True' - if data is correct, otherwise 'false'.
        /// <br />
        /// "True" - если данные совпадают, иначе "false".
        /// </returns>
        public bool UserIsPresentInDatabase(UserClientTechnicalDTO user)
        {
            bool doesContain = false;
            using (AuthorizationDatabaseContext context = new())
            {
                foreach (var item in context.Users)
                {
                    if (item.Login.Equals(user.Login))
                    {
                        doesContain = true;
                        break;
                    }
                }
            }
            return doesContain;
        }



        #endregion API






        #region LOGIC


        /// <summary>
        /// Try seed database with default admin accounts in case they have been cleared.
        /// <br />
        /// Попробовать засидить базу дефолтными аккаунтами админов на случай, если их очистили.
        /// </summary>
        private void TrySeedAdmins()
        {
            using (AuthorizationDatabaseContext context = new())
            {
                UserModel admin1 = new();
                UserModel admin2 = new();
                UserModel admin3 = new();
                UserModel admin4 = new();
                UserModel admin5 = new();

                admin1.Login = "admin_alpha";
                admin2.Login = "admin_bravo";
                admin3.Login = "admin_charlie";
                admin4.Login = "admin_delta";
                admin5.Login = "admin_echo";

                admin1.PasswordHash = admin2.PasswordHash = admin3.PasswordHash = admin4.PasswordHash = admin5.PasswordHash = "admin";

                foreach (var user in context.Users)
                {
                    if (user.Login.Equals(admin1.Login))
                    {
                        context.Dispose();
                        return;
                    }
                }

                context.Users.Add(admin1);
                context.Users.Add(admin2);
                context.Users.Add(admin3);
                context.Users.Add(admin4);
                context.Users.Add(admin5);

                context.SaveChanges();
            }
        }


        #endregion LOGIC






        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ServiceController()
        {
            _userList = new();
            clientListener = new ( new IPEndPoint(IPAddress.Any, NetworkConfigurator.ClientAuthorizerPort));
            messengerServiceSocket = new();

            //TrySeedAdmins();
        }


        #endregion CONSTRUCTION


    }
}
