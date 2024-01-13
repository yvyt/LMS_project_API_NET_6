using System.ComponentModel.DataAnnotations;

namespace CourseService.Model
{
    public class ClassesDetails
    {
        public string Id { get; set; }
        public string Teacher { get; set; }
        public string Course { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NumberOfLesson {  get; set; }
        public string NumberOfResource {  get; set; }
    }
}
