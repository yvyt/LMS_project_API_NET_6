using CourseService.Model;
using UserService.Model;

namespace CourseService.Service.ClassesService
{
    public interface IClassService
    {
        Task<ManagerRespone> AddClasses (ClassDTO classes);
        Task<ManagerRespone> DeleteClass(string id);
        Task<ManagerRespone> EditClass(string id,ClassDTO classDTO);
        Task<List<ClassDTO>> GetActiveClasses();
        Task<List<ClassDTO>> GetAll();
        Task<ClassDTO> GetById(string id);
    }
}
