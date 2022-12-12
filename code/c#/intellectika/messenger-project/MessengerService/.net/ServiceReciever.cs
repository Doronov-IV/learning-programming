using NetworkingAuxiliaryLibrary.Objects;
using System.Net.Sockets;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using System;
using Spectre.Console;
using NetworkingAuxiliaryLibrary.Style.Messenger;
using NetworkingAuxiliaryLibrary.Style.Common;

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



        public User CurrentUser { get; set; }



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



        #endregion PROPERTIES - State of an Object




        #region API


        public delegate void MessageRecievedDelegate(MessagePackage recievedMessage);


        public event MessageRecievedDelegate ProcessTextMessageEvent;

        public event MessageRecievedDelegate ProcessFileMessageEvent;

        public delegate void UserTypeDelegate(User userData);

        public event UserTypeDelegate UserDisconnected;


        #endregion API




        #region LOGIC - private Behavior



        /// <summary>
        /// Listen to clients;
        /// <br />
        /// Прослушивать клиентов;
        /// </summary>
        private void Process()
        {
            while (true)
            {

                try
                {
                    var opCode = _packetReader.ReadByte();
                    switch (opCode)
                    {
                        case 5:

                            var textMessage = _packetReader.ReadMessage();
                            ProcessTextMessageEvent.Invoke(textMessage);
                            AnsiConsole.Write(new Markup(ConsoleServiceStyle.GetClientMessageStyle(textMessage)));

                            break;


                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.Write(new Markup(ConsoleServiceStyleCommon.GetUserDisconnection(CurrentUser.Login)));
                    UserDisconnected.Invoke(CurrentUser);
                    ClientSocket.Close(); // if this block is invoked, we can see that the client has disconnected and then we need to invoke the disconnection procedure;
                    break;
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
            CurrentUser = new();
            _packetReader = new PackageReader(ClientSocket.GetStream());
        }



        #endregion CONSTRUCTION - Object Lifetime


    }
}
