using ReversedClient.client_view;
using ReversedClient.LocalService;

namespace ReversedClient.ViewModel.ClientStartupWindow
{
    public partial class ClientLoginWindowViewModel
    {

        #region HANDLING


        /// <summary>
        /// Handle the 'Sign In' button click.
        /// <br />
        /// It uses try-catch syntax to recursively make three efforts of connection.
        /// If all of them are in vain, 'throw' a message box into the user.
        /// <br />
        /// <br />
        /// Обработать клик по кнопке 'Sign In'. Использует синтакс try-catch, чтобы рекурсивно сделать три попытки подключиться.
        /// <br />
        /// Если все они провалились, "выбросить" юхеру месседжбокс.
        /// </summary>
        private async void OnSignInButtonClick()
        {
            /*
            int waitingTimeSpan = 200;
            // first and main try;
            try
            {
                await MakeConnectionEffort();
            }
            catch (Exception ex)
            {
                // second try;
                Task.Delay(waitingTimeSpan).Wait(); // waiting some time in case it was some multithreading issue;
                try
                {
                    await MakeConnectionEffort();
                }
                catch
                {
                    // third try;
                    Task.Delay(waitingTimeSpan).Wait(); // 
                    try
                    {
                        await MakeConnectionEffort();
                    }
                    catch
                    {
                        // the server(s) is(are) down after all;
                        MessageBox.Show($"Service is down. Consider connecting later.", "Unable to connect", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            */

            await MakeConnectionEffort();
        }


        /// <summary>
        /// Make an effort to connect to the authorization service.
        /// <br />
        /// Сделать попытку подклюяиться к сервису авторизации.
        /// </summary>
        private async Task MakeConnectionEffort()
        {
            if (!string.IsNullOrEmpty(_localUserTechnicalData.Password) && !string.IsNullOrEmpty(_localUserTechnicalData.Login))
            {
                if (await ServiceTransmitter.ConnectAndAuthorize(_localUserTechnicalData))
                {
                    ServiceTransmitter.ConnectAndSendLoginToService(_localUserTechnicalData);
                    FullUserServiceData = ServiceTransmitter.GetResponseData();

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



        /// <summary>
        /// A handler for the registration button click which sends user to the registration window.
        /// <br />
        /// Обработчик клика кнопки регистрации, который отсылает пользователя в окно регистрации.
        /// </summary>
        private async void OnSignUpButtonClick()
        {
            WpfWindowsManager.MoveFromLoginToRegister(_localUserTechnicalData, serviceTransmitter);
        }



        /// <summary>
        /// Debug method.
        /// <br />
        /// Метод для дебага.
        /// </summary>
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show($"{message}", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
        }


        #endregion HANDLING

    }
}
