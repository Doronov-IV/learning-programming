using System.Net;
using System.Net.Sockets;

namespace NetworkingAuxiliaryLibrary.Dependencies
{
    public static class TcpListenerDataAccessFactory
    {


        /// <summary>
        /// Get localhost authorizer-messenger listener (port 7111).
        /// <br />
        /// Получить авторайзер-мессенжер локалхост listener (порт 7111).
        /// </summary>
        public static TcpListener GetAuthorizerMessengerListener()
        {
            return new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7111));
        }


        /// <summary>
        /// Get localhost user-authorizer listener (port 7222).
        /// <br />
        /// Получить пользователь-авторайзер локалхост listener (порт 7222).
        /// </summary>
        public static TcpListener GetLocalUserAuthorizerListener()
        {
            return new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7222));
        }


        /// <summary>
        /// Get localhost user-messenger listener (port 7333).
        /// <br />
        /// Получить пользователь-месенжер локалхост listener (порт 7333).
        /// </summary>
        public static TcpListener GetLocalMessengerUserListener()
        {
            return new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7333));
        }


    }
}
