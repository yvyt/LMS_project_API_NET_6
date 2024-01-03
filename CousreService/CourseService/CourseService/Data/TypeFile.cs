using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseService.Data
{
    [Table("TypeFile")]
    public class TypeFile
    {

        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id {  get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
