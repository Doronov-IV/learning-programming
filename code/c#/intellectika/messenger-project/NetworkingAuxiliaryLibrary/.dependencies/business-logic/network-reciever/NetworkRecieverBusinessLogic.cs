using NetworkingAuxiliaryLibrary.Dependencies.DataAccess;
using NetworkingAuxiliaryLibrary.Dependencies.Objects;
using System.Net.Sockets;
namespace NetworkingAuxiliaryLibrary.Dependencies.BusinessLogic
{
    public class NetworkRecieverBusinessLogic : INetworkRecieverDataAccessDependency
    {

        #region DI/IOC


        private NetworkReciever reciever;


        private INetworkRecieverDataAccess _dependency;

        public INetworkRecieverDataAccess Dependency
        {
            get { return _dependency; }
            set { _dependency = value; }
        }

        public void SetDependency(INetworkRecieverDataAccess networkRecieverDataAccess)
        {
            _dependency = networkRecieverDataAccess;
        }


        #endregion DI/IOC



        #region LOGIC


        public void RecieveAsync()
        {
            // connect message

            while (true)
            {
                try
                {
                    var operationCode = reciever.PackageReader.ReadByte();

                    switch (operationCode)
                    {
                        case 5:

                            var textMessage = reciever.PackageReader.ReadMessage();
                            reciever.InvokeTextMessageEvents(textMessage); // controller will broadcast it as well as show output;
                            break;

                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    reciever.InvokeDisconnectionEvents();
                    reciever.ClientSocket.Close();
                    break;
                }
            }
        }


        #endregion LOGIC



        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public NetworkRecieverBusinessLogic(TcpClient client)
        {
            reciever = Dependency.GetNetworkRecieverData(client);
        }


        #endregion CONSTRUCTION



    }
}
