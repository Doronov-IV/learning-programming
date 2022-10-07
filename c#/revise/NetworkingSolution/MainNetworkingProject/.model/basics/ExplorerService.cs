﻿using System.Windows;

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

        public delegate void ServiceOutputDelegate(string sOutputMessage);

        public event ServiceOutputDelegate GetServiceOutput;


        #endregion PROPERTIES - forming the State of an Object






        #region API - public Contract


        /// <summary>
        /// Run server;
        /// <br />
        /// Запустить сервер;
        /// </summary>
        public void Run()
        {
            Server.Bind(iPEndPoint);

            Server.Listen(int.MaxValue);

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

                MessageBox.Show($"{currentServiceUser.UserName} enters the chat.","User entered your channel", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("User Disconnected.","Disconnect",MessageBoxButton.OK, MessageBoxImage.Stop);
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
                MessageBox.Show("User Disconnected.", "Disconnect", MessageBoxButton.OK, MessageBoxImage.Stop);
            }

            GetServiceOutput.Invoke($"{User.UserName} says: " + userMessageStringBuilder.ToString());
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
            iPEndPoint = new(IPAddress.Any, 8000);
            Server = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            ClientList = new();
        }


        #endregion CONSTRUCTION - object Lifetime Control


    }
}
