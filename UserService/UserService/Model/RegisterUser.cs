using System.ComponentModel.DataAnnotations;

namespace UserService.Model
{
    public class RegisterUser
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50,MinimumLength =8)]
        public string Password { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string ConfirmPassword {  get; set; }
        [Required]
        [StringLength(10)]
        public string Phone {  get; set; }
       
    }
}
