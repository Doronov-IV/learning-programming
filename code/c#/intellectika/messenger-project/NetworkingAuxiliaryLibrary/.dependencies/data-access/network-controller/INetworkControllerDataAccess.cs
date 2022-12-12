using NetworkingAuxiliaryLibrary.Dependencies.Objects;
using System.Net.Sockets;

namespace NetworkingAuxiliaryLibrary.Dependencies.DataAccess
{
    public interface INetworkControllerDataAccess
    {
        public NetworkController GetNetworkControllerData(TcpListener listener);

    }
}
