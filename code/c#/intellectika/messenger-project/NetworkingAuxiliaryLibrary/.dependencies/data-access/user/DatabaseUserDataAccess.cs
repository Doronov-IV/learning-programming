using NetworkingAuxiliaryLibrary.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingAuxiliaryLibrary.Dependencies.DataAccess
{
    public class DatabaseUserDataAccess : IUserDataAccess
    {

        public virtual User GetUserData(string login)
        {
            throw new NotImplementedException();
        }

    }
}
