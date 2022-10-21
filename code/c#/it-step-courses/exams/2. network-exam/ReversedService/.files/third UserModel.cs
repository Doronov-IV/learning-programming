using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.MVVM.Model
{
    public class UserModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; } = null!;    

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UID { get; set; } = null!; 

    }
}
