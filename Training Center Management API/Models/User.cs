
namespace Training_Center_Management_API.Models
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }


        public Student? Student { get; set; }

        public Instructor? Instructor { get; set; }



        public string? RefreshTokenHash { get;  set; }
        public DateTime? RefreshTokenExpiresAt { get;  set; }
        public DateTime? RefreshTokenRevokedAt { get;  set; }
    }
}
