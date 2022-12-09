using NetworkingAuxiliaryLibrary.Dependencies.DataAccess;
using NetworkingAuxiliaryLibrary.Dependencies.Objects;

namespace NetworkingAuxiliaryLibrary.Dependencies.BusinessLogic
{
    public class NetworkRecieverBusinessLogic : INetworkRecieverDataAccessDependency
    {

        #region DI/IOC


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


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public NetworkRecieverBusinessLogic()
        {
            _dependency = null;
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public NetworkRecieverBusinessLogic(INetworkRecieverDataAccess networkRecieverDataAccess)
        {
            SetDependency(networkRecieverDataAccess);
        }


        #endregion DI/IOC



        #region LOGIC


        public void Process()
        {
            // connect message

            var reader = Dependency.GetNetworkRecieverData().PackageReader;

            while (true)
            {
                try
                {
                    var operationCode = reader.ReadByte();

                    switch (operationCode)
                    {
                        case 5:

                            var textMessage = reader.ReadMessage();
                            Dependency.GetNetworkRecieverData().InvokeTextMessageEvents(textMessage); // controller will broadcast it as well as show output;
                            break;


                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    Dependency.GetNetworkRecieverData().InvokeDisconnectionEvents();
                }
            }
        }


        #endregion LOGIC



    }
}
