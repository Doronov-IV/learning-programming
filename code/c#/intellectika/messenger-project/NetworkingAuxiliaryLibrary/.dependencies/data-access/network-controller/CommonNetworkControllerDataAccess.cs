using NetworkingAuxiliaryLibrary.Dependencies.DataAccess;
using NetworkingAuxiliaryLibrary.Dependencies.Objects;
using System.Net.Sockets;

namespace NetworkingAuxiliaryLibrary.Dependencies.DataAccess
{
    public class CommonNetworkControllerDataAccess : INetworkControllerDataAccess
    {
        public NetworkController GetNetworkControllerData(TcpListener listener)
        {
            return new NetworkController(listener);
        }
    }
}
