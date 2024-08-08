using Accountant.Model.Dto;

namespace Accountant.Web.Services.Contract
{
    public interface IinstallmentServices
    {
        Task<ICollection<InstallmentDto>> GetInstallments(int LoanID);
        Task<InstallmentDto> GetInstallment (int LoanID, int InstallmentID);
        Task<bool> UpdatePay (int InstallmentID);
    }
}
