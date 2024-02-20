using CourseService.Data;
using CourseService.Model;
using CourseService.Service.DocumentService;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CourseService.Service.ResourceService
{
    public class ResourceService : IResourceService
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
                    if (user.Id != classes.Teacher)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You not have permission to add resource to this class",
                            IsSuccess = false,
                        };
                    }
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

        public async Task<ResourceDTO> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var resources = await _context.Resources.FirstOrDefaultAsync(r => r.Id == id);
                    if (resources == null)
                    {
                        return null;
                    }

                    var lesson = await _context.Lessons.FirstOrDefaultAsync(cl => cl.Id == resources.LessonId);
                    if (lesson == null)
                    {
                        return null;
                    }
                    var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == lesson.TopicId);
                    if (topic == null)
                    {
                        return null;
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(cl => cl.Id == topic.ClassId);
                    if (classes == null)
                    {
                        return null;
                    }
                    var typ = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Id == resources.TypeId);
                    if (typ == null)
                    {
                        return null;
                    }
                    if (classes.Teacher == user.Id)
                    {
                        ResourceDTO res = new ResourceDTO
                        {
                            Id = resources.Id,
                            Lesson = lesson.Title,
                            Type = typ.Name,
                            Name = resources.Name,
                        };
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;

        }

        public async Task<List<ResourceDetails>> GetByLesson(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {

                    var lessons = await _context.Lessons.FirstOrDefaultAsync(ls => ls.Id == id);
                    if (lessons == null)
                    {
                        return null;
                    }
                    var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == lessons.TopicId);
                    if (topic == null)
                    {
                        return null;
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(cl => cl.Id == topic.ClassId);
                    if (classes == null)
                    {
                        return null;
                    }
                    var res = _context.Resources.Where(r => r.LessonId == lessons.Id).ToList();
                    List<ResourceDetails> rs = new List<ResourceDetails>();

                    foreach (var r in res)
                    {
                        var typ = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Id == r.TypeId);
                        if (typ == null)
                        {
                            return null;
                        }
                        var sta = "Awaiting approval";
                        if (r.Status == true)
                        {
                            sta = "Approved";
                        }
                        var owner = await GetUsersFromUserServiceAsync(r.createBy, accessToken);
                        if (owner == null)
                        {
                            return null;
                        }
                        var updateBy = await GetUsersFromUserServiceAsync(r.updateBy, accessToken);
                        if (classes.Teacher == user.Id)
                        {
                            ResourceDetails resource = new ResourceDetails
                            {
                                Id = r.Id,
                                Lesson = lessons.Title,
                                Type = typ.Name,
                                Name = r.Name,
                                Status = sta,
                                CreateAt = r.CreatedAt.ToString(),
                                CreateBy = owner.UserName,
                                UpdateAt = r.UpdatedAt.ToString(),
                                UpdateBy = updateBy.UserName,
                            };
                            rs.Add(resource);
                        }
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
                    var res = await _context.Resources.FirstOrDefaultAsync(r => r.Id == resourceDTO.Id);
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

                    var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == lesson.TopicId);
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == topic.ClassId);
                    if (user.Id != classes.Teacher)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You not have permission to add resource to this class",
                            IsSuccess = false,
                        };
                    }
                    var d = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == res.DocumentId);
                    var link = "";
                    var documentId = "";
                    if (d != null)
                    {
                        link = d.link;
                        documentId = d.DocumentId;
                    }
                    string path = $"Upload/{classes.Name}/{topic.Name}/Resources";
                    var document = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == res.DocumentId);
                    if (resourceDTO.FileContent != null && d != null)
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
                        documentId = document.DocumentId;

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
                    if (resourceDTO.FileContent != null && d != null)
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
                    var res = await _context.Resources.FirstOrDefaultAsync(rs => rs.Id == id);
                    if (res == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist resource with id={id}",
                            IsSuccess = false
                        };
                    }
                    var lessons = await _context.Lessons.FirstOrDefaultAsync(ls => ls.Id == res.LessonId);
                    if (lessons == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist lesson with id={res.LessonId}",
                            IsSuccess = false
                        };
                    }
                    var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == lessons.TopicId);
                    if (topic == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist topic with id={lessons.TopicId}",
                            IsSuccess = false
                        };
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(cl => cl.Id == topic.ClassId);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist classes with id={topic.ClassId}",
                            IsSuccess = false
                        };
                    }
                    if (classes.Teacher != user.Id)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You don't have permission to delete this resource",
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
                    var resources = _context.Resources.Where(r => r.IsActive == true).ToList();
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
                        var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == lesson.TopicId);
                        if (topic == null)
                        {
                            return null;
                        }
                        var classes = await _context.Classes.FirstOrDefaultAsync(cl => cl.Id == topic.ClassId);
                        if (classes == null)
                        {
                            return null;
                        }
                        var typ = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Id == re.TypeId);
                        if (typ == null)
                        {
                            return null;
                        }
                        if (classes.Teacher == user.Id)
                        {
                            ResourceDTO res = new ResourceDTO
                            {
                                Id = re.Id,
                                Lesson = lesson.Title,
                                Type = typ.Name,
                                Name = re.Name,
                            };
                            r.Add(res);
                        }
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

        public async Task<(Stream, string)> DownloadResource(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var file = await _context.Resources.FirstOrDefaultAsync(x => x.Id == id);
                    if (file == null)
                    {
                        return (null, $"Don't exist resource with id={id}");
                    }
                    var document = await _context.Documents.FirstOrDefaultAsync(x => x.DocumentId == file.DocumentId);
                    var fileName = document.FileName;
                    if (string.IsNullOrEmpty(fileName))
                    {
                        return (null, $"File Name is Empty");
                    }

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), document.link);

                    if (!File.Exists(filePath))
                    {
                        return (null, $"File not found: {fileName}");
                    }
                    return (System.IO.File.OpenRead(filePath), document.FileName);


                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return (null, $"Error '{ex.Message}' when download.");
                }
            }
            return (null, $"Unthorize");
        }

        public async Task<ManagerRespone> ApproveResource(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var res = await _context.Resources.FirstOrDefaultAsync(r => r.Id == id);
                    if (res == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist resource with id={id}",
                            IsSuccess = false
                        };
                    }
                    res.Status = true;
                    res.UpdatedAt = DateTime.Now;
                    res.updateBy = user.Id;
                    _context.Resources.Update(res);
                    int number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when approve resource",
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
                        Message = $"Error when approve resource {ex.StackTrace}",
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
        public async Task<UserDTO> GetUsersFromUserServiceAsync(string id, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"https://localhost:44357/User/UserById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content to your User class
                var jsonContent = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<UserDTO>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return user;
            }
            return null;
        }
    }
}
