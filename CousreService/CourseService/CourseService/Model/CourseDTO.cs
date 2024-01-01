using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class CourseDTO
    {
        [Required]
        public string Name { get; set; }
        

    }
}
