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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Type)
                .WithMany(e => e.Exams)
                .HasForeignKey(e => e.TypeId);
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
