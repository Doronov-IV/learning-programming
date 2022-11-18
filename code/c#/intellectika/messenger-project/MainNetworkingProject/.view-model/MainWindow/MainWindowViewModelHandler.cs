using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace MainNetworkingProject.ViewModel.MainWindow
{
    /// <summary>
    /// The main window view-model.
    /// <br />
    /// Вью-модель основного окна (main window).
    /// </summary>
    public partial class MainWindowViewModel
    {


        #region HANDLERS


        /// <summary>
        /// Launch client button click event handler.
        /// <br />
        /// Обработчик нажатия кнопки "запустить клиент".
        /// </summary>
        public async void OnLaunchClientButtonClickAsync()
        {
            await Task.Run(() =>
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = "../../../../ReversedClient/bin/Release/net6.0-windows/ReversedClient.exe";
                    process.StartInfo.WorkingDirectory = "../../../../ReversedClient/bin/Release/net6.0-windows";
                    process.StartInfo.Arguments = "-noexit";
                    process.StartInfo.CreateNoWindow = false;
                    process.Start();
                }
            });
        }


        /// <summary>
        /// Launch service button click event handler.
        /// <br />
        /// Обработчик нажатия кнопки "запустить сёрвис".
        /// </summary>
        public async void OnLaunchServiceButtonClickAsync()
        {
            await Task.Run(() =>
            {
                Process.GetProcesses().ToList().Find(n => n.ProcessName == "ReversedService")?.Kill();

                using (var process = new Process())
                {
                    process.StartInfo.FileName = "../../../../ReversedService/bin/Release/net6.0-windows/ReversedService.exe";
                    process.StartInfo.WorkingDirectory = "../../../../ReversedService/bin/Release/net6.0-windows";
                    process.StartInfo.Arguments = "-noexit";
                    process.StartInfo.CreateNoWindow = false;
                    process.Start();
                }
            });
        }


        /// <summary>
        /// Kill service process (obsolete but needed to be kept anyway).
        /// <br />
        /// Отменить процесс сервиса (устарел, но необходимо оставить в любом случае).
        /// </summary>
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


        /// <summary>
        /// Clear service and clients' files folders.
        /// <br />
        /// Очистить папаки ".files" клиентов и сервиса.
        /// </summary>
        public void OnClearFoldersButtonClick()
        {
            // service "Downloads" folder;
            DirectoryInfo serviceDownloadsDirectory = new("..\\..\\..\\..\\ReversedService\\Downloads");

            if (!Directory.Exists(serviceDownloadsDirectory.FullName))
            {
                Directory.CreateDirectory(serviceDownloadsDirectory.FullName);
            }
            else
            {
                foreach (FileInfo file in serviceDownloadsDirectory.GetFiles())
                {
                    try
                    {
                        File.Delete(file.FullName);
                    }
                    catch { }
                }
            }


            // client "Downloads" folder;
            DirectoryInfo clientDownloadsDirectory = new("..\\..\\..\\..\\ReversedClient\\Downloads");

            if (!Directory.Exists(clientDownloadsDirectory.FullName))
            {
                Directory.CreateDirectory(clientDownloadsDirectory.FullName);
            }
            else
            {
                foreach (FileInfo file in clientDownloadsDirectory.GetFiles())
                {
                    try
                    {
                        File.Delete(file.FullName);
                    }
                    catch { }
                }
            }
        }


        #endregion HANDLERS


    }
}
