using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CourseService.Data
{
    public class Documents
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DocumentId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string link {  get; set; }
        public Lesson Lesson { get; set; }
    }
}