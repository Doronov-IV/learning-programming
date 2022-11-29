namespace ReversedService.Model.Entities
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


        public DateOnly Date { get; set; }


        public TimeOnly Time { get; set; }


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
