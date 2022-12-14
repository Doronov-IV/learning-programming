using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingAuxiliaryLibrary.Net.Auxiliary.Objects
{
    public class MessageDTO
    {
        public string Sender { get; set; }

        public string Contents { get; set; }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MessageDTO()
        {

        }

    }
}
