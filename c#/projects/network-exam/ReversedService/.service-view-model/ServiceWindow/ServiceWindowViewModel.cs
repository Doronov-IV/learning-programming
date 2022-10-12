using ReversedService.Model.Basics;
using System.Collections.ObjectModel;
using System.Threading;

namespace ReversedService.ViewModel.ServiceWindow
{
    public partial class ServiceWindowViewModel : INotifyPropertyChanged
    {





        #region PROPERTIES


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


        private ServiceHub _Service;

        public ServiceHub Service
        {
            get { return _Service; }
            set
            {
                _Service = value;
                OnPropertyChanged(nameof(Service));
            }
        }




        private AsyncObservableCollection<string> _ServiceLog;

        public AsyncObservableCollection<string> ServiceLog
        {
            get { return _ServiceLog; }
            set
            {
                _ServiceLog = value;
                OnPropertyChanged(nameof(ServiceLog));
            }
        }


        private bool _IsRunning;

        private bool _IsNotRunning;


        public bool IsRunning
        {
            get { return _IsRunning; }
            set
            {
                _IsRunning = value;
                OnPropertyChanged(nameof(IsRunning));
            }
        }


        public bool IsNotRunning
        {
            get { return _IsNotRunning; }
            set
            {
                _IsNotRunning = value;
                OnPropertyChanged(nameof(IsNotRunning));
            }
        }


        #endregion PROPERTIES




        #region COMMANDS


        public DelegateCommand RunServiceCommand { get; }


        #endregion COMMANDS




        #region CONSTRUCTION





        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ServiceWindowViewModel()
        {
            _IsRunning = false;
            _IsNotRunning = true;

            _Service = new();
            _Handler = new(this);
            _ServiceLog = new();

            RunServiceCommand = new(Handler.OnRunButtonClick);


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
