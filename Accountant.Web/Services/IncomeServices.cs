using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using System.Text.Unicode;

namespace Accountant.Web.Services
{
    public class IncomeServices : IIncomeServices
    {
        private readonly HttpClient _client;
        public IncomeServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> AddMultiIncomeTransaction(AddTransactionsStandardDto incomeTransaction, int Count)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"api/IncomeTransaction/{Count}", incomeTransaction);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"the error message : {message} and Http status is : {response.StatusCode}");
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<IncomeTransactionDto> AddTransaction(AddTransactionsStandardDto standardDto)
        {
            var response = await _client.PostAsJsonAsync($"api/IncomeTransaction", standardDto);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new IncomeTransactionDto();
                }
                return await response.Content.ReadFromJsonAsync<IncomeTransactionDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http Status : {response.Content} message : {message}");
            }
        }

        public async Task<IncomeTransactionDto> DeleteTransaction(int userid, int transactionid)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/IncomeTransaction/{transactionid}/{userid}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new IncomeTransactionDto();
                    }
                    return await response.Content.ReadFromJsonAsync<IncomeTransactionDto>(); // Exception
                }

                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http StatusCode : {response.StatusCode} message : {message}");
                }
            }

            catch (Exception)
            {
                // Log Exception
                throw;
            }
        }

        public async Task<IncomeTransactionDto> DeleteTransactions(int userid)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/IncomeTransaction/{userid}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new IncomeTransactionDto();
                    }

                    return await response.Content.ReadFromJsonAsync<IncomeTransactionDto>();
                }

                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code : {response.StatusCode} Message - {message}");
                }
            }

            catch (Exception)
            {
                //Log Exception
                throw;
            }
        }

        public async Task<IncomeTransactionDto> GetTransaction(int userid, int transactionid)
        {
            try
            {
                var response = await _client.GetAsync($"api/IncomeTransaction/{userid}/{transactionid}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new IncomeTransactionDto();
                    }
                    return await response.Content.ReadFromJsonAsync<IncomeTransactionDto>();
                }

                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http StatusCode : {response.StatusCode} message : {message}");
                }
            }

            catch (Exception)
            {
                // Log Exception
                throw;
            }
        }

        public async Task<ICollection<IncomeTransactionDto>> GetTransactions(int userid)
        {
            try
            {
                var response = await _client.GetAsync($"api/IncomeTransaction/{userid}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<IncomeTransactionDto>().ToList();
                    }
                    else
                    {
                        return await response.Content.ReadFromJsonAsync<ICollection<IncomeTransactionDto>>();
                    }
                }

                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status : {response.StatusCode} message {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<IncomeTransactionDto> UpdateTransaction(int userid, IncomeTransactionDto transaction)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(transaction);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

                var response = await _client.PatchAsync($"api/IncomeTransaction/{userid}", content);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new IncomeTransactionDto();
                    }
                    return await response.Content.ReadFromJsonAsync<IncomeTransactionDto>(); //Exception
                }

                else
                {
                    var message = response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code : {response.StatusCode} , message : {message}");
                }
            }

            catch (Exception)
            {
                //Log Exception
                throw;
            }
        }
    }
}
