using ExamService.Data;
using ExamService.Model;

namespace ExamService.Service.ExamService
{
    public interface IExamService
    {
        Task<ManagerRespone> AddExam(ExamDTO examDTO);
        Task<ManagerRespone> DeleteExam(string id);
        Task<ManagerRespone> EditExam(ExamDTO examDTO);
        Task<List<ExamDTO>> GetActive();
        Task<List<ExamDTO>> GetAll();
        Task<ExamDTO> GetById(string id);
    }
}
