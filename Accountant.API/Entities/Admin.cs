namespace Accountant.API.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string AdminAlias { get; set; }
        public string AdminPassWord { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string NationalCode { get; set; }
        public string NationalCardImgURL { get; set; }
        public string? AdminPhoto { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<IncomeTransaction> IncomeTransaction { get; set; }
        public ICollection<PaymentTransaction> PaymentTransaction { get; set; }
    }
}
