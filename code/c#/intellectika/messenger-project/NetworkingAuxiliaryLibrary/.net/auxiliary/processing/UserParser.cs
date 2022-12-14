using NetworkingAuxiliaryLibrary.Net.Auxiliary.Objects;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingAuxiliaryLibrary.Net.Auxiliary.Processing
{
    public static class UserParser
    {

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
                
                for (int j = 0, jSize = user.ChatList[i].UserList.Count; j < jSize; j++)
                {
                    chatDto.Members[j] = user.ChatList[i].UserList[j].PublicId;
                }

                for (int j = 0, jSize = user.ChatList[i].MessageList.Count; j < jSize; j++)
                {
                    chatDto.Messages[j] = new MessageDTO();
                    chatDto.Messages[j].Sender = user.ChatList[i].MessageList[j].Author.PublicId;
                    chatDto.Messages[j].Contents = user.ChatList[i].MessageList[j].Contents;
                }

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

                    tempMessage.Chat = complexChat;

                    tempMessage.Contents = message.Contents;

                    complexChat.MessageList.Add(tempMessage);

                    if (res.PublicId.Equals(tempMessage.Author.PublicId)) res.MessageList.Add(tempMessage);
                }
                res.ChatList.Add(complexChat);
            }
            return res;
        }
    }
}
