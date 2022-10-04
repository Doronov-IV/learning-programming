global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Microsoft.EntityFrameworkCore;

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








    }
}
