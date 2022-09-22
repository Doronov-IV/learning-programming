

namespace AdoNetHomework
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {



        #region Property changed legacy


        /// <summary>
        /// Propery changed event handler;
        /// <br />
        /// Делегат-обработчик события 'property changed';
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// Handler-method of the 'property changed' delegate;
        /// <br />
        /// Метод-обработчик делегата 'property changed';
        /// </summary>
        /// <param name="propName">The name of the property;<br />Имя свойства;</param>
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        #endregion Property changed legacy


        private string _dbName;

        public string dbName { get { return _dbName; } set {  _dbName = value; } }

        public DelegateCommand OnClickCommand { get; }


        private bool _isNotConnected;

        public bool IsNotConnected 
        {
            get 
            {
                return _isNotConnected; 
            } 
            set 
            {
                _isNotConnected = value;
                OnPropertyChanged(nameof(IsNotConnected));
            } 
        }


        public MainWindowViewModel()
        {
            OnClickCommand = new DelegateCommand(OnConnectButtonClickAsync);
            IsNotConnected = true;
        }


        private async void OnConnectButtonClickAsync()
        {
            // my local server id - DoronovLocalDb

            string connectionString = $"Server=.\\{dbName};Database = master;Trusted_Connection=true;Encrypt=false";

            SqlConnection connection = new SqlConnection(connectionString); // MSSQLLocalDB

            await connection.OpenAsync();


            MessageBox.Show($"Connection Established.\nId: {connection.ClientConnectionId}", "Success.", MessageBoxButton.OK, MessageBoxImage.Information);

            IsNotConnected = false;
        }





    }
}
