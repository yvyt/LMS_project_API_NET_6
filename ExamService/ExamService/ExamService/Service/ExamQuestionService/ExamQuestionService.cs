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
                    if (user.Id != classes.Teacher)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You don't have permission to create new exam",
                            IsSuccess = false,
                        };
                    }
                    
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
                        if (question.ClassId != examQuestionDTO.Class)
                        {
                            return new ManagerRespone
                            {
                                Message = $"Question don't match with class in exam",
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

        public async Task<ManagerRespone> AddMoreQuestion(QuestionExamAdd examQuestionDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    
                    var exam = await _context.Exams.FirstOrDefaultAsync(x => x.Id == examQuestionDTO.Exam);
                    if (exam == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist exam with id={examQuestionDTO.Exam}",
                            IsSuccess = false,
                        };
                    }
                    if (user.Id != exam.createBy)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You don't have permission to delete this exam",
                            IsSuccess = false,
                        };
                    }
                    var numberOfNewQuestion = examQuestionDTO.Questions.Count;
                    exam.NumberQuestion = exam.NumberQuestion + numberOfNewQuestion;
                    exam.UpdatedAt=DateTime.Now;
                    exam.updateBy = user.Id;
                    _context.Exams.Update(exam);
                    List<ExamQuestion> result = new List<ExamQuestion>();
                    foreach (var q in examQuestionDTO.Questions)
                    {
                        var checkContain = _context.examQuestions.Where(qu => qu.ExamId==examQuestionDTO.Exam && qu.QuestionId.Contains(q) ).ToList();
                        if (checkContain.Count > 0)
                        {
                            return new ManagerRespone
                            {
                                Message = $"Question: {q} has already exist in exam",
                                IsSuccess = false,
                            };
                        }
                    
                        var question = await _context.Question.FirstOrDefaultAsync(qu => qu.Id == q);
                        if (question == null)
                        {
                            return new ManagerRespone
                            {
                                Message = $"Don't exist question with id={q}",
                                IsSuccess = false,
                            };
                        }
                        
                        ExamQuestion newEx = new ExamQuestion
                        {
                            ExamId = exam.Id,
                            QuestionId = question.Id,
                            createBy = user.Id,
                            updateBy = user.Id,
                        };
                        result.Add(newEx);
                    }
                    await _context.examQuestions.AddRangeAsync(result);
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

        public async Task<ManagerRespone> DeleteExamQuestion(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var examQuestion = await _context.examQuestions.FirstOrDefaultAsync(x => x.Id == id);
                    if (examQuestion == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist exam question with id= {id}.",
                            IsSuccess = false,
                        };
                    }
                    examQuestion.updateBy = user.Id;
                    examQuestion.IsActive = false;
                    examQuestion.UpdatedAt = DateTime.Now;                    
                    _context.examQuestions.Update(examQuestion);
                    int number = await _context.SaveChangesAsync();
                    if (number > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Successfully delete exam question",
                            IsSuccess = true,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Error when delete exam question.",
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

        public async Task<ManagerRespone> EditExamQuestion(ExamQuestionUpdateDTO examQuestionDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var examQuestion = await _context.examQuestions.FirstOrDefaultAsync(x => x.Id == examQuestionDTO.Id);
                    if (examQuestion == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist exam question with id= {examQuestionDTO.Id}.",
                            IsSuccess = false,
                        };
                    }
                    var exam = await _context.Exams.FirstOrDefaultAsync(x => x.Id == examQuestion.ExamId);
                    if (exam == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist exam with id= {examQuestion.ExamId}.",
                            IsSuccess = false,
                        };
                    }
                    var classes = await GetClassFromClassServiceAsync(examQuestionDTO.Class, accessToken);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist class with id= {examQuestionDTO.Class}.",
                            IsSuccess = false,
                        };
                    }
                    if(user.Id!= examQuestion.createBy)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You don't have permission to edit this exam question.",
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

                    exam.Name = examQuestionDTO.Name;
                    exam.TypeId = typ.Id;
                    exam.ClassId = classes.Id;
                    exam.UserId = user.Id;
                    exam.NumberQuestion = examQuestionDTO.NumberQuestion;
                    exam.updateBy = user.Id;
                    exam.IsMutipleChoice = examQuestionDTO.IsMutipleChoice;
                    exam.UpdatedAt = DateTime.Now;
                    _context.Exams.Update(exam);
                    var question = await _context.Question.FirstOrDefaultAsync(q => q.Id == examQuestionDTO.Questions);
                    if(question==null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist question with id= {examQuestionDTO.Questions}.",
                            IsSuccess = false,
                        };
                    }
                    examQuestion.QuestionId = question.Id;
                    examQuestion.UpdatedAt = DateTime.Now;
                    examQuestion.updateBy = user.Id;
                    _context.examQuestions.Update(examQuestion);
                    int number = await _context.SaveChangesAsync();
                    if (number > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Successfully update {number} changes to the database.",
                            IsSuccess = true,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Error when update exam question.",
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

        public async Task<List<ExamQuestionUpdateDTO>> GetActive()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    List<ExamQuestionUpdateDTO> result =new  List<ExamQuestionUpdateDTO>();
                    var examQuestions = _context.examQuestions.Where(x=>x.IsActive==true).ToList();
                    foreach(var examQuestion in examQuestions)
                    {
                        var exam = await _context.Exams.FirstOrDefaultAsync(x => x.Id == examQuestion.ExamId);
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
                        var question = await _context.Question.FirstOrDefaultAsync(q => q.Id == examQuestion.QuestionId);
                        if (question == null)
                        {
                            return null;
                        }
                        if (classes.Teacher == user.Id)
                        {
                            ExamQuestionUpdateDTO examQuestionDTO = new ExamQuestionUpdateDTO
                            {
                                Id = examQuestion.Id,
                                Name = exam.Name,
                                Class = classes.Name,
                                Type = typ.Name,
                                IsMutipleChoice = exam.IsMutipleChoice,
                                NumberQuestion = exam.NumberQuestion,
                                Questions = question.ContentQuestion
                            };
                            result.Add(examQuestionDTO);
                        }
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
                    if (classes.Teacher == user.Id)
                    {
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
