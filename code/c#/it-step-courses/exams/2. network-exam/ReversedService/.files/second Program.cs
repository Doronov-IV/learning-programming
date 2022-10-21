using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClassWordUDPSocket
{
    public class Program
    {
        private static int _myProt;

        private static int _remotePort;

        private static Socket _listener;

        static void Main(string[] args)
        {
            Console.WriteLine("Press _myPort");
            _myProt = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Press _remotePort");
            _remotePort = Int32.Parse(Console.ReadLine());

            try
            {
                _listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                Task.Run(Listen);

                while (true)
                {
                    Console.WriteLine("Enter message");
                    byte[] data = Encoding.UTF8.GetBytes(Console.ReadLine());

                    EndPoint remoteEndPort = new IPEndPoint(IPAddress.Parse("192.168.88.208"), _remotePort);

                    _listener.SendTo(data, remoteEndPort);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Listen()
        {
            try
            {

                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.88.196"), _myProt);

                _listener.Bind(iPEndPoint);

                while (true)
                {
                    var sb = new StringBuilder();

                    int bytes = 0;

                    byte[] data = new byte[256];

                    EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

                    do
                    {

                        bytes = _listener.ReceiveFrom(data, ref endPoint);

                        sb.Append(Encoding.UTF8.GetString(data));

                    } while (_listener.Available > 0);

                    Console.WriteLine(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}