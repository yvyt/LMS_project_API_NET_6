using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseService.Data
{
    [Table("Topic")]
    public class Topic
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string ClassId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public bool IsDeleted { get; set; } = false;

        public virtual Classes Classes { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }


    }
}
