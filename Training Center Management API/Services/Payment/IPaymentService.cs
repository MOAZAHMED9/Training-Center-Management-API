using Training_Center_Management_API.Dtos.Payment;

namespace Training_Center_Management_API.Services.Payment
{
    public interface IPaymentService
    {
        Task<List<PaymentDto>> GetAllAsync();

        Task<PaymentDto?> GetByIdAsync(int id);

        Task<PaymentDto?> CreateAsync(CreatePaymentDto dto);

        Task<bool> UpdateAsync(int id,UpdatePaymentDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
