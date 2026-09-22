using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Training_Center_Management_API.Authorization.StudentOwner
{
    public class StudentOwnerOrAdminHandler : AuthorizationHandler<StudentOwnerOrAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,StudentOwnerOrAdminRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (context.User.IsInRole("Instructor"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }


            var studentId = context.User.FindFirstValue("StudentId");

            if (string.IsNullOrEmpty(studentId))
            {
                return Task.CompletedTask;
            }

            // . نجيب StudentId من الـ URL
            if (context.Resource is HttpContext httpContext)
            {
                var routeStudentId = httpContext.Request.RouteValues["id"]?.ToString();

                if (studentId == routeStudentId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}