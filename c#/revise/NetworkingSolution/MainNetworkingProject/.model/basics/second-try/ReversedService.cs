using System.Security.Cryptography.X509Certificates;

namespace MainNetworkingProject.Model.Basics
{
    public class ReversedService
    {


        #region PROPERTIES


        /// <summary>
        /// Предоставляет клиентские подключения для сетевых служб протокола TCP.
        /// </summary>
        TcpClient _client;

        public PacketReader PacketReader;

        /// <summary>
        /// событие подключения
        /// </summary>
        public event Action connectedEvent;

        /// <summary>
        /// событие получения сообщения
        /// </summary>
        public event Action msgReceivedEvent;

        /// <summary>
        /// событие отключения пользователя от сервера
        /// </summary>
        public event Action userDisconnectEvent;


        #endregion PROPERTIES


    }
}
