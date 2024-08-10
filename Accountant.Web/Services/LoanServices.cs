using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;

namespace Accountant.Web.Services
{
    public class LoanServices : ILoanServices
    {
        private readonly HttpClient _client;

        public LoanServices(HttpClient client)
        {
            _client = client;
        }
        public async Task<bool> AddNewLoan(LoanDto Loan)
        {
            try
            {
                var Response = await _client.PostAsJsonAsync<LoanDto>("api/Loan", Loan);

                if (Response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {

                    return false;
                }

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteLoan(int UserID, int LoanID)
        {
            try
            {
                var Response = await _client.DeleteAsync($"/api/Loan/{UserID}/{LoanID}");

                return Response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteLoans(int UserID)
        {
            try
            {
                var Response = await _client.DeleteAsync($"/api/Loan/{UserID}");

                return Response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<LoanDto> GetLoanByLoanID(int UserID, int LoanID)
        {
            try
            {
                var Response = await _client.GetAsync($"api/Loan/{UserID}/{LoanID}");
                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<LoanDto>();
                }
                else
                {
                    var message = await Response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code : {Response.StatusCode} Message - {message}");
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<ICollection<LoanDto>> GetUserLoan(int UserID)
        {
            try
            {
                var Response = await _client.GetAsync($"api/Loan/{UserID}");
                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadFromJsonAsync<ICollection<LoanDto>>();
                }
                else
                {
                    var message = await Response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code : {Response.StatusCode} Message - {message}");
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateLoan(int LoanID, LoanDto Loan)
        {
            try
            {
                var Serilize = JsonConvert.SerializeObject(Loan);
                var Content = new StringContent(Serilize, Encoding.UTF8, "application/json-patch+json");
                var Response = await _client.PutAsync($"api/Loan/{LoanID}", Content);

                return Response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
