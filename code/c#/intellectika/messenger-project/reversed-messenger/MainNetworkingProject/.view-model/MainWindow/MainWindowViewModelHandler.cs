using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json.Nodes;
using System.Threading;
using Tools.Toolbox;

using Newtonsoft.Json;

using NetworkingAuxiliaryLibrary.Dependencies.Factories;
using NetworkingAuxiliaryLibrary.Net.Config;

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
                    process.StartInfo.FileName = "../../../ReversedClient/bin/Release/net6.0-windows/ReversedClient.exe";
                    process.StartInfo.WorkingDirectory = "../../../ReversedClient/bin/Release/net6.0-windows";
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
                Process.GetProcesses().ToList().Find(n => n.ProcessName == "MessengerService")?.Kill();

                using (var process = new Process())
                {
                    process.StartInfo.FileName = "../../../MessengerService/bin/Release/net7.0/MessengerService.exe";
                    process.StartInfo.WorkingDirectory = "../../../MessengerService/bin/Release/net7.0";
                    process.StartInfo.Arguments = "-noexit";
                    process.StartInfo.CreateNoWindow = false;
                    process.Start();
                }
            });
        }


        public async void OnLaunchAuthorizerButtonClick()
        {
            await Task.Run(() =>
            {
                Process.GetProcesses().ToList().Find(n => n.ProcessName == "AuthorizationService")?.Kill();

                using (var process = new Process())
                {
                    process.StartInfo.FileName = "../../../AuthorizationService/bin/Release/net6.0/AuthorizationServiceProject.exe";
                    process.StartInfo.WorkingDirectory = "../../../AuthorizationService/bin/Release/net6.0";
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
        public void OnClearFoldersButtonClickInParallel()
        {
            DirectoryInfo dirInfo = new("..\\..\\..\\..\\");
            Parallel.ForEach(dirInfo.GetDirectories(), (name) =>
            {
                CreateOrClearDownloads(name.Name);
            });
        }



        public void OnClearMessagesButtonClick()
        {
            using (SqlConnection connection = new("Server=.\\doronoviv;Database=MessengerDatabase;Trusted_Connection=True;Encrypt=false;"))
            {
                connection.Open();
                SqlCommand command = new(
                    "USE MessengerDatabase;" +
                    "DELETE FROM Messages; DBCC CHECKIDENT ('[Messages]', RESEED, 0); " +
                    "DELETE FROM ChatUser;" +
                    "DELETE FROM Chats DBCC CHECKIDENT ('[Chats]', RESEED, 0);"
                    , connection);
                command.ExecuteNonQuery();
            }
        }


        public void OnClearAutorizationButtonClick()
        {
            using (SqlConnection connection = new("Server=.\\doronoviv;Database=MessengerAuthorizationDatabase;Trusted_Connection=True;Encrypt=false;"))
            {
                connection.Open();
                SqlCommand command = new(
                    "USE MessengerAuthorizationDatabase; DELETE FROM Users; DBCC CHECKIDENT ('[Users]', RESEED, 0);"
                    , connection);
                command.ExecuteNonQuery();
            }
        }


        #endregion HANDLERS




        #region MISC



        /// <summary>
        /// Create folder "Downloads" in all networking projects. If the folder is already present, clear it.
        /// <br />
        /// Создать папку "Downloads" во всех сетевых проектах Если папка уже есть, очистить её.
        /// </summary>
        /// <param name="projectFolderName">
        /// The relative name of the project folder.
        /// <br />
        /// Короткое имя папки проекта.
        /// </param>
        private void CreateOrClearDownloads(string projectFolderName)
        {
            DirectoryInfo currentDownloadsDirectory = new($"..\\..\\..\\..\\{projectFolderName}\\Downloads");

            if (!Directory.Exists(currentDownloadsDirectory.FullName))
            {
                Directory.CreateDirectory(currentDownloadsDirectory.FullName);
            }
            else
            {
                foreach (FileInfo file in currentDownloadsDirectory.GetFiles())
                {
                    try
                    {
                        File.Delete(file.FullName);
                    }
                    catch { }
                }
            }
        }


        /// <summary>
        /// Create initial JSON network-config file.
        /// <br />
        /// Создать изначальный json-файл сетевого конфига.
        /// </summary>
        private void CreateNetworkingConfigFile()
        {
            List<string> listOfProjects = new();

            DirectoryInfo directoryInfo;

            listOfProjects.AddRange( new List<string>() { "ReversedClient", "MessengerService", "AuthorizationService"});

            NetworkConfiguration config = new();

            config.addresses = new string[3] {  Utilizer.GetLocalIPAddress(), Utilizer.GetLocalIPAddress() , Utilizer.GetLocalIPAddress() };

            var jsonString = JsonConvert.SerializeObject(config);

            listOfProjects.ForEach(p =>
            {
                directoryInfo = new(@$"..\..\..\{p}\.config");

                Directory.CreateDirectory(directoryInfo.FullName);

                string fileName = directoryInfo.FullName + "/network-config.json";

                File.WriteAllText(fileName, jsonString);
               
            });
        }



        #endregion MISC


    }
}
