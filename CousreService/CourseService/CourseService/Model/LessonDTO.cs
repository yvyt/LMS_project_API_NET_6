using CourseService.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class LessonDTO
    {
        [Required]

        public string Topic { get; set; }
        [Required]
        public string Type { get; set; }

        [Required]
        public string Title { get; set; }

        public IFormFile FileContent { get; set; }

        public string? Id {  get; set; }
    }
}
