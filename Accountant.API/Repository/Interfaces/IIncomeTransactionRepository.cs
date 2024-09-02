using Accountant.API.Entities;
using Accountant.Model.Dto;

namespace Accountant.API.Repository.Interfaces
{
    public interface IIncomeTransactionRepository
    {
        Task<ICollection<IncomeTransaction>> GetAllIncomes();
        Task<ICollection<IncomeTransaction>> IncomeTransactions(int userid);
        Task<IncomeTransaction> IncomeTransaction(int userid, int transactionid);
        Task<bool> AddMultiIncomes(List<IncomeTransaction> incomeTransactions);
        Task<IncomeTransaction> AddIncomeTransaction(IncomeTransaction standardTransaction);
        Task<IncomeTransaction> UpdateIncomeTransactioin(IncomeTransaction transaction);
        Task<IncomeTransaction> DeleteIncomeTransaction(IncomeTransaction transaction);
        public Task<bool> DeleteIncomeTransactions(int userid);
        Task<bool> TransactionExists(int transactionid);
        Task<bool> Save();
        Task<bool> IncomeExists(int transactionid);
    }
}
