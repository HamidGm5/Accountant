using Accountant.Model.Dto;

namespace Accountant.Web.Services.Contract
{
    public interface IPaymentServices
    {
        Task<ICollection<PaymentTransactionDto>> GetTransactions(int userid);
        Task<PaymentTransactionDto> GetTransaction(int userid, int transactionid);
        Task<PaymentTransactionDto> AddTransaction(AddTransactionsStandardDto paymentsStandard);
        Task<bool> AddMultiPaymentTransaction(AddTransactionsStandardDto paymentTransaction, int Count);
        Task<PaymentTransactionDto> UpdateTransaction(int userid, PaymentTransactionDto transaction);
        Task <PaymentTransactionDto> DeleteTransaction(int userid,int transactionid);
        Task<PaymentTransactionDto> DeleteTransactions(int userid);
    }
}
