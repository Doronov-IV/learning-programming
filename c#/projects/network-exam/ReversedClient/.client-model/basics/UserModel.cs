using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversedClient.Model.Basics
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
        public string UID { get; set; } = null!;

    }
}
