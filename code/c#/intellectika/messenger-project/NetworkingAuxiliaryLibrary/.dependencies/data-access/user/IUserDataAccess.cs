using NetworkingAuxiliaryLibrary.Objects.Entities; 

namespace NetworkingAuxiliaryLibrary.Dependencies.DataAccess
{
    public interface IUserDataAccess
    {
        public User GetUserData(int Id);

    }
}
