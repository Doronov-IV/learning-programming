// Sic parvis magna
using AdoNetHomework.Model;
using AdoNetHomework.Model.Wrappers;
using AdoNetHomework.Service;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
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
        private ObservableCollection<UserTableItem> _PrimaryUserList;

        /// <summary>
        /// Current order list for pushing into db;
        /// <br />
        /// Текущий список заказов для отправки в бд;
        /// </summary>
        private ObservableCollection<OrderTableItem> _PrimaryOrderList;

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
        /// Custon service order generator instance;
        /// <br />
        /// Экземпляр генератора заказов;
        /// </summary>
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

        /// <summary>
        /// A property for deletion of the selected user from 'Users' table;
        /// <br />
        /// Свойство для удаления выбранного элемента из таблицы пользователей;
        /// </summary>
        private UserTableItem _SelectedUser;





        /// <summary>
        /// User Name input field;
        /// <br />
        /// Поле ввода для имени пользователя;
        /// </summary>
        private string _NameAddUserInputField;

        /// <summary>
        /// User P/N input field;
        /// <br />
        /// Поле ввода для номера телефона пользователя;
        /// </summary>
        private string _PhoneNumberAddUSerInputField;




        /// <summary>
        /// Order C/N input field;
        /// <br />
        /// Поле ввода для номера заказчика в соответствующей таблице;
        /// </summary>
        private string _CustomerNumberAddOrderInputField;

        /// <summary>
        /// Order Sum input field;
        /// <br />
        /// Поле ввода для суммы заказа в соответствующей таблице;
        /// </summary>
        private string _SummAddOrderInputField;

        /// <summary>
        /// Order date input field;
        /// <br />
        /// Поле ввода для даты в соответствующей таблице;
        /// </summary>
        private string _DateAddOrderInputField;



        ///
        /// Label binding values; 
        ///


        /// <summary>
        /// A value for the "Min" label;
        /// <br />
        /// Значение для этикетки "Min";
        /// </summary>
        private string _MinOderPriceValue;

        /// <summary>
        /// A value for the "Max" label;
        /// <br />
        /// Значение для этикетки "Max";
        /// </summary>
        private string _MaxOderPriceValue;

        /// <summary>
        /// A value for the "Overall" label;
        /// <br />
        /// Значение для этикетки "Overall";
        /// </summary>
        private string _OverallOderPriceValue;


        #endregion Private references




        #region Public properties



        /// <summary>
        /// @see private string _ServerName;
        /// </summary>
        public string ServerName { get { return _serverName; } set { _serverName = value; } }

        /// <summary>
        /// @see private User _SelectedUser;
        /// </summary>
        public UserTableItem SelectedUser
        {
            get
            {
                return _SelectedUser;
            }

            set
            {
                _SelectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }




        ///
        /// Add user button fields;
        ///

        /// <summary>
        /// @see private string _NameAddUserInputField;
        /// </summary>
        public string NameAddUserInputField
        {
            get
            {
                return _NameAddUserInputField;
            }

            set
            {
                _NameAddUserInputField = value;
                OnPropertyChanged(nameof(NameAddUserInputField));
            }
        }

        /// <summary>
        /// @see private string _PhoneNumberAddUSerInputField;
        /// </summary>
        public string PhoneNumberAddUSerInputField
        {
            get
            {
                return _PhoneNumberAddUSerInputField;
            }

            set
            {
                _PhoneNumberAddUSerInputField = value;
                OnPropertyChanged(nameof(PhoneNumberAddUSerInputField));
            }
        }



        ///
        /// Add order button fields;
        ///

        /// <summary>
        /// @see private string _CustomerNumberAddOrderInputField;
        /// </summary>
        public string CustomerNumberAddOrderInputField
        {
            get
            {
                return _CustomerNumberAddOrderInputField;
            }
            set
            {
                _CustomerNumberAddOrderInputField = value;
                OnPropertyChanged(nameof(CustomerNumberAddOrderInputField));
            }
        }

        /// <summary>
        /// @see private string _SummAddOrderInputField;
        /// </summary>
        public string SummAddOrderInputField
        {
            get
            {
                return _SummAddOrderInputField;
            }
            set
            {
                _SummAddOrderInputField = value;
                OnPropertyChanged(nameof(SummAddOrderInputField));
            }
        }

        /// <summary>
        /// @see private string _DateAddOrderInputField;
        /// </summary>
        public string DateAddOrderInputField
        {
            get
            {
                return _DateAddOrderInputField;
            }

            set
            {
                _DateAddOrderInputField = value;
                OnPropertyChanged(nameof(DateAddOrderInputField));
            }
        }



        ///
        /// Label binding values; 
        ///


        /// <summary>
        /// @see private string _MinOderPriceValue;
        /// </summary>
        public string MinOderPriceValue 
        {
            get { return _MinOderPriceValue; }
            set
            {
                _MinOderPriceValue = value;
                OnPropertyChanged(nameof(MinOderPriceValue));
            }
        }

        /// <summary>
        /// @see private string _MaxOderPriceValue;
        /// </summary>
        public string MaxOderPriceValue
        {
            get { return _MaxOderPriceValue; }
            set
            {
                _MaxOderPriceValue = value;
                OnPropertyChanged(nameof(MaxOderPriceValue));
            }
        }

        /// <summary>
        /// @see private string _OverallOderPriceValue;
        /// </summary>
        public string OverallOderPriceValue 
        {
            get { return _OverallOderPriceValue; }
            set
            {
                _OverallOderPriceValue = value;
                OnPropertyChanged(nameof(OverallOderPriceValue));
            }
        }



        #region Observable collections



        /// <summary>
        /// @see private List<User> _PrimaryUserList;
        /// </summary>
        public ObservableCollection<UserTableItem> PrimaryUserList
        {
            get
            {
                return _PrimaryUserList;
            }
            set
            {
                _PrimaryUserList = value;
                OnPropertyChanged(nameof(PrimaryUserList));
            }
        }


        /// <summary>
        /// @see private List<Order> _PrimaryOrderList;
        /// </summary>
        public ObservableCollection<OrderTableItem> PrimaryOrderList
        {
            get
            {
                return _PrimaryOrderList;
            }
            set
            {
                _PrimaryOrderList = value;
                OnPropertyChanged(nameof(PrimaryOrderList));
            }
        }



        #endregion Observable collections




        #region Buttons Commands



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
        /// Delete 'Users' table command;
        /// <br />
        /// Команда удаления элемента из таблицы 'Users';
        /// </summary>
        public DelegateCommand OnUserDataGridDeleteKeyDownCommand { get; }


        /// <summary>
        /// AddUser click handler command;
        /// <br />
        /// Команда добавления пользователя при клике по кнопке "Add User";
        /// </summary>
        public DelegateCommand OnAddUserButtonClickCommand { get; }


        /// <summary>
        /// AddOrder click handler command;
        /// <br />
        /// Команда добавления заказа при клике по кнопке "Add Order";
        /// </summary>
        public DelegateCommand OnAddOrderButtonClickCommand { get; }




        #endregion Buttons Commands




        #region Connection status


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


        #endregion Connection status






        #endregion Public properties




        #endregion PROPERTIES - forming the State of an Object




        #region LOGIC




        #region HANDLERS - User Input Handling






        #region View Controls - Buttons and stuff


        /// <summary>
        /// Launch connection procedure;
        /// <br />
        /// Запустить процесс подключения;
        /// </summary>
        private async void OnConnectButtonClickAsync()
        {
            // my local server id - DoronovLocalDb

            string connectionString = $"Server={ServerName};Database = master;Trusted_Connection=true;Encrypt=false";

            connection = new SqlConnection(connectionString);

            // try connect;
            try
            {
                ConnectionStatus = "Connecting .....";

                await connection.OpenAsync();

                MessageBox.Show($"Connection Established.\nId: {connection.ClientConnectionId}", "Success.", MessageBoxButton.OK, MessageBoxImage.Information);

                ToggleConnectionState();

                ConnectionStatus = $"Connected to {ServerName}.";

                try
                {
                    ExecuteSQLCommand($"USE {reservedDbName};");

                    RefreshLists();
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

                        RefreshLists();
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
                        "   [Date] NVARCHAR(24)" +
                        ")"
                    );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong. Please, try another name.\nIf you are sure of this name, please check your server settings.\n\nException: {ex.Message}.", "Error. Server not found.", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    $"USE {reservedDbName}; INSERT INTO Orders (CustomerId, Summ, Date) VALUES('{order.CustomerId}','{Math.Round(order.Summ, 1).ToString(CultureInfo.InvariantCulture)}', '{order.Date}');";
                TryExecuteSQLCommand(queryString);
            }

            ShowSuccessChangesMessageBox();

            RefreshLists();
        }


        /// <summary>
        /// Launch table clear procedure;
        /// <br />
        /// Запустить процесс очистки таблиц;
        /// </summary>
        private async void OnClearButtonClickAsync()
        {
            var a = PrimaryUserList;
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


        /// <summary>
        /// Delete element from 'Users' table command handler;
        /// <br />
        /// Хендлер комманды удаления элемента из таблицы 'Users';
        /// </summary>
        private void OnUserDataGridDeleteKeyDown()
        {
            ObservableCollection<UserTableItem> tempUserList = new ObservableCollection<UserTableItem>();

            SqlCommand command;

            foreach(var user in PrimaryUserList)
            {
                if (!user.Equals(SelectedUser))
                {
                    tempUserList.Add(user);
                }
                else
                {
                    foreach(Order order in PrimaryOrderList)
                    {
                        if (order.CustomerId == user.Id)
                        {
                            queryString = $"USE {reservedDbName}; DELETE FROM Orders WHERE CustomerId = {user.Id};";

                            ExecuteSQLCommand(queryString);
                        }
                    }

                    queryString = $"USE {reservedDbName}; DELETE FROM Users WHERE Id = {user.Id};";

                    ExecuteSQLCommand(queryString);
                }
            }

            PrimaryUserList = tempUserList;

            RefreshLists();
        }



        /// <summary>
        /// 'AddUser' button command handler;
        /// <br />
        /// Хендлер команды нажатия на кпопку "Add";
        /// </summary>
        private void OnAddUserButtonClick()
        {
            if (NameAddUserInputField.Length <= 24)
            {
                SqlParameter phoneNumberParam = new SqlParameter("@phone",PhoneNumberAddUSerInputField);

                SqlParameter nameParam = new SqlParameter("@name", NameAddUserInputField);

                if (PhoneNumberAddUSerInputField.StartsWith("+44") && PhoneNumberAddUSerInputField.Length == 13)
                {
                    queryString = $"USE {reservedDbName}; INSERT INTO Users (Name, PhoneNumber) VALUES (@name, @phone);";

                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.Add(nameParam);

                    command.Parameters.Add(phoneNumberParam);

                    command.ExecuteNonQuery();

                    ShowSuccessChangesMessageBox();

                    RefreshLists();
                }
                else
                {
                    MessageBox.Show("Wrong phone number format. It should start with '+44' and include 13 symbols at max.", "Wrong Phone Number format.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        /// <summary>
        /// 'AddOrder' button command handler;
        /// <br />
        /// Хендлер команды нажатия на кпопку "Add Order";
        /// </summary>
        private void OnAddOrderButtonClick()
        {
            // Check if we have that userNumber;
            List<int> NumbersList = new List<int>();
            PrimaryUserList.ToList().ForEach(user => NumbersList.Add(user.TableNumber));
            int customerNumberTryParse = 0;
            int CustomerIdByTableNumber = 0;

            try
            {
                customerNumberTryParse = int.Parse(CustomerNumberAddOrderInputField);

                CustomerIdByTableNumber = PrimaryUserList.ToList().Find(user => user.TableNumber == int.Parse(CustomerNumberAddOrderInputField)).Id;
            }
            catch (Exception e)
            {
                MessageBox.Show("Please, enter digital number.", "Wrong Customer Number.", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            if (NumbersList.Contains(customerNumberTryParse))
            {
                string[] datetimeString = DateAddOrderInputField.Split("-");
                int[] datetimeInt = new int[datetimeString.Length];

                try
                {
                    SqlParameter idParam = new SqlParameter("@customerId", CustomerIdByTableNumber);
                    SqlParameter summParam = new SqlParameter("@summ", SummAddOrderInputField);
                    SqlParameter dateParam = new SqlParameter("@date", DateTime.ParseExact(DateAddOrderInputField, "dd-mm-yyyy", CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture));


                    queryString = $"USE {reservedDbName}; INSERT INTO Orders (CustomerId, Summ, Date) VALUES " +

                        $"(@customerId, @summ, @date);";

                    SqlCommand command = new SqlCommand(queryString, connection);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(summParam);
                    command.Parameters.Add(dateParam);

                    command.ExecuteNonQuery();

                    ShowSuccessChangesMessageBox();

                    RefreshLists();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please, enter proper date (dd-mm-yyyy)", "Wrong Customer Number.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please, use existing customer number", "Wrong Customer Number.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion View Controls - Buttons and stuff




        #region Collection handlers - item changed and other delegates


        /// <summary>
        /// When whole user list changed;
        /// <br />
        /// Когда изменяется сам список пользователей;
        /// </summary>
        private void OnUserListCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= OnUserListItemPropertyChanged;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += OnUserListItemPropertyChanged;
            }
        }

        /// <summary>
        /// When whole order list changed;
        /// <br />
        /// Когда изменяется сам список заказов;
        /// </summary>
        private void OnOrderListCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= OnOrderListItemPropertyChanged;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += OnOrderListItemPropertyChanged;
            }
        }


        /// <summary>
        /// When user list item changed;
        /// <br />
        /// Когда изменяется объект в списке пользователей;
        /// </summary>
        private void OnUserListItemPropertyChanged(object? sender, EventArgs e)
        {
            ObservableCollection<UserTableItem> tempUsersForComparison = new ObservableCollection<UserTableItem>();

            SqlCommand command = new SqlCommand($"SELECT * FROM Users", connection);

            UserTableItem userRef = new UserTableItem();

            // create copy of db data in a list;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    userRef = new UserTableItem(Id: reader.GetInt32(0), Name: reader.GetString(1), PhoneNumber: reader.GetString(2));
                    tempUsersForComparison.Add(userRef);
                }
            }

            // for each view collection item;
            foreach (UserTableItem newUser in PrimaryUserList)
            {
                // for each db item;
                foreach (UserTableItem oldUser in tempUsersForComparison)
                {
                    // if they differ;
                    if (oldUser.Id == newUser.Id && !oldUser.Equals(newUser))
                    {
                        // push changes;
                        ExecuteSQLCommand($"USE {reservedDbName}; UPDATE Users SET [Name] = '{newUser.Name}', [PhoneNumber] = '{newUser.PhoneNumber}' WHERE Id = {oldUser.Id};");

                    }
                }
            }


            #region Critical Exception

            //RefreshUserList();

            /*
            
            Если раскомментить, то на первом изменённом элементе выдаёт ElementOutOfRangeException.
            При этом, этот метод вызывается в других местах, где он работает корректно.
            Дебаг ничего не дал.

            Так как здесь метод опционален, было принято решение пока от него избавиться.

             */

            #endregion Critical Exception

        }


        /// <summary>
        /// When order list item changed;
        /// <br />
        /// Когда изменяется объект в списке заказов;
        /// </summary>
        public void OnOrderListItemPropertyChanged(object? sender, EventArgs e)
        {
            ObservableCollection<Order> tempOrdersForComparison = new ObservableCollection<Order>();

            SqlCommand command = new SqlCommand($"SELECT * FROM Orders", connection);

            Order orderRef = new Order();

            // create copy of db data in a list;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    orderRef = new Order(id: reader.GetInt32(0), customerId: reader.GetInt32(1), summ: reader.GetSqlDouble(2).Value, DateTimeNow: reader.GetString(3));
                    tempOrdersForComparison.Add(orderRef);
                }
            }

            // for each view collection item;
            foreach (Order newOrder in PrimaryOrderList)
            {
                // for each db item;
                foreach (Order oldOrder in tempOrdersForComparison)
                {
                    // if they differ;
                    if (oldOrder.Id == newOrder.Id && !oldOrder.Equals(newOrder))
                    {
                        // push changes;
                        ExecuteSQLCommand($"USE {reservedDbName}; UPDATE Orders SET [CustomerId] = '{newOrder.CustomerId}', [Summ] = '{Math.Round(newOrder.Summ, 1).ToString(CultureInfo.InvariantCulture)}', [Date] = '{newOrder.Date.ToString(CultureInfo.InvariantCulture)}' WHERE Id = {oldOrder.Id};");
                    }
                }
            }

            #region Critical Exception

            //RefreshUserList();

            /*
            
            Если раскомментить, то на первом изменённом элементе выдаёт ElementOutOfRangeException.
            При этом, этот метод вызывается в других местах, где он работает корректно.
            Дебаг ничего не дал.

            Так как здесь метод опционален, было принято решение пока от него избавиться.

             */

            #endregion Critical Exception
        }


        #endregion Collection handlers - item changed and other delegates







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


        /// <summary>
        /// Execute SQL command in non-async mode;
        /// <br />
        /// Выполнить SQL запрос в однопоточном режиме;
        /// </summary>
        /// <param name="QueryString">
        /// The string which represents SQL quety;
        /// <br />
        /// Строка SQL-запроса;
        /// </param>
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





        ///
        /// Refresh tables;
        ///

        /// <summary>
        /// Pull data from db to collection;
        /// <br />
        /// Достать данные из бд в коллекцию;
        /// </summary>
        private void RefreshUserList()
        {
            PrimaryUserList.Clear();

            SqlCommand command = new SqlCommand($"USE {reservedDbName}; SELECT * FROM Users", connection);

            UserTableItem user = new UserTableItem();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    user = new UserTableItem(Id: reader.GetInt32(0), Name: reader.GetString(1), PhoneNumber: reader.GetString(2));
                    PrimaryUserList.Add(user);
                }
            }

            RefreshTableItemsNumbers();
        }


        /// <summary>
        /// Pull data from db to collection;
        /// <br />
        /// Достать данные из бд в коллекцию;
        /// </summary>
        private void RefreshOrderList()
        {
            PrimaryOrderList.Clear();

            SqlCommand command = new SqlCommand($"USE {reservedDbName}; SELECT * FROM Orders", connection);

            OrderTableItem order = new OrderTableItem();

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    order = new OrderTableItem(id: reader.GetInt32(0), customerId: reader.GetInt32(1), summ: reader.GetSqlDouble(2).Value, DateTimeNow: reader.GetString(3));
                    PrimaryOrderList.Add(order);
                }
            }

            RefreshTableItemsNumbers();
        }


        /// <summary>
        /// 'Refresh' all lists;
        /// <br />
        /// Обновить оба списка;
        /// </summary>
        private void RefreshLists()
        {
            RefreshUserList();
            RefreshOrderList();
            RefreshLabelValues();
        }


        /// <summary>
        /// Refresh view-table numbers to avoid id exposing for the user;
        /// <br />
        /// Обновить номера значений в таблице представления, чтобы избежать раскрытия id для пользователя;
        /// </summary>
        private void RefreshTableItemsNumbers()
        {
            int i = 1;
            foreach (UserTableItem userWrapper in PrimaryUserList)
            {
                userWrapper.TableNumber = i++;
                foreach(OrderTableItem orderWrapper in PrimaryOrderList)
                {
                    if (orderWrapper.CustomerId.Equals(userWrapper.Id))
                    {
                        orderWrapper.CustomerTableNumber = userWrapper.TableNumber;
                    }
                }
            }
        }


        /// <summary>
        /// Refresh those label values that show min, max and overall values;
        /// <br />
        /// Обновить значения для лейблов, которые показывают минимальное, максимальное и среднее значения;
        /// </summary>
        private void RefreshLabelValues()
        {
            if (!PrimaryOrderList.IsNullOrEmpty())
            {
                var tempColl = PrimaryOrderList.ToList().Select(order => order.Summ).ToList();
                MinOderPriceValue = tempColl.Min().ToString();
                MaxOderPriceValue = tempColl.Max().ToString();
                OverallOderPriceValue = tempColl.Sum().ToString("n2");
            }
            else
            {
                MinOderPriceValue = "0";
                MaxOderPriceValue = "0";
                OverallOderPriceValue = "0";
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
            // Debug;
            //ServerName = "DoronovIV";


            // Button commands;
            OnConnectButtonClickCommand = new DelegateCommand(OnConnectButtonClickAsync);
            OnFillButtonClickCommand = new DelegateCommand(OnFillButtonClick);
            OnClearButtonClickCommand = new DelegateCommand(OnClearButtonClickAsync);
            OnUserDataGridDeleteKeyDownCommand = new DelegateCommand(OnUserDataGridDeleteKeyDown);
            OnAddUserButtonClickCommand = new DelegateCommand(OnAddUserButtonClick);
            OnAddOrderButtonClickCommand = new DelegateCommand(OnAddOrderButtonClick);


            // Connection status;
            IsNotConnected = true;
            IsConnected = false;
            ConnectionStatus = "Waiting for connection.";

            // Initializing lists;
            PrimaryUserList = new ObservableCollection<UserTableItem>();
            PrimaryOrderList = new ObservableCollection<OrderTableItem>();

            // Adding events for user input handling;
            PrimaryUserList.CollectionChanged += OnUserListCollectionChanged;
            PrimaryOrderList.CollectionChanged += OnOrderListCollectionChanged;

            // Initializing gemerator instances for object generating;
            userGenerator = new UserGenerator();
            orderGenerator = new OrderGenerator();


            // Initializing label strings;
            MinOderPriceValue = 0.ToString();
            MaxOderPriceValue = 0.ToString();
            OverallOderPriceValue = 0.ToString();
        }


        #endregion CONSTRUCTION




    }
}
