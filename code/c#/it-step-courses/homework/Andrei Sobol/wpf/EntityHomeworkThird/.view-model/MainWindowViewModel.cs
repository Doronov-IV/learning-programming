using EntityHomeworkThird.Model.Context;
using Prism.Commands;

namespace EntityHomeworkThird.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {


        #region PROPERTIES


        /// <summary>
        /// Handler is a file with all event handler methods;
        /// <br />
        /// Handler - это файл со всеми методами обработки событий;
        /// </summary>
        private MainWindowViewModelHandler _Handler;


        /// <summary>
        /// A connection string for ado net instructions (see Handler);
        /// <br />
        /// Строка подключения для команд "ado net", (см. Handler);
        /// </summary>
        public static string ConnectionString;


        /// <summary>
        /// @see public string ServerName;
        /// </summary>
        private string _ServerName;


        /// <summary>
        /// The name of the MSSQL Server;
        /// <br />
        /// Имя SQL-сёрвера;
        /// </summary>
        public string ServerName
        {
            get { return _ServerName; }
            set
            {
                _ServerName = value;
                OnPropertyChanged(nameof(ServerName));
            }
        }



        #endregion PROPERTIES






        #region DB CONNECTION STATUS



        /// <summary>
        /// @see public bool IsConnected;
        /// </summary>
        private bool _IsConnected;


        /// <summary>
        /// True if connected, otherwise false;
        /// <br />
        /// "True" если подключено, иначе "false";
        /// </summary>
        public bool IsConnected
        {
            get { return _IsConnected; }
            set
            {
                _IsConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }


        /// <summary>
        /// @see public bool IsNotConnected;
        /// </summary>
        private bool _IsNotConnected;


        /// <summary>
        /// True if NOT connected, otherwise false;
        /// <br />
        /// "True" если НЕ подключено, иначе "false";
        /// </summary>
        public bool IsNotConnected
        {
            get { return _IsNotConnected; }
            set
            {
                _IsNotConnected = value;
                OnPropertyChanged(nameof(IsNotConnected));
            }
        }


        /// <summary>
        /// @see public string ConnectionStatus;
        /// </summary>
        private string _ConnectionStatus;


        /// <summary>
        /// Verbose connection status string;
        /// <br />
        /// Строка развёрнутого статуса подключения;
        /// </summary>
        public string ConnectionStatus
        {
            get { return _ConnectionStatus; }
            set
            {
                _ConnectionStatus = value;
                OnPropertyChanged(nameof(ConnectionStatus));
            }
        }


        /// <summary>
        /// Invert the connection flags;
        /// <br />
        /// Инвертировать флаги подключения;
        /// </summary>
        public void ToggleConnection()
        {
            var bTemp = IsConnected;
            IsConnected = IsNotConnected;
            IsNotConnected = bTemp;
        }



        #endregion DB CONNECTION STATUS



        



        #region COMMANDS


        /// <summary>
        /// Fill database button command;
        /// <br />
        /// Команда заполнить базу;
        /// </summary>
        public DelegateCommand FillCommand { get; }


        /// <summary>
        /// Clear database tables contents command;
        /// <br />
        /// Команда очистить содержимое таблиц в б/д;
        /// </summary>
        public DelegateCommand ClearCommand { get; }


        /// <summary>
        /// Connect to database command;
        /// <br />
        /// Команда подключения к бд;
        /// </summary>
        public DelegateCommand ConnectCommand { get; }


        #endregion COMMANDS






        #region CONSTRUCTION



        #region Property changed


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


        #endregion Property changed




        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {
            ConnectionString = "";   
            _ServerName = "";

            _ConnectionStatus = "Waiting";

            _IsConnected = false;
            _IsNotConnected = true;

            _Handler = new(this);

            FillCommand = new(_Handler.OnFillButtonClick);
            ClearCommand = new(_Handler.OnClearButtonClick);
            ConnectCommand = new(_Handler.OnConnectButtonClick);
        }


        #endregion CONSTRUCTION


    }
}
