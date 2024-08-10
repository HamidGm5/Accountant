using Accountant.API.Entities;

namespace Accountant.API.Repository.Interfaces
{
    public interface IinstallmentRepository
    {
        Task<ICollection<Installment>> GetInstallments();
        Task<ICollection<Installment>> GetInstallmentsByLoanID(int LoanID);
        Task<Installment> GetInstallmentByID(int InstallmentID ,int LoanID);
        Task<bool> AddNewInstallments(ICollection<Installment> installments);
        Task<bool> UpdateInstallment(ICollection<Installment> installments);
        Task<bool> UpdatePayInstallment(int installmentID);
        Task<bool> RemoveInstallments(int LoanID);
        Task<bool> Save();
        Task<bool> InstallmentExist(int installmentID);

    }
}
