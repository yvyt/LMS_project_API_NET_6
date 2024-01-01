using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class ClassDTO
    {
        [Required]
        public string Teacher { get; set; }

        [Required]
        public string Course { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }


        [MaxLength(255)]
        public string Description { get; set; }

        public string? Id {  get; set; }
    }
}
