using CourseService.Model;
using UserService.Model;

namespace CourseService.Service.ClassesService
{
    public interface IClassService
    {
        Task<ManagerRespone> AddClasses (ClassDTO classes);
        Task<List<ClassDTO>> GetAll();
        Task<ClassDTO> GetById(string id);
    }
}
