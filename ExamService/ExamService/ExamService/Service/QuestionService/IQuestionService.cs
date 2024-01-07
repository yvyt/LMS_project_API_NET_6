using ExamService.Model;

namespace ExamService.Service.QuestionService
{
    public interface IQuestionService
    {
        Task<ManagerRespone> AddQuestion(QuestionDTO questionDTO);
        Task<ManagerRespone> DeleteQuestion(string id);
        Task<ManagerRespone> EditQuestion(QuestionDTO questionDTO);
        Task<List<QuestionDTO>> GetActive();
        Task<List<QuestionDTO>> GetAll();
        Task<QuestionDTO> GetById(string id);
    }
}
