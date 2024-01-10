using Microsoft.EntityFrameworkCore;

namespace PrivateFileService.Data
{
    public class PrivateFileContext:DbContext
    {
        public PrivateFileContext(DbContextOptions<PrivateFileContext> options)
       : base(options)
        {
        }
        public DbSet<PrivateFile> PrivateFiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
