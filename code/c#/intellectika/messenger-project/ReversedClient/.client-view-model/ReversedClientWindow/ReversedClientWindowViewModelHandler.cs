using ReversedClient.client_view;
using ReversedClient.Model.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedClient.ViewModel
{
    public partial class ReversedClientWindowViewModel
    {




        #region LOGIC - internal behavior



        /// <summary>
        /// Remove a user from the client list;
        /// <br />
        /// Удалить пользователя из списка клиентов;
        /// </summary>
        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();

            // foreach (var user in )
            Application.Current.Dispatcher.Invoke(() => Users.Remove(user));   // removing disconnected user;
        }



        /// <summary>
        /// Recieve user message;
        /// <br />
        /// Получить сообщение от пользователя;
        /// </summary>
        private void RecieveMessage()
        {
            string msg = _server.PacketReader.ReadMessage();                   // reading new message via our packet reader;
            Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));    // adding it to the observable collection;
        }



        /// <summary>
        /// Recieve a file from the network stream.
        /// <br />
        /// Получить файл из сетевого стрима.
        /// </summary>
        private void RecieveFile()
        {
            var aaaaaaaa = _server.PacketReader.ReadFile(UserName);
            Application.Current.Dispatcher.Invoke(() => Messages.Add("File recieved."));
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
        }



        /// <summary>
        /// Connect new user;
        /// <br />
        /// Подключить нового пользователя;
        /// </summary>
        public void ConnectUser()
        {
            // create new user instance;
            var user = new UserModel()
            {
                UserName = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage(),
            };

            /*
             
           [!] In case there's no such user in collection we add them manualy;
            To prevent data duplication;
            
             */

            if (!Users.Any(x => x.UID == user.UID))
            {
                Application.Current.Dispatcher.Invoke(() => Users.Add(user));
                Application.Current.Dispatcher.Invoke(() => Messages.Add($"{user.UserName} joins chat."));
            }
        }


        // Does not work!!!!
        public void DisconnectFromServer()
        {
            Application.Current.MainWindow.Close();

            loginWindowReference.Show();

            Application.Current.MainWindow = loginWindowReference;
            
            chatWindowReference = new();
            //
        }



        /// <summary>
        /// Open file dialog to choose a file to send.
        /// <br />
        /// Открыть файловый диалог, чтобы выбрать файл к отправке.
        /// </summary>
        public void SelectFile()
        {
            try
            {
                if (_DialogService.OpenFileDialog())
                {
                    UserFile = new(_DialogService.FilePath);
                }
            }
            catch (Exception ex)
            {
                _DialogService.ShowMessage(ex.Message);
            }
        }



        /// <summary>
        /// Send a message to the service;
        /// <br />
        /// Is needed to nullify the chat message field after sending;
        /// <br />
        /// <br />
        /// Отправить сообщение на сервис;
        /// <br />
        /// Необходимо, чтобы стереть сообщение после отправкиж
        /// </summary>
        private void SendMessage()
        {
            if (Message != string.Empty)
            {
                _server.SendMessageToServer(Message);
                Message = string.Empty;
            }

            if (UserFile != null)
            {
                _server.SendFileToServerAsync(UserFile);
                Application.Current.Dispatcher.Invoke(() => Messages.Add($"File sent."));
                UserFile = null;
            }
        }


        private void ConnectToService()
        {
            _server.ConnectToServer(UserName);
        }



        private void ShowErrorMessage(string sMessage)
        {
            MessageBox.Show(sMessage, "Exception", MessageBoxButton.OK, MessageBoxImage.Information);
        }



        #endregion LOGIC - internal behavior






        #region HANDLERS



        /// <summary>
        /// Handle the 'Sign In' button;
        /// <br />
        /// Обработать клик по кнопке 'Sign In';
        /// </summary>
        public void OnSignInButtonClick()
        {
            try
            {
                chatWindowReference = new();
                loginWindowReference = new();

                Server.ConnectToServer(UserName);

                //// [!] In this particular order;
                //
                WindowHeaderString = UserName + " - common chat";
                chatWindowReference.Show();
                //
                loginWindowReference.Close();

                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = chatWindowReference;
                // ===============================
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to connect.\n\nException: {ex.Message}", "Exception intercepted", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        /// <summary>
        /// Users collection changed handler.
        /// <br />
        /// Обработчик изменения коллекции пользователей.
        /// </summary>
        public void OnUsersCollectionChanged(object? sender, EventArgs e)
        {
            if (Users.Count > 1) TheMembersString = "members";
            else TheMembersString = "member";
        }



        #endregion HANDLERS




    }
}
