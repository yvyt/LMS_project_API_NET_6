using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class TopicDTO
    {
        public string? Id { get; set; }
        [Required]
        public string Class { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
