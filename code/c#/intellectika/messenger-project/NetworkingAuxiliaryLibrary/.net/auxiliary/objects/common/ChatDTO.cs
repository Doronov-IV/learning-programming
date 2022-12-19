using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingAuxiliaryLibrary.Objects.Common
{
    public class ChatDTO
    {
        public string[]? Members { get; set; }

        public MessageDTO[]? Messages { get; set; }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public ChatDTO()
        {

        }

    }
}
