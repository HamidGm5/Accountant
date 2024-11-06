using Accountant.API.Entities;

namespace Accountant.API.Repository.Interfaces
{
    public interface IAdminRepository
    {
        Task<ICollection<Admin>> GetAdmins();
        Task<Admin> GetAdminById(int id);
        Task<Admin> GetAdminByAlias(string AliasName);
        Task<ICollection<User>> GetUsers();
        Task<User> GetUserByUsernameOrEmail(string spec);
        Task<bool> UpdateUserPassword(string spec , string password);
        Task<bool> DeleteUser(string spec);
        Task<bool> Save();
    }
}
