using Microsoft.EntityFrameworkCore;

namespace ShijiGroup.Models
{
    public class WordFinderContext : DbContext
    {
        public WordFinderContext(DbContextOptions<WordFinderContext> options)
             : base(options)
        {
        }

        public DbSet<Matrix> Matrixes { get; set; }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Matrix>().HasKey(x => x.Id);
        }
    }
}