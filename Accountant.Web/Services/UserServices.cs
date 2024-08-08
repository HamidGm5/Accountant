using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Accountant.Web.Services
{
    public class UserServices : IUserServices
    {
        private readonly HttpClient _client;

        public UserServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<UserDto> DeleteUser(int userid)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/User/{userid}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<UserDto>();
                }
                return default(UserDto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDto> Login(string username, string password)
        {
            try
            {
                var response = await _client.GetAsync($"api/User/{username}/{password}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return null;
                    }
                    return await response.Content.ReadFromJsonAsync<UserDto>();
                }

                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status : {response.StatusCode} message : {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDto> SignUp(UserDto user)
        {
            try
            {
                var response = await _client.PostAsJsonAsync<UserDto>($"api/User", user);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return null;
                    }
                    return await response.Content.ReadFromJsonAsync<UserDto>();
                }

                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status : {response.StatusCode} message : {message}");
                }
            }

            catch (Exception)
            {
                //Log Exception
                throw;
            }
        }

        public async Task<UserDto> UpdateUser(UserDto newuser)
        {
            var jsonRequest = JsonConvert.SerializeObject(newuser);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
            var response = await _client.PutAsync($"api/User/{newuser.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }
            return null;
        }
    }
}