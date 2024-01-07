using ExamService.Data;
using ExamService.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ExamService.Service.QuestionService
{
    public class QuestionService : IQuestionService
    {
        private ExamsContext _context { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        public QuestionService(ExamsContext context, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<ManagerRespone> AddQuestion(QuestionDTO questionDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {

                    var classes = await GetClassFromClassServiceAsync(questionDTO.Class, accessToken);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist class with id= {questionDTO.Class}.",
                            IsSuccess = false,
                        };
                    }

                    Question question = new Question
                    {
                        ClassId = questionDTO.Class,
                        Level = questionDTO.Level,
                        ContentQuestion = questionDTO.ContentQuestion,
                        IsMultipleChoice = questionDTO.IsMultipleChoice,
                        IsMultipleAnswer = questionDTO.IsMultipleAnswer,
                        createBy = user.Id,
                        updateBy = user.Id,
                    };
                    await _context.Question.AddAsync(question);
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
                        Message = $"Error when create new question.",
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
        public async Task<ClassesDTO> GetClassFromClassServiceAsync(string id, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"https://localhost:44367/Classes/GetClassById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content to your User class
                var jsonContent = await response.Content.ReadAsStringAsync();
                var classes = System.Text.Json.JsonSerializer.Deserialize<ClassesDTO>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return classes;
            }
            return null;
        }

        public async Task<List<QuestionDTO>> GetAll()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var questions = _context.Question.ToList();
                    List<QuestionDTO> result = new List<QuestionDTO>();
                    foreach (var q in questions)
                    {
                        var classes = await GetClassFromClassServiceAsync(q.ClassId, accessToken);
                        if (classes == null)
                        {
                            return null;
                        }
                        QuestionDTO question = new QuestionDTO
                        {
                            Id = q.Id,
                            Level = q.Level,
                            Class = classes.Name,
                            IsMultipleAnswer = q.IsMultipleAnswer,
                            IsMultipleChoice = q.IsMultipleChoice,
                            ContentQuestion = q.ContentQuestion,

                        };
                        result.Add(question);
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

        public async Task<QuestionDTO> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var q = await _context.Question.FirstOrDefaultAsync(x => x.Id == id);
                    if (q == null)
                    {
                        return null;
                    }
                    var classes = await GetClassFromClassServiceAsync(q.ClassId, accessToken);
                    if (classes == null)
                    {
                        return null;
                    }
                    QuestionDTO question = new QuestionDTO
                    {
                        Id = q.Id,
                        Level = q.Level,
                        Class = classes.Name,
                        IsMultipleAnswer = q.IsMultipleAnswer,
                        IsMultipleChoice = q.IsMultipleChoice,
                        ContentQuestion = q.ContentQuestion,

                    };
                    return question;

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return null;
                }
            }
            return null;
        }

        public async Task<ManagerRespone> EditQuestion(QuestionDTO questionDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var q= await _context.Question.FirstOrDefaultAsync(x=>x.Id==questionDTO.Id);
                    if (q == null)
                    {
                        return null;
                    }
                    var classes = await GetClassFromClassServiceAsync(q.ClassId, accessToken);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist class with id= {questionDTO.Class}.",
                            IsSuccess = false,
                        };
                    }
                    q.ContentQuestion = questionDTO.ContentQuestion;
                    q.ClassId = questionDTO.Class;
                    q.IsMultipleAnswer= questionDTO.IsMultipleAnswer;
                    q.IsMultipleChoice= questionDTO.IsMultipleChoice;
                    q.Level = questionDTO.Level;
                    q.UpdateAt = DateTime.Now;
                    q.updateBy=user.Id;
                    _context.Question.Update(q);
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
                        Message = $"Error when update question.",
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

        public async Task<ManagerRespone> DeleteQuestion(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var q = await _context.Question.FirstOrDefaultAsync(x => x.Id == id);
                    if (q == null)
                    {
                        return null;
                    }
                    var classes = await GetClassFromClassServiceAsync(q.ClassId, accessToken);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist class with id= {q.ClassId}.",
                            IsSuccess = false,
                        };
                    }
                    q.IsActive = false;
                    q.UpdateAt = DateTime.Now;
                    q.updateBy = user.Id;
                    _context.Question.Update(q);
                    int numberOfChanges = await _context.SaveChangesAsync();
                    if (numberOfChanges > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Successfully delete question",
                            IsSuccess = true,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Error when delete question.",
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

        public async Task<List<QuestionDTO>> GetActive()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var questions = _context.Question.Where(x=>x.IsActive==true).ToList();
                    List<QuestionDTO> result = new List<QuestionDTO>();
                    foreach (var q in questions)
                    {
                        var classes = await GetClassFromClassServiceAsync(q.ClassId, accessToken);
                        if (classes == null)
                        {
                            return null;
                        }
                        QuestionDTO question = new QuestionDTO
                        {
                            Id = q.Id,
                            Level = q.Level,
                            Class = classes.Name,
                            IsMultipleAnswer = q.IsMultipleAnswer,
                            IsMultipleChoice = q.IsMultipleChoice,
                            ContentQuestion = q.ContentQuestion,

                        };
                        result.Add(question);
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
    }
}
