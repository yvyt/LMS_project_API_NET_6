using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseService.Data
{

    [Table("Classes")]
    public class Classes
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Teacher { get; set; }

        [Required]
        public string CourseId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

       
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public bool IsDeleted { get; set; } = false;
        public virtual Course Course { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }


    }
}
