using System.Net.Sockets;

namespace NetworkingAuxiliaryLibrary.Dependencies.DataAccess
{
    public class CommonNetworkRecieverDataAccess : INetworkRecieverDataAccess
    {
        public NetworkReciever GetNetworkRecieverData(TcpClient client)
        {
            return new NetworkReciever(client);
        }

    }
}
