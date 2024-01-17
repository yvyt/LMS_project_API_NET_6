using UserService.Data;

namespace UserService.Model
{
    public class RoleDetail
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RolePermission> Permissions { get; set; }
    }
}
