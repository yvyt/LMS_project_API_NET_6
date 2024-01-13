using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class ResourceDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Lesson { get; set; }
        public string Type { get; set; }
        public string Status {  get; set; }
        public string CreateBy {  get; set; }
        public string UpdateBy { get; set; }
        public string CreateAt { get; set; }
        public string UpdateAt { get; set; }
    }
}
