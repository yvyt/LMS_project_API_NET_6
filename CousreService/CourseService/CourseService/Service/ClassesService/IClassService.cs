using CourseService.Model;
using CourseService.Model;

namespace CourseService.Service.ClassesService
{
    public interface IClassService
    {
        Task<ManagerRespone> AddClasses (ClassDTO classes);
        Task<ManagerRespone> DeleteClass(string id);
        Task<ManagerRespone> EditClass(string id,ClassDTO classDTO);
        Task<List<ClassesDetails>> GetActiveClasses();
        Task<List<ClassesDetails>> GetAll();
        Task<ClassDTO> GetById(string id);
        Task<ClassesDetails> GetDetailClass(string id);

    }
}
