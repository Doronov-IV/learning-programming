using ReversedClient.client_view;

namespace ReversedClient.ViewModel
{
    public class ReversedClientWindowViewModelHandler
    {



        #region PROPERTIES


        private ReversedClientWindowViewModel _CurrentViewModelReference;


        #endregion PROPERTIES




        #region HANDLERS


        public void OnSignInButtonClick()
        {
            try
            {
                _CurrentViewModelReference.Server.ConnectToServer(_CurrentViewModelReference.UserName);

                //// [!] In this particular order;
                //
                ReversedClientWindow clientChatWindow = new();
                clientChatWindow.Show();
                //
                ClientLoginWindow? clientLoginWindow = Application.Current.MainWindow as ClientLoginWindow;
                clientLoginWindow?.Close();
                // ------------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to connect.\n\nException: {ex.Message}", "Exception intercepted", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion HANDLERS




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ReversedClientWindowViewModelHandler(ReversedClientWindowViewModel CurrentViewModelReference)
        {
            _CurrentViewModelReference = CurrentViewModelReference;
        }


        #endregion CONSTRUCTION



    }
}
