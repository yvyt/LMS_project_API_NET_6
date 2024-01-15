using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace CourseService.Data
{
    [Table("Lesson")]

    public class Lesson
    {

        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string TopicId { get; set; }
        [Required]
        public string TypeId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
      
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public bool IsDeleted { get; set; } = false;
        [Required]
        public string createBy { get; set; }
        [Required]
        public string updateBy { get; set; }
        [Required]
        public string DocumentId { get; set; }
        public Documents Document { get; set; }
        public virtual Topic Topics { get; set; }

        public virtual TypeFile TypeFile { get; set; }
        public virtual ICollection<Resources> Resources { get; set; }

        public virtual ICollection<LessonQuestion> LessonQuestion { get; set; }
    }

}
