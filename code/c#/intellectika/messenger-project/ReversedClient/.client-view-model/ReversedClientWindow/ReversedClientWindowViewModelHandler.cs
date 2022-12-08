using ReversedClient.client_view;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using System.Runtime.CompilerServices;
using System.Windows.Media.Converters;
using ReversedClient.ViewModel.Misc;
using ReversedClient.LocalService;
using ReversedClient.Model.Basics;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

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
            var uid = serviceTransmitter.MessangerPacketReader.ReadMessage().Message;
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
                var msg = serviceTransmitter.MessangerPacketReader.ReadMessage(); // reading new message via our packet reader;
                var msgCopy = msg;
                if (_currentUserModel.UserName != msg.Sender) // if the message was sent to us from other user
                {
                    var someChat = ChatList.FirstOrDefault(c => c.Addressee.PublicId == msg.Sender);
                    if (someChat is null)
                    {
                        someChat = new(addressee: OnlineMembers.First(u => u.UserName == msg.Sender), addresser: CurrentUserModel);
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
            Application.Current.Dispatcher.Invoke(() => _activeChat.MessageList.Add("File recieved."));
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
                UserName = serviceTransmitter.MessangerPacketReader.ReadMessage().Message as string,
                PublicId = serviceTransmitter.MessangerPacketReader.ReadMessage().Message as string,
            };

            /*
             
           [!] In case there's no such user in collection we add them manualy;
            To prevent data duplication;
            
             */

            MessengerChat newChat;
            if (!OnlineMembers.Any(x => x.PublicId == user.PublicId))
            {
                if (user.PublicId != _currentUserModel.PublicId)
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
                if (dialogService.OpenFileDialog())
                {
                    UserFile = new(dialogService.FilePath);
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
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
                    serviceTransmitter.SendMessageToServer(new TextMessagePackage(_currentUserModel.UserName, SelectedOnlineMember.UserName, Message));
                    var someChat = ChatList.FirstOrDefault(c => c.Addressee == SelectedOnlineMember);
                    if (someChat is null)
                    {
                        someChat = new(addresser: CurrentUserModel, addressee: SelectedOnlineMember);
                        ChatList.Add(someChat);
                    }
                    Message = string.Empty;
                }

                if (UserFile != null)
                {
                    serviceTransmitter.SendFileToServerAsync(new FileMessagePackage(_currentUserModel.UserName, ActiveChat.Addressee.UserName, UserFile));
                    Application.Current.Dispatcher.Invoke(() => _activeChat.MessageList.Add($"File sent."));
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
            serviceTransmitter.ConnectAndAuthorize(CurrentUserDTO);
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


        private void FillChats(ObservableCollection<MessengerChat> chatList, User user)
        {
            foreach (Chat chat in user.ChatList)
            {
                var usrRef = chat.UserList.Select(u => u).Where(u => !u.PublicId.Equals(user.PublicId)).FirstOrDefault();
                var chatRef = new MessengerChat(addresser: _currentUserModel, addressee: new UserModel(usrRef.CurrentNickname, usrRef.PublicId));

                foreach (var message in chat.MessageList)
                {
                    if (message.AuthorId.Equals(_currentUserModel.PublicId)) chatRef.AddOutgoingMessage(message.Contents);
                    else chatRef.AddIncommingMessage(message.Contents);
                }

                chatList.Add(chatRef);
            }
        }


        private void DisconnectFromService()
        {
            WpfWindowsManager.FromChatToLogin(currentServiceSiteUser.Login);
        }


        #endregion LOGIC - internal behavior






        #region HANDLERS







        #endregion HANDLERS




    }
}
