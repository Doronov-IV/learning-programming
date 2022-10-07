using MainNetworkingProject.Model.Basics;
using System.Collections.ObjectModel;
using System.Threading;

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


        private ExplorerService _Service;

        public ExplorerService Service
        {
            get { return _Service; }
            set
            {
                _Service = value;
                OnPropertyChanged(nameof(Service));
            }
        }


        public string _Text;

        public string Text 
        {
            get { return _Text; }
            set
            {
                _Text = value;
                OnPropertyChanged(Text);
            }
        }



        public static readonly object ServiceLogLock = new object();

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
            //_State = new();
            _Handler = new(this);
            _Service = new();
            Service.GetServiceOutput += OnServiceOutput;
            _ServiceLog = new();

            ServiceLog.Add("Test.");

            RunServiceCommand = new(OnRunButtonClick);
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







        #region REF


        public ExplorerService service = new();


        public void OnServiceOutput(ref string sServiceOutput)
        {
            Text = new string(sServiceOutput);
            ServiceLog.Add(sServiceOutput);
        }


        public void OnRunButtonClick()
        {
            new Thread(() =>
            {
                Service.Run();
            }).Start();
        }




        #endregion REF



    }
}
