namespace Training_Center_Management_API.Models
{
    public class Payment : BaseEntity
    {
        public int StudentId { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; }

        public string TransactionReference { get; set; }

        public Student Student { get; set; }
    }
}
