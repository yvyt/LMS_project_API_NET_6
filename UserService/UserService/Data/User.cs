using Microsoft.AspNetCore.Identity;

namespace UserService.Data
{

    public class User
    {
        public string Id {  get; set; }
        public string Fullname {  get; set; }
        public string Email { get; set; }
        public string Gender {  get; set; }
        public string Address { get; set; }

    }
}
