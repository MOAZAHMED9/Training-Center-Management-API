using Training_Center_Management_API.Models;

namespace Training_Center_Management_API.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {


                if (!context.Departments.Any())
                {
                    context.Departments.AddRange(
                        NewData.Departments
                    );
                }


                if (!context.Students.Any())
                {
                    context.Students.AddRange(
                        NewData.Students
                    );
                }

                if (!context.Instructors.Any())
                {
                    context.Instructors.AddRange(
                        NewData.Instructors
                    );
                }

                if (!context.Courses.Any())
                {
                    context.Courses.AddRange(
                        NewData.Courses
                    );
                }

                if (!context.CourseInstructors.Any())
                {
                    context.CourseInstructors.AddRange(
                        NewData.courseInstructors);
                }


                if (!context.Enrollments.Any())
                {
                    context.Enrollments.AddRange(
                        NewData.enrollments);

                }


                if (!context.Payments.Any())
                {
                    context.Payments.AddRange(
                        NewData.Payments);
                }


                if (!context.Certificates.Any())
                {
                    context.Certificates.AddRange(
                        NewData.Certificates);

                }


                if (!context.Users.Any())
                {
                    context.Users.AddRange(NewData.users);
                }


                context.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
}
    }
}