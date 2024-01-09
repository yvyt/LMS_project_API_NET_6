using ExamService.Model;

namespace ExamService.Service.ExamQuestionService
{
    public interface IExamQuestionService
    {
        Task<ManagerRespone> AddExamQuestion(ExamQuestionDTO examQuestionDTO);
        Task<ManagerRespone> AddMoreQuestion(QuestionExamAdd examQuestionDTO);
        Task<ManagerRespone> DeleteExamQuestion(string id);
        Task<ManagerRespone> EditExamQuestion(ExamQuestionUpdateDTO examQuestionDTO);
        Task<List<ExamQuestionUpdateDTO>> GetActive();
        Task<ExamQuestionDTO> GetByExam(string examId);
        Task<ExamQuestionDTO> GetById(string id);
    }
}
