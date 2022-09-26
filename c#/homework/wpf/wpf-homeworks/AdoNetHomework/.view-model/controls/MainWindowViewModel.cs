// Sic parvis magna
using AdoNetHomework.Model;
using AdoNetHomework.Service;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Windows.Controls;
using System.Windows.Input;

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
        private ObservableCollection<User> _UserList;


        private ObservableCollection<Order> _OrderList;


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


        OrderGenerator orderGenerator;


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
        public ObservableCollection<User> UserList
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


        public ObservableCollection<Order> OrderList
        {
            get
            {
                return _OrderList;
            }
            set
            {
                _OrderList = value;
                OnPropertyChanged(nameof(OrderList));
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

        public BindingList<User> UserBindingList;

        public BindingList<Order> OrderBindingList;


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
                ConnectionStatus = "Connecting .....";

                await connection.OpenAsync();

                MessageBox.Show($"Connection Established.\nId: {connection.ClientConnectionId}", "Success.", MessageBoxButton.OK, MessageBoxImage.Information);

                ToggleConnectionState();

                ConnectionStatus = $"Connected to {ServerName}.";
            }
            // if the name was not found;
            catch (Exception e)
            {
                MessageBox.Show($"Something went wrong. Please, try another name.\nIf you are sure of this name, please check your server settings.\n\nException: {e.Message}.", "Error. Server not found.", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            try
            {
                ExecuteSQLCommand($"USE {reservedDbName};");
            }
            catch (Exception e)
            {
            }
            finally
            {
                try
                {
                    ExecuteSQLCommand($"USE {reservedDbName}; SELECT * FROM Users;");
                }
                catch (Exception e)
                {
                }
                finally
                {
                    RefreshLists();
                }
            }
        }


        /// <summary>
        /// Launch database seeding procedure;
        /// <br />
        /// Запустить процесс создания и заполнения базы данных;
        /// </summary>
        private void OnFillButtonClick()
        {

            // Readme;
            /*
            
            Я пробовал сделать этот метод асинхронным, но проблема в том, что мне не хватает знаний, чтобы развести потоки
            заполнения первой таблицы от потоков заполнения второй. Таким образом, элементы второй таблицы могут создаться
            быстрее, чем элементы первой таблицы, на которую они ссылаются, что вызывает Exception.

            Может быть, можно как-то сделать лучше, но я уже второй день сижу над этими двумя методами, так что мне проще
            сейчас оставить всё как есть.

             */


            User user;
            Order order;

            Random random = new Random();
            int nRandomUsersQuantity = random.Next(10,30);


            try
            {
                ExecuteSQLCommand($"USE {reservedDbName};");
            }
            catch (Exception e)
            {
                ExecuteSQLCommand($"USE master; CREATE DATABASE {reservedDbName};");
            }
            finally
            {
                try
                {
                    ExecuteSQLCommand($"USE {reservedDbName}; SELECT * FROM Users;");
                }
                catch (Exception e)
                {
                    ExecuteSQLCommand(

                    $"USE {reservedDbName};" +
                    $"CREATE TABLE {reservedDbName}..Users" +
                    "(" +
                    "   [Id] INT PRIMARY KEY IDENTITY(0,1)," +
                    "   [Name] NVARCHAR(24) NOT NULL," +
                    "   [PhoneNumber] NVARCHAR(14)" +
                    ")" +

                    $"USE {reservedDbName};" +
                    $"CREATE TABLE {reservedDbName}..Orders" +
                    "(" +
                    "   [Id] INT PRIMARY KEY IDENTITY(0,1)," +
                    "   [CustomerId] INT FOREIGN KEY REFERENCES Users(Id)," +
                    "   [Summ] FLOAT," +
                    "   [Date] DATE" +
                    ")"
                );
                }
                finally
                {
                    // gererating Users;
                    for (int i = 0, iSize = nRandomUsersQuantity; i < iSize; ++i)
                    {
                        //UserList.Add(userGenerator.GetUser());
                        user = userGenerator.GetRandomUser();
                        queryString =
                            $"USE {reservedDbName}; INSERT INTO Users (Name, PhoneNumber) VALUES(N'{user.Name}','{user.PhoneNumber}');";
                        TryExecuteSQLCommand(queryString);
                    }


                    // Таблица заказов связана вторичным ключом с таблоицей пользователей, чтобы не получить Exception,
                    // нам необходимо узнать id пользователей;
                    int[] UsersIdSchemeForRandomOrders = GetCurrentUsersIdInfo();


                    // generating Orders;
                    for (int i = 0, iSize = nRandomUsersQuantity; i < iSize; ++i)
                    {
                        order = orderGenerator.GetRandomOrder(UsersIdSchemeForRandomOrders);
                        queryString =
                            $"USE {reservedDbName}; INSERT INTO Orders (CustomerId, Summ, Date) VALUES('{order.CustomerId}','{Math.Round(order.Summ, 1).ToString(CultureInfo.InvariantCulture)}', '{order.Date.ToString("yyyy-MM-dd")}');";
                        TryExecuteSQLCommand(queryString);
                    }

                    ShowSuccessChangesMessageBox();

                    RefreshLists();
                }
            }


            //ExecuteSQLCommand(

            //    "USE DoronovAdoNetCoreHomework" +
                
            //    "--Create the users table;" +
            //    "CREATE TABLE[Users]" +
            //    "(" +
            //    "   [Id] INT PRIMARY KEY IDENTITY(0,1)," +
            //    "   [Name] NVARCHAR(24) NOT NULL," +
            //    "   [PhoneNumber] NVARCHAR(14)" +
            //    ")" +

            //    "--Create the orders table;" +
            //    "CREATE TABLE[Orders]" +
            //    "(" +
            //    "   [Id] INT PRIMARY KEY IDENTITY(0,1)," +
            //    "   [CustomerId] INT FOREIGN KEY REFERENCES Users(Id)," +
            //    "   [Summ] FLOAT," +
            //    "   [Date] DATE" +
            //    ")"
            //    );



            //// gererating Users;
            //for (int i = 0, iSize = nRandomUsersQuantity; i < iSize; ++i)
            //{
            //    //UserList.Add(userGenerator.GetUser());
            //    user = userGenerator.GetRandomUser();
            //    queryString =
            //        $"USE {reservedDbName}; INSERT INTO Users (Name, PhoneNumber) VALUES(N'{user.Name}','{user.PhoneNumber}');";
            //    ExecuteSQLCommand(queryString);
            //}


            //// Таблица заказов связана вторичным ключом с таблоицей пользователей, чтобы не получить Exception,
            //// нам необходимо узнать id пользователей;
            //int[] UsersIdSchemeForRandomOrders = GetCurrentUsersIdInfo();


            //// generating Orders;
            //for (int i = 0, iSize = nRandomUsersQuantity; i < iSize; ++i)
            //{
            //    order = orderGenerator.GetRandomOrder(UsersIdSchemeForRandomOrders);
            //    queryString =
            //        $"USE {reservedDbName}; INSERT INTO Orders (CustomerId, Summ, Date) VALUES('{order.CustomerId}','{Math.Round(order.Summ, 1).ToString(CultureInfo.InvariantCulture)}', '{order.Date.ToString("yyyy-MM-dd")}');";
            //    ExecuteSQLCommand(queryString);
            //}

            //ShowSuccessChangesMessageBox();

            //RefreshLists();
        }


        /// <summary>
        /// Launch table clear procedure;
        /// <br />
        /// Запустить процесс очистки таблиц;
        /// </summary>
        private async void OnClearButtonClickAsync()
        {
            try
            {
                queryString = $"USE {reservedDbName} DELETE FROM Orders; DELETE FROM Users";

                await ExecuteSQLCommandAsync(queryString);
            }
            catch (Exception e)
            {

            }

            ShowSuccessChangesMessageBox();

            RefreshLists();
        }


        private void UpdateUserTable(object sender, NotifyCollectionChangedEventArgs e)
        {
            ExecuteSQLCommand($"USE {reservedDbName}; DELETE FROM Users;");

            foreach (var user in UserList)
            {
                queryString =
                            $"USE {reservedDbName}; INSERT INTO Users (Name, PhoneNumber) VALUES(N'{user.Name}','{user.PhoneNumber}');";
                TryExecuteSQLCommand(queryString);
            }
        }

        private void UpdateOrderTable(object sender, NotifyCollectionChangedEventArgs e)
        {
            ExecuteSQLCommand($"USE {reservedDbName}; DELETE FROM Orders;");

            foreach (var order in OrderList)
            {
                queryString =
                            $"USE {reservedDbName}; INSERT INTO Orders (CustomerId, Summ, Date) VALUES('{order.CustomerId}','{Math.Round(order.Summ, 1).ToString(CultureInfo.InvariantCulture)}', '{order.Date.ToString("yyyy-MM-dd")}');";
                TryExecuteSQLCommand(queryString);
            }
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
        /// <param name="QueryString">
        /// A string that represents the query;
        /// <br />
        /// Строка, представляющая собой запрос;
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


        /// <summary>
        /// Executes SQL command in a non-async way;
        /// <br />
        /// Запустить комманду SQL в однопоточном режиме;
        /// </summary>
        /// <param name="QueryString">
        /// A string that represents the query;
        /// <br />
        /// Строка, представляющая собой запрос;
        /// </param>
        private void TryExecuteSQLCommand(string QueryString)
        {
            if (IsConnected)
            {
                try
                {
                    SqlCommand command = new SqlCommand(QueryString, connection);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Failed to execute your querry.\nException: {e.Message}", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void ExecuteSQLCommand(string QueryString)
        {
            if (IsConnected)
            {
                SqlCommand command = new SqlCommand(QueryString, connection);

                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Show a message box whenever query ends with successful result;
        /// <br />
        /// Отобразить "MessageBox" каждый раз, когда запрос заканчивается успешно;
        /// </summary>
        private void ShowSuccessChangesMessageBox()
        {
            MessageBox.Show($"Data changed successfully. Please, check your server.", "Success.", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        /// <summary>
        /// Connect to the 'Users' table and get list of current ids;
        /// <br />
        /// Подключиться к таблице "Users" и получить список актуальных id;
        /// </summary>
        /// <returns>
        /// An array of int 'Id' values;
        /// <br />
        /// Массив целых значений "Id";
        /// </returns>
        private int[] GetCurrentUsersIdInfo()
        {
            List<int> idsList = new List<int>();

            SqlCommand command = new SqlCommand($"SELECT * FROM Users", connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    idsList.Add(reader.GetInt32(0));
                }
            }

            return idsList.ToArray();
        }



        private void RefreshUserList()
        {
            UserList.Clear();

            SqlCommand command = new SqlCommand($"USE {reservedDbName}; SELECT * FROM Users", connection);

            User user = new User();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = new User(Id: reader.GetInt32(0), Name: reader.GetString(1), PhoneNumber: reader.GetString(2));
                    UserList.Add(user);
                }
            }
        }


        private void RefreshOrderList()
        {
            OrderList.Clear();

            SqlCommand command = new SqlCommand($"USE {reservedDbName}; SELECT * FROM Orders", connection);

            Order order = new Order();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    order = new Order(id: reader.GetInt32(0), customerId: reader.GetInt32(1), summ: reader.GetSqlDouble(2).Value, DateTimeNow: reader.GetDateTime(3).Date);
                    OrderList.Add(order);
                }
            }
        }




        



        private void RefreshLists()
        {
            RefreshUserList();
            UserBindingList = new BindingList<User>(UserList);
            RefreshOrderList();
            OrderBindingList = new BindingList<Order>(OrderList);
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
            OnFillButtonClickCommand = new DelegateCommand(OnFillButtonClick);
            OnClearButtonClickCommand = new DelegateCommand(OnClearButtonClickAsync);

            IsNotConnected = true;
            IsConnected = false;
            ConnectionStatus = "Waiting for connection.";
            UserList = new ObservableCollection<User>();
            UserList.CollectionChanged += UpdateUserTable;
            OrderList = new ObservableCollection<Order>();
            OrderList.CollectionChanged += UpdateOrderTable;

            userGenerator = new UserGenerator();
            orderGenerator = new OrderGenerator();
        }


        #endregion CONSTRUCTION




    }
}
