using CourseService.Data;
using Microsoft.EntityFrameworkCore;
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
        }

    }
}
