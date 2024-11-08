using Accountant.API.Entities;

namespace Accountant.API.Repository.Interfaces
{
    public interface IAdminRepository
    {
        Task<ICollection<Admin>> GetAdmins();
        Task<Admin> GetAdminById(int id);
        Task<Admin> loginAdmin(string AdminSpec , string password);
        Task<User> GetUserByUsernameOrEmail(string spec);
        Task<bool> UpdateUserPassword(string spec , string password);
        Task<bool> DeleteUser(string Email);
        Task<bool> Save();
    }
}
