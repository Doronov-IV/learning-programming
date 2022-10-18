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
        public string ConnectionString { get; set; }



        #endregion PROPERTIES





        #region DB CONNECTION STATUS


        private bool _IsConnected;

        public bool IsConnected
        {
            get { return _IsConnected; }
            set
            {
                _IsConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }


        private bool _IsNotConnected;

        public bool IsNotConnected
        {
            get { return _IsNotConnected; }
            set
            {
                _IsNotConnected = value;
                OnPropertyChanged(nameof(IsNotConnected));
            }
        }


        private void ToggleConnection()
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
            _IsConnected = false;
            ConnectionString = $"Server=.\\DoronovIV;Database = master;Trusted_Connection=true;Encrypt=false";

            _Handler = new(this);

            FillCommand = new(_Handler.OnFillButtonClick);
            ClearCommand = new(_Handler.OnClearButtonClick);
        }


        #endregion CONSTRUCTION


    }
}
