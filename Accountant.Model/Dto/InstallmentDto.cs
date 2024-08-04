using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.Model.Dto
{
    public class InstallmentDto
    {
        public int ID { get; set; }
        public double Amount { get; set; }
        public DateTime PayTime { get; set; }
        public bool PayOrNo { get; set; } = false;
    }
}
