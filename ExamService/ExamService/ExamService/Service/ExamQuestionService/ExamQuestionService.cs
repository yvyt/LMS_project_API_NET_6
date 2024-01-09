using ExamService.Data;
using ExamService.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ExamService.Service.ExamQuestionService
{
    public class ExamQuestionService : IExamQuestionService
    {
        private readonly ExamsContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        public ExamQuestionService(ExamsContext context, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<ManagerRespone> AddExamQuestion(ExamQuestionDTO examQuestionDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var classes = await GetClassFromClassServiceAsync(examQuestionDTO.Class, accessToken);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist class with id= {examQuestionDTO.Class}.",
                            IsSuccess = false,
                        };
                    }
                    var typ = await _context.ExamsType.FirstOrDefaultAsync(t => t.Name == examQuestionDTO.Type);
                    if (typ == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Only allow exam with type is 45 mitutes or 15 minutes",
                            IsSuccess = false,
                        };
                    }

                    Exam ex = new Exam
                    {
                        Name = examQuestionDTO.Name,
                        TypeId = typ.Id,
                        ClassId = classes.Id,
                        UserId = user.Id,
                        NumberQuestion = examQuestionDTO.NumberQuestion,
                        Status = false,
                        createBy = user.Id,
                        updateBy = user.Id,
                        IsMutipleChoice = examQuestionDTO.IsMutipleChoice,
                        DateBegin = DateTime.Now,
                    };
                    await _context.Exams.AddAsync(ex);
                    var examId = ex.Id;
                    List<ExamQuestion> exams = new List<ExamQuestion>();
                    foreach (var q in examQuestionDTO.Questions)
                    {
                        var question = await _context.Question.FirstOrDefaultAsync(qu => qu.Id == q);
                        if (question == null)
                        {
                            return new ManagerRespone
                            {
                                Message = $"Don't exist question with id={q}",
                                IsSuccess = false,
                            };
                        }
                        ExamQuestion examQuestion = new ExamQuestion
                        {
                            ExamId = examId,
                            QuestionId = q,
                            createBy = user.Id,
                            updateBy = user.Id,
                        };
                        exams.Add(examQuestion);
                    }
                    await _context.examQuestions.AddRangeAsync(exams);
                    int number = await _context.SaveChangesAsync();
                    if (number > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Successfully saved {number} changes to the database.",
                            IsSuccess = true,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Error when create new exam question.",
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

        public async Task<ExamQuestionDTO> GetByExam(string examId)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var examsQuestion = _context.examQuestions.Where(e => e.ExamId == examId).ToList();
                    if (examsQuestion == null)
                    {
                        return null;
                    }
                    List<String> contentQuestion = new List<string>();
                    var ex = examsQuestion[0];

                    var exam = await _context.Exams.FirstOrDefaultAsync(e => e.Id == ex.ExamId);
                    if (exam == null)
                    {
                        return null;
                    }
                    var classes = await GetClassFromClassServiceAsync(exam.ClassId, accessToken);
                    if (classes == null)
                    {
                        return null;
                    }
                    var typ = await _context.ExamsType.FirstOrDefaultAsync(t => t.Id == exam.TypeId);
                    if (typ == null)
                    {
                        return null;
                    }
                    foreach (var e in examsQuestion)
                    {
                        var question = await _context.Question.FirstOrDefaultAsync(x => x.Id == e.QuestionId);
                        if (question == null)
                        {
                            return null;
                        }
                        contentQuestion.Add(question.ContentQuestion);

                    }
                    ExamQuestionDTO dto = new ExamQuestionDTO
                    {

                        Name = exam.Name,
                        Type = typ.Name,
                        Class = classes.Name,
                        IsMutipleChoice = exam.IsMutipleChoice,
                        NumberQuestion = exam.NumberQuestion,
                        Questions = contentQuestion
                    };
                    return dto;
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return null;
                }
            }
            return null;
        }

        public async Task<ExamQuestionDTO> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var examsQuestion = await _context.examQuestions.FirstOrDefaultAsync(e => e.Id == id);
                    if (examsQuestion == null)
                    {
                        return null;
                    }
                    var exam = await _context.Exams.FirstOrDefaultAsync(e => e.Id == examsQuestion.ExamId);
                    if (exam == null)
                    {
                        return null;
                    }
                    var classes = await GetClassFromClassServiceAsync(exam.ClassId, accessToken);
                    if (classes == null)
                    {
                        return null;
                    }
                    var typ = await _context.ExamsType.FirstOrDefaultAsync(t => t.Id == exam.TypeId);
                    if (typ == null)
                    {
                        return null;
                    }
                    var question = await _context.Question.FirstOrDefaultAsync(x => x.Id == examsQuestion.QuestionId);
                    if (question == null)
                    {
                        return null;
                    }
                    List<String> contentQuestion = new List<string>();
                    contentQuestion.Add(question.ContentQuestion);
                    ExamQuestionDTO dto = new ExamQuestionDTO
                    {
                        Id = examsQuestion.Id,
                        Name = exam.Name,
                        Type = typ.Name,
                        Class = classes.Name,
                        IsMutipleChoice = exam.IsMutipleChoice,
                        NumberQuestion = exam.NumberQuestion,
                        Questions = contentQuestion
                    };
                    return dto;

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return null;
                }
            }
            return null;
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


    }
}
