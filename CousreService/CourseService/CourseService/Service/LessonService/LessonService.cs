using CourseService.Data;
using CourseService.Model;
using CourseService.Service.DocumentService;
using Microsoft.EntityFrameworkCore;
using CourseService.Model;

namespace CourseService.Service.LessonService
{
    public class LessonService : ILessonService
    {
        private CourseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly IDocumentService _documenService;

        public LessonService(IDocumentService documentService, CourseContext context, IHttpContextAccessor httpContext, HttpClient httpClient = null)
        {
            _context = context;
            _httpContextAccessor = httpContext;
            _httpClient = httpClient;
            _documenService = documentService;
        }

        public async Task<ManagerRespone> AddLesson(LessonDTO lessonDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == lessonDTO.Topic);
                    if (topic == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't have topic with id={lessonDTO.Topic}",
                            IsSuccess = false
                        };
                    }
                    var type = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Name == lessonDTO.Type);
                    if (type == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Only allow update file with type is document or slide",
                            IsSuccess = false
                        };
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == topic.ClassId);
                    if (user.Id != classes.Teacher)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You not have permission to add lesson to this class",
                            IsSuccess = false,
                        };
                    }
                    string path = $"Upload/{classes.Name}/{topic.Name}/Lesson";
                    if (lessonDTO.FileContent == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You need to upload file",
                            IsSuccess = false,
                        };
                    }
                    var document = await _documenService.UploadFile(lessonDTO.FileContent, path);
                    if (document == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when upload new file",
                            IsSuccess = false,
                        };
                    }
                    var lesson = new Lesson
                    {
                        Title = lessonDTO.Title,
                        DocumentId = document.DocumentId,
                        TopicId = topic.Id,
                        TypeId = type.Id,
                        createBy = user.Id,
                        updateBy = user.Id,
                    };
                    await _context.Lessons.AddAsync(lesson);
                    int number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when create new lesson",
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
                        Message = $"Error when create new Lesson {ex.StackTrace}",
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

        public async Task<List<LessonDTO>> GetAll()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var lessons = _context.Lessons.ToList();
                    if (lessons == null)
                    {
                        return null;
                    }
                    List<LessonDTO> r = new List<LessonDTO>();
                    foreach (var lesson in lessons)
                    {
                        var topic = await _context.Topics.FirstOrDefaultAsync(cl => cl.Id == lesson.TopicId);
                        if (topic == null)
                        {
                            return null;
                        }
                        var typ = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Id == lesson.TypeId);
                        if (typ == null)
                        {
                            return null;
                        }
                        LessonDTO lessonDTO = new LessonDTO
                        {
                            Id = lesson.Id,
                            Topic = topic.Name,
                            Title = lesson.Title,
                            Type = typ.Name,

                        };
                        r.Add(lessonDTO);
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

        public async Task<List<LessonDTO>> GetByTopic(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var topic = await _context.Topics.FirstOrDefaultAsync(cl => cl.Id == id);
                    if (topic == null)
                    {
                        return null;
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(cl => cl.Id == topic.ClassId);
                    if (classes == null || classes.Teacher!=user.Id)
                    {
                        return null;
                    }
                    var lessons = _context.Lessons.Where(l => l.TopicId == id).ToList();
                    if (lessons == null)
                    {
                        return null;
                    }
                    List<LessonDTO> r = new List<LessonDTO>();
                    foreach (var lesson in lessons)
                    {

                        var typ = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Id == lesson.TypeId);
                        if (typ == null)
                        {
                            return null;
                        }
                        LessonDTO lessonDTO = new LessonDTO
                        {
                            Id = lesson.Id,
                            Topic = topic.Name,
                            Title = lesson.Title,
                            Type = typ.Name,

                        };
                        r.Add(lessonDTO);
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

        public async Task<ManagerRespone> EditLesson(string id, LessonDTO lessonDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var less = await _context.Lessons.FirstOrDefaultAsync(ls => ls.Id == id);
                    if (less == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist lesson with id={id}",
                            IsSuccess = false
                        };
                    }
                    var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == lessonDTO.Topic);
                    if (topic == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't have topic with id={lessonDTO.Topic}",
                            IsSuccess = false
                        };
                    }
                    var type = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Name == lessonDTO.Type);
                    if (type == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Only allow update file with type is document or slide",
                            IsSuccess = false
                        };
                    }
                   

                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == topic.ClassId);
                    if (user.Id != classes.Teacher)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You not have permission to add lesson to this class",
                            IsSuccess = false,
                        };
                    }
                    var d = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == less.DocumentId);
                    var link = "";
                    var documentId = "";
                    if (d != null)
                    {
                        link = d.link;
                        documentId = d.DocumentId;
                    }
                    string path = $"Upload/{classes.Name}/{topic.Name}/Lesson";
                    if (d != null && lessonDTO.FileContent != null)
                    {
                        var document = await _documenService.UploadFile(lessonDTO.FileContent, path);
                        if (document == null)
                        {
                            return new ManagerRespone
                            {
                                Message = $"Failed to upload new lesson",
                                IsSuccess = false,
                            };
                        }
                        documentId = document.DocumentId;
                    }
                    less.Title = lessonDTO.Title;
                    less.DocumentId = documentId;
                    less.TopicId = topic.Id;
                    less.TypeId = type.Id;
                    less.updateBy = user.Id;
                    less.UpdatedAt = DateTime.Now;
                    _context.Lessons.Update(less);
                    int number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when update lesson",
                            IsSuccess = false,
                        };
                    }
                    if (lessonDTO.FileContent != null && d != null)
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



        public async Task<ManagerRespone> DeleteLesson(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var less = await _context.Lessons.FirstOrDefaultAsync(ls => ls.Id == id);
                    if (less == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist lesson with id={id}",
                            IsSuccess = false
                        };
                    }
                    var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == less.TopicId);
                    if (topic == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist topic with id={less.TopicId}",
                            IsSuccess = false
                        };
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(cl => cl.Id == topic.ClassId);
                    if (classes == null || classes.Teacher != user.Id)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist classes with id={topic.ClassId} or you don't have permission to delete this lesson",
                            IsSuccess = false
                        };
                    }
                    less.IsActive = false;
                    less.UpdatedAt = DateTime.Now;
                    _context.Lessons.Update(less);
                    int number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when delete lesson",
                            IsSuccess = false,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Successfully delete lesson",
                        IsSuccess = true,
                    };
                }
                catch (Exception ex)
                {
                    return new ManagerRespone
                    {
                        Message = $"Error when delete Lesson {ex.StackTrace}",
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

        public async Task<List<LessonDTO>> GetActiveLesson()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var lessons = _context.Lessons.Where(l => l.IsActive == true).ToList();
                    if (lessons == null)
                    {
                        return null;
                    }
                    List<LessonDTO> r = new List<LessonDTO>();
                    foreach (var lesson in lessons)
                    {
                        var topic = await _context.Topics.FirstOrDefaultAsync(cl => cl.Id == lesson.TopicId);
                        if (topic == null)
                        {
                            return null;
                        }
                        var classes = await _context.Classes.FirstOrDefaultAsync(cl => cl.Id == topic.ClassId);
                        if (classes == null)
                        {
                            return null;
                        }
                        var typ = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Id == lesson.TypeId);
                        if (typ == null)
                        {
                            return null;
                        }
                        if (user.Id == classes.Teacher)
                        {
                            LessonDTO lessonDTO = new LessonDTO
                            {
                                Id = lesson.Id,
                                Topic = topic.Name,
                                Title = lesson.Title,
                                Type = typ.Name,

                            };
                            r.Add(lessonDTO);
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

        public async Task<LessonDTO> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {

                    var lessons = await _context.Lessons.FirstOrDefaultAsync(l => l.Id == id);
                    if (lessons == null)
                    {
                        return null;
                    }

                    var topic = await _context.Topics.FirstOrDefaultAsync(cl => cl.Id ==lessons.TopicId);
                    if (topic == null)
                    {
                        return null;
                    }
                    var typ = await _context.TypeFiles.FirstOrDefaultAsync(t => t.Id == lessons.TypeId);
                    if (typ == null)
                    {
                        return null;
                    }
                    LessonDTO lessonDTO = new LessonDTO
                    {
                        Id = lessons.Id,
                        Topic = topic.Name,
                        Title = lessons.Title,
                        Type = typ.Name,
                    };
                    return lessonDTO;
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
