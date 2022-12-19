using ReversedClient.client_view;
using ReversedClient.LocalService;

namespace ReversedClient.ViewModel.ClientStartupWindow
{
    public partial class ClientLoginWindowViewModel
    {

        #region HANDLING


        /// <summary>
        /// Handle the 'Sign In' button;
        /// <br />
        /// Обработать клик по кнопке 'Sign In';
        /// </summary>
        public async void OnSignInButtonClick()
        {
            try
            {
                if (!string.IsNullOrEmpty(_userDTOdata.Password) && !string.IsNullOrEmpty(_userDTOdata.Login))
                {
                    if (ServiceTransmitter.ConnectAndAuthorize(_userDTOdata))
                    {
                        ServiceTransmitter.ConnectAndSendLoginToService(_userDTOdata);
                        var ServerSideUserDTOdata = ServiceTransmitter.GetResponseData(); // deadlock

                        WpfWindowsManager.MoveFromLoginToChat(ServerSideUserDTOdata, ServiceTransmitter);
                    }
                    else
                    {
                        MessageBox.Show("Authorization failed due to the incurrect data input.", "Please, check your input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
                else
                {
                    MessageBox.Show("Neither login nor password should be empty.", "Please, check your input", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Service is down. Consider connecting later.", "Unable to connect", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void ShowErrorMessage(string message)
        {
            MessageBox.Show($"{message}", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
        }


        #endregion HANDLING

    }
}
