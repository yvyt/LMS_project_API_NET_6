using CourseService.Data;
using CourseService.Model;
using System.Reflection.Metadata;
using UserService.Model;

namespace CourseService.Service.DocumentService
{
    public interface IDocumentService
    {
        Task<ManagerRespone> Delete(Documents d);
        Task<Documents> UploadFile(LessonDTO lessonModel,string path);

    }
}
