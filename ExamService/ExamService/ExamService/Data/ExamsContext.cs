using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExamService.Data
{
    public class ExamsContext:DbContext
    {
        public ExamsContext(DbContextOptions<ExamsContext> options)
        : base(options)
        {
        }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamType> ExamsType { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Answer>   Answers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Type)
                .WithMany(e => e.Exams)
                .HasForeignKey(e => e.TypeId);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(a => a.Answers)
                .HasForeignKey(a => a.QuestionId);
            //SeedType(modelBuilder);

        }
        private void SeedType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamType>()
                .HasData(new ExamType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "45 minutes"
                },
               new Exam()
               {
                   Id = Guid.NewGuid().ToString(),
                   Name = "15 minutes"
               });
        }
    }
}
