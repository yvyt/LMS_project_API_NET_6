using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamService.Data
{
    [Table("Exam")]
    public class Exam
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string TypeId { get; set; }
        [Required]
        public bool IsMutipleChoice { get; set; } = true;

        [Required]
        public string ClassId { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public bool Status { get; set; } = false;
        [Required]
        public int NumberQuestion { get; set; }
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


        [Required]
        public DateTime DateBegin { get; set; } = DateTime.Now;
        [Required]
        public string DocumentId { get; set; }

        // Navigation property
        public virtual ExamType Type { get; set; }

    }
}
