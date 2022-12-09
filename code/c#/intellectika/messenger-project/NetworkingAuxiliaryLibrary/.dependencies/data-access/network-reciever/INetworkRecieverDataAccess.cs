using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingAuxiliaryLibrary.Dependencies.DataAccess
{
    public interface INetworkRecieverDataAccess
    {
        public NetworkReciever GetNetworkRecieverData(TcpClient client);

    }
}
