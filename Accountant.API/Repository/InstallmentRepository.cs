using Accountant.API.Data;
using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accountant.API.Repository
{
    public class InstallmentRepository : IinstallmentRepository
    {
        private readonly AccountantContext _context;
        public InstallmentRepository(AccountantContext context)
        {
            _context = context;
        }

        public async Task<bool> AddNewInstallment(Installment installment)      // controller should give loan parameter
        {
            var AddInstallment = await _context.Installments.AddAsync(installment);
            await Save();
            return AddInstallment != null;
        }

        public async Task<Installment> GetInstallmentByID(int InstallmentID , int LoanID)
        {
            var Installment = await _context.Installments.Where(i => i.ID == InstallmentID && 
                                                    i.loan.ID == LoanID).FirstOrDefaultAsync();
            return Installment;
        }

        public async Task<ICollection<Installment>> GetInstallments()
        {
            var Installments = await _context.Installments.ToListAsync();
            return Installments;
        }

        public async Task<ICollection<Installment>> GetInstallmentsByLoanID(int LoanID)
        {
            var Installments = await _context.Installments.Where(li => li.loan.ID == LoanID).ToListAsync();
            return Installments;
        }

        public async Task<bool> InstallmentExist(int installmentID)
        {
            var FindInstallment = await _context.Installments.FindAsync(installmentID);
            return FindInstallment != null;
        }

        public async Task<bool> RemoveInstallments(int LoanID)
        {
            var FindInstallments = await _context.Installments.Where(li => li.loan.ID == LoanID).ToListAsync();

            _context.Installments.RemoveRange(FindInstallments);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateInstallment(Installment installment)
        {
            //var FindInstallment = await _context.Installments.FindAsync(installment.ID);
            var Update = _context.Installments.Update(installment);
            return await Save();

            //if (FindInstallment != null)
            //{
            //    FindInstallment.Amount = installment.Amount;
            //    FindInstallment.PayOrNo = installment.PayOrNo;
            //    FindInstallment.PayTime = installment.PayTime;
            //    _context.Installments.Update(FindInstallment);

            //}
            //return false;
        }


    }
}
