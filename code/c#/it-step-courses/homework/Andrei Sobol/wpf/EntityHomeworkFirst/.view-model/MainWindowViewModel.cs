global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.Windows;
global using Microsoft.EntityFrameworkCore;
global using System.Collections.ObjectModel;

using EntityHomeworkFirst.Model;
using Prism.Commands;
using System.Data.SqlClient;
using Tools.Connection;

namespace EntityHomeworkFirst.ViewModel
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {


        #region PROPERTIES


        /// <summary>
        /// @see public ObservableCollection<Order> OrderList;
        /// </summary>
        private ObservableCollection<Order> _OrderList;


        /// <summary>
        /// The list of orders;
        /// <br />
        /// Список заказов;
        /// </summary>
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
        /// @see public CustomConnectionStatus ConnectionStatus;
        /// </summary>
        private CustomConnectionStatus _ConnectionStatus;


        /// <summary>
        /// A set of flags providing view with connection info;
        /// <br />
        /// Набор флагов для информирования вида;
        /// </summary>
        public CustomConnectionStatus ConnectionStatus
        {
            get { return _ConnectionStatus; }
            set
            {
                _ConnectionStatus = value;
            }
        }



        /// <summary>
        /// A connection string for ado net instructions (see Handler);
        /// <br />
        /// Строка подключения для команд "ado net", (см. Handler);
        /// </summary>
        public static string ConnectionString = "";


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




        #region COMMANDS


        public DelegateCommand FillCommand { get; }


        public DelegateCommand ClearCommand { get; }


        public DelegateCommand ConnectCommand { get; }


        #endregion COMMANDS




        #region HANDLERS


        /// <summary>
        /// Fill button click event handler;
        /// <br />
        /// Обработчик нажатия кнопки "Fill";
        /// </summary>
        public void OnFillButtonClick()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Order order1 = new() { Summ = 1.5, Date = "02.02.2002" };
                Order order2 = new() { Summ = 1.7, Date = "02.02.2003" };

                context.Orders.AddRange(order1, order2);

                var a = context.SaveChanges();
            }
        }


        /// <summary>
        /// Clear button click event handler;
        /// <br />
        /// Обработчик нажатия кнопки "Clear";
        /// </summary>
        public void OnClearButtonClick()
        {
            using (ApplicationContext context = new())
            {
                try
                {
                    // Здесь я пробовал сделать через контекст, но SSMS затупил и не отображал изменения в базе;
                    // Понял это уже когда переделал через команды бывшего ADO;
                    using (SqlConnection adoConnection = new(MainWindowViewModel.ConnectionString))
                    {
                        adoConnection.Open();

                        string comandText = "USE DoronovEFCfirst DELETE FROM Orders;";

                        SqlCommand command = new(comandText, adoConnection);

                        command.ExecuteNonQuery();

                        adoConnection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database clearance failed.\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        /// <summary>
        /// Connect button click event handler;
        /// <br />
        /// Обработчик нажатия кнопки "Connect";
        /// </summary>
        public void OnConnectButtonClick()
        {
            // Если нет подключения, воткните после "Server=" символы ".\\";
            MainWindowViewModel.ConnectionString = $"Server={ServerName};Database = master;Trusted_Connection=true;Encrypt=false";

            using (SqlConnection connection = new(MainWindowViewModel.ConnectionString))
            {
                try
                {
                    connection.Open();

                    connection.Close();

                    ConnectionStatus.Toggle();

                    // Если не работает Entity, воткните после "Server=" символы ".\";
                    MainWindowViewModel.ConnectionString = $@"Server={ServerName};Database = DoronovEFCfirst;Trusted_Connection=true;Encrypt=false";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed. (location: .view-model/'main-vm'/OnConnectButtonClick)\n\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        #endregion HANDLERS






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
            _ConnectionStatus = new();

            _ServerName = "";

            _OrderList = new ObservableCollection<Order>();

            FillCommand = new DelegateCommand(OnFillButtonClick);

            ClearCommand = new DelegateCommand(OnClearButtonClick);

            ConnectCommand = new(OnConnectButtonClick);
        }


        #endregion CONSTRUCTION




    }
}
