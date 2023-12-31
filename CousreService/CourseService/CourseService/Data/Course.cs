using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseService.Data
{
    [Table("Course")]
        public class Course
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
            public DateTime ApprovalAt { get; set; } = DateTime.Now;
            [Required]
            public bool Status { get; set; } = false;
            [Required]
            public string userId {  get; set; }
            [Required]
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            [Required]

            public DateTime UpdatedAt { get; set; } = DateTime.Now;
            [Required]
            public bool IsActive { get; set; } = true;
            [Required]
            public bool IsDeleted { get; set; } = false;
            
            public virtual ICollection<Classes> Classes { get; set; }
            


    }
}

