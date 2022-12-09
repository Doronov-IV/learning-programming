namespace NetworkingAuxiliaryLibrary.Dependencies.DataAccess
{
    public class CommonNetworkRecieverDataAccess : INetworkRecieverDataAccess
    {
        public NetworkReciever GetNetworkRecieverData()
        {
            return new NetworkReciever();
        }

    }
}
