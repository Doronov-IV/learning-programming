using AuthorizationServiceProject.Net;

namespace AuthorizationServiceProject.Net
{
    public class ServiceReciever
    {


        #region STATE



        private UserDTO? _currentUser = null;


        private PackageReader? reader = null;


        private ServiceController? controller = null;


        private TcpClient? _clientSocket = null;




        public TcpClient? ClientSocket
        {
            get { return _clientSocket; }
            set { _clientSocket = value; }
        }


        public UserDTO? CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }




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



        #endregion STATE




        #region API

        public UserDTO ReadAuthorizationData(MessagePackage message)
        {
            var queue = message.Message as string;

            var strings = queue.Split("|");

            UserDTO pair = new();

            if (strings.Length == 2)
            {
                pair.Login = strings[0];
                pair.Password = strings[1];
            }
            else if (strings.Length == 1)
                pair.Login = pair.Password = strings[0];

            return new UserDTO(login: pair.Login, password: pair.Password);
        }



        public async void ProcessAsync()
        {
            await Task.Run(() => Process());
        }



        #endregion API



        #region LOGIC


        private void Process()
        {
            while (true)
            {
                try
                {
                    var operationCode = reader.ReadByte();

                    switch (operationCode)
                    {
                        case 0:


                            var msg = reader.ReadMessage();
                            CurrentUser = ReadAuthorizationData(msg);
                            bool bRes = controller.AddNewUser(CurrentUser);
                            controller.SendClientResponse(this, bRes);
                            SendOutput.Invoke($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}] user has registered and connected with login \"{CurrentUser.Login}\".");

                            break;

                        case 1:


                            var signInMessage = reader.ReadMessage();
                            CurrentUser = ReadAuthorizationData(signInMessage);
                            bool bAuthorizationRes = controller.CheckPresentUser(CurrentUser);
                            if (bAuthorizationRes)
                            {
                                controller.SendClientResponse(this, bAuthorizationRes);
                                controller.SendLoginToService(this);
                                SendOutput.Invoke($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}] user has connected with login \"{CurrentUser.Login}\".");
                            }
                            else controller.SendClientResponse(this, bAuthorizationRes);


                            break;
                    }
                }
                catch (Exception ex)
                {
                    SendOutput.Invoke($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}] user \"{CurrentUser.Login}\" has disconnected.");
                    break;
                }
            }
        }


        #endregion LOGIC




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ServiceReciever()
        {
            _currentUser = new();
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        /// <param name="controller">
        /// Controller instance.
        /// <br />
        /// Экземпляр контроллера. 
        /// </param>
        public ServiceReciever(TcpClient client, ServiceController controller) : this()
        {
            this.controller = controller;
            _clientSocket = client;
            SendOutput += controller.SendOutputMessage;
            reader = new(ClientSocket.GetStream());
        }


        #endregion CONSTRUCTION


    }
}
