using Tools.Interfaces;

namespace MainNetworkingProject.Model.Basics
{

    /// <summary>
    /// A client reference used by server to manipulate with client data;
    /// <br />
    /// Ссылка на клиента, используемая сёрвером для обработки пользовательских данных;
    /// </summary>
    public class ReversedClient
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Свойство: User ID. Структура Guid представляет глобальный уникальный идентификатор (GUID).
        /// </summary>
        public Guid UID { get; set; }

        /// <summary>
        /// Предоставляет клиентские подключения для сетевых служб протокола TCP.
        /// </summary>
        public TcpClient ClientSocket { get; set; }

        PacketReader _packetReader;


        private static ServiceHub StaticServiceHub = null!;


        public delegate void PendOutputDelegate(string sOutputMessage);

        public event PendOutputDelegate SendOutput;



        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="client">Клиент</param>
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

        void Process()
        {
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
                    SendOutput.Invoke($"User {UserName} (id = {UID.ToString()}) has disconnected!");//сообщение об отключении от сервера клиента
                    StaticServiceHub.BroadcastDisconnect(UID.ToString());
                    ClientSocket.Close();//Удаление клиента и закрытие подключения.  Close(): Удаляет данный экземпляр TcpClient и запрашивает закрытие базового подключения TCP.
                    break;

                }
            }
        }
    }
}
