using Prism.Commands;
using Tools.Toolbox;

namespace MasterProject.ViewModel.MainWindow
{
    /// <summary>
    /// View-model of the main window;
    /// <br />
    /// Вью-модель основного окна;
    /// </summary>
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {



        #region COMMANDS


        public DelegateCommand LaunchClientCommand { get; }
        public DelegateCommand LaunchServiceCommand { get; }
        public DelegateCommand LaunchAuthorizerCommand { get; }
        public DelegateCommand KillServiceCommand { get; }
        public DelegateCommand ClearFoldersCommand { get; }
        public DelegateCommand ClearMessagesCommand { get; }
        public DelegateCommand ClearAuthorizationCommand { get; }


        #endregion COMMANDS





        #region XTRACTION


        /// <summary>
        /// Extract networking and tools libraries.
        /// <br />
        /// Вытянуть файлы библиотек "tools" и "networking".
        /// </summary>
        private void ExtractLibraries()
        {
            try
            {
                /// Toolbox

                // target;
                FileInfo dllTargetInfo1 = new(@"..\..\..\..\..\..\..\..\..\..\..\tools\toolbox\Toolbox\bin\Debug\net6.0\Toolbox.dll");
                FileInfo dllTargetInfo2 = new(@"..\..\..\..\..\..\..\..\..\..\..\tools\toolbox\Toolbox\bin\Debug\net6.0\Toolbox.pdb");
                // destination (precomplied dll folder);
                List<DirectoryInfo> toolsExtractorDestinations = new();
                toolsExtractorDestinations.Add(new DirectoryInfo(@"..\..\..\..\..\tools\precompiled-dll"));
                // extracting;
                LibraryExtractor toolsLibraryExtractor = new(new List<FileInfo>() { dllTargetInfo1, dllTargetInfo2 }, toolsExtractorDestinations);
                toolsLibraryExtractor.Extract();


                /// NET

                // target;
                FileInfo targetInfo1 = new("..\\..\\..\\NetworkingAuxiliaryLibrary\\bin\\Release\\net6.0\\NetworkingAuxiliaryLibrary.dll");
                FileInfo targetInfo2 = new("..\\..\\..\\NetworkingAuxiliaryLibrary\\bin\\Release\\net6.0\\NetworkingAuxiliaryLibrary.pdb");
                // destination 1 (client);
                List<DirectoryInfo> extractorDestinations = new();
                extractorDestinations.Add(new DirectoryInfo("..\\MessengerClient\\.net"));
                // destination 2 (service);
                extractorDestinations.Add(new DirectoryInfo("..\\MessengerService\\.net"));
                // destination 3 (authorizer);
                extractorDestinations.Add(new DirectoryInfo("..\\AuthorizationService\\.net"));
                // extracting;
                LibraryExtractor networkingLibraryExtractor = new(new List<FileInfo>() { targetInfo1, targetInfo2}, extractorDestinations);
                networkingLibraryExtractor.Extract();
            }
            catch { }
        }


        #endregion XTRACTION





        #region CONSTRUCTION




        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {
            ExtractLibraries();
            //CreateNetworkingConfigFile();

            LaunchClientCommand = new(OnLaunchClientButtonClickAsync);
            LaunchServiceCommand = new(OnLaunchServiceButtonClickAsync);
            LaunchAuthorizerCommand = new(OnLaunchAuthorizerButtonClick);
            KillServiceCommand = new(OnKillServiceButtonClick);
            ClearFoldersCommand = new(OnClearFoldersButtonClickInParallel);
            ClearMessagesCommand = new(OnClearMessagesButtonClick);
            ClearAuthorizationCommand = new(OnClearAutorizationButtonClick);
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
