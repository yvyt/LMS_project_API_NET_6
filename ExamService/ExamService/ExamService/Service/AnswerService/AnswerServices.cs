using ExamService.Data;
using ExamService.Model;
using Microsoft.EntityFrameworkCore;

namespace ExamService.Service.AnswerService
{
    public class AnswerServices : IAnswerService
    {
        private ExamsContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        public AnswerServices(ExamsContext context, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<ManagerRespone> AddAnswer(AnswerDTO answerDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var question = await _context.Question.FirstOrDefaultAsync(x => x.Id == answerDTO.Question);
                    if (question == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist question with id= {answerDTO.Question}.",
                            IsSuccess = false,
                        };
                    }
                    if (question.createBy != user.Id)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You don't have permission to create answer for this question",
                            IsSuccess = false,
                        };
                    }
                    Answer answer = new Answer
                    {
                        ContentAnswer = answerDTO.ContentAnswer,
                        QuestionId = answerDTO.Question,
                        IsCorrect = answerDTO.IsCorrect,
                        createBy = user.Id,
                        updateBy = user.Id
                    };
                    await _context.Answers.AddAsync(answer);
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

        public async Task<List<AnswerDTO>> GetAll()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var answers = _context.Answers.ToList();
                    List<AnswerDTO> result = new List<AnswerDTO>();
                    foreach (var answer in answers)
                    {
                        var question = await _context.Question.FirstOrDefaultAsync(x => x.Id == answer.QuestionId);
                        if (question == null)
                        {
                            return null;
                        }
                        AnswerDTO a = new AnswerDTO
                        {
                            Id = answer.Id,
                            Question = question.ContentQuestion,
                            ContentAnswer = answer.ContentAnswer,
                            IsCorrect = answer.IsCorrect,
                        };
                        result.Add(a);
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

        public async Task<AnswerDTO> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var answer = await _context.Answers.FirstOrDefaultAsync(x => x.Id == id);
                    if (answer == null)
                    {
                        return null;
                    }
                    var question = await _context.Question.FirstOrDefaultAsync(x => x.Id == answer.QuestionId);
                    if (question == null)
                    {
                        return null;
                    }
                    AnswerDTO a = new AnswerDTO
                    {
                        Id = answer.Id,
                        Question = question.ContentQuestion,
                        ContentAnswer = answer.ContentAnswer,
                        IsCorrect = answer.IsCorrect,
                    };
                    return a;

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return null;
                }
            }
            return null;
        }

        public async Task<List<AnswerDTO>> GetByQuestion(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var answers = _context.Answers.Where(x => x.QuestionId == id && x.IsActive==true).ToList();

                    List<AnswerDTO> result = new List<AnswerDTO>();
                    foreach (var answer in answers)
                    {
                        var question = await _context.Question.FirstOrDefaultAsync(x => x.Id == answer.QuestionId);
                        if (question == null)
                        {
                            return null;
                        }
                        AnswerDTO a = new AnswerDTO
                        {
                            Id = answer.Id,
                            Question = question.ContentQuestion,
                            ContentAnswer = answer.ContentAnswer,
                            IsCorrect = answer.IsCorrect,
                        };
                        result.Add(a);
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

        public async Task<AnswerDTO> GetCorrectAnswer(string questionId)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var answer = await _context.Answers.FirstOrDefaultAsync(x => x.QuestionId == questionId && x.IsCorrect==true && x.IsActive==true);
                    if (answer == null)
                    {
                        return null;
                    }
                    var question = await _context.Question.FirstOrDefaultAsync(x => x.Id == answer.QuestionId);
                    if (question == null)
                    {
                        return null;
                    }
                    AnswerDTO a = new AnswerDTO
                    {
                        Id = answer.Id,
                        Question = question.ContentQuestion,
                        ContentAnswer = answer.ContentAnswer,
                        IsCorrect = answer.IsCorrect,
                    };
                    return a;

                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return null;
                }
            }
            return null;
        }

        public async Task<ManagerRespone> EditAnswer(AnswerDTO answerDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var answer = await _context.Answers.FirstOrDefaultAsync(x => x.Id == answerDTO.Id);
                    if (answer == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist answer with id= {answerDTO.Question}.",
                            IsSuccess = false,
                        };
                    }
                    var question = await _context.Question.FirstOrDefaultAsync(x => x.Id == answerDTO.Question);
                    if (question == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist question with id= {answerDTO.Question}.",
                            IsSuccess = false,
                        };
                    }
                    if (question.createBy != user.Id)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You don't have permission to edit answer for this question",
                            IsSuccess = false,
                        };
                    }
                    answer.ContentAnswer = answerDTO.ContentAnswer;
                    answer.QuestionId = answerDTO.Question;
                    answer.IsCorrect = answerDTO.IsCorrect;
                    answer.UpdateAt = DateTime.Now;
                    answer.updateBy = user.Id;

                     _context.Answers.Update(answer);
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
                        Message = $"Error when update answer",
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
                    var answer = await _context.Answers.FirstOrDefaultAsync(x => x.Id ==id);
                    if (answer == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist answer with id= {id}.",
                            IsSuccess = false,
                        };
                    }
                    var question = await _context.Question.FirstOrDefaultAsync(x => x.Id == answer.QuestionId);
                    if (question == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist question with id= {answer.QuestionId}.",
                            IsSuccess = false,
                        };
                    }
                    
                    if (question.createBy != user.Id)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You don't have permission to delete for this question",
                            IsSuccess = false,
                        };
                    }
                    answer.IsActive = false;
                    answer.UpdateAt = DateTime.Now;
                    answer.updateBy = user.Id;

                    _context.Answers.Update(answer);
                    int numberOfChanges = await _context.SaveChangesAsync();
                    if (numberOfChanges > 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Successfully delete answer",
                            IsSuccess = true,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Error when delete answer",
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

        public async Task<List<AnswerDTO>> GetActive()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var answers = _context.Answers.Where(x => x.IsActive==true).ToList();

                    List<AnswerDTO> result = new List<AnswerDTO>();
                    foreach (var answer in answers)
                    {
                        var question = await _context.Question.FirstOrDefaultAsync(x => x.Id == answer.QuestionId);
                        if (question == null)
                        {
                            return null;
                        }
                        AnswerDTO a = new AnswerDTO
                        {
                            Id = answer.Id,
                            Question = question.ContentQuestion,
                            ContentAnswer = answer.ContentAnswer,
                            IsCorrect = answer.IsCorrect,
                        };
                        result.Add(a);
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
