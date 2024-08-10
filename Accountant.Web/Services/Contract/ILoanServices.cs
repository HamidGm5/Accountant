using Accountant.Model.Dto;

namespace Accountant.Web.Services.Contract
{
    public interface ILoanServices
    {
        Task<ICollection<LoanDto>> GetUserLoan(int UserID);
        Task<LoanDto> GetLoanByLoanID(int UserID, int LoanID);
        Task<bool> AddNewLoan(LoanDto Loan);
        Task<bool> UpdateLoan(int LoanID, LoanDto Loan);
        Task<bool> DeleteLoans(int UserID);
        Task<bool> DeleteLoan(int UserID, int LoanID);
    }
}
