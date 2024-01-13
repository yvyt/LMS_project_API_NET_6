using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserService.Data
{
    public class AppicationDbContext : IdentityDbContext
    {
        public AppicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedPermission(builder);
        }

        private void SeedPermission(ModelBuilder builder)
        {
            List<string> modules = new List<string> { "Course", "Class", "PrivateFile", "Topic", "Lesson", "Resource", "Exam", "Question", "Answer", "ExamQuestion", "Notification", "System", "User" };
            List<string> action = new List<string> { "Edit", "Delete","Create", "View"};
            foreach (var module in modules)
            {
                foreach (var ac in action)
                {
                    builder.Entity<Permission>().HasData(
                    new Permission()
                    {
                        Id=Guid.NewGuid().ToString(),
                        PermissionName = $"{ac}:{module}"
                    }
                );
                }
            }
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Name = "Leadership",
                    ConcurrencyStamp = "1",
                    NormalizedName = "Leadership"
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
                    ConcurrencyStamp = "3",
                    NormalizedName = "Student"
                }
                );
        }
    }
}
