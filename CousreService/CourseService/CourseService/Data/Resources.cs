using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseService.Data
{
    [Table("Resource")]
    public class Resources
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string TypeId { get; set; }


        [Required]
        public string LessonId { get; set; }

        [Required]
        public bool Status { get; set; } = false;


        [Required]

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [Required]
        public string createBy { get; set; }
        [Required]
        public string updateBy { get; set; }
        [Required]
        public string DocumentId { get; set; }
        [Required]
        public bool IsActive { get; set; }=true;

        [Required]
        public bool IsDeleted { get; set; } = false;

        // Navigation property
        public virtual TypeFile Type { get; set; }
        public virtual Lesson Lessons { get; set; }
        public Documents Document { get; set; }

    }
}
