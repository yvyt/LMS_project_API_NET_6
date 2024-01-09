using ExamService.Model;

namespace ExamService.Service.ExamQuestionService
{
    public interface IExamQuestionService
    {
        Task<ManagerRespone> AddExamQuestion(ExamQuestionDTO examQuestionDTO);
        Task<ExamQuestionDTO> GetByExam(string examId);
        Task<ExamQuestionDTO> GetById(string id);
    }
}
