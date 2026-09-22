using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Dtos.Enrollment;
using Training_Center_Management_API.Dtos.Certificate;
using Training_Center_Management_API.Dtos.Payment;
using Training_Center_Management_API.Services.Auditing;
using Training_Center_Management_API.Services.Payment;
using Training_Center_Management_API.Services.certificate;

namespace Training_Center_Management_API.Services.Enrollment
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPaymentService _paymentService;
        private readonly ICertificateService _certificateService;
        public EnrollmentService(AppDbContext context , ICurrentUserService currentUserService, IPaymentService paymentService ,ICertificateService certificateService)
        {
            _context = context;
            _currentUserService = currentUserService;
            _paymentService = paymentService;
            _certificateService = certificateService;
        }


        public async Task<List<EnrollmentDto>> GetAllAsync()
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Select(e => new EnrollmentDto
                {
                    Id = e.Id,

                    StudentId = e.StudentId,
                    StudentName = e.Student.FullName,

                    CourseId = e.CourseId,
                    CourseName = e.Course.Name,

                    EnrollmentDate = e.EnrollmentDate,

                    Status = e.Status,

                    Grade = e.Grade
                })
                .ToListAsync();
        }


        public async Task<EnrollmentDto?> GetByIdAsync(int id)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Select(e => new EnrollmentDto
                {
                    Id = e.Id,

                    StudentId = e.StudentId,
                    StudentName = e.Student.FullName,

                    CourseId = e.CourseId,
                    CourseName = e.Course.Name,

                    EnrollmentDate = e.EnrollmentDate,

                    Status = e.Status,

                    Grade = e.Grade
                })
                .FirstOrDefaultAsync();
        }


        public async Task<EnrollmentDto?> CreateAsync(CreateEnrollmentDto dto)
        {
           using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var id = _currentUserService.UserId;


                var courseExists = await _context.Courses
                    .FirstOrDefaultAsync(c => c.Id == dto.CourseId);

                if (courseExists is null)
                {
                    return null;
                }


                var alreadyEnrolled = await _context.Enrollments.AnyAsync(e => e.StudentId == id.Value && e.CourseId == dto.CourseId);


                if (alreadyEnrolled)
                {
                    return null;
                }


                var enrollment = new Models.Enrollment
                {
                    StudentId = id.Value,

                    CourseId = dto.CourseId,

                    EnrollmentDate = DateTime.UtcNow,

                    Status = "InProgress"
                };


                var payment = new CreatePaymentDto
                {

                    StudentId = id.Value,
                    Amount = courseExists.Price,
                    
                    PaymentMethod = "paypal",
                    TransactionReference = Guid.NewGuid().ToString()
                };


                _context.Enrollments.Add(enrollment);
               
                await _paymentService.CreateAsync(payment);



                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                
                return await GetByIdAsync(enrollment.Id);    //// بتحفظو يعدين تجيب الid من الداتا بيز
            }
            catch (Exception ex)
            {

                await transaction.RollbackAsync();
                throw new Exception("An error occurred while creating the enrollment and payment.", ex);
            }

        }

       

        public async Task<bool> UpdateAsync( int id, UpdateEnrollmentDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {

                var enrollment = await _context.Enrollments
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (enrollment == null)
                {
                    return false;
                }

                enrollment.Status = dto.Status;

                enrollment.Grade = dto.Grade;

                await _certificateService.chickCertificate(enrollment, dto);


                await _context.SaveChangesAsync();
                await transaction.CommitAsync();


            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("An error occurred while updating the enrollment and checking for certificate.", ex);
            }
            return true;
        }




        public async Task<bool> DeleteAsync(int id)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enrollment == null)
            {
                return false;
            }


            // Soft Delete
            enrollment.IsDeleted = true;       /// لو حزفت ال child الparent مش هيتاثر  
            //اقدر اعملها hard كدا كدا دي علاقه 

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
