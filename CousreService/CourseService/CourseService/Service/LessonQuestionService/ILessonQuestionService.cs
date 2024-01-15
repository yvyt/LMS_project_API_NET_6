using CourseService.Model;

namespace CourseService.Service.LessonQuestionService
{
    public interface ILessonQuestionService
    {
        Task<ManagerRespone> AddLessonQuestion(LessonQuestionDTO questionDTO);
        Task<ManagerRespone> AddLessonQuestionFromTeacher(LessonQuestionFromTeacher questionDTO);
        Task<List<LessonQuestionDetail>> GetQuestionByTime();
    }
}
