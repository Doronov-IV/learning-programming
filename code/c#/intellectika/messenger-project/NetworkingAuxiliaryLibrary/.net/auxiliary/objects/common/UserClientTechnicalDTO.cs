namespace NetworkingAuxiliaryLibrary.Objects.Common
{
    /// <summary>
    /// An object to encapsulate user data transfered to various windows.
    /// <br />
    /// Объект для инкапсуляции пользовательских данных для передачи разным окнам.
    /// </summary>
    public class UserClientTechnicalDTO
    {


        #region STATE



        /// <inheritdoc cref="Login"/>
        private string _login;


        /// <inheritdoc cref="PublicId"/>
        private string _publicId;


        /// <inheritdoc cref="Password"/>
        private string _password;



        /// <summary>
        /// The login of the user.
        /// <br />
        /// Логин пользователя.
        /// </summary>
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
            }
        }


        /// <summary>
        /// The public id of the user.
        /// <br />
        /// Публичный id пользователя.
        /// </summary>
        public string PublicId
        {
            get { return _publicId; }
            set
            {
                _publicId = value;
            }
        }


        /// <summary>
        /// The password of the user.
        /// <br />
        /// Пароль пользователя.
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }



        #endregion STATE




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public UserClientTechnicalDTO()
        {
            _login = string.Empty;
            _password= string.Empty;
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметрами.
        /// </summary>
        public UserClientTechnicalDTO(string login, string password)
        {
            this._password = password;
            this._login = login;
        }



        #endregion CONSTRUCTION


    }
}
