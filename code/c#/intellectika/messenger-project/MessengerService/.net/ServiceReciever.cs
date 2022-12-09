using NetworkingAuxiliaryLibrary.Objects;
using ReversedService.ViewModel.ServiceWindow;
using System.Net.Sockets;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using System;

namespace MessengerService.Datalink
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
        private PackageReader _packetReader;


        /// <summary>
        /// A 'ServiceController' object instance common for all users;
        /// <br />
        /// Объект класса "ServiceController", общий для всех клиентов;
        /// </summary>
        private static ServiceController staticController = null!;





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



        #endregion PROPERTIES - State of an Object




        #region API


        public delegate void MessageRecievedDelegate(MessagePackage recievedMessage);


        public event MessageRecievedDelegate ProcessTextMessageEvent;

        public event MessageRecievedDelegate ProcessFileMessageEvent;


        #endregion API




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

            while (!ServiceWindowViewModel.cancellationTokenSource.IsCancellationRequested)
            {

                try
                {
                    var opCode = _packetReader.ReadByte();
                    switch (opCode)
                    {
                        case 5:

                            var textMessage = _packetReader.ReadMessage();
                            ProcessTextMessageEvent.Invoke(textMessage);
                            SendOutput.Invoke($"[{DateTime.Now.ToString("dd.MM.yy HH:mm")}] user \"{CurrentUserName}\" says: {textMessage.Message as string}.");

                            break;


                        case 6:

                            // file messages are temporally unavailable in dev version;
                            //var fileMessage = _packetReader.ReadFile(UserName: CurrentUserName);
                            //ProcessFileMessageEvent.Invoke(fileMessage);
                            //SendOutput.Invoke($"[{DateTime.Now}] user \"{CurrentUserName}\" sent a file.");

                            break;


                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    if (staticController.Status.IsRunning)
                    {
                        SendOutput.Invoke($"User {CurrentUserName} (id = {CurrentUID.ToString()}) has disconnected!");
                        staticController.BroadcastDisconnect(CurrentUID.ToString());
                        ClientSocket.Close(); // if this block is invoked, we can see that the client has disconnected and then we need to invoke the disconnection procedure;
                        break;
                    }
                }
            }
        }



        /// <summary>
        /// Put 'Process' method in a separate task.
        /// <br />
        /// Положить метод "Process" в отдельный task.
        /// </summary>
        public async Task ProcessAsync()
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
        public ServiceReciever(TcpClient client)
        {
            ClientSocket = client;

            _packetReader = new PackageReader(ClientSocket.GetStream());
        }



        #endregion CONSTRUCTION - Object Lifetime


    }
}
