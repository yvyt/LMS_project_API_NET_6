using CourseService.Data;
using CourseService.Model;
using UserService.Model;

namespace CourseService.Service.DocumentService
{
    public class DocumentServices : IDocumentService
    {
        private CourseContext _context;
        public DocumentServices(CourseContext context)
        {
            _context = context;
        }

        public async Task<ManagerRespone> Delete(Documents d)
        {

            try
            {
                if (File.Exists(d.link))
                {
                    File.Delete(d.link);
                    _context.Documents.Remove(d);
                    await _context.SaveChangesAsync();
                    return new ManagerRespone
                    {
                        Message="Delete Success",
                        IsSuccess=true,
                    };
                }
                else
                {
                    return new ManagerRespone
                    {
                        Message = "Don't exist file path",
                        IsSuccess = false,
                    };
                }
            }
            catch (Exception ex)
            {
                return new ManagerRespone
                {
                    Message = $"Error {ex.StackTrace}",
                    IsSuccess = false,
                };
            }

        }

        public async Task<Documents> UploadFile(IFormFile file, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            

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
