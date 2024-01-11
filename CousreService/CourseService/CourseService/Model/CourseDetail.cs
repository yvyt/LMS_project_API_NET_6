using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class CourseDetail
    {
        public string Id { get; set; }
       
        public string Name { get; set; }
        public DateTime ApprovalAt { get; set; } = DateTime.Now;
        public bool Status { get; set; } = false;
        public string CreateBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
