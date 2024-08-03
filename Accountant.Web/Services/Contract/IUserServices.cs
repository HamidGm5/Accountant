using Accountant.Model.Dto;

namespace Accountant.Web.Services.Contract
{
    public interface IUserServices
    {
        Task<UserDto> Login(string username, string password);
        Task<UserDto> SignUp(UserDto user);
        Task<UserDto> UpdateUser(UserDto newuser);
        Task<UserDto> DeleteUser(int userid);
    }
}
