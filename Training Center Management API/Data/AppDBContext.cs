using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Training_Center_Management_API.Models;
using Training_Center_Management_API.Services.Auditing;
//using TrainingCenterAPI.Models;

namespace Training_Center_Management_API.Data
{
    public class AppDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public AppDbContext(DbContextOptions<AppDbContext> options , ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }


        // محطتش virtual علشان مش هستخدم lazy
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);                    ///

            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Instructor>().ToTable("Instructors");
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Department>().ToTable("Departments");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollments");
            modelBuilder.Entity<CourseInstructor>().ToTable("CourseInstructors");
            modelBuilder.Entity<Payment>().ToTable("Payments");
            modelBuilder.Entity<Certificate>().ToTable("Certificates");
            modelBuilder.Entity<User>().ToTable("Users");




            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);





            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.User)
                .WithOne(u => u.Instructor)
                .HasForeignKey<Instructor>(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.Entity<User>(u =>
            {
                u.Property(e => e.FullName).IsRequired().HasMaxLength(150);
                u.Property(e => e.Email).IsRequired().HasMaxLength(150);
                u.HasIndex(e => e.Email).IsUnique();
                u.Property(x => x.PasswordHash).IsRequired();
                u.Property(x => x.Role).IsRequired().HasMaxLength(50);

                u.HasQueryFilter(x => !x.IsDeleted);
                u.Property(e => e.RefreshTokenHash);
                u.Property(e => e.RefreshTokenExpiresAt);
                u.Property(e => e.RefreshTokenRevokedAt);



            });




            modelBuilder.Entity<Department>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            modelBuilder.Entity<Department>(x =>
            x.HasIndex(d => d.Name).IsUnique());

            modelBuilder.Entity<Department>()
                .Property(x => x.Description)
                .HasMaxLength(200);




            modelBuilder.Entity<Student>(s =>
            {
                s.Property(e => e.FullName).IsRequired().HasMaxLength(200);

                s.Property(e => e.Email).IsRequired().HasMaxLength(200);
                s.HasIndex(e => e.Email).IsUnique();

                s.Property(e => e.Phone).IsRequired().HasMaxLength(20);

                s.Property(e => e.Address).IsRequired().HasMaxLength(200);
                s.HasIndex(s => s.UserId).IsUnique();



            }
            );

            
            //.Property(x => x.FullName)
            //.IsRequired()
            //.HasMaxLength(150) ;

            //modelBuilder.Entity<Student>()
            //    .Property(x => x.Email)
            //    .IsRequired()
            //    .HasMaxLength(150);

            //modelBuilder.Entity<Student>()
            //    .HasIndex(x => x.Email)
            //    .IsUnique();

            //modelBuilder.Entity<Student>()
            //    .Property(x => x.Phone)
            //    .HasMaxLength(20);

            //modelBuilder.Entity<Student>()
            //    .Property(x => x.Address)
            //    .HasMaxLength(250);


            // =========================
            // Instructor
            // =========================

            //modelBuilder.Entity<Instructor>()
            //    .Property(x => x.FullName)
            //    .IsRequired()
            //    .HasMaxLength(150);


            modelBuilder.Entity<Instructor>(i => i.Property(e => e.FullName).IsRequired().HasMaxLength(150));

            modelBuilder.Entity<Instructor>()
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Instructor>()
                .HasIndex(x => x.Email)               ////////////////////////////////
                .IsUnique();

            modelBuilder.Entity<Instructor>()
                .Property(x => x.Phone)
                .HasMaxLength(20);

            modelBuilder.Entity<Instructor>()
                .Property(x => x.Specialization)
                .HasMaxLength(100);

            modelBuilder.Entity<Instructor>()
                .Property(x => x.Salary)
                .HasPrecision(18, 2);           ////////////////////////////////

            modelBuilder.Entity<Instructor>()
                .HasIndex(i => i.UserId)
                .IsUnique();

            // =========================
            // Course
            // =========================

            modelBuilder.Entity<Course>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Course>()
                .Property(x => x.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Course>()
                .Property(x => x.Price)
                .HasPrecision(18, 2);


            // =========================
            // Department → Courses
            // One Department
            // Many Courses
            // =========================

            modelBuilder.Entity<Course>()
                .HasOne(x => x.Department)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);               /////////////////////////////


            // =========================
            // Enrollment
            // =========================

            modelBuilder.Entity<Enrollment>()
                .Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Enrollment>()
                .Property(x => x.Grade)
                .HasPrecision(5, 2);


            // Student → Enrollment
            // One Student → Many Enrollments

            modelBuilder.Entity<Enrollment>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);             ////////////////////////////////


            // Course → Enrollment
            // One Course → Many Enrollments

            modelBuilder.Entity<Enrollment>()
                .HasOne(x => x.Course)
                .WithMany(x => x.Enrollments)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);              ////////////


            // Prevent duplicate enrollment
            modelBuilder.Entity<Enrollment>()                        /////////////////////////
                .HasIndex(x => new
                {
                    x.StudentId,
                    x.CourseId
                })
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");          ////////////////////////


            // =========================
            // CourseInstructor
            // =========================

            // Course → CourseInstructor

            modelBuilder.Entity<CourseInstructor>()
                .HasOne(x => x.Course)
                .WithMany(x => x.CourseInstructors)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);


            // Instructor → CourseInstructor

            modelBuilder.Entity<CourseInstructor>()
                .HasOne(x => x.Instructor)
                .WithMany(x => x.CourseInstructors)
                .HasForeignKey(x => x.InstructorId)
                .OnDelete(DeleteBehavior.Cascade);


            // Prevent duplicate Course + Instructor

            modelBuilder.Entity<CourseInstructor>()
                .HasIndex(x => new
                {
                    x.CourseId,
                    x.InstructorId
                })
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");


            // =========================
            // Payment
            // =========================

            modelBuilder.Entity<Payment>()
                .Property(x => x.Amount)
                .HasPrecision(18, 2);                      //////////////////

            modelBuilder.Entity<Payment>()
                .Property(x => x.PaymentMethod)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Payment>()
                .Property(x => x.TransactionReference)
                .HasMaxLength(100);


            // Student → Payments

            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            // =========================
            // Certificate
            // =========================

            modelBuilder.Entity<Certificate>()
                .Property(x => x.CertificateNumber)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Certificate>()
                .HasIndex(x => x.CertificateNumber)
                .IsUnique();

            modelBuilder.Entity<Certificate>()
                .Property(x => x.Grade)
                .HasMaxLength(10);


            // Student → Certificates

            modelBuilder.Entity<Certificate>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Certificates)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            // Course → Certificates

            modelBuilder.Entity<Certificate>()
                .HasOne(x => x.Course)
                .WithMany()                                 //////
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Student>()
                .HasQueryFilter(x => !x.IsDeleted);  //////

            modelBuilder.Entity<Course>()
                .HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Department>()
                .HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Instructor>()
                .HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Enrollment>()
                .HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<CourseInstructor>()
                .HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Payment>()
                .HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.Entity<Certificate>()
                .HasQueryFilter(x => !x.IsDeleted);




            ////////////////////////////////// seeding data
            ///


            // =========================
            // Seed Data
            // =========================

            //var createdAt = new DateTime(2026, 1, 1);

            //// =========================
            //// Departments
            //// =========================

            //modelBuilder.Entity<Department>().HasData(
            //    new Department
            //    {
            //        Id = 1,
            //        Name = "Programming",
            //        Description = "Programming and Software Development",
            //        CreatedAt = createdAt,
            //        CreatedBy = "system",
            //        IsDeleted = false
            //    },
            //    new Department
            //    {
            //        Id = 2,
            //        Name = "Database",
            //        Description = "Database and SQL Server",
            //        CreatedAt = createdAt,
            //        CreatedBy = "system",
            //        IsDeleted = false
            //    },
            //   
        }


        public override async Task<int> SaveChangesAsync( CancellationToken cancellationToken = default)
        {
            ApplyAuditing();

            return await base.SaveChangesAsync(cancellationToken);
        }



        private void ApplyAuditing()
        {

            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;

                    entry.Entity.CreatedBy =  _currentUserService.UserName  ?? "System";      // لو null حوطلي system
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;

                    entry.Entity.UpdatedBy = _currentUserService.UserName ?? "System";

                    // منع تعديل بيانات الإنشاء ف الداتا بيز
                    entry.Property(x => x.CreatedAt)
                        .IsModified = false;

                    entry.Property(x => x.CreatedBy)
                        .IsModified = false;
                }
            }
        }



    }
}