


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


        UserGenerator userGenerator;


        private string _dbName;

        private bool _isNotConnected;

        private bool _isConnected;

        private string _connectionStatus;

        private List<User> _UserList;

        private SqlConnection connection;

        public string dbName { get { return _dbName; } set {  _dbName = value; } }

        public List<User> UserList 
        {
            get 
            {
                return _UserList; 
            }
            set 
            {
                _UserList = value;
                OnPropertyChanged(nameof(UserList));
            }
        }


        public string ConnectionStatus
        {
            get 
            {
                return _connectionStatus; 
            }
            set 
            {
                _connectionStatus = value;
                OnPropertyChanged(nameof(ConnectionStatus));
            }
        }

        public DelegateCommand OnConnectButtonClickCommand { get; }

        public DelegateCommand OnCreateButtonClickCommand { get; }


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


        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }


        private void ToggleConnectionState()
        {
            var bSwapValue = IsConnected;
            IsConnected = IsNotConnected;
            IsNotConnected = bSwapValue;
        }


        public MainWindowViewModel()
        {
            OnConnectButtonClickCommand = new DelegateCommand(OnConnectButtonClickAsync);
            OnCreateButtonClickCommand = new DelegateCommand(OnCreateButtonClickAsync);

            IsNotConnected = true;
            IsConnected = false;
            ConnectionStatus = "Waiting for connection.";
            UserList = new List<User>();
            userGenerator = new UserGenerator();
        }


        private async void OnConnectButtonClickAsync()
        {
            // my local server id - DoronovLocalDb

            string connectionString = $"Server=.\\{dbName};Database = master;Trusted_Connection=true;Encrypt=false";

            connection = new SqlConnection(connectionString); // MSSQLLocalDB

            // try connect;
            try
            {
                ConnectionStatus = "Connecting.";

                await connection.OpenAsync();

                MessageBox.Show($"Connection Established.\nId: {connection.ClientConnectionId}", "Success.", MessageBoxButton.OK, MessageBoxImage.Information);

                ToggleConnectionState();

                ConnectionStatus = $"Connected to {dbName}.";
            }
            // if the name was not found;
            catch (Exception e)
            {
                MessageBox.Show($"Something went wrong. Please, try another name.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void OnCreateButtonClickAsync()
        {
            if (IsConnected)
            {
                string queryString =

                    "-- Create the demonstration db;" +
                    "CREATE DATABASE DoronovAdoNetCoreHomework;" +

                    "--Create the users table;" +
                    "CREATE TABLE Users" +
                    "(" +
                        "[Id] INT PRIMARY KEY IDENTITY(0,1)," +
	                    "[Name] NVARCHAR(24) NOT NULL," +
                        "[PhoneNumber] NVARCHAR(11)" +
                    ")" +

                    "--Create the orders table;" +
                    "CREATE TABLE Orders" +
                    "(" +
                        "[Id] INT PRIMARY KEY IDENTITY(0,1)," +
                        "[CustomerId] INT NOT NULL," +
                        "[Sum] INT," +
                        "[Date] DATE" +
                    ")";

                SqlCommand command = new SqlCommand(queryString, connection);

                int rowsAffected = command.ExecuteNonQuery();

                MessageBox.Show($"Database created successfully. Please, check your server.\nRows affected: {rowsAffected}.", "Success.", MessageBoxButton.OK, MessageBoxImage.Information) ;
            }
        }








    }
}
