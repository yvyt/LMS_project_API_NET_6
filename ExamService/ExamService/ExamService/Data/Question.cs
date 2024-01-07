using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamService.Data
{
    [Table("Question")]
    public class Question
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public int Level { get; set; }
        [Required]
        public bool IsMultipleChoice { get; set; } = false;
        [Required]
        public bool IsMultipleAnswer { get; set; } = false;

        [StringLength(500)]
        [Required]
        public string ContentQuestion { get; set; }

        [Required]
        public string ClassId { get; set; }

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


        public virtual ICollection<Answer> Answers { get; set; }
    }
}
