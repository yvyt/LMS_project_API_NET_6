using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamService.Data
{
    [Table("ExamQuestion")]
    public class ExamQuestion
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string QuestionId { get; set; }

        [Required]
        public string ExamId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [Required]
        public string createBy { get; set; }
        [Required]
        public string updateBy { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Question Question { get; set; }
        public virtual Exam Exam { get; set; }
    }
}
