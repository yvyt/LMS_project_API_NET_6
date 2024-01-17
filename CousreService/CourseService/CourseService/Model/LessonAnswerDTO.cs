using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class LessonAnswerDTO
    {
        
        [Required]
        public string LessonQuestionId { get; set; }
        [Required]
        public string ContentAnswer { get; set; }
        
    }
}
