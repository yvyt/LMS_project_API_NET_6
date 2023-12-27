using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserService.Data
{
    public class AppicationDbContext: IdentityDbContext
    {
        public AppicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {  get; set; }
    }
}
