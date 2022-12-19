using ReversedClient.LocalService;

namespace ReversedClient.ViewModel.ClientSignUpWindow
{
    public partial class ClientSignUpWindowViewModel
    {



        #region HANDLING



        private void OnRegisterButtonClick()
        {
            try
            {
                if (AllFieldsAreInitialized())
                {
                    if (RepeatedPassword.Equals(UserData.Password))
                    {
                        if (transmitter.RegisterNewUser(UserData))
                        {
                            WpfWindowsManager.FromRegisterToLogin(UserData, transmitter);
                        }
                        else
                        {
                            MessageBox.Show($"This login or public id is already present on server.", "Try another one", MessageBoxButton.OK, MessageBoxImage.Hand);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Passwords do not match.", "Check your input", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
                else
                {
                    MessageBox.Show($"All fields are required to proceed.", "Check your input", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Service is down. Consider register later.", "Unable to sign up", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        #endregion HANDLING



        #region AUXILIARY



        public bool AllFieldsAreInitialized()
        {
            return !string.IsNullOrEmpty(UserData.Login) && !string.IsNullOrEmpty(UserData.Password) && !string.IsNullOrEmpty(UserData.PublicId) && !string.IsNullOrEmpty(RepeatedPassword);
        }



        #endregion AUXILIARY


    }
}
