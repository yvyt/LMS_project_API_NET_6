using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamService.Data
{
    [Table("Answer")]
    public class Answer
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string QuestionId { get; set; }

        [StringLength(500)]
        [Required]
        public string ContentAnswer { get; set; }

        [Required]

        public bool IsCorrect { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        [Required]
        public string createBy { get; set; }
        [Required]
        public string updateBy { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        // Navigation property
        public virtual Question Question { get; set; }
    }

}
