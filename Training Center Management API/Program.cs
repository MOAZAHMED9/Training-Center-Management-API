
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using Training_Center_Management_API.Authorization;
using Training_Center_Management_API.Authorization.Enrollment;
using Training_Center_Management_API.Authorization.InstructorOwnerCourse;
using Training_Center_Management_API.Authorization.StudentOwner;
using Training_Center_Management_API.Data;
using Training_Center_Management_API.Middleware;
using Training_Center_Management_API.Services;
using Training_Center_Management_API.Services.Auditing;
using Training_Center_Management_API.Services.certificate;
using Training_Center_Management_API.Services.Course;
using Training_Center_Management_API.Services.CourseInstructorService;
using Training_Center_Management_API.Services.DashBoard;
using Training_Center_Management_API.Services.Department;
using Training_Center_Management_API.Services.Enrollment;
using Training_Center_Management_API.Services.Instrucrot;
using Training_Center_Management_API.Services.Payment;
using Training_Center_Management_API.Services.Student;

namespace Training_Center_Management_API
{
    public class Program
    {
        public static void Main(string[] args)
        {



            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendPolicy", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });           



            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
            ));     


            builder.Services.AddScoped<IAuthorizationHandler, EnrollmentOwnerCourseHandler>(); 
            builder.Services.AddScoped<IAuthorizationHandler, InstructorOwnerCourseHandler>(); 
            builder.Services.AddScoped<IAuthorizationHandler, StudentOwnerOrAdminHandler>(); 
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();           
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<ICourseInstructorService, CourseInstructorService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IDashboardService, DashBoardService>();
            builder.Services.AddScoped<ICertificateService, CertificateService>();
            builder.Services.AddScoped<JwtService>();           





            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                           Array.Empty<string>()

                    }
                });
            });      






             
            #region   اعدادات الjwt يعني صحيحه ولا لا 

            var jwtSettings = builder.Configuration.GetSection("Jwt");                

            var key = Encoding.UTF8.GetBytes( jwtSettings["Key"]!);    



            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)     
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = jwtSettings["Issuer"],
                            ValidAudience = jwtSettings["Audience"],

                            IssuerSigningKey = new SymmetricSecurityKey(key),

                            ClockSkew = TimeSpan.Zero
                        };
                });
            #endregion            





            builder.Services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("AuthPolicy", limiterOptions =>
                {
                    limiterOptions.PermitLimit = 5;

                    limiterOptions.Window = TimeSpan.FromMinutes(1);

                    limiterOptions.QueueLimit = 0;                 //متخزنش طلب
                });

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });                   




            builder.Services.AddAuthorization(options =>                     
            {
                options.AddPolicy("StudentOwner", policy =>
                {
                    policy.RequireAuthenticatedUser();

                    policy.AddRequirements(new StudentOwnerOrAdminRequirement());
                });
            });


            builder.Services.AddAuthorization(options =>                      
            {
                options.AddPolicy("InstructorOwner", policy =>
                {
                    policy.RequireAuthenticatedUser();

                    policy.AddRequirements(new InstructorOwnerCourseRequirement());
                });
            });


            builder.Services.AddAuthorization(options =>                      
            {
                options.AddPolicy("EnrollmentOwner", policy =>
                {
                    policy.RequireAuthenticatedUser();

                    policy.AddRequirements(new EnrollmentOwnerCourseRequirement());
                });
            });





            builder.Services.AddHttpContextAccessor();               




            var app = builder.Build();




            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>(); 

                DbSeeder.Seed(context);
            }      



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("FrontendPolicy");


            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRateLimiter();

            app.UseAuthentication();





            app.UseMiddleware<SecurityLoggingMiddleware>();

            app.UseAuthorization();

         

            app.MapControllers();



            app.Run();
        }
    }
}
