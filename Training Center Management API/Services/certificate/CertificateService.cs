using Microsoft.EntityFrameworkCore;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Dtos.Certificate;
using Training_Center_Management_API.Dtos.Enrollment;
namespace Training_Center_Management_API.Services.certificate
{
    public class CertificateService : ICertificateService
    {
        private readonly AppDbContext _context;

        public CertificateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task chickCertificate(Models.Enrollment enrol, UpdateEnrollmentDto dto)
        {

            if (dto.Status == "Completed" && dto.Grade >=50m )
            {

                var certificateExists = new CreateCertificateDto
                {
                    StudentId = enrol.StudentId,
                    CourseId = enrol.CourseId,
                    IssueDate = DateTime.UtcNow,
                    CertificateNumber = Guid.NewGuid().ToString(),
                    Grade = (dto.Grade >= 90m) ? "A" : (dto.Grade >= 80m) ? "B" : (dto.Grade >= 70m) ? "C" : (dto.Grade >= 60m) ? "D" : "F"
                };

                await Createcertificate(certificateExists);
            }
        }

        public async Task<bool> Createcertificate(CreateCertificateDto dto )
        {
            var csers = await _context.Certificates
                    .AnyAsync(c => c.StudentId == dto.StudentId && c.CourseId == dto.CourseId);

            if (csers)
            {
                return false;
            }

            var certificate = new Models.Certificate
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                IssueDate = dto.IssueDate,
                CertificateNumber = dto.CertificateNumber,
                Grade = dto.Grade
            };

            await _context.Certificates.AddAsync(certificate);
            return true;
        }

       
    }
}
