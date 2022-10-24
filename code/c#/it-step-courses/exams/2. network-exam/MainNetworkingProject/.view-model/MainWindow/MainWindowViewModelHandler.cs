
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace MainNetworkingProject.ViewModel.MainWindow
{
    public partial class MainWindowViewModel
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
            // service ".files" folder;
            DirectoryInfo serviceFileDirectory = new("..\\..\\..\\..\\ReversedService\\.files");
            foreach (FileInfo file in serviceFileDirectory.GetFiles())
            {   
                try
                {
                    File.Delete(file.FullName);
                }
                catch { }
            }


            // service "Downloads" folder;
            DirectoryInfo downloadsDirectory = new("..\\..\\..\\..\\ReversedService\\Downloads");
            foreach (FileInfo file in downloadsDirectory.GetFiles())
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch { }
            }


            // clients ".files" folder;
            DirectoryInfo clientFileDirectory = new("..\\..\\..\\..\\ReversedClient\\.files");
            foreach (FileInfo file in clientFileDirectory.GetFiles())
            {
                try
                {
                    File.Delete(file.FullName);
                }
                catch { }
            }
        }


        #endregion HANDLERS


    }
}
