using CourseService.Data;
using CourseService.Model;
using CourseService.Service.DocumentService;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;
using UserService.Model;

namespace CourseService.Service.TopicService
{
    public class TopicService : ITopicService
    {
        private CourseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly IDocumentService _documenService;

        public TopicService(CourseContext context, IHttpContextAccessor httpContext, IDocumentService documentService,HttpClient httpClient = null)
        {
            _context = context;
            _httpContextAccessor = httpContext;
            _httpClient = httpClient;
            _documenService = documentService;
        }

        public async Task<ManagerRespone> AddTopic(TopicDTO topicDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == topicDTO.Class);
                    if (classes == null || classes.IsActive==false)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exit class with id = {topicDTO.Class}",
                            IsSuccess = false,
                        };

                    }
                    Topic topic = new Topic
                    {
                        Name = topicDTO.Name,
                        ClassId = topicDTO.Class,
                    };
                    await _context.Topics.AddAsync(topic);
                    var number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when create new topic",
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

        public async Task<List<TopicDTO>> GetAll()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var topics = _context.Topics.ToList();
                    List<TopicDTO> result = new List<TopicDTO> ();
                    
                    foreach (var t in topics)
                    {
                        var classes= await _context.Classes.FirstOrDefaultAsync(cl=>cl.Id==t.ClassId);
                        TopicDTO topicDTO = new TopicDTO
                        {
                           Id=t.Id,
                           Name=t.Name,
                           Class=classes.Name
                        };
                        result.Add(topicDTO);
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

        public async Task<TopicDTO> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var topic = await _context.Topics.FirstOrDefaultAsync(c=>c.Id==id);
                    if (topic == null)
                    {
                        return null;
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == topic.ClassId);
                    if (classes == null || classes.IsActive == false)
                    {
                        return null;

                    }
                    TopicDTO topicDTO = new TopicDTO
                    {
                        Id=topic.Id,
                        Name = topic.Name,
                        Class = classes.Name,
                    };
                    return topicDTO;

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return null;
                }
            }
            return null;
        }

        public async Task<List<TopicDTO>> GetByClass(string classId)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == classId);
                    if (classes == null || classes.IsActive == false)
                    {
                        return null;

                    }
                    var topics = _context.Topics.Where(c => c.ClassId == classes.Id).ToList() ;
                    List<TopicDTO> result = new List<TopicDTO>();
                    if (topics == null)
                    {
                        return null;
                    }
                    foreach(var topic in topics)
                    {
                        TopicDTO t = new TopicDTO
                        {
                            Id = topic.Id,
                            Name = topic.Name,
                            Class = classes.Name,
                        };
                        result.Add(t);
                    }
                    return result;

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return null;
                }
            }
            return null;
        }

        public async Task<ManagerRespone> Edit(string id, TopicDTO topicDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {

                    Topic topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);
                    if (topic == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exit topic with id = {id}",
                            IsSuccess = false,
                        };
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == topicDTO.Class);
                    if (classes == null || classes.IsActive == false)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exit class with id = {topicDTO.Class}",
                            IsSuccess = false,
                        };

                    }
                    string oldPath = $"Upload/{classes.Name}/{topic.Name}";
                    string newPath = $"Upload/{classes.Name}/{topicDTO.Name}";
                    if (!oldPath.Equals(newPath))
                    {
                        var rename = await _documenService.Rename(oldPath, newPath);
                        if (rename.IsSuccess)
                        {
                            var documents = _context.Documents.Where(d => d.link.Contains(oldPath)).ToList();
                            foreach (var d in documents)
                            {
                                var up = await _documenService.UpdateLink(d, oldPath, newPath);
                                if (!up.IsSuccess)
                                {
                                    return up;
                                }
                            }
                        }
                        topic.Name = topicDTO.Name;
                        topic.ClassId = topicDTO.Class;
                        topic.UpdatedAt = DateTime.Now;
                        _context.Topics.Update(topic);
                        var number = await _context.SaveChangesAsync();
                        if (number == 0)
                        {
                            return new ManagerRespone
                            {
                                Message = $"Error when update topic",
                                IsSuccess = false,
                            };
                        }
                        return new ManagerRespone
                        {
                            Message = $"Successfully update {number} changes to the database.",
                            IsSuccess = true,
                        };
                    }
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

        public async Task<ManagerRespone> DeleteTopic(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {

                    Topic topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == id);
                    if (topic == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exit topic with id = {id}",
                            IsSuccess = false,
                        };
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(c => c.Id == topic.ClassId);
                    if (classes == null || classes.IsActive == false)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exit class with id = {topic.ClassId}",
                            IsSuccess = false,
                        };

                    }
                    topic.IsActive = false;
                    topic.UpdatedAt = DateTime.Now;
                    _context.Topics.Update(topic);
                    var number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when delete topic",
                            IsSuccess = false,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Successfully detele topic",
                        IsSuccess = true,
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

        public async Task<List<TopicDTO>> GetActiveTopic()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var topics = _context.Topics.Where(c=>c.IsActive==true).ToList();
                    List<TopicDTO> result = new List<TopicDTO>();

                    foreach (var t in topics)
                    {
                        var classes = await _context.Classes.FirstOrDefaultAsync(cl => cl.Id == t.ClassId);
                        TopicDTO topicDTO = new TopicDTO
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Class = classes.Name
                        };
                        result.Add(topicDTO);
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
