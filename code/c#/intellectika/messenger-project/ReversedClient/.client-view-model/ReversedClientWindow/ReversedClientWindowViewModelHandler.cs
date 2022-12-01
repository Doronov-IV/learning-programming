using ReversedClient.client_view;
using ReversedClient.Model.Basics;
using ReversedClient.ViewModel.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ReversedClient.ViewModel.ClientChatWindow
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
            var uid = serviceTransmitter.PacketReader.ReadMessage().Message;
            var user = OnlineMembers.Where(x => x.PublicId.Equals(uid)).FirstOrDefault();

            // foreach (var user in )
            Application.Current.Dispatcher.Invoke(() => OnlineMembers.Remove(user));   // removing disconnected user;
        }



        /// <summary>
        /// Recieve user message;
        /// <br />
        /// Получить сообщение от пользователя;
        /// </summary>
        private void RecieveMessage()
        {
            try 
            {
                var msg = serviceTransmitter.PacketReader.ReadMessage(); // reading new message via our packet reader;
                var msgCopy = msg;
                if (currentUser.UserName != msg.Sender) // if the message was sent to us from other user
                {
                    var someChat = ChatList.FirstOrDefault(c => c.Addressee.PublicId == msg.Sender);
                    if (someChat is null)
                    {
                        someChat = new(addressee: OnlineMembers.First(u => u.UserName == msg.Sender), addresser: CurrentUser);
                        Application.Current.Dispatcher.Invoke(() => ChatList.Add(someChat));
                    }
                    Application.Current.Dispatcher.Invoke(() => someChat.AddIncommingMessage(msgCopy.Message as string));
                }
                else // if we sent this message
                {
                    var someChat = ChatList.FirstOrDefault(c => c.Addressee.PublicId.Equals(msg.Reciever));
                    Application.Current.Dispatcher.Invoke(() => someChat.AddOutgoingMessage(msg.Message as string));
                }
            }
            catch (Exception ex)
            {

            }
        }



        /// <summary>
        /// Recieve a file from the network stream.
        /// <br />
        /// Получить файл из сетевого стрима.
        /// </summary>
        private void RecieveFile()
        {
            Application.Current.Dispatcher.Invoke(() => activeChat.MessageList.Add("File recieved."));
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
                UserName = serviceTransmitter.PacketReader.ReadMessage().Message as string,
                PublicId = serviceTransmitter.PacketReader.ReadMessage().Message as string,
            };

            /*
             
           [!] In case there's no such user in collection we add them manualy;
            To prevent data duplication;
            
             */

            MessengerChat newChat;
            if (!OnlineMembers.Any(x => x.PublicId == user.PublicId))
            {
                if (user.PublicId != currentUser.PublicId)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        OnlineMembers.Add(user);
                    });
                }
            }
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
                if (_dialogService.OpenFileDialog())
                {
                    UserFile = new(_dialogService.FilePath);
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowMessage(ex.Message);
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
            try
            {
                if (Message != string.Empty)
                {
                    serviceTransmitter.SendMessageToServer(new TextMessagePackage(currentUser.UserName, SelectedOnlineMember.UserName, Message));
                    var someChat = ChatList.FirstOrDefault(c => c.Addressee == SelectedOnlineMember);
                    if (someChat is null)
                    {
                        someChat = new(addresser: CurrentUser, addressee: SelectedOnlineMember);
                        ChatList.Add(someChat);
                    }
                    Message = string.Empty;
                }

                if (UserFile != null)
                {
                    serviceTransmitter.SendFileToServerAsync(new FileMessagePackage(currentUser.UserName, ActiveChat.Addressee.UserName, UserFile));
                    Application.Current.Dispatcher.Invoke(() => activeChat.MessageList.Add($"File sent."));
                    UserFile = null;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Please, choose a contact.", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }



        /// <summary>
        /// Connect to service.
        /// <br />
        /// Подключиться к сервису.
        /// </summary>
        private void ConnectToService()
        {
            serviceTransmitter.ConnectToServer(currentUser.UserName, Login, Pass);
        }


        
        /// <summary>
        /// Show exception output message.
        /// <br />
        /// Показать вывод сообщения ошибки/исключения.
        /// </summary>
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
                if (!string.IsNullOrEmpty(Pass) && !string.IsNullOrEmpty(Login))
                {
                    CurrentUser.UserName = Login;

                    // Debug feature [!]
                    currentUser.PublicId = currentUser.UserName;

                    _chatWindowReference = new();
                    _loginWindowReference = new();

                    ServiceTransmitter.ConnectToServer(currentUser.UserName, Login, Pass);

                    //// [!] In this particular order;
                    //
                    WindowHeaderString = currentUser.UserName + " - common chat";
                    _chatWindowReference.Show();
                    //

                    _loginWindowReference.Close();
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = _chatWindowReference;
                    // ===============================
                }
                else
                {
                    MessageBox.Show("Neither login nor password should be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to connect.\n\nException: {ex.Message}", "Exception intercepted", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        #endregion HANDLERS




    }
}
