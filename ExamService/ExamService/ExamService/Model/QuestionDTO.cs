using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamService.Model
{
    public class QuestionDTO
    {
        
        public string? Id { get; set; }

        [Required]
        public string Class { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public bool IsMultipleChoice { get; set; } = false;
        [Required]
        public bool IsMultipleAnswer { get; set; } = false;

        [StringLength(500)]
        [Required]
        public string ContentQuestion { get; set; }

    }
}
