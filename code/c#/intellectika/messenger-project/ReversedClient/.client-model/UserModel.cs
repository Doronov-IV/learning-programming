﻿namespace ReversedClient.Model.Basics
{
    public class UserModel
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

    }
}
