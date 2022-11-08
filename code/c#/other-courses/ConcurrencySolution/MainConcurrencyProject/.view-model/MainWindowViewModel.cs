using System.Collections.Specialized;
using System.ComponentModel;

namespace MainConcurrencyProject.ViewModel
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {


        #region STATE



        /// <summary>
        /// A locker for concurrent shared resources management.
        /// <br />
        /// Локер для многопоточного использования разделяемых ресурсов.
        /// </summary>
        private object _locker;


        /// <summary>
        /// An auto reset event instance for lockers demo.
        /// <br />
        /// Экземпляр AutoResetEvent для демки локеров.
        /// </summary>
        private AutoResetEvent _autoResetHandler;


        /// <summary>
        /// A string that accumulates output in a console-like way.
        /// <br />
        /// Строка, которая накапливает инпут по типу консоли.
        /// </summary>
        private string _largeMessage;



        #endregion STATE







        #region COMMANDS



        /// <summary>
        /// 'Do Action' button click command.
        /// <br />
        /// Команда клика по кнопке "Do Action".
        /// </summary>
        public DelegateCommand DoActionClickCommand { get; }



        #endregion COMMANDS







        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MainWindowViewModel()
        {
            DoActionClickCommand = new(OnDoActionButtonClick);
            _largeMessage = string.Empty;
            _locker = new();
            _autoResetHandler = new(initialState: true);
        }



        #region property changed



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



        #endregion property changed



        #endregion CONSTRUCTION


    }
}
