// Sic parvis magna
using AdoNetHomework.Model;
using AdoNetHomework.Service;


namespace AdoNetHomework.ViewModel
{
    /// <summary>
    /// View Model of the Application;
    /// <br />
    /// View Model Приложения;
    /// </summary>
    public partial class MainWindowViewModel : INotifyPropertyChanged
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




        #region PROPERTIES - forming the State of an Object




        #region Private references


        /// <summary>
        /// The name of the db server;
        /// <br />
        /// Имя сервера базы данных;
        /// </summary>
        private string _serverName;


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


        /// <summary>
        /// A string reference for sending queries into the db;
        /// <br /> 
        /// Строка для отправки запросов в б/д;
        /// </summary>
        private string queryString;


        /// <summary>
        /// Reserves db name;
        /// <br />
        /// Зарезервированное имя базы данных;
        /// </summary>
        private readonly string reservedDbName = "DoronovAdoNetCoreHomework";


        #endregion Private references




        #region Public properties


        /// <summary>
        /// @see private string _dbName;
        /// </summary>
        public string ServerName { get { return _serverName; } set { _serverName = value; } }


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
        public DelegateCommand OnFillButtonClickCommand { get; }


        /// <summary>
        /// ClearTablesButton click handler delegate;
        /// <br />
        /// Делегат хендлера элемента "ClearTablesButton";
        /// </summary>
        public DelegateCommand OnClearButtonClickCommand { get; }


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




        #endregion PROPERTIES - forming the State of an Object




        #region LOGIC




        #region HANDLERS - User Input Handling


        /// <summary>
        /// Launch connection procedure;
        /// <br />
        /// Запустить процесс подключения;
        /// </summary>
        private async void OnConnectButtonClickAsync()
        {
            // my local server id - DoronovLocalDb

            string connectionString = $"Server=.\\{ServerName};Database = master;Trusted_Connection=true;Encrypt=false";

            connection = new SqlConnection(connectionString);

            // try connect;
            try
            {
                ConnectionStatus = "Connecting.";

                await connection.OpenAsync();

                MessageBox.Show($"Connection Established.\nId: {connection.ClientConnectionId}", "Success.", MessageBoxButton.OK, MessageBoxImage.Information);

                ToggleConnectionState();

                ConnectionStatus = $"Connected to {ServerName}.";
            }
            // if the name was not found;
            catch (Exception e)
            {
                MessageBox.Show($"Something went wrong. Please, try another name.\nIf you are sure of this name, please check your server settings.", "Error. Server not found.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        /// Launch database seeding procedure;
        /// <br />
        /// Запустить процесс создания и заполнения базы данных;
        /// </summary>
        private async void OnFillButtonClickAsync()
        {
            User user;

            for (int i = 0, iSize = 10; i < iSize; ++i)
            {
                //UserList.Add(userGenerator.GetUser());
                user = userGenerator.GetUser();
                queryString =
                    $"USE {reservedDbName}; INSERT INTO Users (Name, PhoneNumber) VALUES(N'{user.Name}','{user.PhoneNumber}');";
                await ExecuteSQLCommandAsync(queryString);
            }

            MessageBox.Show($"Database created successfully. Please, check your server.", "Success.", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// Launch table clear procedure;
        /// <br />
        /// Запустить процесс очистки таблиц;
        /// </summary>
        private async void OnClearButtonClickAsync()
        {
            queryString = $"USE {reservedDbName} DELETE FROM Users; DELETE FROM Orders";

            await ExecuteSQLCommandAsync(queryString);
        }


        #endregion HANDLERS - User Input Handling





        #region AUXILIARY - secondary Methods


        /// <summary>
        /// Change connection status;
        /// <br />
        /// Изменить статус подключения;
        /// </summary>
        private void ToggleConnectionState()
        {
            bool bTemp = IsConnected;
            IsConnected = IsNotConnected;
            IsNotConnected = bTemp;
        }


        /// <summary>
        /// Launch query execution;
        /// <br />
        /// Запустить выполнение запроса;
        /// </summary>
        /// <param name="queryString">
        /// SQL query string;
        /// <br />
        /// Строка запроса SQL;
        /// </param>
        private async Task ExecuteSQLCommandAsync(string QueryString)
        {
            if (IsConnected)
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    await command.ExecuteNonQueryAsync();
                }
                catch(Exception e)
                {
                    MessageBox.Show($"Failed to execute your querry.\nException: {e.Message}", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        #endregion AUXILIARY - secondary Methods




        #endregion LOGIC




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {
            OnConnectButtonClickCommand = new DelegateCommand(OnConnectButtonClickAsync);
            OnFillButtonClickCommand = new DelegateCommand(OnFillButtonClickAsync);
            OnClearButtonClickCommand = new DelegateCommand(OnClearButtonClickAsync);

            IsNotConnected = true;
            IsConnected = false;
            ConnectionStatus = "Waiting for connection.";
            UserList = new List<User>();
            userGenerator = new UserGenerator();
        }


        #endregion CONSTRUCTION




    }
}
