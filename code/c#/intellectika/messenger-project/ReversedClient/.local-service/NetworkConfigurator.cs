using NetworkingAuxiliaryLibrary.Dependencies.Factories;

namespace ReversedClient.LocalService
{
    public static class NetworkConfigurator
    {

        private static int ClientAuthorizerPort = ;

        public static IPEndPoint GetAuthorizerEndPoint()
        {
            File.ReadAllText(@"..\..\..\.config\networking-configuration.json");

            return new IPEndPoint(IPAddress.Any, 0);
        }


        public static IPEndPoint GetMessengerEndPoint()
        {
            return IpEndPointDataAccessFactory.GetLocalMessengerClientPoint();
        }

    }
}
