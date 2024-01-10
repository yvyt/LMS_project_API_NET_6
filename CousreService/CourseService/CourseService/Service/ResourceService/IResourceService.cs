using CourseService.Model;
using CourseService.Model;

namespace CourseService.Service.ResourceService
{
    public interface IResourceService
    {
        Task<ManagerRespone> AddResouce(ResourceDTO resouceDTO);
        Task<ManagerRespone> DeleteResource(string id);
        Task<(Stream,string)> DownloadResource(string id);
        Task<ManagerRespone> EditResource(ResourceDTO resourceDTO);
        Task<List<ResourceDTO>> GetActive();
        Task<List<ResourceDTO>> GetAll();
        Task<ResourceDTO> GetById(string id);
        Task<List<ResourceDTO>> GetByLesson(string id);
    }
}
