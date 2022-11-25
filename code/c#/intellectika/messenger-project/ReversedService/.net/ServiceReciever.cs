using NetworkingAuxiliaryLibrary.ClientService;
using ReversedService.ViewModel.ServiceWindow;
using System.Net.Sockets;

namespace NetworkingAuxiliaryLibrary.ClientService
{

    /// <summary>
    /// A client reference used by server to manipulate with client data;
    /// <br />
    /// Ссылка на клиента, используемая сёрвером для обработки пользовательских данных;
    /// </summary>
    public class ServiceReciever
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
        public string CurrentUID { get; set; }



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
        /// A 'ServiceController' object instance common for all users;
        /// <br />
        /// Объект класса "ServiceController", общий для всех клиентов;
        /// </summary>
        private static ServiceController StaticServiceHub = null!;





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

                if (ServiceWindowViewModel.cancellationTokenSource.IsCancellationRequested) break;

                try
                {
                    var opCode = _packetReader.ReadByte();
                    switch (opCode)
                    {
                        case 5:
                            var msg = _packetReader.ReadMessage();
                            SendOutput.Invoke($"[{DateTime.Now}] user {CurrentUserName} says: {msg.Message as string}");
                            StaticServiceHub.BroadcastMessage(msg);
                            break;
                        case 6:
                            var fileMsg = _packetReader.ReadFile(UserName: CurrentUserName);
                            SendOutput.Invoke($"[{DateTime.Now}] user {CurrentUserName} sent a filePack.");
                            StaticServiceHub.BroadcastFileInParallel(fileMsg as FileMessagePackage);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (StaticServiceHub.IsRunning)
                    {
                        SendOutput.Invoke($"User {CurrentUserName} (id = {CurrentUID.ToString()}) has disconnected!");
                        StaticServiceHub.BroadcastDisconnect(CurrentUID.ToString());
                        ClientSocket.Close(); // if this block is invoked, we can see that the client has disconnected and then we need to invoke the disconnection procedure;
                        break;
                    }
                }
            }
        }



        private async Task ProcessAsync()
        {
            await Task.Run(() => Process());
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
        /// An instance of the 'ServiceController';
        /// <br />
        /// Экземпляр объекта класса "ServiceController";
        /// </param>
        public ServiceReciever(TcpClient client, ServiceController serviceHub)
        {
            ServiceReciever.StaticServiceHub = serviceHub;

            SendOutput += serviceHub.PassOutputMessage;

            ClientSocket = client;

            _packetReader = new PackageReader(ClientSocket.GetStream());

            var opCode = _packetReader.ReadByte();

            var msgRef = _packetReader.ReadMessage();

            //имени пользователя присваивается прочитанная строка
            CurrentUserName = msgRef.Message as string;
            CurrentUID = CurrentUserName;

            ProcessAsync();
        }


        #endregion CONSTRUCTION - Object Lifetime


    }
}
