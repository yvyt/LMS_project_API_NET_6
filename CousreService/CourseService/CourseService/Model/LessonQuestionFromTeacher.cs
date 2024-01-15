using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class LessonQuestionFromTeacher
    {
        public string? TopicId { get; set; }
        [Required]

        public string LessonId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ContentQuestion { get; set; }
    }
}
