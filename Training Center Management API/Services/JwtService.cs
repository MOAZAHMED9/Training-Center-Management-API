using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Training_Center_Management_API.Models;
using Training_Center_Management_API.Data;

namespace Training_Center_Management_API.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public JwtService(IConfiguration configuration, AppDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");              //بجيب الاعدادات من الjson

            var key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(jwtSettings["Key"]!));     //ننشاء المفاتح

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);       //الخوارزميات   


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };


            if (user.Student != null)
            {
                claims.Add(new Claim("StudentId",
                    user.Student.Id.ToString()));
            }

            if (user.Instructor != null)
            {
                claims.Add(new Claim("InstructorId",
                    user.Instructor.Id.ToString()));
            }


            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(jwtSettings["AccessTokenMinutes"])),            ///////////////
                signingCredentials: credentials);                                  ////////////


            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }




        public async Task<string> GenerateRefreshToken(User user)
        {
            var bytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            var refreshtocken=Convert.ToBase64String(bytes);

            user.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshtocken);
            user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
            user.RefreshTokenRevokedAt = null;

            await _context.SaveChangesAsync();

            return refreshtocken;
        }

    }
}