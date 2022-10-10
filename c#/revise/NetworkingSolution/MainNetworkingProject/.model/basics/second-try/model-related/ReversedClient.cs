namespace MainNetworkingProject.Model.Basics
{
    public class Client
    {
        /// <summary>
        /// Свойство: Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Свойство: User ID. Структура Guid представляет глобальный уникальный идентификатор (GUID).
        /// </summary>
        public Guid UID { get; set; }

        ServiceHub hub = new ServiceHub();

        /// <summary>
        /// Предоставляет клиентские подключения для сетевых служб протокола TCP.
        /// </summary>
        public TcpClient ClientSocket { get; set; }

        PacketReader _packetReader;


        public delegate void ServiceOutputDelegate(string sOutputMessage);

        public event ServiceOutputDelegate SendServiceOutput;



        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="client">Клиент</param>
        public Client(TcpClient client)
        {
            ClientSocket = client;

            //Генерация нового идентификатора пользователя при каждом создании экземляра клиента
            UID = Guid.NewGuid();

            _packetReader = new PacketReader(ClientSocket.GetStream());

            var opCode = _packetReader.ReadByte();

            //имени пользователя присваивается прочитанная строка
            UserName = _packetReader.ReadMessage();

            //Отображение в консоли времени подключения клиента и его имени пользователя
            SendServiceOutput.Invoke($"[{DateTime.Now}]: Client has connected with the userName: {UserName}");

            Task.Run(() => Process());
        }

        void Process()
        {
            while (true)
            {
                try
                {
                    var opCode = _packetReader.ReadByte();
                    switch (opCode)
                    {
                        case 5://case 5 так как мы ранее присвоили отправке сообщений код операции равный 5
                            var msg = _packetReader.ReadMessage();
                            SendServiceOutput.Invoke($"[{DateTime.Now}]: Message received! {msg}");
                            hub.BroadcastMessage($"[{DateTime.Now}]: [{UserName}]: {msg}");
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    SendServiceOutput.Invoke($"[{UID.ToString()}]: Disconnected!");//сообщение об отключении от сервера клиента
                    hub.BroadcastDisconnect(UID.ToString());
                    ClientSocket.Close();//Удаление клиента и закрытие подключения.  Close(): Удаляет данный экземпляр TcpClient и запрашивает закрытие базового подключения TCP.
                    break;

                }
            }
        }
    }
}
