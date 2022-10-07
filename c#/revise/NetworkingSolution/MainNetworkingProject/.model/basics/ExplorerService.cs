using System.Windows;

namespace MainNetworkingProject.Model.Basics
{
    public class ExplorerService
    {


        #region PROPERTIES - forming the State of an Object


        public static IPEndPoint iPEndPoint { get; set; } = null!;
        public static Socket Server { get; set; } = null!;
        public static Socket Client { get; set; } = null!;
        public static Socket? ClientSocket { get; private set; }
        public static List<ServiceUser> ClientList { get; set; } = null!;

        public delegate void ServiceOutputDelegate(ref string sOutputMessage);

        public event ServiceOutputDelegate GetServiceOutput;


        #endregion PROPERTIES - forming the State of an Object






        #region API - public Contract



        private void PrintDebugMessage()
        {
            string s = $"This shit will never work. Get back to the console.";
            GetServiceOutput.Invoke(ref s);
        }


        /// <summary>
        /// Run server;
        /// <br />
        /// Запустить сервер;
        /// </summary>
        public void Run()
        {
            PrintDebugMessage();

            Server.Bind(iPEndPoint);

            Server.Listen(int.MaxValue);

            string s = $"Service is listenning.";
            GetServiceOutput.Invoke(ref s);

            while (true)
            {
                Client = Server.Accept();

                byte[] ClientName = new byte[256];

                Client?.Receive(ClientName);

                // Wrap client binMessage into ServiceUser object;
                ServiceUser currentServiceUser = new() { UserName = Encoding.UTF8.GetString(ClientName), UserSocket = Client };

                Task.Run(() =>
                {
                    JoinChat(currentServiceUser);
                });

                ClientList.Add(currentServiceUser);

                s = $"{currentServiceUser.UserName} enters chat.";
                GetServiceOutput.Invoke(ref s);
            }
        }


        #endregion API - public Contract






        #region LOGIC - internal Behavior


        private void SendEveryone(string Message)
        {
            try
            {
                byte[] binMessage = Encoding.UTF8.GetBytes(Message);

                ClientList.ForEach(client => client.UserSocket?.Send(binMessage));
            }
            catch (Exception ex)
            {
                string s = "User Disconnected.";
                GetServiceOutput.Invoke(ref s);
            } 
        }



        private void JoinChat(ServiceUser User)
        {
            StringBuilder userMessageStringBuilder = new();

            try
            {
                byte[] binData = new byte[256];

                do
                {
                    User.UserSocket?.Receive(binData);

                    userMessageStringBuilder.Append(Encoding.UTF8.GetString(binData));

                } while (User.UserSocket?.Available > 0);
            }
            catch (Exception ex)
            {
                string s = "User Disconnected.";
                GetServiceOutput.Invoke(ref s);
            }
            string ss = $"{User.UserName} says: " + userMessageStringBuilder.ToString();
            GetServiceOutput.Invoke(ref ss);
            SendEveryone($"{User.UserName}: " + userMessageStringBuilder.ToString());
        }


        #endregion LOGIC - internal Behavior






        #region CONSTRUCTION - object Lifetime Control


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ExplorerService()
        {
            iPEndPoint = new(IPAddress.Any, 7999);
            Server = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            ClientList = new();
            
        }


        #endregion CONSTRUCTION - object Lifetime Control


    }
}
