using NetworkingAuxiliaryLibrary.Dependencies.DataAccess;
using NetworkingAuxiliaryLibrary.Dependencies.Objects;
using NetworkingAuxiliaryLibrary.Packages;
using NetworkingAuxiliaryLibrary.Processing;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace NetworkingAuxiliaryLibrary.Dependencies.BusinessLogic
{
    public class NetworkControllerBusinessLogic : INetworkControllerDataAccessDependency
    {


        #region DI/IoC


        private NetworkController tempControllerReference;


        private INetworkControllerDataAccess _dependency;

        public INetworkControllerDataAccess Dependency
        {
            get { return _dependency; }
            set { _dependency = value; }
        }

        public void SetDependency(INetworkControllerDataAccess networkControllerDataAccess)
        {
            _dependency = networkControllerDataAccess;
        }


        private NetworkController tempControlerReference;


        #endregion DI/IoC



        public async Task Listen()
        {
            tempControlerReference.Listener.Start();

            NetworkReciever client;
            PackageReader reader = null;
            MessagePackage incommingMessage = null;

            tempControlerReference.ProcessingStatus.ToggleCompletion();

            while (true)
            {
                client = null;

                await Task.Run(() => client = new(tempControlerReference.Listener.AcceptTcpClient()));

                if (client is not null)
                {
                    client.ProcessTextMessage += AddNewMessageToDb;
                    client.ProcessTextMessage += BroadcastMessage;
                    client.ProcessDisconnection += BroadcastDisconnection;
                    client.SendMessageOutput += ShowMessageOutput;
                    client.SendDisconnectionOutput += ShowDisconnectionOutput;

                    reader = new(client.ClientSocket.GetStream());

                    await Task.Run(() => incommingMessage = reader.ReadMessage());

                    // write notification about user conenction

                    if (incommingMessage is not null)
                    {
                        NetworkRecieverBusinessLogic newClientReference = new NetworkRecieverBusinessLogic(client.ClientSocket);
                        tempControlerReference.ConnectionList.Add(newClientReference);

                        var user = client.User.GetUserData(incommingMessage.Message as string);

                        if (incommingMessage.Sender.Equals("Authorizer")) 
                        {
                            if (incommingMessage.Reciever.Equals("Messenger")) // authorizer sent login to messenger;
                            {
                                CheckLoginPresence(incommingMessage);
                            }
                            else
                            {
                                GetAuthorizerResponse(incommingMessage); // authorizer sent authorization respond to user;
                            }
                        }
                        else if (incommingMessage.Sender.Equals("Messenger")) // messenger sent us user model;
                        {
                            if (!incommingMessage.Reciever.Equals("Authorizer")) // just in case;
                            {
                                //SynchronizeChats();
                            }
                        }
                        else // user sent message to user;
                        {
                            ShowMessageOutput(incommingMessage);
                        }
                    }
                }
            }

        }



        #region LOGIC


        /// <summary>
        /// For Messenger.
        /// </summary>
        protected virtual void AddNewMessageToDb(MessagePackage message)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// For Messenger.
        /// </summary>
        protected virtual void BroadcastMessage(MessagePackage message)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// For Messenger.
        /// </summary>
        protected virtual void BroadcastDisconnection(IUserDataAccess userDataAccess)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// For Messenger.
        /// </summary>
        protected virtual void ShowDisconnectionOutput(IUserDataAccess userDataAccess)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// For Messenger and Authorizer.
        /// </summary>
        protected virtual void CheckLoginPresence(MessagePackage message)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// For Client and Messenger.
        /// </summary>
        protected virtual void ShowMessageOutput(MessagePackage message)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// For client.
        /// </summary>
        protected virtual void GetAuthorizerResponse(MessagePackage message)
        {
            throw new NotImplementedException();
        }


        #endregion LOGIC



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public NetworkControllerBusinessLogic(TcpListener listenner)
        {
            tempControlerReference = Dependency.GetNetworkControllerData(listenner);
        }


    }
}
