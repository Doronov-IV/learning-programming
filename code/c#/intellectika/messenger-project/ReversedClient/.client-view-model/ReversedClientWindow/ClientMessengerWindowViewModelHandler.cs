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
using System.Windows.Interop;
using Newtonsoft.Json;

namespace ReversedClient.ViewModel.ClientChatWindow
{
    public partial class ClientMessengerWindowViewModel
    {




        #region Transmition handlers



        /// <summary>
        /// Remove a user from the client list;
        /// <br />
        /// Удалить пользователя из списка клиентов;
        /// </summary>
        private void RemoveUser()
        {
            var uid = _serviceTransmitter.MessengerPacketReader.ReadMessage().Message;
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
            var msg = JsonConvert.DeserializeObject<JsonMessagePackage>(_serviceTransmitter.MessengerPacketReader.ReadJsonMessage());
            var msgCopy = msg;

            try 
            {
                // if the _message was sent to us from other user
                if (_currentUserModel.PublicId != msg.Sender)
                {
                    var someChat = ChatList.Where(c => c.Addressee.PublicId == msg.Sender).FirstOrDefault();
                    if (someChat is null)
                    {
                        someChat = new(addressee: OnlineMembers.First(u => u.PublicId == msg.Sender), addresser: CurrentUserModel);
                        Application.Current.Dispatcher.Invoke(() => ChatList.Add(someChat));
                    }

                    Application.Current.Dispatcher.Invoke(() => someChat.AddIncommingMessage(msgCopy.Message as string));
                }
                // if we sent this _message
                else
                {
                    VisualizeOutgoingMessage(msgCopy);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Message collection changing exception: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DeleteMessageAfterServiceRespond()
        {
            var msg = JsonConvert.DeserializeObject<JsonMessagePackage>(_serviceTransmitter.MessengerPacketReader.ReadJsonMessage());
            var msgCopy = msg;
            try
            {
                var someChat = ChatList.Where(c => c.MessageList.Contains(msg.Message)).FirstOrDefault();
                MessengerChat someChatCopy = new(addresser: someChat.Addresser, addressee: someChat.Addressee);
                if (someChat is not null)
                {
                    foreach (string message in someChat.MessageList)
                    {
                        if (!(message.Contains(StringDateTime.FromThreeToTwoSections(msg.Time)) && message.Contains(msg.Message as string)))
                            someChatCopy.MessageList.Add(message);
                    }

                    someChat = someChatCopy;
                    OnPropertyChanged(nameof(ChatList));
                }
                else MessageBox.Show($"Error. Chat not found. (transmitter, VM-handler)", "Unexpected scenario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message} (transmitter, VM-handler)", "Unexpected exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void InitiateMessageDeletion()
        {
            var message = MessengerChat.FromClientChatMessageToPackageMessage(SelectedMessage);

            var chat = acceptedUserData.ChatArray.Where(c => c == c.Messages.Where(m => m.Contents.Equals(message))).FirstOrDefault();

            MessageDTO dto = chat.Messages.Where(m => m.Contents.Equals(message)).FirstOrDefault();

            TextMessagePackage pack = new(sender: ActiveChat.Addresser.PublicId, reciever: ActiveChat.Addressee.PublicId, date: dto.Date, time: dto.Time,  message: message);

            _serviceTransmitter.SendMessageDeletionToServer(pack);
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
                UserName = _serviceTransmitter.MessengerPacketReader.ReadMessage().Message as string,
                PublicId = _serviceTransmitter.MessengerPacketReader.ReadMessage().Message as string,
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

                    _serviceTransmitter.SendMessageToServer(new TextMessagePackage(_currentUserModel.PublicId, currentAddressee.PublicId, date: DateTime.Now.ToString("dd.MM.yyyy"), time: DateTime.Now.ToString("HH:MM:ss"), Message));
                    var someChat = ChatList.FirstOrDefault(c => (c.Addressee.PublicId.Equals(currentAddressee.PublicId)));
                    if (someChat is null)
                    {
                        someChat = new(addresser: CurrentUserModel, addressee: SelectedOnlineMember);
                        ChatList.Add(someChat);
                    }
                    someChat.AddOutgoingMessage(Message);
                    ActiveChat = someChat;

                    MessageDTO dto = new();
                    var chatDto = acceptedUserData.ChatArray.Where(c => c.Members.ToList().Contains(ActiveChat.Addressee.PublicId)).FirstOrDefault();
                    dto.Time = DateTime.Now.ToString("HH:mm:ss");
                    dto.Date = DateTime.Now.ToString("dd.MM.yyyy");
                    dto.Sender = ActiveChat.Addresser.PublicId;
                    dto.Contents = Message;
                    chatDto.Messages.ToList().Add(dto);

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


        #endregion Transmition handlers





        #region LOGIC



        /// <summary>
        /// Add or sign out and outhoing message sent by client on recieving from service.
        /// <br />
        /// Добавить или отметить исходящее сообщение, отправленное клиентом, при получении его от сервиса.
        /// </summary>
        private void VisualizeOutgoingMessage(MessagePackage msg)
        {
            var someChat = ChatList.FirstOrDefault(c => c.Addressee.PublicId.Equals(msg.Reciever));
            string newMessage = string.Empty;
            string oldMessage = string.Empty;


            oldMessage = someChat.MessageList.Select(m => m).Where(m => (m.Contains(msg.Sender) && m.Contains(msg.Message as string) && !m.Contains("✓✓"))).FirstOrDefault();
            if (oldMessage is not null)
            {
                newMessage = oldMessage + "✓";
            }
            else Application.Current.Dispatcher.Invoke(() => someChat.AddIncommingMessage((msg.Message as string) + " ✓✓"));


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



        #endregion LOGIC






        #region HANDLERS







        #endregion HANDLERS




    }
}
