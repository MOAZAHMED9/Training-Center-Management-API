using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Training_Center_Management_API.Authorization.InstructorOwnerCourse
{
    public class InstructorOwnerCourseHandler : AuthorizationHandler<InstructorOwnerCourseRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, InstructorOwnerCourseRequirement requirement)
        {
           if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
           
            var instructorId = context.User.FindFirstValue("InstructorId");
            if (string.IsNullOrEmpty(instructorId))
            {
                return Task.CompletedTask;
            }

            if (context.Resource is HttpContext httpContext)
            {
                var routeInstructorId = httpContext.Request.RouteValues["instructorId"]?.ToString();
                if (instructorId == routeInstructorId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;

        }
    }
}
