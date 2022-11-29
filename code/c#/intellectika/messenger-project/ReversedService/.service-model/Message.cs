using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedService.Model
{
    /// <summary>
    /// An entity of a 'Messages' table.
    /// <br />
    /// Представитель таблицы "Messages".
    /// </summary>
    public class Message
    {

        public int SenderId { get; set; }


        public int RecieverId { get; set; }


        public string MessageContents { get; set; } = null!;



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Message()
        {

        }

    }
}
