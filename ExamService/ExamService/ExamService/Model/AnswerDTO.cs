using System.ComponentModel.DataAnnotations;

namespace ExamService.Model
{
    public class AnswerDTO
    {
        public string? Id { get; set; }

        [Required]
        public string Question { get; set; }

        [StringLength(500)]
        [Required]
        public string ContentAnswer { get; set; }

        [Required]

        public bool IsCorrect { get; set; } = false;
    }
}
