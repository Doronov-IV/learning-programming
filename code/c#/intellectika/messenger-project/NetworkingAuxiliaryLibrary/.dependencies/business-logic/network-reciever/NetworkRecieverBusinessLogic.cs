using NetworkingAuxiliaryLibrary.Dependencies.DataAccess;
using NetworkingAuxiliaryLibrary.Dependencies.Objects;
using System.Net.Sockets;
namespace NetworkingAuxiliaryLibrary.Dependencies.BusinessLogic
{
    public class NetworkRecieverBusinessLogic : INetworkRecieverDataAccessDependency
    {


        #region DI/IoC


        private NetworkReciever tempRecieverReference;


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


        #endregion DI/IoC





        public async Task RecieveAsync()
        {
            // connect message
            byte operationCode = 77;
            while (true)
            {
                try
                {
                    await Task.Run(() => operationCode = tempRecieverReference.PackageReader.ReadByte());

                    if (operationCode != 77)
                    {
                        switch (operationCode)
                        {
                            case 0:
                                
                                break;

                            case 1:

                                string userNameMessage = tempRecieverReference.PackageReader.ReadMessage().Message as string;
                                string userPublicId = tempRecieverReference.PackageReader.ReadMessage().Message as string;

                                var user = tempRecieverReference.User.GetPublicUserData(userNameMessage, userPublicId);



                                break;

                            case 5:

                                var textMessage = tempRecieverReference.PackageReader.ReadMessage();
                                tempRecieverReference.InvokeTextMessageEvents(textMessage); // controller will broadcast it as well as show output;
                                break;

                            default:

                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    tempRecieverReference.InvokeDisconnectionEvents();
                    tempRecieverReference.ClientSocket.Close();
                    break;
                }
            }
        }





        /// <summary>
        /// TcpClient constructor.
        /// <br />
        /// Конструктор через TcpClient.
        /// </summary>
        public NetworkRecieverBusinessLogic(TcpClient client)
        {
            tempRecieverReference = Dependency.GetNetworkRecieverData(client);
        }



    }
}
