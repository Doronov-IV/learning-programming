using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools.Formatting;

namespace NetworkingAuxiliaryLibrary.Objects.Common
{
    public class UserServerSideDTO
    {

        public string CurrentNickname { get; set; }

        public string CurrentPublicId { get; set; }

        public List<ChatDTO>? ChatArray { get; set; }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public UserServerSideDTO()
        {
            ChatArray= new();
        }


        public void SortMessageList()
        {
            foreach (ChatDTO chat in ChatArray)
            {
                chat.Messages.Sort((MessageDTO A, MessageDTO B) =>
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

    }
}
