using Accountant.API.Entities;
using Accountant.Model.Dto;

namespace Accountant.API.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAllUser();
        Task<User> GetUserById(int id);
        Task<User> GetByUserName(string username);
        Task<User> SignUp(User user);
        Task<User> Login(string username, string password);
        Task<bool> UpdateUser(int userid,User user);
        Task <bool>DeleteUser(int id);
        Task<bool> UserExists(int userid);
        Task<bool> Save();
    }
}
