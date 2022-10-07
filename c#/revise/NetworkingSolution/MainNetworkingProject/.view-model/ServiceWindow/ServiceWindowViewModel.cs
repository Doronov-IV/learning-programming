namespace MainNetworkingProject.ViewModel.ServiceWindow
{
    public partial class ServiceWindowViewModel : INotifyPropertyChanged
    {


        #region PROPERTIES


        private ServiceWindowViewModelState _State;
        public ServiceWindowViewModelState State
        {
            get { return _State; }
            set
            {
                _State = value;
                OnPropertyChanged(nameof(State));
            }
        }

        private ServiceWindowViewModelHandler _Handler;
        public ServiceWindowViewModelHandler Handler
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
        public ServiceWindowViewModel()
        {
            _State = new();
            _Handler = new(this);

             
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
