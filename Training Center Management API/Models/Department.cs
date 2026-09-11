
namespace Training_Center_Management_API.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}