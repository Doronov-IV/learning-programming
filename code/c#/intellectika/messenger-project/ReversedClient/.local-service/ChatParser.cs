using NetworkingAuxiliaryLibrary.Objects.Common;
using ReversedClient.Model;

namespace ReversedClient.LocalService
{

    /// <summary>
    /// A service for parsing 'ChatList' property of a 'User' object into a view-model 'ChatList' one.
    /// <br />
    /// Сервис для парсинга свойства "ChatList" объекта класса "User" в свойство "ChatList" вью-модели.
    /// </summary>
    public static class ChatParser
    {

        /// <summary>
        /// Parse chat list of a user into an view-model observable collection.
        /// <br />
        /// Спарсить список чатов пользователя в ObservableCollection вью-модели.
        /// </summary>
        public static void FillChats(UserServerSideDTO user, ref ObservableCollection<MessengerChat> ChatList)
        {
            if (user.ChatArray is not null && user.ChatArray.Length != 0)
            {

                foreach (ChatDTO chat in user.ChatArray)
                {
                    var userRef = chat.Members.Select(u => u).Where(u => !u.Equals(user.CurrentPublicId)).FirstOrDefault();
                    var chatRef = new MessengerChat(addresser: new(user.CurrentNickname, user.CurrentPublicId), addressee: new(userRef, userRef));

                    foreach (var message in chat.Messages)
                    {

                        if (message.Sender.Equals(user.CurrentPublicId)) chatRef.AddCheckedOutgoingMessage(message);
                        else chatRef.AddIncommingMessage(message);
                    }

                    ChatList.Add(chatRef);
                }
            }
        } 

    }
}
