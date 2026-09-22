using Training_Center_Management_API.Dtos.Certificate;
using Training_Center_Management_API.Dtos.Enrollment;
namespace Training_Center_Management_API.Services.certificate
{
    public interface ICertificateService
    {
        public Task<bool> Createcertificate( CreateCertificateDto dto);
        Task  chickCertificate(Models.Enrollment enrol, UpdateEnrollmentDto dto);
    }
}
