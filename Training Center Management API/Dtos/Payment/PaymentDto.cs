namespace Training_Center_Management_API.Dtos.Payment
{
    public class PaymentDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public string? TransactionReference { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
