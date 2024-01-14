using ExamService.Data;
using ExamService.Model;

namespace ExamService.Service.ExamService
{
    public interface IExamService
    {
        Task<ManagerRespone> AddExam(ExamDTO examDTO);
        Task<ManagerRespone> ApproveExam(string id);
        Task<ManagerRespone> DeleteExam(string id);
        Task<ManagerRespone> EditExam(ExamDTO examDTO);
        Task<List<ExamDTO>> GetActive();
        Task<List<ExamDTO>> GetAll();
        Task<ExamDetail> GetById(string id);
    }
}
