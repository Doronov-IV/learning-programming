namespace ReversedService.Model.Entities
{
    public class User
    {

        public int Id { get; set; }


        public string PublicId { get; set; } = null!;


        public string CurrentNickname { get; set; } = null!;


        public List<Chat>? ChatList { get; set; }


        public List<Message>? MessageList { get; set; }


        public AuthorizationPair AuthorizationPair { get; set; } = null!;


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public User()
        {
            ChatList = new();
            MessageList = new();
        }
    }
}
