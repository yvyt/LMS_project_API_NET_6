using System.ComponentModel.DataAnnotations;

namespace ExamService.Model
{
    public class ExamDTO
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }
        [Required]
        public bool IsMutipleChoice { get; set; } = true;

        [Required]
        public string Class { get; set; }
       
        [Required]
        public int NumberQuestion { get; set; }
        public IFormFile? FileContent { get; set; }

    }
}
