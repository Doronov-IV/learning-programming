using ReversedService.Net.Auxiliary;

namespace ReversedService.Net.Main
{

    /// <summary>
    /// A client reference used by server to manipulate with client data;
    /// <br />
    /// Ссылка на клиента, используемая сёрвером для обработки пользовательских данных;
    /// </summary>
    public class ReversedClient
    {


        #region PROPERTIES - State of an Object



        /// <summary>
        /// The name of a user, aka login and nickname;
        /// <br />
        /// Имя пользователя, так же логин и никнейм;
        /// </summary>
        public string CurrentUserName { get; set; }


        /// <summary>
        /// Grants a global id (in form of four hexadecimal values);
        /// <br />
        /// Предоставляет глобальный идентификатор;
        /// </summary>
        public Guid CurrentUID { get; set; }



        /// <summary>
        /// Client's connection socket;
        /// <br />
        /// Сокет подключения клиента;
        /// </summary>
        public TcpClient ClientSocket { get; set; }


        /// <summary>
        /// An object that helps reading/writing messages;
        /// <br />
        /// Объект, который предоставляет помощь в чтении/записи сообщений;
        /// </summary>
        PackageReader _packetReader;


        /// <summary>
        /// A 'ServiceHub' object instance common for all users;
        /// <br />
        /// Объект класса "ServiceHub", общий для всех клиентов;
        /// </summary>
        private static ServiceHub StaticServiceHub = null!;





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

        /// <summary>
        /// @see public delegate void PendOutputDelegate(string sOutputMessage);
        /// </summary>
        public event PendOutputDelegate SendOutput;



        #endregion PROPERTIES - State of an Object




        #region LOGIC - private Behavior



        /// <summary>
        /// Listen to clients;
        /// <br />
        /// Прослушивать клиентов;
        /// </summary>
        private void Process()
        {
            // we invoke it here cause we cannot do this in the constructor while delegate object is still not initialized;
            SendOutput.Invoke($"[{DateTime.Now}] user has connected with the name: {CurrentUserName}");

            while (true)
            {
                try
                {
                    var opCode = _packetReader.ReadByte();
                    switch (opCode)
                    {
                        case 5:
                            var msg = _packetReader.ReadMessage();
                            SendOutput.Invoke($"[{DateTime.Now}] user {CurrentUserName} says: {msg}");
                            StaticServiceHub.BroadcastMessage($"[{DateTime.Now}] {CurrentUserName}: {msg}");
                            break;
                        case 6:
                            var file = _packetReader.ReadFile(UserName: CurrentUserName);
                            SendOutput.Invoke($"[{DateTime.Now}] user {CurrentUserName} sent an image.");
                            StaticServiceHub.BroadcastFile(file);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    SendOutput.Invoke($"User {CurrentUserName} (id = {CurrentUID.ToString()}) has disconnected!");
                    StaticServiceHub.BroadcastDisconnect(CurrentUID.ToString());
                    ClientSocket.Close(); // if this block is invoked, we can see that the client has disconnected and then we need to invoke the disconnection procedure;
                    break;

                }
            }
        }



        #endregion LOGIC - private Behavior




        #region CONSTRUCTION - Object Lifetime


        /// <summary>
        /// Parametrised constructor;
        /// <br />
        /// Конструктор с параметрами;
        /// </summary>
        /// <param name="client">
        /// Client's socket;
        /// <br />
        /// Сокет клиента;
        /// </param>
        /// <param name="serviceHub">
        /// An instance of the 'ServiceHub';
        /// <br />
        /// Экземпляр объекта класса "ServiceHub";
        /// </param>
        public ReversedClient(TcpClient client, ServiceHub serviceHub)
        {
            ReversedClient.StaticServiceHub = serviceHub;

            SendOutput += serviceHub.PassOutputMessage;

            ClientSocket = client;

            //Генерация нового идентификатора пользователя при каждом создании экземляра клиента
            CurrentUID = Guid.NewGuid();

            _packetReader = new PackageReader(ClientSocket.GetStream());

            var opCode = _packetReader.ReadByte();

            //имени пользователя присваивается прочитанная строка
            CurrentUserName = _packetReader.ReadMessage();

            Task.Run(() => Process());
        }


        #endregion CONSTRUCTION - Object Lifetime


    }
}
