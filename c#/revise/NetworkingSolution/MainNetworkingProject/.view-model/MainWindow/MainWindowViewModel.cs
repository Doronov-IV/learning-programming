namespace MainNetworkingProject.ViewModel.MainWindow
{
    /// <summary>
    /// View-model of the main window;
    /// <br />
    /// Вью-модель основного окна;
    /// </summary>
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {



        #region PROPERTIES


        /// <summary>
        /// @see public MainWindowViewModelState State;
        /// </summary>
        private MainWindowViewModelState _State;


        /// <summary>
        /// @see public MainWindowViewModelHandler Handler;
        /// </summary>
        private MainWindowViewModelHandler _Handler;


        /// <summary>
        /// State of the viewmodel;
        /// <br />
        /// Состояние вьюмодели;
        /// </summary>
        public MainWindowViewModelState State
        {
            get { return _State; }
            set
            {
                _State = value;
                OnPropertyChanged(nameof(State));
            }
        }


        /// <summary>
        /// Handler of the viewmodel;
        /// <br />
        /// Хендлер вьюмодели;
        /// </summary>
        public MainWindowViewModelHandler Handler
        {
            get { return _Handler; }
            set
            {
                _Handler = value;
                OnPropertyChanged(nameof(Handler));
            }
        }


        #endregion PROPERTIES






        #region COMMANDS





        #endregion COMMANDS






        #region CONSTRUCTION




        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {
             // Assosiated members definition;
            _State = new();
            _Handler = new();
        }




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





        #endregion CONSTRUCTION


    }
}
