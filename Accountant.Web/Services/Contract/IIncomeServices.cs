using Accountant.Model.Dto;

namespace Accountant.Web.Services.Contract
{
    public interface IIncomeServices
    {
        Task<ICollection<IncomeTransactionDto>> GetTransactions(int userid);
        Task<IncomeTransactionDto> GetTransaction(int userid, int transactionid);
        Task<IncomeTransactionDto> AddTransaction(AddTransactionsStandardDto transaction);
        Task<bool> AddMultiIncomeTransaction(AddTransactionsStandardDto incomeTransaction , int Count);
        Task<IncomeTransactionDto> UpdateTransaction(int userid, IncomeTransactionDto transaction);
        Task<IncomeTransactionDto> DeleteTransaction(int userid, int transactionid);
        Task<IncomeTransactionDto> DeleteTransactions(int userid);
    }
}
