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
using EntityHomeworkFirst.ViewModel.Handling;
using Prism.Commands;
using System.Data.SqlClient;

namespace EntityHomeworkFirst.ViewModel
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {


        #region PROPERTIES


        private ObservableCollection<Order> _OrderList;



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


        #endregion PROPERTIES




        #region COMMANDS


        public DelegateCommand FillCommand { get; }


        public DelegateCommand ClearCommand { get; }


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

                        string comandText = "USE DoronovEntityCoreFirst DELETE FROM Orders;";

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
            OrderList = new ObservableCollection<Order>();

            FillCommand = new DelegateCommand(OnFillButtonClick);

            ClearCommand = new DelegateCommand(OnClearButtonClick);
        }


        #endregion CONSTRUCTION




    }
}
