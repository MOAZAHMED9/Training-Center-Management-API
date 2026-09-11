using System.ComponentModel.DataAnnotations;

namespace Training_Center_Management_API.Dtos.Payment
{
    public class UpdatePaymentDto
    {
        [Range(typeof(decimal), "0.01", "9999999")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [StringLength(100)]
        public string? TransactionReference { get; set; }

    }
}
