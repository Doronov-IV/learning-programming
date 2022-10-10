using System.Windows;

namespace MainNetworkingProject.Model.Basics
{
    public class ExplorerService
    {


        #region PROPERTIES - forming the State of an Object


        public static IPEndPoint iPEndPoint { get; set; } = null!;
        public static TcpListener Server { get; set; } = null!;
        public static Socket Client { get; set; } = null!;
        public static List<ServiceUser> ClientList { get; set; } = null!;

        public delegate void ServiceOutputDelegate(string sOutputMessage);

        public event ServiceOutputDelegate SendServiceOutput;


        #endregion PROPERTIES - forming the State of an Object






        #region API - public Contract


        /// <summary>
        /// Run server;
        /// <br />
        /// Запустить сервер;
        /// </summary>
        public void Run()
        {
            Server.Start();

            SendServiceOutput.Invoke($"Service is listenning.");

            while (true)
            {
                /*var Client = Server.AcceptTcpClientAsync();

                using (NetworkStream stream = Client.GetStream())
                {
                    // Wrap client binMessage into ServiceUser object;
                    ServiceUser currentServiceUser = new() { UserName = Encoding.UTF8.GetString(ClientName), UserSocket = Client };

                    ClientList.Add(currentServiceUser);

                    SendServiceOutput.Invoke($"{currentServiceUser.UserName.Trim()} enters chat.");

                    Task.Run(() =>
                    {
                        JoinChat(currentServiceUser);
                    });
                }

                    SendFeedbackToUser($"You are connected.");*/
            }
        }



        public async Task Accept(TcpClient tcpClient)
        {
            /*
            using (NetworkStream stream = tcpClient.GetStream())
            {
                // Wrap client binMessage into ServiceUser object;
                ServiceUser currentServiceUser = new() { UserName = Encoding.UTF8.GetString(ClientName), UserSocket = Client };

                ClientList.Add(currentServiceUser);

                SendServiceOutput.Invoke($"{currentServiceUser.UserName.Trim()} enters chat.");

                Task.Run(() =>
                {
                    JoinChat(currentServiceUser);
                });
            }
            */
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
                SendServiceOutput.Invoke("User Disconnected.");
            } 
        }


        private void SendFeedbackToUser(string Message)
        {
            try
            {
                byte[] binMessage = Encoding.UTF8.GetBytes(Message);

                Client.Send(binMessage);
            }
            catch (Exception ex)
            {
                SendServiceOutput.Invoke("User Disconnected.");
            }
        }



        private void JoinChat(ServiceUser User)
        {
            StringBuilder userMessageStringBuilder = new();

            SendFeedbackToUser($"You've joined chat.");

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
                SendServiceOutput.Invoke("User Disconnected.");
            }

            SendServiceOutput.Invoke($"{User.UserName} says: " + userMessageStringBuilder.ToString().Trim());
            SendEveryone($"{User.UserName}: " + userMessageStringBuilder.ToString().Trim());
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
            iPEndPoint = new(IPAddress.Parse("10.61.140.35"), 7999);
            //Server = new();
            ClientList = new();
        }


        #endregion CONSTRUCTION - object Lifetime Control


    }
}
