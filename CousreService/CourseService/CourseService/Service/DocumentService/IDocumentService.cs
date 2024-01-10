using CourseService.Data;
using CourseService.Model;
using System.Reflection.Metadata;
using CourseService.Model;

namespace CourseService.Service.DocumentService
{
    public interface IDocumentService
    {
        Task<ManagerRespone> Delete(Documents d);
        Task<Documents> UploadFile(IFormFile file,string path);
        Task<ManagerRespone> Rename(string oldPath,string newPath);
        Task<ManagerRespone> UpdateLink(Documents d,string oldPath, string newPath);
        Task<DocumentDTO> AddDocument(DocumentDTO d);
        Task<DocumentDTO> GetById(string id);
        Task<ManagerRespone> DeleteDocument(string id);
        Task<ManagerRespone> UpdateLinkAsync(string id, string oldPath, string newPath);
    }
}
