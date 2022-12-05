namespace NetworkingAuxiliaryLibrary.Objects
{
    /// <summary>
    /// An object to encapsulate user data transfered to various windows.
    /// <br />
    /// Объект для инкапсуляции пользовательских данных для передачи разным окнам.
    /// </summary>
    public class UserDTO
    {


        #region STATE



        /// <inheritdoc cref="Login"/>
        private string login;


        /// <inheritdoc cref="Password"/>
        private string password;



        /// <summary>
        /// The login of the user.
        /// <br />
        /// Логин пользователя.
        /// </summary>
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
            }
        }


        /// <summary>
        /// The password of the user.
        /// <br />
        /// Пароль пользователя.
        /// </summary>
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
            }
        }



        #endregion STATE




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public UserDTO()
        {
            login = string.Empty;
            password= string.Empty;
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметрами.
        /// </summary>
        public UserDTO(string login, string password)
        {
            this.password = password;
            this.login = login;
        }



        #endregion CONSTRUCTION


    }
}
