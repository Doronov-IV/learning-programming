
using System.Diagnostics;
using System.Runtime.InteropServices;
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
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "../../../../ReversedClient/bin/Debug/net6.0-windows/ReversedClient.exe";
                    process.StartInfo.WorkingDirectory = "../../../../ReversedClient/bin/Debug/net6.0-windows";
                    process.StartInfo.Arguments = "-noexit";
                    process.StartInfo.CreateNoWindow = false;
                    process.Start();
                }
            }


            public async void OnLaunchServiceButtonClickAsync()
            {
                await Task.Run(() =>
                {
                    Process.GetProcesses().ToList().Find(n => n.ProcessName == "ReversedService")?.Kill();

                    using (var process = new Process())
                    {
                        process.StartInfo.FileName = "../../../../ReversedService/bin/Debug/net6.0-windows/ReversedService.exe";
                        process.StartInfo.WorkingDirectory = "../../../../ReversedService/bin/Debug/net6.0-windows";
                        process.StartInfo.Arguments = "-noexit";
                        process.StartInfo.CreateNoWindow = false;
                        process.Start();
                    }
                });
            }



            public void OnKillServiceButtonClick()
            {
                try
                {
                    var dupeProcess = Process.GetProcesses().ToList().Find(n => n.ProcessName == "ReversedService");
                    dupeProcess?.Kill();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"The service is unavailable or down.\nException: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
