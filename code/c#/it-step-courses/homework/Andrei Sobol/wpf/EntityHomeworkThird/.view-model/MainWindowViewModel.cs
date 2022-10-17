using EntityHomeworkThird.Model.Context;

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


        #endregion PROPERTIES





        #region API


        //


        #endregion API





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
            _Handler = new(this);

            using (CurrentDatabaseContext context = new())
            {

            }
        }


        #endregion CONSTRUCTION


    }
}
