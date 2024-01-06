using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace ExamService.Model
{
    public class DocumentDTO
    {
        public string? DocumentId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string Link { get; set; }
    }
}
