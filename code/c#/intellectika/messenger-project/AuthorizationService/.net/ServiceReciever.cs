using AuthorizationServiceProject.Net;
using NetworkingAuxiliaryLibrary.Style.Authorizer;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using NetworkingAuxiliaryLibrary.Style.Common;

namespace AuthorizationServiceProject.Net
{
    /// <summary>
    /// A wrapper which attaches to a user TcpClient.
    /// <br />
    /// Обёртка, которая привязывается к TcpClient'у пользователя.
    /// </summary>
    public class ServiceReciever
    {


        #region STATE


        /// <summary>
        /// Deserializaing reader.
        /// <br />
        /// Десериализатор.
        /// </summary>
        private PackageReader? reader = null;


        /// <summary>
        /// Reference to the controller.
        /// <br />
        /// Ссылка на контроллер.
        /// </summary>
        private ServiceController? controller = null;


        /// <inheritdoc cref="CurrentUser"/>
        private UserClientSideDTO? _currentUser = null;


        /// <inheritdoc cref="ClientSocket"/>
        private TcpClient? _clientSocket = null;






        /// <summary>
        /// A reference to the TcpClient whose socket is attached to the client.
        /// <br />
        /// Ссылка на TcpClient, чей сокет привязан к клиенту.
        /// </summary>
        public TcpClient? ClientSocket
        {
            get { return _clientSocket; }
            set { _clientSocket = value; }
        }


        /// <summary>
        /// Attached user data.
        /// <br />
        /// Информация привязанного пользователя.
        /// </summary>
        public UserClientSideDTO? CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }



        #endregion STATE






        #region API



        /// <summary>
        /// Parse incomming text message into UserDTO object.
        /// <br />
        /// Спарсить входящее текстовое сообщение в объект типа UserDTO.
        /// </summary>
        public UserClientSideDTO ReadAuthorizationData(MessagePackage message)
        {
            var queue = message.Message as string;

            var strings = queue.Split("|");

            UserClientSideDTO pair = new();

            if (strings.Length == 2)
            {
                pair.Login = strings[0];
                pair.Password = strings[1];
            }
            else if (strings.Length == 1)
                pair.Login = pair.Password = strings[0];

            return new UserClientSideDTO(login: pair.Login, password: pair.Password);
        }



        /// <summary>
        /// Run processing in a new task.
        /// <br />
        /// Запустить обработку в новой задаче.
        /// </summary>
        public async void ProcessAsync()
        {
            await Task.Run(() => Process());
        }



        #endregion API







        #region LOGIC



        /// <summary>
        /// Process client input.
        /// <br />
        /// Обрабатывать инпут клиента.
        /// </summary>
        private void Process()
        {
            while (true)
            {
                try
                {
                    var operationCode = reader.ReadByte(); // read first byte from the stream;
                    switch (operationCode)
                    {
                        case 0:


                            var msg = reader.ReadMessage();
                            CurrentUser = ReadAuthorizationData(msg);
                            bool bSuccessfulRegistration = controller.TryAddNewUser(CurrentUser);
                            if (bSuccessfulRegistration)
                            {
                                AnsiConsole.Write(new Markup($"{ConsoleServiceStyle.GetUserRegistrationStyle(CurrentUser.Login)}"));
                            }
                            controller.SendClientResponse(this, bSuccessfulRegistration);

                            break;


                        case 1:


                            var signInMessage = reader.ReadMessage();
                            CurrentUser = ReadAuthorizationData(signInMessage);

                            bool bAuthorizationRes = controller.UserIsPresentInDatabase(CurrentUser);
                            if (bAuthorizationRes)
                            {
                                controller.SendClientResponse(this, bAuthorizationRes);

                                if (controller.TrySendLoginToService(this)) // if the other service is online and we have sent data to it;
                                    AnsiConsole.Write(new Markup($"{ConsoleServiceStyleCommon.GetUserConnection(CurrentUser.Login)}"));
                            }
                            else controller.SendClientResponse(this, bAuthorizationRes);

                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is IOException)
                    {
                        AnsiConsole.Write(new Markup($"{ConsoleServiceStyleCommon.GetUserDisconnection(CurrentUser.Login)}"));
                        break;
                    }
                    else
                    {
                        AnsiConsole.Write(new Markup("[red on white]Unexpected Exception.[/] " + ex.Message + "\n"));
                        break;
                    }
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
            reader = new(ClientSocket.GetStream());
        }


        #endregion CONSTRUCTION


    }
}
