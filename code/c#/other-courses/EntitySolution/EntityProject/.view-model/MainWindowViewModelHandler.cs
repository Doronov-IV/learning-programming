using Microsoft.Data.SqlClient;

namespace MainEntityProject.ViewModel
{
    public partial class MainWindowViewModel
    {

        #region HANDLERS








        /// <summary>
        /// Handle connect button click event;
        /// <br />
        /// Обработать нажатие кнопки "Connect";
        /// </summary>
        public void OnConnectButtonClick()
        {
            // Connection string инициализируется в конструкторе вью-модели.

            using (SqlConnection connection = new(connectionString))
            {
                try
                {
                    connection.OpenAsync();

                    connection.Close();

                    ConnectionStatus.Toggle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed. (location: .view-model/Handler/OnConnectButtonClick)\n\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        #endregion HANDLERS

    }
}
