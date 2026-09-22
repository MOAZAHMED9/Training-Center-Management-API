using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Training_Center_Management_API.Authorization.StudentOwner
{
    public class StudentOwnerOrAdminHandler : AuthorizationHandler<StudentOwnerOrAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,StudentOwnerOrAdminRequirement requirement)
        {
            // 1. Admin يقدر يشوف أي Student
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            // 1. instructor يقدر يشوف أي Student
            if (context.User.IsInRole("Instructor"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }


            // 2. نجيب StudentId من الـ JWT
            var studentId = context.User.FindFirstValue("StudentId");

            if (string.IsNullOrEmpty(studentId))
            {
                return Task.CompletedTask;
            }

            // 3. نجيب StudentId من الـ URL
            if (context.Resource is HttpContext httpContext)
            {
                var routeStudentId = httpContext.Request.RouteValues["id"]?.ToString();

                // 4. نقارن
                if (studentId == routeStudentId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}