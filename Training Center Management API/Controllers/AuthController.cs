using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Dtos.Auth;
using Training_Center_Management_API.Models;
using Training_Center_Management_API.Services;

namespace Training_Center_Management_API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context,JwtService jwtService, ILogger<AuthController> logger)
        {
            _context = context;
            _jwtService = jwtService;
            _logger = logger;
        }


        [EnableRateLimiting("AuthPolicy")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {

            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";



            var user = await _context.Users
                .Include(s=> s.Student)
                .Include(s=> s.Instructor)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                _logger.LogWarning("Failed login attempt for email: {Email} ip : {ip}",dto.Email,ip);

                return Unauthorized("Invalid email or password.");
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            if (!isPasswordValid)
            {

                _logger.LogWarning("Failed login attempt for email: {Email} ip : {ip}", dto.Email, ip);


                return Unauthorized("Invalidd email or password.");
            }

            var token = _jwtService.GenerateToken(user);



            var refreshToken = _jwtService.GenerateRefreshToken();


            user.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);
            user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
            user.RefreshTokenRevokedAt = null;
            await _context.SaveChangesAsync();


            _logger.LogInformation(
             "Successful login. UserId={UserId}, Email={Email}, IP={IP}",
             user.Id,
             user.Email,
             ip
            );

            return Ok(new
            {
                AccessToken = token,
                RefreshToken = refreshToken
            });








            //return Ok(new
            //{

            //    Message = "Login successful",
            //    AccessToken = token
            //});





            #region returnvalue
            //return Ok(new
            //{
            //    user.Id,
            //    user.Email,
            //    user.Role,

            //    Student = user.Student == null ? null : new
            //    {
            //        user.Student.Id,
            //        user.Student.FullName,
            //        user.Student.UserId
            //    },

            //    Instructor = user.Instructor == null ? null : new
            //    {
            //        user.Instructor.Id,
            //        user.Instructor.FullName,
            //        user.Instructor.UserId
            //    }
            //});
            #endregion

        }





        [EnableRateLimiting("AuthPolicy")]
        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh(RefreshDto dto)
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";


            var user = await _context.Users
                .Include(u => u.Student)
                .Include(u => u.Instructor)
                .FirstOrDefaultAsync(u =>u.Email == dto.Email);

            if (user == null)
            {

                _logger.LogWarning("Failed login attempt for email: {Email} ip : {ip}", dto.Email, ip);

                return Unauthorized("Invalid refresh token.");

            }

            if (user.RefreshTokenExpiresAt <= DateTime.UtcNow)
            {
                _logger.LogWarning(
                      "Refresh attempt using expired token. UserId={UserId}, Email={Email}, IP={IP}",
                      user.Id,
                      user.Email,
                      ip
                  );

                return Unauthorized("Refresh token expired.");
            }


            if (user.RefreshTokenRevokedAt != null)
            {
                _logger.LogWarning(
                "Invalid refresh attempt (email not found). Email={Email}, IP={IP}",
                dto.Email,
                ip
                );

                return Unauthorized("Refresh token revoked.");
                
            }



            bool refreshValid = BCrypt.Net.BCrypt.Verify(dto.RefreshToken, user.RefreshTokenHash);
            
            if(!refreshValid)
            {

                _logger.LogWarning(
                    "Refresh attempt using expired token. UserId={UserId}, Email={Email}, IP={IP}",
                    user.Id,
                    user.Email,
                    ip
                );

                return Unauthorized("Invalid refresh token.");
            }

            var newAccessToken =
                _jwtService.GenerateToken(user);

            var newRefreshToken =
               _jwtService.GenerateRefreshToken();

            user.RefreshTokenHash =
                BCrypt.Net.BCrypt.HashPassword(newRefreshToken);

            user.RefreshTokenExpiresAt =
                DateTime.UtcNow.AddDays(7);

            user.RefreshTokenRevokedAt = null;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }


    }
}
