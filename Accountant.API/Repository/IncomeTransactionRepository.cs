using Accountant.API.Data;
using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace Accountant.API.Repository
{
    public class IncomeTransactionRepository : IIncomeTransactionRepository
    {
        private readonly AccountantContext _context;

        public IncomeTransactionRepository(AccountantContext context)
        {
            _context = context;
        }

        public async Task<IncomeTransaction> AddIncomeTransaction(IncomeTransaction incomeTransaction)
        {
            await _context.IncomeTransactions.AddAsync(incomeTransaction);
            await _context.SaveChangesAsync();
            return incomeTransaction;
        }

        public async Task<bool> AddMultiIncomes(List<IncomeTransaction> incomeTransactions)
        {
            try
            {
                // try for do by AddRangeAsync But just one data going to save insted of all datak
                foreach (var item in incomeTransactions)
                {
                    await _context.IncomeTransactions.AddAsync(new IncomeTransaction()
                    {
                        Amount = item.Amount,
                        Descriptions = item.Descriptions,
                        TransactionTime = item.TransactionTime,
                        User = item.User
                    });
                }
                return await Save();
            }
            catch
            {
                return false;
            }
        }

        public async Task<IncomeTransaction> DeleteIncomeTransaction(IncomeTransaction transaction)
        {
            _context.IncomeTransactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> DeleteIncomeTransactions(int userid)
        {
            foreach (var transaction in _context.IncomeTransactions.Where(i => i.User.Id == userid))
            {
                _context.Remove(transaction);
            }

            return await Save();
        }

        public async Task<ICollection<IncomeTransaction>> GetAllIncomes()
        {
            return await _context.IncomeTransactions.ToListAsync();
        }

        public async Task<bool> IncomeExists(int transactionid)
        {
            return await _context.IncomeTransactions.AnyAsync(it => it.Id == transactionid);
        }

        public async Task<IncomeTransaction> IncomeTransaction(int userid, int transactionid)
        {
            var IncomeTransaction = await _context.IncomeTransactions.Where(it => it.User.Id == userid
                                  && it.Id == transactionid).FirstOrDefaultAsync();
            if (IncomeTransaction == null)
                return new IncomeTransaction();
            return IncomeTransaction;
        }

        public async Task<ICollection<IncomeTransaction>> IncomeTransactions(int userid)
        {
            return await _context.IncomeTransactions.Where(ui => ui.User.Id == userid).OrderBy
                                                    (it => it.TransactionTime).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public Task<bool> TransactionExists(int transactionid)
        {
            return _context.IncomeTransactions.AnyAsync(ti => ti.Id == transactionid);
        }

        public async Task<IncomeTransaction> UpdateIncomeTransactioin(IncomeTransaction transaction)
        {
            var FindTransaction = await _context.IncomeTransactions.FindAsync(transaction.Id);

            if (FindTransaction != null)
            {
                FindTransaction.Amount = transaction.Amount;
                FindTransaction.TransactionTime = transaction.TransactionTime;
                FindTransaction.Descriptions = transaction.Descriptions;

                await _context.SaveChangesAsync();

                return FindTransaction;
            }

            return new IncomeTransaction();
        }
    }
}
