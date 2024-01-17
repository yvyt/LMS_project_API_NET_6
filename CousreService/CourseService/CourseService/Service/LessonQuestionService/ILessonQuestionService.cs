using CourseService.Model;

namespace CourseService.Service.LessonQuestionService
{
    public interface ILessonQuestionService
    {
        Task<ManagerRespone> AddLessonQuestion(LessonQuestionDTO questionDTO);
        Task<ManagerRespone> AddLessonQuestionFromTeacher(LessonQuestionFromTeacher questionDTO);
        Task<ManagerRespone> DeleteQuestion(string id);
        Task<ManagerRespone> EditQuestionFromTeacher(string id,LessonQuestionFromTeacher questionDTO);
        Task<List<LessonQuestionDetail>> GetActive();
        Task<List<LessonQuestionDetail>> GetByLesson(string lessonId);
        Task<List<LessonQuestionDetail>> GetQuestionByAnswer();
        Task<List<LessonQuestionDetail>> GetQuestionByTime();
        Task<ManagerRespone> LikeQuestion(string id);
    }
}
