using ExamService.Data;
using ExamService.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;
namespace ExamService.Service.ExamService
{
    public class ExamServices:IExamService
    {
        public ExamsContext _context {  get; set; }
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
                            Message = $"Don't exist course with id= {examDTO.Class}.",
                            IsSuccess = false,
                        };
                    }
                    var typ= await _context.ExamsType.FirstOrDefaultAsync(t=>t.Name == examDTO.Type);
                    if(typ == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Only allow exam with type is 45 mitutes or 15 minutes",
                            IsSuccess = false,
                        };
                    }
                    var path = $"Upload/{classes.Name}/Exams/";
                    var document = await UploadExamFileFromCourseServiceAsync(examDTO.FileContent, path,accessToken);
                    if (document == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Failed to upload new exam",
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
                        DocumentId=document.DocumentId
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
                FileName=uniqueFileName,
                ContentType = Path.GetExtension(file.FileName),
                Link=filePath
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


    }
}
