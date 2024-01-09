using System.ComponentModel.DataAnnotations;

namespace ExamService.Model
{
    public class QuestionExamAdd
    {
     
        [Required]
        public string Exam { get; set; }

        [Required]
        public List<String> Questions { get; set; }
    }
}
