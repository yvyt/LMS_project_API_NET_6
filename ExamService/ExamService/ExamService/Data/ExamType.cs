using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamService.Data
{
    [Table("ExamType")]
    public class ExamType
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Column("name")]
        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        public ICollection<Exam> Exams { get; set; }

    }
}