namespace Accountant.API.Entities
{
    public class Installment
    {
        public int ID { get; set; }
        public double Amount { get; set; }
        public DateTime PayTime { get; set; }
        public bool PayOrNo { get; set; } = false;

        public Loan loan { get; set; }
    }
}
