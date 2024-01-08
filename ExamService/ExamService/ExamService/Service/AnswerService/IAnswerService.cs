using ExamService.Model;

namespace ExamService.Service.AnswerService
{
    public interface IAnswerService
    {
        Task<ManagerRespone> AddAnswer(AnswerDTO answerDTO);
        Task<ManagerRespone> DeleteAnswer(string id);
        Task<ManagerRespone> EditAnswer(AnswerDTO answerDTO);
        Task<List<AnswerDTO>> GetActive();
        Task<List<AnswerDTO>> GetAll();
        Task<AnswerDTO> GetById(string id);
        Task<List<AnswerDTO>> GetByQuestion(string id);
        Task<AnswerDTO> GetCorrectAnswer(string questionId);
    }
}
