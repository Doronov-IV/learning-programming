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


        private TcpListener serviceListener;


        private TcpClient messangerService;


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


        public void Run()
        {
            try
            {
                ServiceReciever newClient;

                clientListener.Start();

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
            if (checkResult) client.ClientSocket.Client.Send(BitConverter.GetBytes(1));
            else client.ClientSocket.Client.Send(BitConverter.GetBytes(0));
        }


        public void SendLoginToService(ServiceReciever user)
        {
            if (!messangerService.Connected) messangerService.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7111));

            if (messangerService.Connected)
            {
                TextMessagePackage package= new TextMessagePackage("authorizer", "messanger", $"{user.CurrentUser.Login}");
                PackageBuilder builder = new();
                builder.WriteMessage(package);
                messangerService.Client.Send(builder.GetPacketBytes());
            }
        }


        public void SendOutputMessage(string message)
        {
            SendServiceOutput.Invoke(message);
        }


        #endregion API



        #region LOGIC


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
        }


        #endregion CONSTRUCTION


    }
}
