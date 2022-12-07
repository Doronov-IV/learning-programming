using AuthorizationServiceProject.Net;
using Toolbox.Flags;
using AuthorizationServiceProject.Model.Context;
using AuthorizationServiceProject.Model.Entities;

namespace AuthorizationServiceProject.Net
{
    public class ServiceController
    {


        #region STATE


        private TcpListener clientListener;


        private TcpClient messangerService;


        /// <inheritdoc cref="UserList">
        private List<ServiceReciever> _userList;


        private IPEndPoint staticMessangerServiceEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7111);



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


        #endregion STATE




        #region API


        public void ListenToClients()
        {
            try
            {
                ServiceReciever newClient;

                clientListener.Start();

                try
                {
                    if (!messangerService.Connected) messangerService.Connect(staticMessangerServiceEndpoint);
                }
                catch (Exception ex)
                {
                    SendOutputMessage("Messenger service was down. Renewing connection.");
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
                SendOutputMessage("Exception: " + ex.Message);
            }
        }


        public bool CheckAuthorizationData(UserDTO pair)
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



        public bool AddNewUser(UserDTO user)
        {
            bool doesContain = CheckPresentUser(user);
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



        public bool CheckPresentUser(UserDTO user)
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



        public void SendClientResponse(ServiceReciever client, bool checkResult)
        {
            PackageBuilder builder = new PackageBuilder();
            TextMessagePackage package;

            if (checkResult) package = new ("Authorizer", "User", "Granted");
            else package = new ("Authorizer", "User", "Denied");

            builder.WriteMessage(package);

            client.ClientSocket.Client.Send(builder.GetPacketBytes());
        }


        public void SendOutputMessage(string message)
        {
            SendServiceOutput.Invoke(message);
        }


        public void SendLoginToService(ServiceReciever user)
        {
            try
            {
                if (!messangerService.Connected) messangerService.Connect(staticMessangerServiceEndpoint);
            }
            catch (Exception ex)
            {
                SendOutputMessage("\tMessenger service was down. Renewing connection....");
                try
                {
                    messangerService = new();
                    messangerService.Connect(staticMessangerServiceEndpoint);
                }
                catch (Exception inex)
                {
                    SendOutputMessage("\tSomething went wrong. Connection was not renewed. Shutting down.\n");
                    System.Environment.Exit(0);
                }
            }

            if (messangerService.Connected)
            {
                TextMessagePackage package = new TextMessagePackage("authorizer", "messanger", $"{user.CurrentUser.Login}");
                PackageBuilder builder = new();
                builder.WriteMessage(package);
                messangerService.Client.Send(builder.GetPacketBytes());
            }
        }


        #endregion API



        #region LOGIC


        private void TrySeedAdmins()
        {
            using (AuthorizationDatabaseContext context = new())
            {
                UserModel admin1 = new();
                UserModel admin2 = new();

                admin1.Login = "admin_alpha";
                admin2.Login = "admin_bravo";

                admin1.PasswordHash = admin2.PasswordHash = "admin";

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
            clientListener = new ( new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7222));
            messangerService = new();

            TrySeedAdmins();
        }


        #endregion CONSTRUCTION


    }
}
