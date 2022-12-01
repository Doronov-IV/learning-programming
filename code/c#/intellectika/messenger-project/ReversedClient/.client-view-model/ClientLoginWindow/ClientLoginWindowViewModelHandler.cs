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
                if (!string.IsNullOrEmpty(UserData.Password) && !string.IsNullOrEmpty(UserData.Login))
                {
                    if (await ServiceTransmitter.ConnectToServer(UserData.Login, UserData.Password))
                    {
                        ReversedClientWindow window = new ReversedClientWindow(UserData, ServiceTransmitter);
                        window.Show();

                        Application.Current.MainWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("Authorization failed due to the incurrect data input.", "Please, check your input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }

                    ////// [!] In this particular order;
                    ////
                    //WindowHeaderString = currentUser.UserName + " - common chat";
                    //_chatWindowReference.Show();
                    ////

                    //_loginWindowReference.Close();
                    //Application.Current.MainWindow.Close();
                    //Application.Current.MainWindow = _chatWindowReference;
                    //// ===============================
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
