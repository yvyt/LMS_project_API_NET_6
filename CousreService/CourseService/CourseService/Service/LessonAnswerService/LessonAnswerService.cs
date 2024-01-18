using CourseService.Data;
using CourseService.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CourseService.Service.LessonAnswerService
{
    public class LessonAnswerService:ILessonAnswerService
    {
        private CourseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public LessonAnswerService(CourseContext context, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<ManagerRespone> AddLessonAnswer(LessonAnswerDTO answerDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var question = await _context.LessonQuestions.FirstOrDefaultAsync(x => x.Id == answerDTO.LessonQuestionId && x.isActive==true);
                    if (question == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist question with id={answerDTO.LessonQuestionId}",
                            IsSuccess = false
                        };
                    }
                    var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == question.LessonId);
                    if(lesson == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not found class contain lesson",
                            IsSuccess = false,
                        };
                    }
                    var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == lesson.TopicId);
                    if (topic == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not found class contain lesson",
                            IsSuccess = false,
                        };
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(x => x.Id == topic.ClassId);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not found class contain lesson",
                            IsSuccess = false,
                        };
                    }
                    if (classes.Teacher != user.Id)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You not teach in this class. Don't have permission to create question for this class",
                            IsSuccess = false,
                        };
                    }
                    
                    LessonAnswer answer = new LessonAnswer
                    {
                        LessonQuestionId=answerDTO.LessonQuestionId,
                        ContentAnswer = answerDTO.ContentAnswer,
                        createBy=user.Id,
                        updateBy=user.Id,
                    };
                    await _context.LessonAnswers.AddAsync(answer);
                    question.IsAnswer = true;
                    _context.LessonQuestions.Update(question);
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
                        Message = $"Error when create new answer.",
                        IsSuccess = false,
                    };

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return new ManagerRespone
                    {
                        Message = $"Error: {ex.StackTrace}",
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

        public async Task<LessonAnswerDetail> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var answer = await _context.LessonAnswers.FirstOrDefaultAsync(x=>x.Id == id);
                    if (answer==null)
                    {
                        return null;
                    }
                    var question = await _context.LessonQuestions.FirstOrDefaultAsync(x => x.Id == answer.LessonQuestionId);
                    if (question == null)
                    {
                        return null;
                    }
                    var owner = await GetUsersFromUserServiceAsync(answer.createBy, accessToken);
                    if (owner == null)
                    {
                        return null;
                    }
                    LessonAnswerDetail detail = new LessonAnswerDetail
                    {
                        Id=answer.Id,
                        Question=question.ContentQuestion,
                        Content=answer.ContentAnswer,
                        createAt=answer.createAt.ToString(),
                        createBy=owner.UserName,
                        updateAt=answer.updateAt.ToString(),
                        updateBy=owner.UserName,
                    };
                    return detail;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return null;
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

        public async Task<List<LessonAnswerDetail>> GetByQuestion(string questionId)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var answer = await _context.LessonAnswers.Where(x => x.LessonQuestionId==questionId && x.isActive==true).ToListAsync();
                    List<LessonAnswerDetail> result = new List<LessonAnswerDetail>();
                    foreach(var a in answer)
                    {
                        var question = await _context.LessonQuestions.FirstOrDefaultAsync(x => x.Id == a.LessonQuestionId);
                        if (question == null)
                        {
                            return null;
                        }
                        var owner = await GetUsersFromUserServiceAsync(a.createBy, accessToken);
                        if (owner == null)
                        {
                            return null;
                        }
                        LessonAnswerDetail detail = new LessonAnswerDetail
                        {
                            Id = a.Id,
                            Question = question.ContentQuestion,
                            Content = a.ContentAnswer,
                            createAt = a.createAt.ToString(),
                            createBy = owner.UserName,
                            updateAt = a.updateAt.ToString(),
                            updateBy = owner.UserName,
                        };
                        result.Add(detail);
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

        public async Task<ManagerRespone> EditLessonAnswer(string id, LessonAnswerDTO answerDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var answer = await _context.LessonAnswers.FirstOrDefaultAsync(x => x.Id == id);
                    if (answer==null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist answer with id={id}",
                            IsSuccess = false
                        };
                    }
                    var question = await _context.LessonQuestions.FirstOrDefaultAsync(x => x.Id == answerDTO.LessonQuestionId);
                    if (question == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist question with id={answerDTO.LessonQuestionId}",
                            IsSuccess = false
                        };
                    }
                    var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == question.LessonId);
                    if (lesson == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not found class contain lesson",
                            IsSuccess = false,
                        };
                    }
                    var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == lesson.TopicId);
                    if (topic == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not found class contain lesson",
                            IsSuccess = false,
                        };
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(x => x.Id == topic.ClassId);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not found class contain lesson",
                            IsSuccess = false,
                        };
                    }
                    if (answer.createBy != user.Id)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't have permission to edit answer for this class",
                            IsSuccess = false,
                        };
                    }
                    if (user.Id !=classes.Teacher)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't have permission to edit answer for this class",
                            IsSuccess = false,
                        };
                    }
                
                    answer.updateAt = DateTime.Now;
                    answer.updateBy = user.Id;
                    answer.LessonQuestionId = answerDTO.LessonQuestionId;
                    answer.ContentAnswer= answerDTO.ContentAnswer;
                    _context.LessonAnswers.Update(answer);
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

        public async Task<ManagerRespone> DeleteAnswer(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var answer = await _context.LessonAnswers.FirstOrDefaultAsync(x => x.Id == id);
                    if (answer == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist answer with id={id}",
                            IsSuccess = false
                        };
                    }
                    
                    if (answer.createBy != user.Id)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't have permission to delete question for this class",
                            IsSuccess = false,
                        };
                    }
                    answer.updateAt = DateTime.Now;
                    answer.updateBy = user.Id;
                    answer.isActive = false;
                    _context.LessonAnswers.Update(answer);
                    int numberOfChanges = await _context.SaveChangesAsync();
                    if (numberOfChanges > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Successfully delete",
                            IsSuccess = true,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Error when delete answer.",
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

        public async Task<List<LessonAnswerDetail>> GetActive()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    List<LessonAnswerDetail> result = new List<LessonAnswerDetail>();
                    var answers = _context.LessonAnswers.Where(x => x.isActive == true).ToList();
                    
                    foreach (var a in answers)
                    {
                        var q = await _context.LessonQuestions.FirstOrDefaultAsync(x => x.Id == a.LessonQuestionId);
                        if (q == null)
                        {
                            return null;
                        }
                        var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == q.LessonId);
                        if (lesson == null)
                        {
                            return null;
                        };
                        var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == lesson.TopicId);
                        if (topic == null)
                        {
                            return null;
                        }
                        var classes = await _context.Classes.FirstOrDefaultAsync(x => x.Id == topic.ClassId);
                        if (classes == null)
                        {
                            return null;
                        }
                        UserDTO owner = await GetUsersFromUserServiceAsync(a.createBy, accessToken);
                        if (user.Id == classes.Teacher)
                        {

                            LessonAnswerDetail detail = new LessonAnswerDetail
                            {
                                Id = a.Id,
                                Question = q.ContentQuestion,
                                Content = a.ContentAnswer,
                                createAt = a.createAt.ToString(),
                                createBy = owner.UserName,
                                updateAt = a.updateAt.ToString(),
                                updateBy = owner.UserName,
                            };
                            result.Add(detail);

                        }
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
