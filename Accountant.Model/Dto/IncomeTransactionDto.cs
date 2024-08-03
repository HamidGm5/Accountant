using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.Model.Dto
{
    public class IncomeTransactionDto
    {
        public int? Id { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionTime { get; set; } = DateTime.Now;
        public string? Descriptions { get; set; }
    }
}
