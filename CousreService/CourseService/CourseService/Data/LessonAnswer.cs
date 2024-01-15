using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseService.Data
{
    [Table("LessonAnswer")]
    public class LessonAnswer
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string LessonQuestionId { get; set; }
        [Required]
        public string ContentAnswer {  get; set; }
        [Required]
        public string createBy {  get; set; }
        [Required]
        public string updateBy { get; set; }
        [Required]
        public DateTime createAt { get; set; } = DateTime.Now;
        [Required]
        public DateTime updateAt { get; set; }= DateTime.Now;
        public virtual LessonQuestion LessonQuestion { get; set; }
    }
}
