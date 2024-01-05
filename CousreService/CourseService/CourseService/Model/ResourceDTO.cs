using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class ResourceDTO
    {
        public string? Id { get; set; }

        [Required]

        public string Lesson { get; set; }
        [Required]
        public string Type { get; set; }
        public string? Name { get; set; }

        public IFormFile? FileContent { get; set; }

    }
}
