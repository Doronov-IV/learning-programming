global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.Windows;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;
global using System.Collections.ObjectModel;

using EntityHomeworkSecond.Model;
using Prism.Commands;
using EntityHomeworkSecond.Model.Context;
using EntityHomeworkSecond.Model.Entities;
using Tools.Connection;
using Microsoft.Data.SqlClient;

namespace EntityHomeworkSecond.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {



        #region PROPERTIES


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


        /// <summary>
        /// Fill database command;
        /// <br />
        /// Команда заполнения бд;
        /// </summary>
        public DelegateCommand FillDatabaseClickCommand { get; }


        /// <summary>
        /// Connect to database command;
        /// <br />
        /// Команда подключения к бд;
        /// </summary>
        public DelegateCommand ConnectCommand { get; }


        #endregion COMMANDS





        #region HANDLING



        /// <summary>
        /// Fill button click event handler;
        /// <br />
        /// Обработчик ивента нажатия на кнопку "Fill";
        /// </summary>
        private void OnFillDatabaseClick()
        {
            using (LocalDbContext context = new())
            {
                try
                {
                    Card card1;
                    Card card2;

                    Card card3;

                    Student student1 = new() { FirstName = "John", LastName = "von Neumann", Birthay = "28/12/1903", PhoneNumber = "88005553535" };
                    Student student2 = new() { FirstName = "Ada", LastName = "Lovelace", Birthay = "10/12/1815", PhoneNumber = "88005553534" };

                    card1 = new() { Student = student1 };
                    card2 = new() { Student = student2 };
                    // Проверил, не запушилось, потому что ссылка на студента повторяется;
                    card3 = new() { Student = student2 };

                    student1.Card = card1;
                    student2.Card = card2;

                    context.Students.AddRange(student1, student2);
                    context.Cards.AddRange(card1, card2, card3);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong.\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        /// <summary>
        /// Connect button click event handler;
        /// <br />
        /// Обработчик ивента нажатия на кнопку "Connect";
        /// </summary>
        private void OnConnectButtonClick()
        {
            // Если нет подключения, воткните после "Server=" символы ".\\";
            MainWindowViewModel.ConnectionString = $"Server=.\\{ServerName};Database = master;Trusted_Connection=true;Encrypt=false";

            using (SqlConnection connection = new(MainWindowViewModel.ConnectionString))
            {
                try
                {
                    connection.Open();

                    connection.Close();

                    ConnectionStatus.Toggle();
                    // Если не работает Entity, воткните после "Server=" символы ".\";
                    MainWindowViewModel.ConnectionString = $@"Server=.\{ServerName};Database = DoronovEFCsecond;Trusted_Connection=true;Encrypt=false";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed. (location: .view-model/'main-vm'/OnConnectButtonClick)\n\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        #endregion HANDLING




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

            FillDatabaseClickCommand = new DelegateCommand(OnFillDatabaseClick);
            ConnectCommand = new(OnConnectButtonClick);
        }


        #endregion CONSTRUCTION


    }
}
