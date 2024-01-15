﻿using CourseService.Data;
using CourseService.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CourseService.Service.LessonQuestionService
{
    public class LessonQuestionService:ILessonQuestionService
    {
        private CourseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public LessonQuestionService(CourseContext context, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<ManagerRespone> AddLessonQuestion(LessonQuestionDTO questionDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == questionDTO.LessonId);
                    if (lesson == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist lesson with Id={questionDTO.LessonId}",
                            IsSuccess = false,
                        };
                    }
                    
                    var t = await _context.Topics.FirstOrDefaultAsync(x => x.Id == lesson.TopicId);
                    if (t == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not found class contain lesson",
                            IsSuccess = false,
                        };
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(x => x.Id == t.ClassId);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not found class contain lesson",
                            IsSuccess = false,
                        };
                    }
                    var st = await _context.StudentCourses.FirstOrDefaultAsync(x => x.StudentId == user.Id && x.ClassId == classes.Id);
                    if(st == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You not join this class. Don't have permission to create question for this lesson",
                            IsSuccess = false,
                        };
                    }
                    LessonQuestion q = new LessonQuestion
                    {
                        LessonId=questionDTO.LessonId,
                        Title= questionDTO.Title,
                        ContentQuestion= questionDTO.ContentQuestion,
                        createBy=user.Id
                    };
                    await _context.LessonQuestions.AddAsync(q);
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

        public async Task<ManagerRespone> AddLessonQuestionFromTeacher(LessonQuestionFromTeacher questionDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var lesson = await _context.Lessons.FirstOrDefaultAsync(x => x.Id == questionDTO.LessonId);
                    if (lesson == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist lesson with Id={questionDTO.LessonId}",
                            IsSuccess = false,
                        };
                    }
                    var t = await _context.Topics.FirstOrDefaultAsync(x => x.Id == lesson.TopicId);
                    if (t == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Not found class contain lesson",
                            IsSuccess = false,
                        };
                    }
                    if (questionDTO.TopicId != null)
                    {
                        if(t.Id!= questionDTO.TopicId)
                        {
                            return new ManagerRespone
                            {
                                Message = $"This topic not contain this lesson",
                                IsSuccess = false,
                            };
                        }
                    }
                    var classes = await _context.Classes.FirstOrDefaultAsync(x => x.Id == t.ClassId);
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

                    LessonQuestion q = new LessonQuestion
                    {
                        TopicId=questionDTO.TopicId,
                        LessonId = questionDTO.LessonId,
                        Title = questionDTO.Title,
                        ContentQuestion = questionDTO.ContentQuestion,
                        createBy = user.Id
                    };
                    await _context.LessonQuestions.AddAsync(q);
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
        public async Task<List<LessonQuestionDetail>> GetQuestionByTime()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    List<LessonQuestionDetail> result = new List<LessonQuestionDetail>();
                    var questions = _context.LessonQuestions.Where(x=>x.isFromTeacher==false).OrderByDescending(x=>x.createAt).ToList();
                    if (questions == null)
                    {
                        return null;
                    }
                    foreach (var q in questions)
                    {
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
                        UserDTO owner = await GetUsersFromUserServiceAsync(q.createBy, accessToken);
                        if (user.Id == classes.Teacher)
                        {
                            
                                LessonQuestionDetail qu = new LessonQuestionDetail
                                {
                                    Id = q.Id,
                                    Lesson = lesson.Title,
                                    Title = q.Title,
                                    Content = q.ContentQuestion,
                                    createAt = q.createAt.ToString(),
                                    createBy = owner.UserName
                                };
                                result.Add(qu);
                            
                        }
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return null ;
                }
            }
            return null;
        }

        public async Task<List<LessonQuestionDetail>> GetQuestionByAnswer()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    List<LessonQuestionDetail> result = new List<LessonQuestionDetail>();
                    var questions = _context.LessonQuestions.Where(x=>x.IsAnswer==false && x.isFromTeacher==false).ToList();
                    if (questions == null)
                    {
                        return null;
                    }
                    foreach (var q in questions)
                    {
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
                        UserDTO owner = await GetUsersFromUserServiceAsync(q.createBy, accessToken);
                        if (user.Id == classes.Teacher)
                        {
                            
                                LessonQuestionDetail qu = new LessonQuestionDetail
                                {
                                    Id = q.Id,
                                    Lesson = lesson.Title,
                                    Title = q.Title,
                                    Content = q.ContentQuestion,
                                    createAt = q.createAt.ToString(),
                                    createBy = owner.UserName
                                };
                                result.Add(qu);
                            
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
