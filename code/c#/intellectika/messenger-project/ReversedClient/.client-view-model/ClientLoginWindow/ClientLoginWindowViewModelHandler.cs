using ReversedClient.client_view;

namespace ReversedClient.ViewModel.ClientLoginWindow
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
                        FullUserData = ServiceTransmitter.GetResponseData();

                        ReversedClientWindow window = new ReversedClientWindow(FullUserData, ServiceTransmitter);

                        window.Owner = Application.Current.MainWindow;
                        Application.Current.MainWindow.Hide();

                        window.Show();
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
                MessageBox.Show($"Exception: {ex.Message}", "Unable to connect", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void ShowErrorMessage(string message)
        {
            MessageBox.Show($"{message}", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
        }


        #endregion HANDLING

    }
}
