using Accountant.API.Entities;
using Accountant.Model.Dto;

namespace Accountant.API.Repository.Interfaces
{
    public interface IPaymentTransactionRepository
    {
        Task<ICollection<PaymentTransaction>> GetAllPaymentTransactions();
        Task<ICollection<PaymentTransaction>> PaymentTransactions(int userid);
        Task<PaymentTransaction> PaymentTransaction(int userid, int transactionid);
        Task<bool> AddMultiPayments(ICollection<PaymentTransaction> paymentTransactions);
        Task<PaymentTransaction> AddPaymentTransaction(PaymentTransaction transactionDto);
        Task<PaymentTransaction> UpdatePaymentTransaction(PaymentTransaction transaction);
        Task<PaymentTransaction> DeletePaymentTransaction(PaymentTransaction payment);
        Task<bool> DeletePaymentTransactions(int userid);
        Task<bool> Save();
        Task<bool> PaymentExists(int transactionid);
    }
}
