using MainNetworkingProject.Model.Basics;
using System.Collections.ObjectModel;

namespace MainNetworkingProject.ViewModel.ClientWindow
{
    public partial class ClientWindowViewModel : INotifyPropertyChanged
    {


        #region PROPERTIES



        private ClientWindowViewModelHandler _Handler;

        public ClientWindowViewModelHandler Handler
        {
            get { return _Handler; }
            set
            {
                _Handler = value;
                OnPropertyChanged(nameof(Handler));
            }
        }



        private ObservableCollection<string> _ChatLog;

        public ObservableCollection<string> ChatLog
        {
            get { return _ChatLog; }
            set
            {
                _ChatLog = value;
                OnPropertyChanged(nameof(ChatLog));
            }
        }



        private string _UserMessage;

        public string UserMessage
        {
            get { return _UserMessage; }
            set
            {
                _UserMessage = value;
                OnPropertyChanged(nameof(UserMessage));
            }
        }

        private ServiceUser _User;


        public ServiceUser User
        {
            get { return _User; }
            set
            {
                _User = value;
                OnPropertyChanged(nameof(User));
            }
        }



        private ExplorerClient _MainExplorerClient;


        public ExplorerClient MainExplorerClient
        {
            get { return _MainExplorerClient; }
            set
            {
                _MainExplorerClient = value;
                OnPropertyChanged(nameof(MainExplorerClient));
            }
        }


        #endregion PROPERTIES




        #region COMMANDS


        public DelegateCommand SendMessageCommand { get; }


        public DelegateCommand ConnectCommand { get; }




        #endregion COMMANDS




        #region CONSTRUCTION





        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ClientWindowViewModel()
        {
            _ChatLog = new();
            _MainExplorerClient = new();
            _Handler = new(this);
            MainExplorerClient.UpdateChatLog += Handler.UpdateChatLog;

            ConnectCommand = new(Handler.Connect);
            SendMessageCommand = new(Handler.SendMessage);
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
