using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.Model.Dto
{
    public class LoanDto
    {
        public int ID { get; set; }
        public int Userid { get; set; }
        public double LoanAmount { get; set; }
        public double RecursiveAmount { get; set; }
        public int PeriodPerMonth { get; set; }
        public float Percentage { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
