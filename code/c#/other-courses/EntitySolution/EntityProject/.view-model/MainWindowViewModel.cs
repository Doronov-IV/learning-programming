using MainEntityProject.Model.Context;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.IO;

namespace MainEntityProject.ViewModel
{
	/// <summary>
	/// The ViewModel for the 'MainWindow'.
	/// <br />
	/// Вью-модель окна "MainWindow".
	/// </summary>
	public partial class MainWindowViewModel : INotifyPropertyChanged
    {



        #region STATE




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   PROPERTIES   ↓                           ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 
        



        /// <summary>
        /// The name of the MSSQL Server;
        /// <br />
        /// Имя SQL-сёрвера;
        /// </summary>
        public string ServerName
        {
            get { return _serverName; }
            set
            {
                _serverName = value;
                OnPropertyChanged(nameof(ServerName));
            }
        }



        /// <summary>
        /// A set of flags providing view with connection info;
        /// <br />
        /// Набор флагов для информирования вида;
        /// </summary>
        public CustomConnectionStatus ConnectionStatus
        {
            get { return _connectionStatus; }
            set
            {
                _connectionStatus = value;
            }
        }




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                               ↓   FIELDS   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 



        /// <inheritdoc cref="ServerName"/>
        private string _serverName;



        /// <inheritdoc cref="ConnectionStatus"/>
        private CustomConnectionStatus _connectionStatus;


        private DbContextOptions<CurrentDatabaseContext> _connectionOptions;


        #endregion STATE





        #region COMMANDS


        /// <summary>
        /// 'Do action' button command.
        /// <br />
        /// Команда кнопки "Do Action".
        /// </summary>
        public DelegateCommand FillTablesCommand { get; }


        /// <summary>
        /// 'Clear Tables' button command.
        /// <br />
        /// Команда кнопки "Clear Tables".
        /// </summary>
        public DelegateCommand ClearTablesCommand { get; }


        /// <summary>
        /// 'Change Authors' button command.
        /// <br />
        /// Команда кнопки "Change Authors".
        /// </summary>
        public DelegateCommand ChangeAuthorsCommand { get; }



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
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MainWindowViewModel()
        {
            InitializeSQLConnection();

            _connectionStatus = new();
            _serverName = string.Empty;

            FillTablesCommand = new(OnDoFillTablesClickAsync);
            ClearTablesCommand = new(OnClearTablesButtonClickAsync);
            ChangeAuthorsCommand = new(OnChangeAuthorsClickAsync);

            ServerName = "doronoviv";
        }



        #endregion CONSTRUCTION



    }
}
