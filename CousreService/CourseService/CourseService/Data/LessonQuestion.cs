using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseService.Data
{
    [Table("LessonQuestion")]
    public class LessonQuestion
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string LessonId {  get; set; }
        public string TopicId {  get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ContentQuestion { get; set; }
        [Required]
        public bool IsLike { get; set; } = false;
        [Required]
        public bool IsAnswer { get; set; } = false;
        [Required]
        public DateTime createAt { get; set; } = DateTime.Now;
        [Required]
        public string createBy { get; set; }

        public virtual Lesson Lesson { get; set; }
        public virtual ICollection<LessonAnswer> LessonAnswers { get; set; }
    }
}
