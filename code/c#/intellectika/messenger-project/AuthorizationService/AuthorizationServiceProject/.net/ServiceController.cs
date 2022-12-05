using AuthorizationServiceProject.Net;
using Toolbox.Flags;

namespace AuthorizationServiceProject.net
{
    public class ServiceController
    {


        #region STATE


        private TcpListener listener;


        /// <inheritdoc cref="UserList">
        private List<ServiceReciever> _userList;



        /// <summary>
        /// A list of users.
        /// <br />
        /// Лист пользователей.
        /// </summary>
        public List<ServiceReciever> UserList
        { 
            get { return _userList; } 
            set
            {
                _userList = value;
            }
        }




        /// <summary>
        /// A delegate for transeffring output to other objects;
        /// <br />
        /// Делегат для передачи аутпута другим объектам;
        /// </summary>
        /// <param name="sOutputMessage">
        /// A message that we want to see somewhere (In this case, in a server console);
        /// <br />
        /// Сообщение, которое мы хотим где-то увидеть (в данном случае, в консоли сервера);
        /// </param>
        public delegate void ServiceOutputDelegate(string sOutputMessage);


        /// <inheritdoc cref="ServiceOutputDelegate"/>
        public event ServiceOutputDelegate SendServiceOutput;


        #endregion STATE




        #region API


        public void Run()
        {
            try
            {
                ServiceReciever newClient;

                while (true)
                {
                    newClient = new(listener.AcceptTcpClient(), this);

                    TextMessagePackage response = new();

                    var res = newClient.ReadAuthorizationData();
                }
            }
            catch { }
        }


        #endregion API



        #region LOGIC
        #endregion LOGIC



        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ServiceController()
        {
            _userList = new();
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
        }


        #endregion CONSTRUCTION


    }
}
