using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accountant.API.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }  
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? ImgURL { get; set; }

        public ICollection<PaymentTransaction> PaymentTransactions { get; set; }
        public ICollection<IncomeTransaction> IncomeTransactions { get; set; }
        public ICollection<Loan> Loans { get; set; }
    }
}
