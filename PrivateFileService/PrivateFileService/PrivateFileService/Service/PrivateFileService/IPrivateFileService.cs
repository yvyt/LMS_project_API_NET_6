using PrivateFileService.Model;

namespace PrivateFileService.Service.PrivateFileService
{
    public interface IPrivateFileService
    {
        Task<ManagerRespone> AddPrivateFile(PrivateFileUploadDTO privateFile);
        Task<List<PrivateFileDTO>> GetAll();
        Task<PrivateFileDTO> GetById(string id);
        Task<ManagerRespone> RenameFile(string id, string newName);
    }
}
