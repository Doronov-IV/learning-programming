


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




        #region Properties




        #region Private references


        /// <summary>
        /// The name of the db server;
        /// <br />
        /// Имя сервера базы данных;
        /// </summary>
        private string _dbName;


        /// <summary>
        /// A flag that triggers the buttons;
        /// <br />
        /// Returns true if connection hasn't been established, otherwise false;
        /// <br />
        /// Флаг, который влияет на кнопки;
        /// <br />
        /// Возвращает истину, если подключение не установлено, иначе ложь;
        /// </summary>
        private bool _isNotConnected;


        /// <summary>
        /// A flag that triggers the buttons;
        /// <br />
        /// Returns true if connection has been established, otherwise false;
        /// <br />
        /// Флаг, который влияет на кнопки;
        /// <br />
        /// Возвращает истину, если подключение установлено, иначе ложь;
        /// </summary>
        private bool _isConnected;


        /// <summary>
        /// A string for user itnterface info;
        /// <br />
        /// Строка для вывода информации для пользователя;
        /// </summary>
        private string _connectionStatus;


        /// <summary>
        /// Current user list for pushing into db;
        /// <br />
        /// Текущий список юзеров для отправки в бд;
        /// </summary>
        private List<User> _UserList;


        /// <summary>
        /// Current SQL connection;
        /// <br />
        /// Текущее подключение;
        /// </summary>
        private SqlConnection connection;


        /// <summary>
        /// Custon service user generator instance;
        /// <br />
        /// Экземпляр генератора юзеров;
        /// </summary>
        UserGenerator userGenerator;


        #endregion Private references




        #region Public properties


        /// <summary>
        /// @see private string _dbName;
        /// </summary>
        public string dbName { get { return _dbName; } set { _dbName = value; } }


        /// <summary>
        /// @see private List<User> _UserList;
        /// </summary>
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


        /// <summary>
        /// @see private string _ConnectionStatus;
        /// </summary>
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


        /// <summary>
        /// ConntectionButton click handler delegate;
        /// <br />
        /// Делегат хендлера элемента "ConntectionButton";
        /// </summary>
        public DelegateCommand OnConnectButtonClickCommand { get; }


        /// <summary>
        /// CreationButton click handler delegate;
        /// <br />
        /// Делегат хендлера элемента "CreationButton";
        /// </summary>
        public DelegateCommand OnCreateButtonClickCommand { get; }


        /// <summary>
        /// @see private bool _IsNotConnected;
        /// </summary>
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


        /// <summary>
        /// @see private bool _IsConnected;
        /// </summary>
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


        #endregion Public properties




        #endregion Properties


        private void ToggleConnectionState()
        {
            bool bTemp = IsConnected;
            IsConnected = IsNotConnected;
            IsNotConnected = bTemp;
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
                MessageBox.Show($"Something went wrong. Please, try another name.\nIf you are sure of this name, please check your server settings.", "Error. Server not found.", MessageBoxButton.OK, MessageBoxImage.Error);
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
