using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseService.Data
{

    [Table("StudentCourse")]
    public class StudentCourse
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string ClassId { get; set; }
        [Required]
        public string StudentId { get; set; }

        public virtual Classes Class { get; set; }


    }
}
