using Accountant.Model.Dto;

namespace Accountant.Web.Services.Contract
{
    public interface IAdminServices
    {
        Task<ICollection<UserDto>> GetAllUsers();
        Task<UserDto> GetUserBySpec(string Email);
        Task<AdminDto> loginAdmin(string AliasOrEmail , string Password);
        Task<bool> UpdateUserPassword(string Email , string Password);
        Task<bool> DeleteUser(string Email , string Password);
    }
}
