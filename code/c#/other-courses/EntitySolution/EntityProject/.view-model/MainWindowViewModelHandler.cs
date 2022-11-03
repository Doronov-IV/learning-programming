using Microsoft.Data.SqlClient;

namespace MainEntityProject.ViewModel
{
    public partial class MainWindowViewModel
    {

        #region HANDLERS



        /// <summary>
        /// Connect button click event handler;
        /// <br />
        /// Обработчик нажатия кнопки "Connect";
        /// </summary>
        public void OnConnectButtonClick()
        {
            // Если нет подключения, воткните после "Server=" символы ".\\";
            connectionString = $@"Server=.\{ServerName};Database = master;Trusted_Connection=true;Encrypt=false";

            using (SqlConnection connection = new(connectionString))
            {
                try
                {
                    connection.OpenAsync();

                    connection.Close();

                    ConnectionStatus.Toggle();

                    // Если не работает Entity, воткните после "Server=" символы ".\";
                    connectionString = $@"Server=.\{ServerName};Database = DoronovEFCthird;Trusted_Connection=true;Encrypt=false";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed. (location .view-model/Handler/OnConnectButtonClick)\n\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        #endregion HANDLERS

    }
}
