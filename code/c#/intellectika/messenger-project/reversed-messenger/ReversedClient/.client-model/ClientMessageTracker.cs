using NetworkingAuxiliaryLibrary.Objects.Common;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace ReversedClient.Model
{
    /// <summary>
    /// An object that manages messages in client dto object.
    /// <br />
    /// Объект, который занимается управлением сообщений в DTO клиента.
    /// </summary>
    public class ClientMessageTracker
    {


        #region STATE



        /// <inheritdoc cref="UserTracked"/>
        private UserServerSideDTO? _userTracked;


        /// <summary>
        /// An instance of the client chatlist to track.
        /// <br />
        /// Экземпляр списка чатов клиента для отслеживания.
        /// </summary>
        public UserServerSideDTO? UserTracked
        {
            get { return _userTracked; }
            set { _userTracked = value; }
        }



        #endregion STATE





        #region API



        /// <summary>
        /// Add message to the dto.
        /// <br />
        /// Добавить сообщение в dto.
        /// </summary>
        public void AddMessage(IMessage newMessage)
        {
            if (!IsMessageAlreadyPresent(newMessage))
            {
                MessageDTO dto = new(newMessage.GetSender(), newMessage.GetMessage() as string, DateTime.Now.ToString("dd.MM.yyyy"), DateTime.Now.ToString("HH:mm:ss"));

                var chatDto = UserTracked.ChatArray.Where(c => c.Members.ToList().Contains(newMessage.GetSender()) || c.Members.ToList().Contains(newMessage.GetSender())).FirstOrDefault();

                if (chatDto is null)
                {
                    chatDto = new();

                    chatDto.Members.Add(newMessage.GetSender());
                    chatDto.Members.Add(newMessage.GetReciever());

                    UserTracked.ChatArray.Add(chatDto);
                }

                chatDto.Messages.Add(dto);
            }
        }


        /// <summary>
        /// Delete message from dto.
        /// <br />
        /// Удалить сообщение из dto.
        /// </summary>
        public void DeleteMessage(IMessage deletionMessage)
        {
            ChatDTO? chatInWhichToDelete = null;
            MessageDTO? messageToDelete = null;

            foreach (var chat in UserTracked.ChatArray)
            {
                foreach (var message in chat.Messages)
                {
                    IMessage tempMessage = JsonConvert.DeserializeObject<JsonMessagePackage>(JsonMessageFactory.GetJsonMessage(message.Sender, "n/a", message.Date, message.Time, message.Contents));

                    if (MessageParser.IsMessageIdenticalToAnotherOne(tempMessage, deletionMessage))
                    {
                        messageToDelete = message;
                        chatInWhichToDelete = chat;
                        break;
                    }
                }
            }
            if (messageToDelete is not null) chatInWhichToDelete?.Messages?.Remove(messageToDelete);
            else throw new NullReferenceException("[Custom] The message chosen for deletion was not found in the lists (client tracker, delete messasge method).");
        }


        /// <summary>
        /// 'True' - if message already present in dto, otherwise 'false'.
        /// <br />
        /// "True" - если сообщение присутствует в dto, иначе "false".
        /// </summary>
        public bool IsMessageAlreadyPresent(IMessage message)
        {
            bool bRes = false;

            if (IsChatAlreadyPresent(message))
            {
                UserTracked.ChatArray.ForEach((chat) =>
                {
                    chat.Messages.ForEach((msg) =>
                    {
                        IMessage tempMessage = JsonConvert.DeserializeObject<JsonMessagePackage>(JsonMessageFactory.GetJsonMessage(msg.Sender, "n/a", msg.Date, msg.Time, msg.Contents));

                        if (MessageParser.IsMessageIdenticalToAnotherOne(tempMessage, message))
                        {
                            bRes = true;
                        }
                    });
                });
            }

            return bRes;
        }


        /// <summary>
        /// 'True' - if chat of the message already present in dto, otherwise 'false'.
        /// <br />
        /// "True" - если чат сообщения присутствует в dto, иначе "false".
        /// </summary>
        public bool IsChatAlreadyPresent(IMessage newMessage)
        {
            bool bRes = false;
            foreach (ChatDTO chat in UserTracked.ChatArray)
            {
                bRes = false;
                foreach (var user in chat.Members)
                {
                    if (!(user.Equals(newMessage.GetReciever()) || user.Equals(newMessage.GetSender()))) bRes = false;
                    else bRes = true;
                }
            }
            return bRes;
        }



        #endregion API





        #region CONSTRUCTION



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ClientMessageTracker(UserServerSideDTO userData)
        {
            _userTracked = userData;
        }



        #endregion CONSTRUCTION


    }
}
