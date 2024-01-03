using CourseService.Data;
using CourseService.Model;
using System.Reflection.Metadata;

namespace CourseService.Service.DocumentService
{
    public interface IDocumentService
    {
        Task<Documents> UploadFile(LessonDTO lessonModel,string path);

    }
}
