using ExamService.Data;
using ExamService.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
namespace ExamService.Service.ExamService
{
    public class ExamServices : IExamService
    {
        public ExamsContext _context { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        public ExamServices(ExamsContext context, IHttpContextAccessor httpContext, HttpClient httpClient = null)
        {
            _context = context;
            _httpContextAccessor = httpContext;
            _httpClient = httpClient;
        }

        public async Task<ManagerRespone> AddExam(ExamDTO examDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {

                    var classes = await GetClassFromClassServiceAsync(examDTO.Class, accessToken);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist class with id= {examDTO.Class}.",
                            IsSuccess = false,
                        };
                    }
                    var typ = await _context.ExamsType.FirstOrDefaultAsync(t => t.Name == examDTO.Type);
                    if (typ == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Only allow exam with type is 45 mitutes or 15 minutes",
                            IsSuccess = false,
                        };
                    }
                    var path = $"Upload/{classes.Name}/Exams/";
                    var documentId = "";
                    if (examDTO.FileContent != null)
                    {
                        var document = await UploadExamFileFromCourseServiceAsync(examDTO.FileContent, path, accessToken);
                        if (document == null)
                        {
                            return new ManagerRespone
                            {
                                Message = $"Failed to upload new exam",
                                IsSuccess = false,
                            };
                        }
                        documentId = document.DocumentId;
                    }
                    if (classes.Teacher != user.Id)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You don't have permission to create exam to this class",
                            IsSuccess = false,
                        };
                    }
                    Exam ex = new Exam
                    {
                        Name = examDTO.Name,
                        TypeId = typ.Id,
                        ClassId = classes.Id,
                        UserId = user.Id,
                        NumberQuestion = examDTO.NumberQuestion,
                        Status = false,
                        createBy = user.Id,
                        updateBy = user.Id,
                        IsMutipleChoice = examDTO.IsMutipleChoice,
                        DateBegin = DateTime.Now,
                        DocumentId = documentId,
                    };
                    await _context.Exams.AddAsync(ex);
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
                        Message = $"Error when create new exam.",
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
        public async Task<DocumentDTO> UploadExamFileFromCourseServiceAsync(IFormFile file, string path, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var uniqueFileName = file.FileName;
            var filePath = Path.Combine(path, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            DocumentDTO documentDTO = new DocumentDTO
            {
                FileName = uniqueFileName,
                ContentType = Path.GetExtension(file.FileName),
                Link = filePath
            };
            var url = "https://localhost:44367/Document/AddDocment";
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(documentDTO);

            // Create the request content with JSON data
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the POST request to the API endpoint
            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content (if any)
                var result = await response.Content.ReadAsStringAsync();
                var resultDocument = System.Text.Json.JsonSerializer.Deserialize<DocumentDTO>(result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return resultDocument;
            }
            else
            {
                return null;
            }

        }

        public async Task<List<ExamDTO>> GetAll()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var exams = _context.Exams.ToList();
                    List<ExamDTO> result = new List<ExamDTO>();
                    foreach (var exam in exams)
                    {
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
                        ExamDTO examDTO = new ExamDTO
                        {
                            Id = exam.Id,
                            Name = exam.Name,
                            Type = typ.Name,
                            Class = classes.Name,
                            IsMutipleChoice = exam.IsMutipleChoice,
                            NumberQuestion = exam.NumberQuestion,
                        };
                        result.Add(examDTO);
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

        public async Task<ExamDTO> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var exam = await _context.Exams.FirstOrDefaultAsync(x => x.Id == id);
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
                    if (classes.Teacher == user.Id)
                    {
                        ExamDTO examDTO = new ExamDTO
                        {
                            Id = exam.Id,
                            Name = exam.Name,
                            Type = typ.Name,
                            Class = classes.Name,
                            IsMutipleChoice = exam.IsMutipleChoice,
                            NumberQuestion = exam.NumberQuestion,
                        };
                        return examDTO;
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

        public async Task<ManagerRespone> EditExam(ExamDTO examDTO)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var exam = await _context.Exams.FirstOrDefaultAsync(e => e.Id == examDTO.Id);
                    if (exam == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist exam with id={examDTO.Id}",
                            IsSuccess = false
                        };
                    }
                    var classes = await GetClassFromClassServiceAsync(examDTO.Class, accessToken);
                    if (classes == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist class with id= {examDTO.Class}.",
                            IsSuccess = false,
                        };
                    }
                    if (classes.Teacher != user.Id)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You don't have permission to edit exam in this class",
                            IsSuccess = false,
                        };
                    }
                    var typ = await _context.ExamsType.FirstOrDefaultAsync(t => t.Name == examDTO.Type);
                    if (typ == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Only allow exam with type is 45 mitutes or 15 minutes",
                            IsSuccess = false,
                        };
                    }
                    var d = await GetDocumentFromDocumentServiceAsync(exam.DocumentId, accessToken);
                    var link = "";
                    var documentId = "";
                    if (d != null)
                    {
                        link = d.Link;
                        documentId = d.DocumentId;
                    }
                    var path = $"Upload/{classes.Name}/Exams/";
                    if (d != null && examDTO.FileContent != null)
                    {
                        var document = await UploadExamFileFromCourseServiceAsync(examDTO.FileContent, path, accessToken);
                        if (document == null)
                        {
                            return new ManagerRespone
                            {
                                Message = $"Failed to upload new exam",
                                IsSuccess = false,
                            };
                        }
                        documentId = document.DocumentId;
                    }
                    exam.Name = examDTO.Name != null ? examDTO.Name : exam.Name;
                    exam.TypeId = typ.Id;
                    exam.ClassId = classes.Id;
                    exam.UserId = user.Id;
                    exam.NumberQuestion = examDTO.NumberQuestion;
                    exam.IsMutipleChoice = examDTO.IsMutipleChoice;
                    exam.UpdatedAt = DateTime.Now;
                    exam.updateBy = user.Id;

                    exam.DocumentId = d == null ? null : documentId;
                    _context.Exams.Update(exam);
                    int numberOfChanges = await _context.SaveChangesAsync();
                    if (numberOfChanges == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when update lesson",
                            IsSuccess = false,
                        };
                    }

                    if (examDTO.FileContent != null && d != null)
                    {
                        try
                        {
                            if (File.Exists(link))
                            {
                                File.Delete(link);
                                var dele = await DeleteDocumentFromDocumentService(d.DocumentId, accessToken);
                                if (!dele.IsSuccess)
                                {
                                    return new ManagerRespone
                                    {
                                        Message = "Error when delete old document",
                                        IsSuccess = false,
                                    };
                                }
                            }
                            else
                            {
                                return new ManagerRespone
                                {
                                    Message = "Don't exist file path",
                                    IsSuccess = false,
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            return new ManagerRespone
                            {
                                Message = $"Error {ex.StackTrace}",
                                IsSuccess = false,
                            };
                        }


                    }
                    return new ManagerRespone
                    {
                        Message = $"Successfully update {numberOfChanges} changes to the database.",
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

        private async Task<ManagerRespone> DeleteDocumentFromDocumentService(string? documentId, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.DeleteAsync($"https://localhost:44367/Document/DeleteDocument?id={documentId}");
            if (response.IsSuccessStatusCode)
            {
                return new ManagerRespone
                {
                    IsSuccess = true,
                };
            }
            return new ManagerRespone
            {
                IsSuccess = false
            };
        }

        public async Task<DocumentDTO> GetDocumentFromDocumentServiceAsync(string id, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync($"https://localhost:44367/Document/GetById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content to your User class
                var jsonContent = await response.Content.ReadAsStringAsync();
                var document = System.Text.Json.JsonSerializer.Deserialize<DocumentDTO>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return document;
            }
            return null;
        }

        public async Task<ManagerRespone> DeleteExam(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var ex = await _context.Exams.FirstOrDefaultAsync(rs => rs.Id == id);
                    if (ex == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist exam with id={id}",
                            IsSuccess = false
                        };
                    }
                    var classes = await GetClassFromClassServiceAsync(ex.ClassId, accessToken);

                    if (classes.Teacher != user.Id)
                    {
                        return new ManagerRespone
                        {
                            Message = $"You don't have permission to delete this exam",
                            IsSuccess = false
                        };
                    }
                    ex.IsActive = false;
                    ex.UpdatedAt = DateTime.Now;
                    _context.Exams.Update(ex);
                    int number = await _context.SaveChangesAsync();
                    if (number == 0)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error when delete exam",
                            IsSuccess = false,
                        };
                    }
                    return new ManagerRespone
                    {
                        Message = $"Successfully delete exam",
                        IsSuccess = true,
                    };
                }
                catch (Exception ex)
                {
                    return new ManagerRespone
                    {
                        Message = $"Error when delete exam {ex.StackTrace}",
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

        public async Task<List<ExamDTO>> GetActive()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var exams = _context.Exams.Where(x => x.IsActive == true).ToList();
                    List<ExamDTO> result = new List<ExamDTO>();
                    foreach (var exam in exams)
                    {
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
                        if (classes.Teacher == user.Id)
                        {
                            ExamDTO examDTO = new ExamDTO
                            {
                                Id = exam.Id,
                                Name = exam.Name,
                                Type = typ.Name,
                                Class = classes.Name,
                                IsMutipleChoice = exam.IsMutipleChoice,
                                NumberQuestion = exam.NumberQuestion,
                            };
                            result.Add(examDTO);
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
    }
}
