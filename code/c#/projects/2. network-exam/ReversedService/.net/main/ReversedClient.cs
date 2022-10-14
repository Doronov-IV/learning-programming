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
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// Свойство: User ID. Структура Guid представляет глобальный уникальный идентификатор (GUID).
        /// </summary>
        public Guid UID { get; set; }


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
        PacketReader _packetReader;


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
            SendOutput.Invoke($"[{DateTime.Now}] user has connected with the name: {UserName}");

            while (true)
            {
                try
                {
                    var opCode = _packetReader.ReadByte();
                    switch (opCode)
                    {
                        case 5:
                            var msg = _packetReader.ReadMessage();
                            SendOutput.Invoke($"[{DateTime.Now}] user {UserName} says: {msg}");
                            StaticServiceHub.BroadcastMessage($"[{DateTime.Now}] {UserName}: {msg}");
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    SendOutput.Invoke($"User {UserName} (id = {UID.ToString()}) has disconnected!");
                    StaticServiceHub.BroadcastDisconnect(UID.ToString());
                    ClientSocket.Close(); // if this block is invoked, we can see that the client has disconnected and then we need to invoke the disconnection procedure;
                    break;

                }
            }
        }



        #endregion LOGIC - private Behavior




        #region CONSTRUCTION - Object Lifetime


        /// <summary>
        /// Parametrized constructor;
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
            UID = Guid.NewGuid();

            _packetReader = new PacketReader(ClientSocket.GetStream());

            var opCode = _packetReader.ReadByte();

            //имени пользователя присваивается прочитанная строка
            UserName = _packetReader.ReadMessage();

            Task.Run(() => Process());
        }


        #endregion CONSTRUCTION - Object Lifetime


    }
}
