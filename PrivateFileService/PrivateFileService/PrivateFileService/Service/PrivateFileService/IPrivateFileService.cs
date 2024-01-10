using PrivateFileService.Model;

namespace PrivateFileService.Service.PrivateFileService
{
    public interface IPrivateFileService
    {
        Task<ManagerRespone> AddPrivateFile(PrivateFileUploadDTO privateFile);
        Task<ManagerRespone> DeletePF(string id);
        Task<(Stream, string)> DownloadPF(string id);
        Task<List<PrivateFileDTO>> GetActive();
        Task<List<PrivateFileDTO>> GetAll();
        Task<PrivateFileDTO> GetById(string id);
        Task<ManagerRespone> RenameFile(string id, string newName);
    }
}
