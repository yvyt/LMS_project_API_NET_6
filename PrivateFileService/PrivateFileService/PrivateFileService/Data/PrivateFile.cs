using Microsoft.VisualBasic.FileIO;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrivateFileService.Data
{
    [Table("PrivateFile")]
    public class PrivateFile
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Size { get; set; }

        [Required]

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Required]

        public string createBy { get; set; }
        [Required]

        public string updateBy { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        [Required]
        public string DocumentId { get; set; }

    }
}
