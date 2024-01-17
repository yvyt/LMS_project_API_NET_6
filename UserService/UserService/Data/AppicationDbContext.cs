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
        public DbSet<RolePermission> RolePermissions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<RolePermission>()
                .HasOne(r => r.Role)
                .WithMany()
                .HasForeignKey(r => r.RoleId);
            builder.Entity<RolePermission>()
                .HasOne(r => r.Permission)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(r => r.PermissionId);
            //SeedPermission(builder);
            SeedRolePermission(builder);
        }

        private void SeedRolePermission(ModelBuilder builder)
        {
            List<string> leaderPermission = new List<string> {
                "0bda25ae-f034-43f3-b6fb-2f06d32870a4","80e94b56-160d-4450-a1d1-0f17f6fe1456","88dbd1f9-d943-41ed-a607-866def46d6a6","95aeb353-24d8-4a8d-8b43-ae4ef3860a03",
                "2a6f25ea-83aa-4c94-a1eb-0434671d5230", "86e1b9f0-acf0-43f2-9f52-9fb253d19c6d", "90065181-d7c7-4258-8c5b-237871454c5a", "c5feb7be-eeea-46e3-b44c-588d37eff052",
                "75530804-26db-41f3-9d1b-e58ed9e8fee4", "7b15cfff-a2ed-4126-8860-e3b99df91dd9", "c0030dfa-5bae-4dfa-8e7c-b367ed42a3db", "c57dadf9-5da7-4166-b0d0-ef64667f2572",
                "27a0b056-02b8-4cb7-a81c-26757569d9cf", "2e8e852d-9e69-4f43-a9bf-deaead6485d1", "a995e571-59cb-4f70-8aa9-2f637bb67e13", "ead1e55a-8213-442d-b8a4-a49fbeaad9a2",
                "13adaeba-2ce2-4f7d-a16d-9af743399612", "28147748-0067-48d7-95d7-4aca515a58c7", "a9ea9725-b0d3-4d31-b6c3-1ac263ac463a", "f702360a-f73b-46e7-adc0-ccaaf9c75db8",
                "ee255870-c765-4451-a019-a408d9e6ee27", "0db91594-128b-4aa5-bf04-77aa417143f4", "c986d59c-57fb-4669-86e0-7b51fd57b3ae", "a184a406-b527-4c2f-a123-f0a99d1737cd", 
                "e1fb5d6a-f234-4b7b-8f4c-9c02a80e7396"
            };
            List<string> teacherPermission = new List<string>
            {
                "1b183555-3067-4485-b563-65b34b29475f", "6d6ad73b-9a19-449f-a809-57794239e882", "940748d7-c97d-4157-8104-fde614869fd9", "ee255870-c765-4451-a019-a408d9e6ee27", 
                "0db91594-128b-4aa5-bf04-77aa417143f4", "6a886d29-1f69-459e-80ca-5b37ef7f2fe6", "9aafbadc-9401-4ebb-95b1-1adf64ecef00", "e368179b-9797-4018-8867-72723cecc92e", 
                "75530804-26db-41f3-9d1b-e58ed9e8fee4", "7b15cfff-a2ed-4126-8860-e3b99df91dd9", "c0030dfa-5bae-4dfa-8e7c-b367ed42a3db", "c57dadf9-5da7-4166-b0d0-ef64667f2572", 
                "34ffbd7d-0ec7-4a56-8d83-179568ba07c5", "3c858bdd-3ef3-4ba6-b4f5-12b45f14ea02", "c5196f8c-95a8-4512-b5f2-c576958ff5cd", "de75d96d-fdb4-439b-8cf8-43d587974865", 
                "1793ca7e-61e6-4961-bdfb-766a3555fd20", "c986d59c-57fb-4669-86e0-7b51fd57b3ae", "ce808469-ac15-40cc-a317-cbc2d7b1c7d2", "d8f4aaed-cd0b-46e8-8b9e-4ffefe9b2192", 
                "46f87f72-afaa-4df4-85d9-87dcd4c9b076", "9c840343-8004-4d7f-b670-9a63c0237bce", "a184a406-b527-4c2f-a123-f0a99d1737cd", "a92d095a-50bf-4ff6-b774-bdd81df6313d", 
                "1e77c1c6-478e-4111-8d76-c03c835c1151", "a3ebb2ea-fc78-4d7a-bc34-b620600a59c4", "b0c9c2e8-aa9e-4a02-a5ac-732152e72261", "e1fb5d6a-f234-4b7b-8f4c-9c02a80e7396", 
                "28147748-0067-48d7-95d7-4aca515a58c7", "86e1b9f0-acf0-43f2-9f52-9fb253d19c6d", "80e94b56-160d-4450-a1d1-0f17f6fe1456"

            };
            List<string> studentPErmission = new List<string>
            {
                "80e94b56-160d-4450-a1d1-0f17f6fe1456", "86e1b9f0-acf0-43f2-9f52-9fb253d19c6d", "28147748-0067-48d7-95d7-4aca515a58c7", "3c858bdd-3ef3-4ba6-b4f5-12b45f14ea02",
                "ee255870-c765-4451-a019-a408d9e6ee27", "0db91594-128b-4aa5-bf04-77aa417143f4"
            };
            foreach(var i in leaderPermission)
            {
                builder.Entity<RolePermission>()
               .HasData(
                     new RolePermission()
                     {
                         Id = Guid.NewGuid().ToString(),
                         RoleId = "48f64b2c-f113-4d03-a342-42429ea39f15",
                         PermissionId = i
                     }
               );
               
            }
            foreach (var i in teacherPermission)
            {
                builder.Entity<RolePermission>()
               .HasData(
                     new RolePermission()
                     {
                         Id = Guid.NewGuid().ToString(),
                         RoleId = "a9465f72-ff0b-4919-92c7-d0d049a91a51",
                         PermissionId = i
                     }
               );

            }
            foreach (var i in studentPErmission)
            {
                builder.Entity<RolePermission>()
               .HasData(
                     new RolePermission()
                     {
                         Id = Guid.NewGuid().ToString(),
                         RoleId = "fd455dd4-097a-457e-95a3-21c40374fb01",
                         PermissionId = i
                     }
               );

            }
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
