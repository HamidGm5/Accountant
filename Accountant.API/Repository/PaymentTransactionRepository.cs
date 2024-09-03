using Accountant.API.Data;
using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Accountant.Model.Dto;
using Microsoft.EntityFrameworkCore;

namespace Accountant.API.Repository
{
    public class PaymentTransactionRepository : IPaymentTransactionRepository
    {
        private readonly AccountantContext _context;

        public PaymentTransactionRepository(AccountantContext context)
        {
            _context = context;
        }
        public async Task<PaymentTransaction> AddPaymentTransaction(PaymentTransaction paymentTransaction)
        {
            await _context.PaymentTransactions.AddAsync(paymentTransaction);
            await _context.SaveChangesAsync();
            return paymentTransaction;
        }

        public async Task<PaymentTransaction> UpdatePaymentTransaction(PaymentTransaction transaction)
        {
            var Findtransaction = await _context.PaymentTransactions.FindAsync(transaction.Id);

            if (Findtransaction != null)
            {
                Findtransaction.Amount = transaction.Amount;
                Findtransaction.TransactionTime = transaction.TransactionTime;
                Findtransaction.Descriptions = transaction.Descriptions;

                await _context.SaveChangesAsync();

                return Findtransaction;
            }

            return null;
        }

        public async Task<bool> DeletePaymentTransactions(int userid)
        {
            foreach (var transactions in _context.PaymentTransactions.Where(ip => ip.User.Id == userid))
            {
                _context.PaymentTransactions.Remove(transactions);
            }

            return await Save();

        }

        public async Task<PaymentTransaction> PaymentTransaction(int userid, int transactionid)
        {
            var Transaction = await _context.PaymentTransactions.Where(pt => pt.User.Id == userid &&
                                                        pt.Id == transactionid).FirstOrDefaultAsync();
            return Transaction;
        }

        public async Task<ICollection<PaymentTransaction>> PaymentTransactions(int userid)
        {
            return await _context.PaymentTransactions.Where(pt => pt.User.Id == userid).OrderBy(pt => pt.TransactionTime).ToListAsync();
        }

        public async Task<bool> Save()
        {
            var Saved = await _context.SaveChangesAsync();
            return Saved > 0 ? true : false;
        }

        public async Task<bool> PaymentExists(int transactionid)
        {
            return await _context.PaymentTransactions.AnyAsync(ti => ti.Id == transactionid);
        }

        public async Task<ICollection<PaymentTransaction>> GetAllPaymentTransactions()
        {
            return await _context.PaymentTransactions.ToListAsync();
        }

        public async Task<PaymentTransaction> DeletePaymentTransaction(PaymentTransaction payment)
        {
            _context.PaymentTransactions.Remove(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<bool> AddMultiPayments(ICollection<PaymentTransaction> paymentTransactions)
        {
            try
            {   
                // i'm try to do with AddRangeAsync but just insert one data of collection and don't work 
                foreach (var item in paymentTransactions)
                {
                    await _context.PaymentTransactions.AddAsync(new PaymentTransaction
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
    }
}
