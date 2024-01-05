using CourseService.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace CourseService.Data
{
    public class CourseContext:DbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options)
        : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<TypeFile> TypeFiles { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<Resources> Resources { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Classes>()
                .HasOne(c => c.Course)
                .WithMany(c => c.Classes)
                .HasForeignKey(c => c.CourseId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Class)
                .WithMany(st => st.StudentCourses)
                .HasForeignKey(sc => sc.ClassId);

            modelBuilder.Entity<Topic>()
                .HasOne(t => t.Classes)
                .WithMany(cl => cl.Topics)
                .HasForeignKey(t => t.ClassId);

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Topics)
                .WithMany(tp => tp.Lessons)
                .HasForeignKey(l => l.TopicId);
            modelBuilder.Entity<Lesson>()
                .HasOne(ty => ty.TypeFile)
                .WithMany(ty => ty.Lessons)
                .HasForeignKey(ty => ty.TypeId);
            modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Document)
            .WithOne(d => d.Lesson)
            .HasForeignKey<Lesson>(d => d.DocumentId);
            modelBuilder.Entity<Resources>()
                .HasOne(r => r.Lessons)
                .WithMany(r => r.Resources)
                .HasForeignKey(r => r.LessonId);
            modelBuilder.Entity<Resources>()
                      .HasOne(l => l.Document)
                      .WithOne(d => d.Resources);
            //SeedType(modelBuilder);
        }

        private void SeedType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeFile>().HasData(
               new TypeFile()
               {
                   Id=Guid.NewGuid().ToString(),
                   Name="Documents"
               },
               new TypeFile()
               {
                   Id = Guid.NewGuid().ToString(),
                   Name = "Slides"
               }
               
               );
        }
    
    }
}
