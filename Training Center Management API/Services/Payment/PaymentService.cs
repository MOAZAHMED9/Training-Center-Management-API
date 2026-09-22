using Microsoft.EntityFrameworkCore;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Dtos.Payment;

namespace Training_Center_Management_API.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PaymentDto>> GetAllAsync()
        {
            return await _context.Payments
                .AsNoTracking()
                .Select(p => new PaymentDto
                {
                    Id = p.Id,

                    StudentId = p.StudentId,
                    StudentName = p.Student.FullName,

                    Amount = p.Amount,

                    PaymentMethod = p.PaymentMethod,

                    TransactionReference =
                        p.TransactionReference,

                    PaymentDate = p.PaymentDate
                })
                .ToListAsync();
        }

        public async Task<PaymentDto?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,

                    StudentId = p.StudentId,
                    StudentName = p.Student.FullName,

                    Amount = p.Amount,

                    PaymentMethod = p.PaymentMethod,

                    TransactionReference =
                        p.TransactionReference,

                    PaymentDate = p.PaymentDate
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PaymentDto?> CreateAsync(CreatePaymentDto dto)
        {
            var studentExists = await _context.Students
                .AnyAsync(s => s.Id == dto.StudentId);

            if (!studentExists)
            {
                return null;
            }

            var payment = new Models.Payment
            {
                StudentId = dto.StudentId,
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod,
                TransactionReference = dto.TransactionReference,

                PaymentDate = DateTime.UtcNow                                      
            };

            _context.Payments.Add(payment);

           

            return await GetByIdAsync(payment.Id);
        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdatePaymentDto dto)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
            {
                return false;
            }

            payment.Amount = dto.Amount;

            payment.PaymentMethod =
                dto.PaymentMethod;

            payment.TransactionReference =
                dto.TransactionReference;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.Id == id);

            if (payment == null)
            {
                return false;
            }

            // Soft Delete
            payment.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}