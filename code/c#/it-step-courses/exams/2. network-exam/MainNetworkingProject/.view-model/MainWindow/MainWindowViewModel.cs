using Prism.Commands;
using Tools.Toolbox;

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


        public DelegateCommand LaunchClientCommand { get; }
        public DelegateCommand LaunchServiceCommand { get; }
        public DelegateCommand KillServiceCommand { get; }


        #endregion COMMANDS




        #region XTRACTION





        #endregion XTRACTION



        private void ExtractLibraries()
        {
            try
            {
                /// Toolbox

                // target;
                FileInfo dllTargetInfo = new(@"C:\Users\i.doronov\source\repos\computer-science-learning\code\c#\tools\toolbox\Toolbox\bin\Debug\net6.0\Toolbox.dll");
                // destination (precomplied dll folder);
                List<DirectoryInfo> toolsExtractorDestinations = new();
                toolsExtractorDestinations.Add(new DirectoryInfo(@"C:\Users\i.doronov\source\repos\computer-science-learning\code\c#\tools\precompiled-dll"));
                // extracting;
                LibraryExtractor toolsLibraryExtractor = new(dllTargetInfo, toolsExtractorDestinations);
                toolsLibraryExtractor.Extract();


                /// NET

                // target;
                FileInfo targetInfo = new("C:\\Users\\i.doronov\\source\\repos\\computer-science-learning\\code\\c#\\" +
        "it-step-courses\\exams\\2. network-exam\\NetworkingAuxiliaryLibrary\\bin\\Debug\\net6.0\\NetworkingAuxiliaryLibrary.dll");
                // destination 1 (client);
                List<DirectoryInfo> extractorDestinations = new();
                extractorDestinations.Add(new DirectoryInfo("C:\\Users\\i.doronov\\source\\repos\\computer-science-learning" +
                    "\\code\\c#\\it-step-courses\\exams\\2. network-exam\\ReversedClient\\.net"));
                // destination 2 (service);
                extractorDestinations.Add(new DirectoryInfo("C:\\Users\\i.doronov\\source\\repos\\computer-science-learning" +
        "\\code\\c#\\it-step-courses\\exams\\2. network-exam\\ReversedService\\.net"));
                // extracting;
                LibraryExtractor networkingLibraryExtractor = new(targetInfo, extractorDestinations);
                networkingLibraryExtractor.Extract();
            }
            catch { }
        }



        #region CONSTRUCTION




        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {
            ExtractLibraries();

            // Assosiated members definition;
            _State = new();
            _Handler = new();

            LaunchClientCommand = new(Handler.OnLaunchClientButtonClickAsync);
            LaunchServiceCommand = new(Handler.OnLaunchServiceButtonClickAsync);
            KillServiceCommand = new(Handler.OnKillServiceButtonClick);
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
