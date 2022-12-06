namespace NetworkingAuxiliaryLibrary.Objects.Entities
{
    public class Message
    {

        public int Id { get; set; }


        /// <summary>
        /// Contents if the message.
        /// <br />
        /// Содержимое сообщения.
        /// </summary>
        public string Contents { get; set; }


        public int AuthorId { get; set; }


        public string Date { get; set; } = null!;


        public string Time { get; set; } = null!;


        public User Author { get; set; } = null!;


        public Chat Chat { get; set; } = null!;


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
