using CourseService.Model;

namespace CourseService.Service.LessonAnswerService
{
    public interface ILessonAnswerService
    {
        Task<ManagerRespone> AddLessonAnswer(LessonAnswerDTO answerDTO);
        Task<ManagerRespone> DeleteAnswer(string id);
        Task<ManagerRespone> EditLessonAnswer(string id, LessonAnswerDTO answerDTO);
        Task<List<LessonAnswerDetail>> GetActive();
        Task<LessonAnswerDetail> GetById(string id);
        Task<List<LessonAnswerDetail>> GetByQuestion(string questionId);
    }
}
