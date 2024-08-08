using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accountant.API.Entities
{
    public class Loan
    {
        [Key]
        public int ID { get; set; }
        public double LoanAmount { get; set; }
        public double RecursiveAmount { get; set; }
        public int PeriodPerMonth { get; set; }
        public float Percentage { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public ICollection<Installment> Intallments { get; set; }
        public User user { get; set; }
    }
}
