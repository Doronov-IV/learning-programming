using System.Net;
using System.Net.Sockets;

namespace NetworkingAuxiliaryLibrary.Dependencies
{
    public static class TcpListenerFactory
    {

        /// <summary>
        /// Get localhost user-messenger listener (port 7333).
        /// <br />
        /// Получить пользователь-месенжер локалхост listener (порт 7333).
        /// </summary>
        public static TcpListener GetLocalMessengerUserListener()
        {
            return new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7333));
        }

        public static TcpListener GetAuthorizerMessengerListener()
        {
            return new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7111));
        }

        public static TcpListener GetLocalUserAuthorizerListener()
        {
            return new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7222));
        }

    }
}
