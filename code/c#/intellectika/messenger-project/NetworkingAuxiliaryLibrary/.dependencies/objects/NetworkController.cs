using NetworkingAuxiliaryLibrary.Dependencies.BusinessLogic;
using System.Net.Sockets;
using Toolbox.Flags;
using Tools.Flags;


namespace NetworkingAuxiliaryLibrary.Dependencies.Objects
{
    public class NetworkController
    {
        public TcpListener UserListenner { get; set; }

        public List<NetworkRecieverBusinessLogic> UserList { get; set; }

        public TcpListener AuthorizerListenner { get; set; }

        public NetworkRecieverBusinessLogic AuthorizerReference { get; set; }

        public CustomProcessingStatus ProcessingStatus { get; set; }

    }
}
