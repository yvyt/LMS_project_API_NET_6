using System.Globalization;

namespace CourseService.Model
{
    public class LessonQuestionDetail
    {
        public string Id {  get; set; }
        public string Title { get; set; }
        public string Content {  get; set; }
        public string Lesson {  get; set; }
        public string createAt {  get; set; }
        public string createBy {  get; set; }
    }
}
