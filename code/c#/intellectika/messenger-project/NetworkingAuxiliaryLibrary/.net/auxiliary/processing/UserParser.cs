using NetworkingAuxiliaryLibrary.Objects.Common;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            List<Chat> userSortedChatList = new(user.ChatList);
            SortChats(ref userSortedChatList);

            res.CurrentNickname = user.CurrentNickname;
            res.CurrentPublicId = user.PublicId;
            res.ChatArray = new ChatDTO[userSortedChatList.Count];

            for (int i = 0, iSize = userSortedChatList.Count; i < iSize; i++)
            {
                ChatDTO chatDto = new();
                chatDto.Members = new string[userSortedChatList[i].UserList.Count];
                chatDto.Messages = new MessageDTO[userSortedChatList[i].MessageList.Count];

                ParseChatMembers(ref chatDto, userSortedChatList[i]);

                ParseChatMessages(ref chatDto, userSortedChatList[i]);

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




        #region LOGIC


        /// <summary>
        /// Sort messages in a user's chat list.
        /// <br />
        /// Отсортировать сообщения в списке чатов пользователя.
        /// </summary>
        private static void SortChats(ref List<Chat> sortedChatList)
        {
            foreach (Chat chat in sortedChatList)
            {
                chat.MessageList.Sort((Message A, Message B) =>
                {
                    if (Int32.Parse(StringDateTime.RemoveSeparation(A.Date)) > Int32.Parse(StringDateTime.RemoveSeparation(B.Date))) return 1;
                    else if (Int32.Parse(StringDateTime.RemoveSeparation(A.Date)) < Int32.Parse(StringDateTime.RemoveSeparation(B.Date))) return -1;
                    else
                    {
                        if (Int32.Parse(StringDateTime.RemoveSeparation(A.Time)) > Int32.Parse(StringDateTime.RemoveSeparation(B.Time))) return 1;
                        else if (Int32.Parse(StringDateTime.RemoveSeparation(A.Time)) < Int32.Parse(StringDateTime.RemoveSeparation(B.Time))) return -1;
                        else return 0;
                    }
                });
            }
        }


        #endregion LOGIC






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
