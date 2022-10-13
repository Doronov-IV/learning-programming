using ReversedClient.client_view;
using ReversedClient.Model.Basics;

namespace ReversedClient.ViewModel
{
    public class ClientWindowViewModelHandler
    {

        private ClientWindowViewModel _ViewModelReference;


        private ReversedClientWindowViewModel _ReversedViewModelReference;



        #region HANDLERS


        public void OnSignInButtonClick()
        {
            ReversedClientWindow clientChatWindow = new();
            _ReversedViewModelReference = clientChatWindow.DataContext as ReversedClientWindowViewModel;

            _ReversedViewModelReference.UserName = _ViewModelReference.UserName;
            _ViewModelReference.Server.ConnectToServer(_ReversedViewModelReference.UserName);
            clientChatWindow.Show();

            ClientLoginWindow clientLoginWindow = Application.Current.MainWindow as ClientLoginWindow;
            clientLoginWindow.Close();
        }


        #endregion HANDLERS




        #region LOGIC



        /// <summary>
        /// Connect new user;
        /// <br />
        /// Подключить нового пользователя;
        /// </summary>
        public void ConnectUser()
        {
            // create new user instance;
            var user = new UserModel
            {
                UserName = _ViewModelReference.Server.PacketReader.ReadMessage(),
                UID = _ViewModelReference.Server.PacketReader.ReadMessage(),
            };

            /*
             
           [!] In case there's no such user in collection we add them manualy;
            To prevent data duplication;
            
             */

            if (!_ReversedViewModelReference.Users.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => _ReversedViewModelReference.Users.Add(user));
            }
        }


        #endregion LOGIC




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ClientWindowViewModelHandler(ClientWindowViewModel viewModelReference)
        {
            _ViewModelReference = viewModelReference;

            
        }


        #endregion CONSTRUCTION


    }
}
