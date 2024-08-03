using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accountant.API.Entities
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionTime { get; set; } = DateTime.Now;
        public string? Descriptions { get; set; }

        public User User { get; set; }
    }
}
