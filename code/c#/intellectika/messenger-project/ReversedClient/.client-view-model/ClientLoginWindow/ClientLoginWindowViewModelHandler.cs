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
                if (!string.IsNullOrEmpty(Pass) && !string.IsNullOrEmpty(Login))
                {
                    _chatWindowReference = new();
                    _loginWindowReference = new();

                    ServiceTransmitter.ConnectToServer(currentUser.UserName, Login, Pass);

                    //// [!] In this particular order;
                    //
                    WindowHeaderString = currentUser.UserName + " - common chat";
                    _chatWindowReference.Show();
                    //

                    _loginWindowReference.Close();
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = _chatWindowReference;
                    // ===============================
                }
                else
                {
                    MessageBox.Show("Neither login nor password should be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to connect.\n\nException: {ex.Message}", "Exception intercepted", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion HANDLING

    }
}
