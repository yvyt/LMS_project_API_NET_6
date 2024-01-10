using PrivateFileService.Data;
using PrivateFileService.Model;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace PrivateFileService.Service.PrivateFileService
{
    public class PrivateFileServices:IPrivateFileService
    {
        public PrivateFileContext _context { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        public PrivateFileServices(PrivateFileContext context, IHttpContextAccessor httpContext, HttpClient httpClient = null)
        {
            _context = context;
            _httpContextAccessor = httpContext;
            _httpClient = httpClient;
        }

        public async Task<ManagerRespone> AddPrivateFile(PrivateFileUploadDTO privateFile)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {

                   
                    var path = $"Upload/{user.Id}/PrivateFile/";
                    var documentId = "";
                    if (privateFile.FileContent != null)
                    {
                        var document = await UploadExamFileFromCourseServiceAsync(privateFile.FileContent, path, accessToken);
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
                    var file = privateFile.FileContent;
                    long fileSizeInBytes = file.Length;

                    PrivateFile privateFile1 = new PrivateFile
                    {
                        Name=file.FileName,
                        Type= Path.GetExtension(file.FileName),
                        Size=ConvertBytesToMB(fileSizeInBytes),
                        createBy=user.Id,
                        updateBy=user.Id,
                        DocumentId=documentId,
                    };
                    await _context.PrivateFiles.AddAsync(privateFile1);
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

        private async Task<DocumentDTO> UploadExamFileFromCourseServiceAsync(IFormFile file, string path, string accessToken)
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
        public string ConvertBytesToMB(long bytes)
        {
            double megabytes = bytes / (1024.0 * 1024.0);
            return $"{megabytes:F2} MB";
        }

        public async Task<List<PrivateFileDTO>> GetAll()
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var files= _context.PrivateFiles.ToList();
                    List<PrivateFileDTO> result = new List<PrivateFileDTO>();
                    foreach (var file in files)
                    {
                        var fileDTO = new PrivateFileDTO
                        {
                            Id=file.Id,
                            Name= file.Name,
                            Type= file.Type,
                            Size=file.Size,
                        };
                        result.Add(fileDTO);
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

        public async Task<PrivateFileDTO> GetById(string id)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var file = await _context.PrivateFiles.FirstOrDefaultAsync(x => x.Id == id);
                    if(file==null)
                    {
                        return null;
                    }
                    var fileDTO = new PrivateFileDTO
                    {
                        Id = file.Id,
                        Name = file.Name,
                        Type = file.Type,
                        Size = file.Size,
                    };
                    return fileDTO;
                }
                catch (Exception ex)
                {
                    // Handle exceptions appropriately
                    return null;
                }
            }
            return null;
        }

        public async Task<ManagerRespone> RenameFile(string id, string newName)
        {
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var user = _httpContextAccessor.HttpContext.Items["User"] as UserDTO; // user create 
            if (user != null)
            {
                try
                {
                    var file = await _context.PrivateFiles.FirstOrDefaultAsync(x => x.Id == id);
                    if (file == null)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Don't exist private file with id={id}",
                            IsSuccess = false,
                        };
                    }
                    var oldName= file.Name;
                    
                    var document = await GetDocumentFromDocumentServiceAsync(file.DocumentId, accessToken);
                    if(document == null)
                    {

                        return new ManagerRespone
                        {
                            Message = $"Don't exist document with id={file.DocumentId}",
                            IsSuccess = false,
                        };
                    }
                    var oldPath = document.Link;
                    var newPath = document.Link.Replace(oldName, newName)+document.ContentType;
                    var result = await UpdateLinkFromDocumentServiceAsync(document.DocumentId,oldPath, newPath,accessToken);
                    if (!result.IsSuccess)
                    {
                        return result;
                    }
                    try
                    {
                        Directory.Move(oldPath, newPath);
                    }
                    catch (Exception ex)
                    {
                        return new ManagerRespone
                        {
                            Message = $"Error while renaming folder: {ex.Message}",
                            IsSuccess = false,
                        };

                    }
                    file.Name = newName+Path.GetExtension(oldName);
                    _context.PrivateFiles.Update(file);
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
                        Message = $"Error when rename",
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

        private async Task<ManagerRespone> UpdateLinkFromDocumentServiceAsync(string id,string oldPath, string newPath,string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            
            var response = await _httpClient.PutAsync($"https://localhost:44367/Document/UploadLink?id={id}&oldPath={oldPath}&newPath={newPath}", null) ;

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the response content to your User class
                var result = await response.Content.ReadAsStringAsync();
                var re = System.Text.Json.JsonSerializer.Deserialize<ManagerRespone>(result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return re;
            }
            return new ManagerRespone
            {
                Message ="Error when upload link",
                IsSuccess=false
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

    }
}
