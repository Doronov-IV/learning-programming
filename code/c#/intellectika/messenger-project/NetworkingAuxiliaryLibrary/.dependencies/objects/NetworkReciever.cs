using NetworkingAuxiliaryLibrary.Packages;
using NetworkingAuxiliaryLibrary.Processing;
using System.Net.Sockets;

namespace NetworkingAuxiliaryLibrary.Dependencies.DataAccess
{
    public class NetworkReciever
    {
        public IUserDataAccess User { get; set; }

        public TcpClient ClientSocket { get; set; }
        
        public PackageReader PackageReader { get; set; }

        public delegate void MessageRecievedDelegate(MessagePackage recievedMessage);

        public event MessageRecievedDelegate ProcessTextMessage;
        public event MessageRecievedDelegate SendMessageOutput;

        public void InvokeTextMessageEvent(MessagePackage recievedMessage)
        {
            ProcessTextMessage?.Invoke(recievedMessage);
            SendMessageOutPut?.Invoke(recievedMessage);
        }

    }
}
