using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserService.Data
{
    public class RolePermission
    {
        [Key]
        [StringLength(255)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string RoleId { get; set; }
        public string PermissionId {  get; set; }
        public virtual IdentityRole Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
