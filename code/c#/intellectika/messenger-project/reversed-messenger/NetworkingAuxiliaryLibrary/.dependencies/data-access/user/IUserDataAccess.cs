using NetworkingAuxiliaryLibrary.Objects.Entities;
using System.Security.Cryptography.X509Certificates;

namespace NetworkingAuxiliaryLibrary.Dependencies.DataAccess
{
    public interface IUserDataAccess
    {
        public User GetUserData(string publicId);

    }
}
