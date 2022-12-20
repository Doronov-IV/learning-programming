using NetworkingAuxiliaryLibrary.Objects.Entities;
using System.Runtime.CompilerServices;
using System.Windows.Media.Converters;
using ReversedClient.LocalService;
using System.Collections.Generic;
using ReversedClient.client_view;
using System.Windows.Threading;
using System.Threading.Tasks;
using ReversedClient.Model;
using System.Linq;
using System.Text;
using System;

using Tools.Formatting;
using NetworkingAuxiliaryLibrary.Objects.Common;

namespace ReversedClient.ViewModel.ClientChatWindow
{
    public partial class ClientMessengerWindowViewModel
    {




        #region LOGIC - internal behavior



        /// <summary>
        /// Remove a user from the client list;
        /// <br />
        /// Удалить пользователя из списка клиентов;
        /// </summary>
        private void RemoveUser()
        {
            var uid = _serviceTransmitter.MessangerPacketReader.ReadMessage().Message;
            var user = OnlineMembers.Where(x => x.PublicId.Equals(uid)).FirstOrDefault();

            // foreach (var user in )
            Application.Current.Dispatcher.Invoke(() => OnlineMembers.Remove(user));   // removing disconnected user;
        }



        /// <summary>
        /// Recieve user _message;
        /// <br />
        /// Получить сообщение от пользователя;
        /// </summary>
        private void RecieveMessage()
        {
            try 
            {
                var msg = _serviceTransmitter.MessangerPacketReader.ReadMessage(); // reading new _message via our packet reader;
                var msgCopy = msg;
                if (_currentUserModel.PublicId != msg.Sender) // if the _message was sent to us from other user
                {
                    var someChat = ChatList.FirstOrDefault(c => c.Addressee.PublicId == msg.Sender);
                    if (someChat is null)
                    {
                        someChat = new(addressee: OnlineMembers.First(u => u.PublicId == msg.Sender), addresser: CurrentUserModel);
                        Application.Current.Dispatcher.Invoke(() => ChatList.Add(someChat));
                    }

                    Application.Current.Dispatcher.Invoke(() => someChat.AddIncommingMessage(msgCopy.Message as string));
                }
                else // if we sent this _message
                {
                    var someChat = ChatList.FirstOrDefault(c => c.Addressee.PublicId.Equals(msg.Reciever));
                    string newMessage = string.Empty;
                    string oldMessage = string.Empty;
                    foreach (var message in someChat.MessageList)
                    {
                        if (message.Contains(msg.Message as string) && message.Contains(msg.Sender))
                        {
                            newMessage = message + "✓";
                            oldMessage = message;
                        }
                    }

                    ObservableCollection<string> newMessageList = new();
                    foreach (string message in someChat.MessageList)
                    {
                        if (!message.Equals(oldMessage))
                        {
                            newMessageList.Add(message);
                        }
                        else
                        {
                            newMessageList.Add(newMessage);
                        }
                    }
                    someChat.MessageList = newMessageList;

                    OnPropertyChanged(nameof(ActiveChat));
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
            var user = new UserClientPublicDTO()
            {
                UserName = _serviceTransmitter.MessangerPacketReader.ReadMessage().Message as string,
                PublicId = _serviceTransmitter.MessangerPacketReader.ReadMessage().Message as string,
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
        /// Send a _message to the service;
        /// <br />
        /// Is needed to nullify the chat _message field after sending;
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
                    UserClientPublicDTO currentAddressee = null;
                    if (SelectedOnlineMember is not null) currentAddressee = SelectedOnlineMember;
                    else if (ActiveChat is not null) currentAddressee = ActiveChat.Addressee;
                    else throw new NullReferenceException("[Custom] No target was selected.");

                    _serviceTransmitter.SendMessageToServer(new TextMessagePackage(_currentUserModel.PublicId, currentAddressee.PublicId, Message));
                    var someChat = ChatList.FirstOrDefault(c => (c.Addressee.PublicId.Equals(currentAddressee.PublicId)));
                    if (someChat is null)
                    {
                        someChat = new(addresser: CurrentUserModel, addressee: SelectedOnlineMember);
                        ChatList.Add(someChat);
                    }
                    someChat.AddOutgoingMessage(Message);
                    ActiveChat = someChat;
                    Message = string.Empty;
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
            _serviceTransmitter.ConnectAndAuthorize(CurrentUserTechnicalDTO);
        }


        
        /// <summary>
        /// Show exception output _message.
        /// <br />
        /// Показать вывод сообщения ошибки/исключения.
        /// </summary>
        private void ShowErrorMessage(string sMessage)
        {
            MessageBox.Show(sMessage, "Exception", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void DisconnectFromService()
        {
            WpfWindowsManager.MoveFromChatToLogin(CurrentUserTechnicalDTO.Login);
        }


        #endregion LOGIC - internal behavior






        #region HANDLERS







        #endregion HANDLERS




    }
}
