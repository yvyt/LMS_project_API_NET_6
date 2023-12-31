using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class Course
    {
        [Required]
        public string Name { get; set; }
       

    }
}
