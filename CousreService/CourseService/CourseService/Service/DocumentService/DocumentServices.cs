using CourseService.Data;
using CourseService.Model;
using Microsoft.Win32.SafeHandles;
using System.Reflection.Metadata;

namespace CourseService.Service.DocumentService
{
    public class DocumentServices : IDocumentService
    {
        private CourseContext _context;
        public DocumentServices(CourseContext context) {
            _context = context;
        }

        public async Task<Documents> UploadFile(LessonDTO lessonModel,string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var file = lessonModel.FileContent;

            var document = new Documents
            {
                DocumentId = Guid.NewGuid().ToString(),
                FileName = file.FileName,
                ContentType = Path.GetExtension(file.FileName)
            };

            var uniqueFileName = document.FileName;
            var filePath = Path.Combine(path, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                document.link = filePath;
            }
            await _context.Documents.AddAsync(document);
            int number = await _context.SaveChangesAsync();
            if (number == 0)
            {
                return null;
            }
           
            return document;
        }
      
    }
}
