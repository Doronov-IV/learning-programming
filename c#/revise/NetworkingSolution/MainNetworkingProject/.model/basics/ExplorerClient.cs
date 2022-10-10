using System.Collections.ObjectModel;
using System.Windows.Markup;
using static MainNetworkingProject.ViewModel.ClientWindow.ClientWindowViewModel;

namespace MainNetworkingProject.Model.Basics
{
    public class ExplorerClient
    {


        #region PROPERTIES




        public static IPEndPoint iPEndPoint { get; set; } = null!;
        public static Socket Server { get; set; } = null!;




        private ObservableCollection<string> _ChatLog;

        public ObservableCollection<string> ChatLog
        {
            get { return _ChatLog; }
            set
            {
                _ChatLog = value;
            }
        }



        private string _UserMessage;

        public string UserMessage
        {
            get { return _UserMessage; }
            set
            {
                UserMessage = value;
            }
        }

        private ServiceUser _User;


        public ServiceUser User
        {
            get { return _User; }
            set
            {
                _User = value;
            }
        }


        #endregion PROPERTIES



        public void SendMessage()
        {
            if (UserMessage != "" && null != UserMessage)
            {
                byte[] binMessage = Encoding.Unicode.GetBytes(UserMessage);
                User.UserSocket?.Send(binMessage);
            }
        }



        public void GetUserNameInput(string sUserName)
        {
            User.UserName = sUserName;
        }


        public void ReadMessage()
        {
            while (Server.Connected)
            {
                byte[] binMessage = new byte[256];
                StringBuilder MessageStringBuilder = new StringBuilder();
                int binMessageLength = 0;

                do
                {
                    binMessageLength = Server.Receive(binMessage, binMessage.Length, 0);
                    MessageStringBuilder.Append(Encoding.Unicode.GetString(binMessage, 0, binMessageLength));
                }
                while (Server.Available > 0);

                ChatLog.Add(MessageStringBuilder.ToString());
            }
        }



        public void Run()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    ReadMessage();
                }
            });
        }





        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ExplorerClient()
        {
            iPEndPoint = new(IPAddress.Parse("127.0.0.1"), 7999);
            _User = new() { UserSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP), UserName = "user_unknown" };
        }


        #endregion CONSTRUCTION

    }
}
