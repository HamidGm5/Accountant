using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Accountant.Web.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly HttpClient _client;

        public AdminServices(HttpClient client)
        {
            _client = client;
        }


        public async Task<bool> DeleteUser(string Email, string Password)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/Admin/{Email}");
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<ICollection<UserDto>> GetAllUsers()
        {
            try
            {
                var response = await _client.GetAsync("api/Admin");
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadFromJsonAsync<ICollection<UserDto>>();
                    }
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http StatusCode : {response.StatusCode} and Message is : {message}");
                }
                return new UserDto[0];
            }
            catch (Exception)
            {
                //log exception
                throw;
            }
        }

        public async Task<UserDto> GetUserBySpec(string Email)
        {
            try
            {
                var response = await _client.GetAsync($"api/Admin/{Email}");

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new UserDto();
                }

                return await response.Content.ReadFromJsonAsync<UserDto>();
            }
            catch (Exception)
            {
                //log exception
                throw;
            }
        }

        public async Task<AdminDto> loginAdmin(string AliasOrEmail, string Password)
        {
            try
            {
                var response = await _client.GetAsync($"api/Admin/{AliasOrEmail}/{Password}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new AdminDto();
                    }

                    return await response.Content.ReadFromJsonAsync<AdminDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"you have error with message : {message} " +
                        $"and code is : {response.StatusCode}");
                }
            }
            catch (Exception)
            {
                // log exception
                throw;
            }
        }

        public async Task<bool> UpdateUserPassword(string Email, string Password)
        {
            try
            {
                var response = await _client.PatchAsync($"api/Admin/{Email}/{Password}", null);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return false;
                    }

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                // log exception
                throw;
            }
        }
    }
}
