using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class LessonQuestionDTO
    {
        [Required]

        public string LessonId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ContentQuestion { get; set; }
    }
}
