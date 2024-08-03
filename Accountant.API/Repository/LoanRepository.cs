using Accountant.API.Data;
using Accountant.API.Entities;
using Accountant.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accountant.API.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AccountantContext _context;
        public LoanRepository(AccountantContext context)
        {
            _context = context;
        }

        public async Task<bool> AddLoan(Loan loan)
        {
            var addNewLoan = await _context.AddAsync(loan);
            await Save();
            return addNewLoan != null;
        }

        public async Task<bool> DeleteAllLoan(int userID)
        {
            foreach (var loan in await _context.Loans.Where(l => l.user.Id == userID).ToListAsync())
            {
                _context.Loans.Remove(loan);
            }

            return await Save();
        }

        public async Task<bool> DeleteLoan(int LoanID, int UserID)
        {
            if (await ExistLoan(LoanID))
            {
                var Loan = await _context.Loans.Where(ul => ul.ID == LoanID && ul.user.Id == UserID).FirstOrDefaultAsync();
                _context.Loans.Remove(Loan);
                return await Save();
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ExistLoan(int LoanID)
        {
            var Loan = await _context.Loans.FindAsync(LoanID);
            if (Loan != null)
                return true;
            else
                return false;
        }

        public async Task<Loan> GetLoanByID(int UserID, int loanID)
        {
            var Loan = await _context.Loans.Where(ul => ul.user.Id == UserID && ul.ID == loanID).FirstOrDefaultAsync();
            if (Loan != null)
            {
                return Loan;
            }
            else
            {
                return null;
            }

        }

        public async Task<ICollection<Loan>> GetUserLoan(int UserID)
        {
            var UserLoan = await _context.Loans.Where(ul => ul.user.Id == UserID).ToListAsync();
            if (UserLoan == null)
            {
                return null;
            }
            else
            {
                return UserLoan;
            }
        }

        public async Task<ICollection<Loan>> GetLoans()
        {
            var Loans = await _context.Loans.ToListAsync();
            return Loans;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UpdateLoan(Loan loan)
        {
            var FindLoan = await _context.Loans.FindAsync(loan.ID);
            if (FindLoan != null) 
            {
                FindLoan.Percentage = loan.Percentage;
                FindLoan.StartTime = loan.StartTime;
                FindLoan.EndTime = loan.EndTime;
                FindLoan.PeriodPerMonth = loan.PeriodPerMonth;
                FindLoan.Description = loan.Description;
                FindLoan.Intallments = loan.Intallments;
                FindLoan.LoanAmount = loan.LoanAmount;
                FindLoan.RecursiveAmount = loan.RecursiveAmount;
                
                return await Save();

            }
            else
            {
                return false;
            }
        }
    }
}
