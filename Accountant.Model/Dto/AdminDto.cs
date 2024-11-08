using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant.Model.Dto
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string AdminAlias { get; set; }
        public string AdminPassword { get; set; }
        public string AdminFirstname { get; set; }
        public string AdminLastname { get; set; }
        public string NationalCode { get; set; }
        public string NationalCardImgURL { get; set; }
        public string? AdminPhoto { get; set; }
    }
}
