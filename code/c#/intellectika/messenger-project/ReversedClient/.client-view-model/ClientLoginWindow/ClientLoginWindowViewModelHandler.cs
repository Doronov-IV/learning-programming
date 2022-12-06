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
        public void OnSignInButtonClick()
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
                        window.Show();

                        Application.Current.MainWindow.Close();
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
                MessageBox.Show($"Unable to connect.\n\nException: {ex.Message}", "Exception intercepted", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void ShowErrorMessage(string message)
        {
            MessageBox.Show($"{message}", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
        }


        #endregion HANDLING

    }
}
