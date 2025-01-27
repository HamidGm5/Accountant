namespace Accountant.API.Entities
{
    public class Admin
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
