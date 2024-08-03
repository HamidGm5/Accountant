using Accountant.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Accountant.API.Data
{
    public class AccountantContext : DbContext
    {
        public AccountantContext(DbContextOptions<AccountantContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<IncomeTransaction> IncomeTransactions { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Installment> Installments { get; set; }

    }
}
