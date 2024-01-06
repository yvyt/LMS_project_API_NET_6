using ExamService.Data;
using ExamService.Model;

namespace ExamService.Service.ExamService
{
    public interface IExamService
    {
        Task<ManagerRespone> AddExam(ExamDTO examDTO);
    }
}
