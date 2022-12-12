using NetworkingAuxiliaryLibrary.Dependencies.BusinessLogic;
using System.Net.Sockets;
using Toolbox.Flags;
using Tools.Flags;


namespace NetworkingAuxiliaryLibrary.Dependencies.Objects
{
    public class NetworkController
    {
        public TcpListener Listener { get; set; }

        public List<NetworkRecieverBusinessLogic> ConnectionList { get; set; }

        public NetworkRecieverBusinessLogic AuthorizerReference { get; set; }

        public CustomProcessingStatus ProcessingStatus { get; set; }



        /// <summary>
        /// Paranetrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public NetworkController(TcpListener listener)
        {
            Listener = listener;
            ConnectionList = new();
            Listener = TcpListenerDataAccessFactory.GetLocalMessengerUserListener();
            ProcessingStatus = new();
        }

    }
}
