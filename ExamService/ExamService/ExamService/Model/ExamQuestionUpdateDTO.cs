using System.ComponentModel.DataAnnotations;

namespace ExamService.Model
{
    public class ExamQuestionUpdateDTO
    {
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }

        [Required]
        public string Type { get; set; }
        [Required]
        public bool IsMutipleChoice { get; set; } = true;

        [Required]
        public string Class { get; set; }

        [Required]
        public int NumberQuestion { get; set; }
        [Required]
        public String Questions { get; set; }
    }
}
