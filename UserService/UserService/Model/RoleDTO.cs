using System.ComponentModel.DataAnnotations;

namespace UserService.Model
{
    public class RoleDTO
    {
        [Required]
        public string RoleName {  get; set; }
        [Required]
        public List<string> Permissions { get; set; }
    }
}
