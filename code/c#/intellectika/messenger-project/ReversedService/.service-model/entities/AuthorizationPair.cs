namespace ReversedService.Model.Entities
{
    public class AuthorizationPair
    {

        public int Id { get; set; }


        public string Login { get; set; } = null!;


        public string PasswordHash { get; set; } = null!;


        public int UserId { get; set; }


        public User User { get; set; } = null!;



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public AuthorizationPair()
        {

        }

    }
}
