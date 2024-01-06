using CourseService.Data;
using CourseService.Model;
using CourseService.Service.DocumentService;
using Microsoft.EntityFrameworkCore;
using System.Resources;
using UserService.Model;

namespace CourseService.Service.ResourceService
{
    public class ResourceService:IResourceService
    {
        private CourseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly IDocumentService _documenService;
        public ResourceService(IDocumentService documentService, CourseContext context, IHttpContextAccessor httpContext, HttpClient httpClient = null)
        {
            _context = context;
            _httpContextAccessor = httpContext;
            _httpClient = httpClient;
            _documenService = documentService;
        }

        public async Task<ManagerRespone> AddResouce(ResourceDTO resouceDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var lesson = await _context.Lessons.FirstOrDefaultAsync(t => t.Id == resouceDTO.Lesson);
                    if (lesson == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't have lesson with id={resouceDTO.Lesson}",
                            IsSuccess = false
                        };
                    }
                    var type = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Name == resouceDTO.Type);
                    if (type == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Only allow update file with type is Documents or Slides",
                            IsSuccess = false
                        };
                    }
                    var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == lesson.TopicId);
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == topic.ClassId);
                    string path = $"Upload/{classes.Name}/{topic.Name}/Resources";
                    if (resouceDTO.FileContent == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"File Content is require.",
                            IsSuccess = false
                        };
                    }
                    var document = await _documenService.UploadFile(resouceDTO.FileContent, path);
                    if (document == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when upload new file",
                            IsSuccess = false,
                        };
                    }
                    var re = new Resources
                    {
                        Name = resouceDTO.Name != null ? resouceDTO.Name : document.FileName,
                        DocumentId = document.DocumentId,
                        LessonId = lesson.Id,
                        TypeId = type.Id,
                        createBy = user.Id,
                        updateBy = user.Id,
                    };
                    await _context.Resources.AddAsync(re);
                    int number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when create new resource",
                            IsSuccess = false,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Successfully saved {number} changes to the database.",
                        IsSuccess = true,
                    };
                }
                catch (Exception ex)
                {
                    return new ManagerRespone
                    {
                        Message = $"Error when create new resource {ex.StackTrace}",
                        IsSuccess = false
                    };
                }
            }
            return new ManagerRespone
            {
                Message = $"Unauthorize",
                IsSuccess = false
            };
        }

        public async Task<List<ResourceDTO>> GetAll()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var resources = _context.Resources.ToList();
                    if (resources == null)
                    {
                        return null;
                    }
                    List<ResourceDTO> r = new List<ResourceDTO>();
                    foreach (var re in resources)
                    {
                        var lesson = await _context.Lessons.FirstOrDefaultAsync(cl => cl.Id == re.LessonId);
                        if (lesson == null)
                        {
                            return null;
                        }
                        var typ = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Id == re.TypeId);
                        if (typ == null)
                        {
                            return null;
                        }
                        ResourceDTO res = new ResourceDTO
                        {
                            Id=re.Id,
                            Lesson=lesson.Title,
                            Type=typ.Name,
                            Name=re.Name,
                        };
                        r.Add(res);
                    }
                    return r;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;

        }

        public async Task<ResourceDTO> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var resources = await _context.Resources.FirstOrDefaultAsync(r=>r.Id==id);
                    if (resources == null)
                    {
                        return null;
                    }
                    
                        var lesson = await _context.Lessons.FirstOrDefaultAsync(cl => cl.Id == resources.LessonId);
                        if (lesson == null)
                        {
                            return null;
                        }
                        var typ = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Id == resources.TypeId);
                        if (typ == null)
                        {
                            return null;
                        }
                        ResourceDTO res = new ResourceDTO
                        {
                            Id = resources.Id,
                            Lesson = lesson.Title,
                            Type = typ.Name,
                            Name = resources.Name,
                        };                    
                    return res;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;

        }

        public async Task<List<ResourceDTO>> GetByLesson(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var lessons= await _context.Lessons.FirstOrDefaultAsync(ls=>ls.Id==id);
                    if(lessons == null)
                    {
                        return null;
                    }
                    var res = _context.Resources.Where(r => r.LessonId == lessons.Id).ToList();
                    List<ResourceDTO> rs = new List<ResourceDTO>();

                    foreach (var r in res){
                        var typ = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Id == r.TypeId);
                        if (typ == null)
                        {
                            return null;
                        }
                        ResourceDTO resource = new ResourceDTO
                        {
                            Id = r.Id,
                            Lesson = lessons.Title,
                            Type = typ.Name,
                            Name = r.Name,
                        };
                        rs.Add(resource);
                    }
                    return rs;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<ManagerRespone> EditResource(ResourceDTO resourceDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var res = await _context.Resources.FirstOrDefaultAsync(r=>r.Id == resourceDTO.Id);
                    if (res == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist resource with id={resourceDTO.Id}",
                            IsSuccess = false
                        };
                    }
                    var lesson = await _context.Lessons.FirstOrDefaultAsync(t => t.Id == resourceDTO.Lesson);
                    if (lesson == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't have lesson with id={resourceDTO.Lesson}",
                            IsSuccess = false
                        };
                    }
                    var type = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Name == resourceDTO.Type);
                    if (type == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Only allow update file with type is document or slide",
                            IsSuccess = false
                        };
                    }
                    var d = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == res.DocumentId);
                    if (d == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't find document with path={d.link}",
                            IsSuccess = false
                        };
                    }
                    var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == lesson.TopicId);
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == topic.ClassId);
                    string path = $"Upload/{classes.Name}/{topic.Name}/Resources";
                    var document = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == res.DocumentId);
                    if(resourceDTO.FileContent!=null)
                    {
                         document = await _documenService.UploadFile(resourceDTO.FileContent, path);
                        if (document == null)
                        {
                            return new ManagerRespone
                            {
                                Message = $"Error when upload new file",
                                IsSuccess = false,
                            };
                        }
                        
                        
                    }

                    res.Name = resourceDTO.Name != null ? resourceDTO.Name : document.FileName;
                    res.DocumentId = document.DocumentId;
                    res.LessonId = lesson.Id;
                    res.TypeId = type.Id;
                    res.updateBy = user.Id;
                    res.UpdatedAt = DateTime.Now;
                    _context.Resources.Update(res);
                    int number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when update lesson",
                            IsSuccess = false,
                        };
                    }
                    if (resourceDTO.FileContent != null)
                    {
                        var dele = await _documenService.Delete(d);
                        if (dele.IsSuccess == false)
                        {
                            return dele;
                        }
                    }
                    return new ManagerRespone
                    {
                        Message = $"Successfully saved {number} changes to the database.",
                        IsSuccess = true,
                    };
                }
                
                catch (Exception ex)
                {
                    return new ManagerRespone
                    {
                        Message = $"Error when update Lesson {ex.StackTrace}",
                        IsSuccess = false
                    };
                }
            }
            return new ManagerRespone
            {
                Message = $"Unauthorize",
                IsSuccess = false
            };
        }

        public async Task<ManagerRespone> DeleteResource(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var res = await _context.Resources.FirstOrDefaultAsync(rs=>rs.Id==id);
                    if (res == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist resource with id={id}",
                            IsSuccess = false
                        };
                    }


                    res.IsActive = false;
                    res.UpdatedAt = DateTime.Now;
                    _context.Resources.Update(res);
                    int number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when delete resource",
                            IsSuccess = false,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Successfully delete resource",
                        IsSuccess = true,
                    };
                }
                catch (Exception ex)
                {
                    return new ManagerRespone
                    {
                        Message = $"Error when delete resource {ex.StackTrace}",
                        IsSuccess = false
                    };
                }
            }
            return new ManagerRespone
            {
                Message = $"Unauthorize",
                IsSuccess = false
            };
        }

        public async Task<List<ResourceDTO>> GetActive()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var resources = _context.Resources.Where(r=>r.IsActive==true).ToList();
                    if (resources == null)
                    {
                        return null;
                    }
                    List<ResourceDTO> r = new List<ResourceDTO>();
                    foreach (var re in resources)
                    {
                        var lesson = await _context.Lessons.FirstOrDefaultAsync(cl => cl.Id == re.LessonId);
                        if (lesson == null)
                        {
                            return null;
                        }
                        var typ = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Id == re.TypeId);
                        if (typ == null)
                        {
                            return null;
                        }
                        ResourceDTO res = new ResourceDTO
                        {
                            Id = re.Id,
                            Lesson = lesson.Title,
                            Type = typ.Name,
                            Name = re.Name,
                        };
                        r.Add(res);
                    }
                    return r;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
