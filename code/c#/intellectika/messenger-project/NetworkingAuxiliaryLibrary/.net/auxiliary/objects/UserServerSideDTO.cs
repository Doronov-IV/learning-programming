using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingAuxiliaryLibrary.Net.Auxiliary.Objects
{
    public class UserServerSideDTO
    {

        public string CurrentNickname { get; set; }

        public string CurrentPublicId { get; set; }

        public ChatDTO[]? ChatArray { get; set; }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public UserServerSideDTO()
        {

        }

    }
}
