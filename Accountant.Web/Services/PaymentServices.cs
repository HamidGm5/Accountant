using Accountant.Model.Dto;
using Accountant.Web.Services.Contract;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Accountant.Web.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly HttpClient _client;
        public PaymentServices(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> AddMultiPaymentTransaction(AddTransactionsStandardDto paymentTransaction, int Count)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"api/PaymentTransaction/{Count}", paymentTransaction);

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
                    var message = response.Content.ReadAsStringAsync();
                    throw new Exception($"The error Status code: {response.StatusCode} and Message : {message}");
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<PaymentTransactionDto> AddTransaction(AddTransactionsStandardDto paymentsStandard)
        {
            try
            {
                var response = await _client.PostAsJsonAsync<AddTransactionsStandardDto>("api/PaymentTransaction", paymentsStandard);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new PaymentTransactionDto();
                    }

                    return await response.Content.ReadFromJsonAsync<PaymentTransactionDto>();  //Exception (Controller Ok)    
                }

                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status : {response.Content} Message - {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<PaymentTransactionDto> DeleteTransaction(int userid, int transactionid)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/PaymentTransaction/{userid}/{transactionid}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PaymentTransactionDto>();
                }

                return new PaymentTransactionDto();
            }

            catch (Exception)
            {
                // Log Exception
                throw;
            }
        }

        public async Task<PaymentTransactionDto> DeleteTransactions(int userid)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/PaymentTransaction/{userid}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new PaymentTransactionDto();
                    }

                    return await response.Content.ReadFromJsonAsync<PaymentTransactionDto>();
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

        public async Task<PaymentTransactionDto> GetTransaction(int userid, int transactionid)
        {
            try
            {
                var response = await _client.GetAsync($"api/PaymentTransaction/{userid}/{transactionid}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new PaymentTransactionDto();
                    }
                    return await response.Content.ReadFromJsonAsync<PaymentTransactionDto>();
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

        public async Task<ICollection<PaymentTransactionDto>> GetTransactions(int userid)
        {
            try
            {
                var response = await _client.GetAsync($"api/PaymentTransaction/{userid}");  //Eror
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<PaymentTransactionDto>().ToList();
                    }
                    else
                    {
                        return await response.Content.ReadFromJsonAsync<ICollection<PaymentTransactionDto>>();
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

        public async Task<PaymentTransactionDto> UpdateTransaction(int userid, PaymentTransactionDto transactionDto)
        {
            try
            {
                var jsonRequest = JsonConvert.SerializeObject(transactionDto);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

                var response = await _client.PatchAsync($"api/PaymentTransaction/{userid}", content);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return new PaymentTransactionDto();
                    }
                    return await response.Content.ReadFromJsonAsync<PaymentTransactionDto>();   //Exception
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
