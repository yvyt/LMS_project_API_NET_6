using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PrivateFileService.Model
{
    public class PrivateFileUploadDTO
    {
        public IFormFile? FileContent { get; set; }
    }
}
