using NetworkingAuxiliaryLibrary.Net.Auxiliary.Objects;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools.Formatting;

namespace NetworkingAuxiliaryLibrary.Net.Auxiliary.Processing
{
    public static class UserParser
    {

        #region API

        public static UserServerSideDTO ParseToDTO(User user)
        {
            UserServerSideDTO res = new();

            res.CurrentNickname = user.CurrentNickname;
            res.CurrentPublicId = user.PublicId;
            res.ChatArray = new ChatDTO[user.ChatList.Count];

            for (int i = 0, iSize = user.ChatList.Count; i < iSize; i++)
            {
                ChatDTO chatDto = new();
                chatDto.Members = new string[user.ChatList[i].UserList.Count];
                chatDto.Messages = new MessageDTO[user.ChatList[i].MessageList.Count];

                ParseChatMembers(ref chatDto, user.ChatList[i]);

                ParseChatMessages(ref chatDto, user.ChatList[i]);

                res.ChatArray[i] = chatDto;
            }
            return res;
        }


        public static User ParseFromDTO(UserServerSideDTO user)
        {
            User res = new();

            res.CurrentNickname = user.CurrentNickname;
            res.PublicId = user.CurrentPublicId;

            foreach (var chatDto in user.ChatArray)
            {
                Chat complexChat = new();

                foreach (string member in chatDto.Members)
                {
                    User tempUser = new();
                    tempUser.PublicId = member;
                    complexChat.UserList.Add(tempUser);
                }

                foreach (var message in chatDto.Messages)
                {
                    Message tempMessage = new();
                    tempMessage.Author = complexChat.UserList.First(m => m.PublicId.Equals(message.Sender));
                    tempMessage.Date = message.Date;
                    tempMessage.Time = StringDateTime.FromThreeToTwoSections(message.Time);

                    tempMessage.Chat = complexChat;

                    tempMessage.Contents = message.Contents;

                    complexChat.MessageList.Add(tempMessage);

                    if (res.PublicId.Equals(tempMessage.Author.PublicId)) res.MessageList.Add(tempMessage);
                }
                res.ChatList.Add(complexChat);
            }
            return res;
        }


        #endregion API






        #region SERIALIZATION



        private static void ParseChatMembers(ref ChatDTO chatDto, Chat unparsedChat)
        {
            for (int j = 0, jSize = unparsedChat.UserList.Count; j < jSize; j++)
            {
                chatDto.Members[j] = unparsedChat.UserList[j].PublicId;
            }
        }


        private static void ParseChatMessages(ref ChatDTO chatDto, Chat unparsedChat)
        {
            for (int j = 0, jSize = unparsedChat.MessageList.Count; j < jSize; j++)
            {
                chatDto.Messages[j] = new MessageDTO();
                chatDto.Messages[j].Sender = unparsedChat.MessageList[j].Author.PublicId;
                chatDto.Messages[j].Contents = unparsedChat.MessageList[j].Contents;
                chatDto.Messages[j].Date = unparsedChat.MessageList[j].Date;
                chatDto.Messages[j].Time = StringDateTime.FromThreeToTwoSections(unparsedChat.MessageList[j].Time);
            }
        }



        #endregion SERIALIZATION






        #region SERIALIZATION



        //



        #endregion SERIALIZATION
    }
}
