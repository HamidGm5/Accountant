using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.Model.Dto
{
    public class AddLoanDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }     //Just addition versus Loan

        public double AllAmount { get; set; }

        public float InterestRate { get; set; }

        public DateTime StartTime { get; set; }

        public int LoanPeriodPerMonth { get; set; }

        public string Description { get; set; }

    }
}
