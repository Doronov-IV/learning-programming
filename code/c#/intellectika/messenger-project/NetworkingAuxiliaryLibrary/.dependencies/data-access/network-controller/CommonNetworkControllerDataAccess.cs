using NetworkingAuxiliaryLibrary.Dependencies.DataAccess;
using NetworkingAuxiliaryLibrary.Dependencies.Objects;

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
