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
                if (!string.IsNullOrEmpty(_localUserTechnicalData.Password) && !string.IsNullOrEmpty(_localUserTechnicalData.Login))
                {
                    if (ServiceTransmitter.ConnectAndAuthorize(_localUserTechnicalData))
                    {
                        ServiceTransmitter.ConnectAndSendLoginToService(_localUserTechnicalData);
                        FullUserServiceData = ServiceTransmitter.GetResponseData(); // deadlock

                        WpfWindowsManager.MoveFromLoginToChat(FullUserServiceData, ServiceTransmitter);
                    }
                    else
                    {
                        MessageBox.Show("Authorization failed due to the incurrect data input.", "Please, check your input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
                else
                {
                    MessageBox.Show("Both login and password fields are required to proceed.", "Please, check your input", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Service is down. Consider connecting later.", "Unable to connect", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public async void OnSignUpButtonClick()
        {
            WpfWindowsManager.MoveFromLoginToRegister(_localUserTechnicalData, serviceTransmitter);
        }



        private void ShowErrorMessage(string message)
        {
            MessageBox.Show($"{message}", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
        }


        #endregion HANDLING

    }
}
