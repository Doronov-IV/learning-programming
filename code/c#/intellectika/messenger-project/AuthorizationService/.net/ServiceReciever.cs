using AuthorizationServiceProject.Net;
using AuthorizationServiceProject.Style;
using NetworkingAuxiliaryLibrary.Objects.Entities;

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
        private UserDTO? _currentUser = null;


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
        public UserDTO? CurrentUser
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
                    var operationCode = reader.ReadByte();

                    switch (operationCode)
                    {
                        case 0:


                            var msg = reader.ReadMessage();
                            CurrentUser = ReadAuthorizationData(msg);
                            bool bRes = controller.TryAddNewUser(CurrentUser);
                            controller.SendClientResponse(this, bRes);
                            AnsiConsole.Write(new Markup($"{ConsoleServiceStyle.GetCurrentTime()} user has [underline]registered and connected[/] with login [green]\"{CurrentUser.Login}\"[/].\n"));

                            break;

                        case 1:


                            var signInMessage = reader.ReadMessage();
                            CurrentUser = ReadAuthorizationData(signInMessage);

                            bool bAuthorizationRes = controller.UserIsPresentInDatabase(CurrentUser);
                            if (bAuthorizationRes)
                            {
                                controller.SendClientResponse(this, bAuthorizationRes);
                                if (controller.TrySendLoginToService(this))
                                    AnsiConsole.Write(new Markup($"{ConsoleServiceStyle.GetCurrentTime()} user has [underline]connected[/] with login [green]\"{CurrentUser.Login}\"[/].\n"));
                            }
                            else controller.SendClientResponse(this, bAuthorizationRes);


                            break;
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.Write(new Markup($"{ConsoleServiceStyle.GetCurrentTime()} user [green]\"{CurrentUser.Login}\"[/] has [underline]disconnected[/].\n"));
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
            reader = new(ClientSocket.GetStream());
        }


        #endregion CONSTRUCTION


    }
}
