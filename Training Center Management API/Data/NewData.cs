using Training_Center_Management_API.Models;
namespace Training_Center_Management_API.Data
{
    public class NewData
    {
        private static readonly DateTime createdAt = DateTime.UtcNow;
        public static List<Student> Students { get; set; } = new List<Student>
        {
            new Student
            {
                //Id = 1,
                FullName = "Ahmed Ali",
                Email = "ahmed@gmail.com",
                Phone = "01011111111",
                BirthDate = new DateTime(2002, 5, 10),
                Address = "Sohag",
                EnrollmentDate = new DateTime(2026, 1, 10),
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Student
            {
                //Id = 2,
                FullName = "Mohamed Hassan",
                Email = "mohamed@gmail.com",
                Phone = "01122222222",
                BirthDate = new DateTime(2001, 8, 15),
                Address = "Cairo",
                EnrollmentDate = new DateTime(2026, 1, 12),
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Student
            {
                //Id = 3,
                FullName = "Sara Ahmed",
                Email = "sara@gmail.com",
                Phone = "01233333333",
                BirthDate = new DateTime(2003, 1, 20),
                Address = "Giza",
                EnrollmentDate = new DateTime(2026, 1, 15),
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Student
            {
                //Id = 4,
                FullName = "Omar Khaled",
                Email = "omar@gmail.com",
                Phone = "01044444444",
                BirthDate = new DateTime(2002, 11, 5),
                Address = "Luxor",
                EnrollmentDate = new DateTime(2026, 1, 18),
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Student
            {
                //Id = 5,
                FullName = "Menna Ali",
                Email = "menna@gmail.com",
                Phone = "01155555555",
                BirthDate = new DateTime(2003, 7, 12),
                Address = "Sohag",
                EnrollmentDate = new DateTime(2026, 1, 20),
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            }

        };


        public static List<Department> Departments = new List<Department>
        {
            new Department
            {
            //Id = 1,
            Name = "Programming",
            Description = "Programming and Software Development",
            CreatedAt = createdAt,
            CreatedBy = "system",
            IsDeleted = false
            },
            new Department
            {
                //Id = 2,
                Name = "Database",
                Description = "Database and SQL Server",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Department
            {
                //Id = 3,
                Name = "Web Development",
                Description = "Web and Backend Development",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Department
            {
                //Id = 4,
                Name = "Artificial Intelligence",
                Description = "AI and Machine Learning",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Department
            {
                //Id = 5,
                Name = "Networking",
                Description = "Computer Networks",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            }
        };


        public static List<Course> Courses = new()
        {
            new Course
            {
                //Id = 1,
                Name = "C# Fundamentals",
                Description = "Learn C# programming fundamentals",
                Price = 3000,
                DurationInHours = 40,
                DepartmentId = 1,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Course
            {
                //Id = 2,
                Name = "Advanced C#",
                Description = "Advanced C# programming",
                Price = 4000,
                DurationInHours = 50,
                DepartmentId = 1,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Course
            {
                //Id = 3,
                Name = "SQL Server",
                Description = "SQL Server and Database Fundamentals",
                Price = 2500,
                DurationInHours = 35,
                DepartmentId = 2,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Course
            {
                //Id = 4,
                Name = "ASP.NET Core API",
                Description = "Build RESTful APIs using ASP.NET Core",
                Price = 4500,
                DurationInHours = 60,
                DepartmentId = 3,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Course
            {
                //Id = 5,
                Name = "AI Fundamentals",
                Description = "Artificial Intelligence Fundamentals",
                Price = 5000,
                DurationInHours = 70,
                DepartmentId = 4,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Course
            {
                //Id = 6,
                Name = "Computer Networks",
                Description = "Networking Fundamentals",
                Price = 3000,
                DurationInHours = 45,
                DepartmentId = 5,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            }
        };


        public static List<Instructor> Instructors = new()
        {
            new Instructor
            {
                //Id = 1,
                FullName = "Ahmed Mohamed",
                Email = "ahmed.instructor@gmail.com",
                Phone = "01066666666",
                Specialization = "C#",
                Salary = 18000,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Instructor
            {
                //Id = 2,
                FullName = "Mahmoud Ali",
                Email = "mahmoud.instructor@gmail.com",
                Phone = "01177777777",
                Specialization = "SQL Server",
                Salary = 16000,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Instructor
            {
                //Id = 3,
                FullName = "Youssef Hassan",
                Email = "youssef.instructor@gmail.com",
                Phone = "01288888888",
                Specialization = "ASP.NET Core",
                Salary = 20000,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Instructor
            {
                //Id = 4,
                FullName = "Sara Khaled",
                Email = "sara.instructor@gmail.com",
                Phone = "01099999999",
                Specialization = "AI",
                Salary = 22000,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Instructor
            {
                //Id = 5,
                FullName = "Omar Ahmed",
                Email = "omar.instructor@gmail.com",
                Phone = "01112345678",
                Specialization = "Networking",
                Salary = 17000,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            }
        };


        public static List<CourseInstructor> courseInstructors = new()
        {
            new CourseInstructor
            {
                //Id = 1,
                CourseId = 1,
                InstructorId = 1,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new CourseInstructor
            {
                //Id = 2,
                CourseId = 2,
                InstructorId = 1,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new CourseInstructor
            {
                //Id = 3,
                CourseId = 3,
                InstructorId = 2,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new CourseInstructor
            {
                //Id = 4,
                CourseId = 4,
                InstructorId = 3,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new CourseInstructor
            {
                //Id = 5,
                CourseId = 5,
                InstructorId = 4,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new CourseInstructor
            {
                //Id = 6,
                CourseId = 6,
                InstructorId = 5,
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            }
        };



        public static List<Enrollment> enrollments = new()
        {
            new Enrollment
            {
                //Id = 1,
                StudentId = 1,
                CourseId = 1,
                EnrollmentDate = new DateTime(2026, 2, 1),
                Grade = 90,
                Status = "Completed",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Enrollment
            {
                //Id = 2,
                StudentId = 1,
                CourseId = 4,
                EnrollmentDate = new DateTime(2026, 2, 5),
                Grade = 85,
                Status = "InProgress",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Enrollment
            {
                //Id = 3,
                StudentId = 2,
                CourseId = 1,
                EnrollmentDate = new DateTime(2026, 2, 2),
                Grade = 78,
                Status = "Completed",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Enrollment
            {
                //Id = 4,
                StudentId = 2,
                CourseId = 3,
                EnrollmentDate = new DateTime(2026, 2, 4),
                Grade = 88,
                Status = "Completed",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Enrollment
            {
                //Id = 5,
                StudentId = 3,
                CourseId = 4,
                EnrollmentDate = new DateTime(2026, 2, 7),
                Grade = 95,
                Status = "Completed",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Enrollment
            {
                //Id = 6,
                StudentId = 4,
                CourseId = 2,
                EnrollmentDate = new DateTime(2026, 2, 8),
                Grade = 82,
                Status = "InProgress",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Enrollment
            {
                //Id = 7,
                StudentId = 5,
                CourseId = 5,
                EnrollmentDate = new DateTime(2026, 2, 10),
                Grade = 91,
                Status = "Completed",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            }
        };


        public static List<Payment> Payments = new()
        {
             new Payment
            {
                //Id = 1,
                StudentId = 1,
                Amount = 3000,
                PaymentDate = new DateTime(2026, 2, 1),
                PaymentMethod = "Cash",
                TransactionReference = "PAY-001",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Payment
            {
                //Id = 2,
                StudentId = 1,
                Amount = 2000,
                PaymentDate = new DateTime(2026, 2, 5),
                PaymentMethod = "Visa",
                TransactionReference = "PAY-002",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Payment
            {
                //Id = 3,
                StudentId = 2,
                Amount = 2500,
                PaymentDate = new DateTime(2026, 2, 2),
                PaymentMethod = "Cash",
                TransactionReference = "PAY-003",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Payment
            {
                //Id = 4,
                StudentId = 2,
                Amount = 3000,
                PaymentDate = new DateTime(2026, 2, 4),
                PaymentMethod = "Visa",
                TransactionReference = "PAY-004",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Payment
            {
                //Id = 5,
                StudentId = 3,
                Amount = 4500,
                PaymentDate = new DateTime(2026, 2, 7),
                PaymentMethod = "Bank",
                TransactionReference = "PAY-005",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Payment
            {
                //Id = 6,
                StudentId = 4,
                Amount = 2000,
                PaymentDate = new DateTime(2026, 2, 8),
                PaymentMethod = "Cash",
                TransactionReference = "PAY-006",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Payment
            {
                //Id = 7,
                StudentId = 5,
                Amount = 5000,
                PaymentDate = new DateTime(2026, 2, 10),
                PaymentMethod = "Visa",
                TransactionReference = "PAY-007",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            }
        };


        public static List<Certificate> Certificates = new()
        {
            new Certificate
            {
                //Id = 1,
                StudentId = 1,
                CourseId = 1,
                CertificateNumber = "CERT-001",
                IssueDate = new DateTime(2026, 3, 1),
                Grade = "A",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Certificate
            {
                //Id = 2,
                StudentId = 2,
                CourseId = 1,
                CertificateNumber = "CERT-002",
                IssueDate = new DateTime(2026, 3, 2),
                Grade = "B+",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Certificate
            {
                //Id = 3,
                StudentId = 2,
                CourseId = 3,
                CertificateNumber = "CERT-003",
                IssueDate = new DateTime(2026, 3, 3),
                Grade = "A",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Certificate
            {
                //Id = 4,
                StudentId = 3,
                CourseId = 4,
                CertificateNumber = "CERT-004",
                IssueDate = new DateTime(2026, 3, 4),
                Grade = "A+",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            },
            new Certificate
            {
                //Id = 5,
                StudentId = 5,
                CourseId = 5,
                CertificateNumber = "CERT-005",
                IssueDate = new DateTime(2026, 3, 5),
                Grade = "A",
                CreatedAt = createdAt,
                CreatedBy = "system",
                IsDeleted = false
            }
        };


        public static List<User> users = new List<User>
        {


            new User
            {
                //Id = 1,
                FullName = "Ahmed Ali",
                Email = "ahmed@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Student",
                CreatedAt = createdAt,
                CreatedBy = "system",
            },

            new User
            {
                //Id = 2,
                FullName = "Mohamed Hassan",
                Email = "mohamed@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword( "123456"),
                Role = "Student",
                CreatedAt = createdAt,
                CreatedBy = "system",
            },

            new User
            {
                //Id = 3,
                FullName = "Sara Ahmed",
                Email = "sara@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword( "123456"),
                Role = "Student",
                CreatedAt = createdAt,
                CreatedBy = "system",
            },



            new User
            {
                //Id = 6,
                FullName = "Ahmed Mohamed",
                Email = "ahmed.instructor@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword( "123456"),
                Role = "Instructor",
                CreatedAt = createdAt,
                CreatedBy = "system",
            },

            new User
            {
                //Id = 7,
                FullName = "Mahmoud Ali",
                Email = "mohamed.instructor@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Instructor",
                CreatedAt = createdAt,
                CreatedBy = "system",
            },

            new User
            {
                //Id = 8,
                FullName = "Youssef Hassan",
                Email = "youssef.instructor@gmail.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Instructor",
                CreatedAt = createdAt,
                CreatedBy = "system",
            },

        


            new User
            {
                //Id = 11,
                FullName = "System Administrator",
                Email = "admin@trainingcenter.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Admin",
                CreatedAt = createdAt,
                CreatedBy = "system",
            }
        };





    }
}
