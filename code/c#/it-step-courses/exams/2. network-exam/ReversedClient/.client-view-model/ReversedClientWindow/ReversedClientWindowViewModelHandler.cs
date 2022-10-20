using ReversedClient.client_view;

namespace ReversedClient.ViewModel
{
    public class ReversedClientWindowViewModelHandler
    {



        #region PROPERTIES



        /// <summary>
        /// A reference for the main file;
        /// <br />
        /// Ссылка на основной файл;
        /// </summary>
        private ReversedClientWindowViewModel _CurrentViewModelReference;


        #endregion PROPERTIES




        #region HANDLERS



        /// <summary>
        /// Handle the 'Sign In' button;
        /// <br />
        /// Обработать клик по кнопке 'Sign In';
        /// </summary>
        public void OnSignInButtonClick()
        {
            try
            {
                _CurrentViewModelReference.Server.ConnectToServer(_CurrentViewModelReference.UserName);


                //// [!] In this particular order;
                //
                _CurrentViewModelReference.WindowHeaderString = _CurrentViewModelReference.UserName + " - common chat";
                ReversedClient.client_view.ReversedClientWindow clientChatWindow = new();
                clientChatWindow.Show();
                //
                ClientLoginWindow? clientLoginWindow = Application.Current.MainWindow as ClientLoginWindow;
                clientLoginWindow?.Close();
                // ===============================
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to connect.\n\nException: {ex.Message}", "Exception intercepted", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void OnUsersCollectionChanged(object? sender, EventArgs e)
        {
            if (_CurrentViewModelReference.Users.Count > 1) _CurrentViewModelReference.TheMembersString = "members";
            else _CurrentViewModelReference.TheMembersString = "member";
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
