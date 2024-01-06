using CourseService.Data;
using CourseService.Model;
using CourseService.Service.DocumentService;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using CourseService.Model;

namespace CourseService.Service.ClassesService
{
    public class ClassesService : IClassService
    {
        private CourseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly IDocumentService _documenService;

        public ClassesService(CourseContext context, IDocumentService documentService, IHttpContextAccessor httpContext, HttpClient httpClient = null)
        {
            _context = context;
            _httpContextAccessor = httpContext;
            _httpClient = httpClient;
            _documenService = documentService;
        }

        public async Task<ManagerRespone> AddClasses(ClassDTO classes)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var teacher = await GetUsersFromUserServiceAsync(classes.Teacher, accessToken);
                    if (teacher == null || teacher.RoleName != "Teacher")
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not exist Teacher with id={classes.Teacher}",
                            IsSuccess = false
                        };
                    }

                    var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == classes.Course);
                    if (course == null || course.IsActive == false)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist course with id= {classes.Course}.",
                            IsSuccess = false,
                        };
                    }
                    Classes cl = new Classes
                    {
                        Name = classes.Name,
                        Teacher = classes.Teacher,
                        CourseId = classes.Course,
                        Description = classes.Description,

                    };
                    await _context.Classes.AddAsync(cl);
                    int numberOfChanges = await _context.SaveChangesAsync();
                    if (numberOfChanges > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Successfully saved {numberOfChanges} changes to the database.",
                            IsSuccess = true,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Error when create new class.",
                        IsSuccess = false,
                    };

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return new ManagerRespone
                    {
                        Message = $"Error: {ex.Message}",
                        IsSuccess = false,
                    };
                }
            }
            return new ManagerRespone
            {
                Message = $"Unthorize",
                IsSuccess = false,
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

        public async Task<List<ClassDTO>> GetAll()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var classes = _context.Classes.ToList();
                    List<ClassDTO> result = new List<ClassDTO>();

                    foreach (var clazz in classes)
                    {
                        var courseName = _context.Courses.FirstOrDefault(c => c.Id == clazz.CourseId);
                        if (courseName == null)
                        {
                            return null;
                        }
                        var TeacherName = await GetUsersFromUserServiceAsync(clazz.Teacher, accessToken);
                        if (TeacherName == null)
                        {
                            return null;
                        }
                        ClassDTO classDTO = new ClassDTO
                        {
                            Id = clazz.Id,
                            Course = courseName.Name,
                            Teacher = TeacherName.Email,
                            Name = clazz.Name,
                            Description = clazz.Description,
                        };
                        result.Add(classDTO);
                    }
                    return result;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<ClassDTO> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == id);
                    if (classes == null)
                    {
                        return null;
                    }

                    var courses = _context.Courses.FirstOrDefault(c => c.Id == classes.CourseId);
                    if (courses == null)
                    {
                        return null;
                    }
                    var teacher = await GetUsersFromUserServiceAsync(classes.Teacher, accessToken);
                    if (teacher == null)
                    {
                        return null;
                    }
                    ClassDTO classDTO = new ClassDTO
                    {
                        Name = classes.Name,
                        Course = courses.Name,
                        Teacher = teacher.Email,
                        Description = classes.Description,
                        Id = classes.Id,
                    };
                    return classDTO;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<ManagerRespone> EditClass(string id, ClassDTO classDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var cl = await _context.Classes.FirstOrDefaultAsync(x => x.Id == id);
                    if (cl == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist class with id = {id}",
                            IsSuccess = false
                        };
                    }
                    var teacher = await GetUsersFromUserServiceAsync(classDTO.Teacher, accessToken);
                    if (teacher == null || teacher.RoleName != "Teacher")
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not exist Teacher with id={classDTO.Teacher}",
                            IsSuccess = false
                        };
                    }

                    var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == classDTO.Course);
                    if (course == null || course.IsActive==false)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist course with id= {classDTO.Course}.",
                            IsSuccess = false,
                        };
                    }
                    string oldPath = $"Upload/{cl.Name}";
                    string newPath =$"Upload/{classDTO.Name}";
                    if (!oldPath.Equals(newPath))
                    {
                        var rename= await _documenService.Rename(oldPath, newPath);
                        if (rename.IsSuccess)
                        {
                            var documents = _context.Documents.Where(d => d.link.Contains(oldPath)).ToList();
                            foreach(var d in documents){
                               var up= await _documenService.UpdateLink(d,oldPath,newPath);
                                if (!up.IsSuccess)
                                {
                                    return up;
                                }
                            }
                        }
                    }
                    cl.Name = classDTO.Name;
                    cl.Teacher = classDTO.Teacher;
                    cl.CourseId = classDTO.Course;
                    cl.Description = classDTO.Description;
                    cl.UpdatedAt = DateTime.Now;
                    _context.Classes.Update(cl);
                    int numberOfChanges = await _context.SaveChangesAsync();
                    if (numberOfChanges > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Successfully update {numberOfChanges} changes to the database.",
                            IsSuccess = true,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Error when update class.",
                        IsSuccess = false,
                    };
                }
                catch (Exception ex)
                {
                    return new ManagerRespone
                    {
                        Message = $"Error: {ex.Message}",
                        IsSuccess = false,
                    };
                }
            }
            return new ManagerRespone
            {
                Message = $"Unthorize",
                IsSuccess = false,
            };
        }

        public async Task<ManagerRespone> DeleteClass(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var cl = await _context.Classes.FirstOrDefaultAsync(x => x.Id == id);
                    if (cl == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist class with id = {id}",
                            IsSuccess = false
                        };
                    }
                    cl.IsActive = false;
                    cl.UpdatedAt = DateTime.Now;
                    _context.Classes.Update(cl);
                    int numberOfChanges = await _context.SaveChangesAsync();
                    if (numberOfChanges > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Successfully delete class.",
                            IsSuccess = true,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Error when delete class.",
                        IsSuccess = false,
                    };
                }
                catch (Exception ex)
                {
                    return new ManagerRespone
                    {
                        Message = $"Error: {ex.Message}",
                        IsSuccess = false,
                    };
                }
            }
            return new ManagerRespone
            {
                Message = $"Unthorize",
                IsSuccess = false,
            };
        }

        public async Task<List<ClassDTO>> GetActiveClasses()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var classes = _context.Classes.Where(x => x.IsActive == true).ToList();
                    List<ClassDTO> result = new List<ClassDTO>();
                    foreach (var clazz in classes)
                    {
                        var courseName = _context.Courses.FirstOrDefault(c => c.Id == clazz.CourseId);
                        if (courseName == null)
                        {
                            return null;
                        }
                        var TeacherName = await GetUsersFromUserServiceAsync(clazz.Teacher, accessToken);
                        if (TeacherName == null)
                        {
                            return null;
                        }
                        ClassDTO classDTO = new ClassDTO
                        {
                            Id = clazz.Id,
                            Course = courseName.Name,
                            Teacher = TeacherName.Email,
                            Name = clazz.Name,
                            Description = clazz.Description,
                        };
                        result.Add(classDTO);
                    }
                    return result;

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
