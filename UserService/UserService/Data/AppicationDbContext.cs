using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserService.Data
{
    public class AppicationDbContext: IdentityDbContext
    {
        public AppicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {  get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }
        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Name="Leadership",ConcurrencyStamp="1",NormalizedName="Leadership"
                },
                new IdentityRole()
                {
                    Name = "Teacher",
                    ConcurrencyStamp = "2",
                    NormalizedName = "Teacher"
                },
                new IdentityRole()
                {
                    Name = "Student",
                    ConcurrencyStamp ="3",
                    NormalizedName = "Student"
                }
                );
        }
    }
}
