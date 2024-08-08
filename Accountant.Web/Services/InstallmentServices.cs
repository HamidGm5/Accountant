using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using System.Net.Http.Json;

namespace Accountant.Web.Services
{
    public class InstallmentServices : IinstallmentServices
    {
        private readonly HttpClient _client;

        public InstallmentServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<InstallmentDto> GetInstallment(int LoanID, int InstallmentID)
        {
            try
            {
                var Response = await _client.GetAsync($"/api/Installment/{LoanID}/{InstallmentID}");
                if (Response == null)
                {
                    return null;
                }
                else
                {
                    return await Response.Content.ReadFromJsonAsync<InstallmentDto>();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<ICollection<InstallmentDto>> GetInstallments(int LoanID)
        {
            try
            {
                var Response = await _client.GetAsync($"/api/Installment/{LoanID}");

                if (Response == null)
                {
                    return new List<InstallmentDto>();
                }
                else
                {
                    return await Response.Content.ReadFromJsonAsync<ICollection<InstallmentDto>>();
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdatePay(int InstallmentID)
        {
            try
            {
                var Response = await _client.PatchAsync($"api/Installment/{InstallmentID}", null);
                return Response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
