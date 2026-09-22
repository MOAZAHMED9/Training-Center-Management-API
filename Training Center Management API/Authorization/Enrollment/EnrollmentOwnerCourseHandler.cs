using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Training_Center_Management_API.Data;

namespace Training_Center_Management_API.Authorization.Enrollment
{
    public class EnrollmentOwnerCourseHandler : AuthorizationHandler<EnrollmentOwnerCourseRequirement>
    {
        private readonly AppDbContext _context;

        public EnrollmentOwnerCourseHandler(AppDbContext context)
        {
            _context = context;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,EnrollmentOwnerCourseRequirement requirement)
        {
           
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return;
            }

            var instructorIdClaim = context.User.FindFirstValue("InstructorId");

            if (!int.TryParse(instructorIdClaim, out var instructorId))                        
                return;

                
            if (context.Resource is not HttpContext httpContext)
                return;

            var enrollmentIdValue = httpContext.Request.RouteValues["id"]?.ToString();

            if (!int.TryParse(enrollmentIdValue, out var enrollmentId))                         
                return;

            

            var ownsEnrollment = await _context.Enrollments
                .AnyAsync(e => e.Id == enrollmentId &&
                e.Course.CourseInstructors.Any(ci => ci.InstructorId == instructorId));

            if (ownsEnrollment)
            {
                context.Succeed(requirement);
            }
        }
    }
}
