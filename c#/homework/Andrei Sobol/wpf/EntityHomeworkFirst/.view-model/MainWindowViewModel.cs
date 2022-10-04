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

namespace EntityHomeworkFirst.ViewModel
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {



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



        public ViewModelEventHandling VMEventHandler;


        #endregion PROPERTIES




        #region COMMANDS


        public DelegateCommand FillCommand { get; }


        public DelegateCommand ClearCommand { get; }


        #endregion COMMANDS




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {
            OrderList = new ObservableCollection<Order>();

            VMEventHandler = new ViewModelEventHandling();

            FillCommand = new DelegateCommand(VMEventHandler.OnFillButtonClick);

            ClearCommand = new DelegateCommand(VMEventHandler.OnClearButtonClick);

        }


        #endregion CONSTRUCTION




    }
}
