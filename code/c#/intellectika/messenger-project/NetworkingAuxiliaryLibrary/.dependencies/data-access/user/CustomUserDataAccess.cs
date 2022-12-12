using NetworkingAuxiliaryLibrary.Dependencies.DataAccess;
using NetworkingAuxiliaryLibrary.Objects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingAuxiliaryLibrary.dependencies.data_access.user
{
    public class CustomUserDataAccess : IUserDataAccess
    {

        public User GetUserData(string publicId)
        {
            return new User { PublicId = publicId };
        }

    }
}
