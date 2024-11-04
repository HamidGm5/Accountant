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
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
                entity.HasMany(it => it.IncomeTransactions).
                WithOne(u => u.User).
                HasForeignKey("userId").
                IsRequired()
            );

            modelBuilder.Entity<User>(entity =>
                entity.HasMany(pt => pt.PaymentTransactions).
                WithOne(u => u.User).
                HasForeignKey("userId")
                .IsRequired()
            );
        }
    }
}
