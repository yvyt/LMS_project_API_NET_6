using CourseService.Model;
using UserService.Model;

namespace CourseService.Service.ResourceService
{
    public interface IResourceService
    {
        Task<ManagerRespone> AddResouce(ResourceDTO resouceDTO);
        Task<ManagerRespone> EditResource(ResourceDTO resourceDTO);
        Task<List<ResourceDTO>> GetAll();
        Task<ResourceDTO> GetById(string id);
        Task<List<ResourceDTO>> GetByLesson(string id);
    }
}
