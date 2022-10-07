using MainNetworkingProject.view;
using System.Threading;

namespace MainNetworkingProject.ViewModel.MainWindow
{
    public partial class MainWindowViewModel
    {
        public class MainWindowViewModelHandler : INotifyPropertyChanged
        {


            #region HANDLERS


            public async void OnLaunchClientButtonClickAsync()
            {
                Thread ClientThread = new Thread(() =>
                {
                    MainNetworkingProject.view.ClientWindow client = new();
                    client.Show();
                    System.Windows.Threading.Dispatcher.Run();
                });
                ClientThread.SetApartmentState(ApartmentState.STA);
                ClientThread.Start();
            }


            public async void OnLaunchServiceButtonClickAsync()
            {
                Thread ServiceThread = new Thread(() =>
                {
                    MainNetworkingProject.view.ServiceWindow server = new();
                    server.Show();
                    System.Windows.Threading.Dispatcher.Run();
                });
                ServiceThread.SetApartmentState(ApartmentState.STA);
                ServiceThread.Start();
            }


            #endregion HANDLERS




            #region LOGIC





            #endregion LOGIC




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
            public MainWindowViewModelHandler()
            {

            }



            #endregion CONSTRUCTION


        }
    }
}
