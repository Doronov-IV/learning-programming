namespace NetworkingAuxiliaryLibrary.Objects.Common
{
    public class UserClientPublicDTO
    {

        /// <summary>
        /// User name;
        /// <br />
        /// Имя пользователя;
        /// </summary>
        public string UserName { get; set; } = null!;


        /// <summary>
        /// User's unique id;
        /// <br />
        /// Уникальный идентификатор пользователя;
        /// </summary>
        public string PublicId { get; set; } = null!;



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public UserClientPublicDTO()
        {

        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public UserClientPublicDTO(string userName, string publicId)
        {
            UserName = userName;
            PublicId = publicId;
        }
    }
}
