using CourseService.Data;
using CourseService.Model;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection.Metadata;

namespace CourseService.Service.DocumentService
{
    public class DocumentServices : IDocumentService
    {
        private CourseContext _context;
        public DocumentServices(CourseContext context)
        {
            _context = context;
        }

        public async Task<DocumentDTO> AddDocument(DocumentDTO d)
        {
            var document = new Documents
            {
                DocumentId = Guid.NewGuid().ToString(),
                FileName = d.FileName,
                ContentType = Path.GetExtension(d.FileName),
                link=d.link
            };

            await _context.Documents.AddAsync(document);
            int number = await _context.SaveChangesAsync();
            if (number == 0)
            {
                return null;
            }

            return new DocumentDTO
            {
                DocumentId=document.DocumentId,
                FileName = document.FileName,
                ContentType = Path.GetExtension(document.FileName),
                link=document.link
            };
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
                        Message = "Delete Success",
                        IsSuccess = true,
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

        public async Task<ManagerRespone> DeleteDocument(string id)
        {
            var doc = await _context.Documents.FirstOrDefaultAsync(d=>d.DocumentId == id);
            if (doc == null)
            {
                return new ManagerRespone
                {
                    Message = $"Don't exist document with id={id}",
                    IsSuccess = false,
                };
            }
            _context.Documents.Remove(doc);
            await _context.SaveChangesAsync();
            return new ManagerRespone
            {
                Message = "Delete Success",
                IsSuccess = true,
            };
        }
        public async Task<DocumentDTO> GetById(string id)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(x=>x.DocumentId == id);
            if (document == null)
            {
                return null;
            }
            return new DocumentDTO
            {
                DocumentId = document.DocumentId,
                FileName = document.FileName,
                ContentType = Path.GetExtension(document.FileName),
                link = document.link
            };
        }
        public async Task<ManagerRespone> Rename(string oldPath, string newPath)
        {
            try
            {
                Directory.Move(oldPath, newPath);

                return new ManagerRespone
                {
                    Message = "Reaname success",
                    IsSuccess = true,
                };

            }
            catch (Exception ex)
            {
                return new ManagerRespone
                {
                    Message = $"Error while renaming folder: {ex.Message}",
                    IsSuccess = false,
                };
                
            }
        }

        public async Task<ManagerRespone> UpdateLink(Documents d, string oldPath,string newPath)
        {
            try
            {
                var path = d.link.Replace(oldPath, newPath);
                d.link = path;
                _context.Documents.Update(d);
                int number = await _context.SaveChangesAsync();
                if (number == 0)
                {
                    return new ManagerRespone
                    {
                        Message = "Error when update link to database",
                        IsSuccess = false
                    };

                }
                return new ManagerRespone
                {
                    Message = "Update link to database success",
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ManagerRespone
                {
                    Message = $"Error while renaming folder: {ex.Message}",
                    IsSuccess = false,
                };

            }

        }

        public async Task<ManagerRespone> UpdateLinkAsync(string id, string oldPath, string newPath)
        {
            try
            {
                string newName = Path.GetFileName(newPath);
                var d = await _context.Documents.FirstOrDefaultAsync(x => x.DocumentId == id);
                if(d == null)
                {
                     return new ManagerRespone
                    {
                        Message = $"Don't exist document with id={id}",
                        IsSuccess = false
                    };
                }
                var path = d.link.Replace(oldPath, newPath);
                d.link = path;
                d.FileName = newName;
                _context.Documents.Update(d);
                int number = await _context.SaveChangesAsync();
                if (number == 0)
                {
                    return new ManagerRespone
                    {
                        Message = "Error when update link to database",
                        IsSuccess = false
                    };

                }
                return new ManagerRespone
                {
                    Message = "Update link to database success",
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new ManagerRespone
                {
                    Message = $"Error while renaming folder: {ex.Message}",
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
