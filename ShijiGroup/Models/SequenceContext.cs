using Microsoft.EntityFrameworkCore;

namespace ShijiGroup.Models
{
    public class SequenceContext : DbContext
    {
        public SequenceContext(DbContextOptions<SequenceContext> options)
             : base(options)
        {
        }

        public DbSet<Sequence> Sequences { get; set; }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}