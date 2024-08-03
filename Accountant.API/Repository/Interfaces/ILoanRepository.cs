using Accountant.API.Entities;

namespace Accountant.API.Repository.Interfaces
{
    public interface ILoanRepository
    {
        Task<ICollection<Loan>> GetLoans();
        Task<ICollection<Loan>> GetUserLoan(int UserID);
        Task<Loan> GetLoanByID(int UserID ,int loanID);
        Task<bool> AddLoan(Loan loan);
        Task<bool> UpdateLoan(Loan loan);
        Task<bool> DeleteLoan(int LoanID , int UserID);
        Task<bool> DeleteAllLoan(int userID);
        Task<bool> Save();
        Task<bool> ExistLoan(int LoanID);
    }
}
