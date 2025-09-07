using Microsoft.EntityFrameworkCore;

namespace Vidi_Health.Models
{
    public class DietContext : DbContext
    {
        public DbSet<User> Users { get; set; }  
        public DbSet<Measurements> Measurements { get; set; }
        public DbSet<BodyCompositions> BodyCompositions { get; set; }

        public DietContext(DbContextOptions<DietContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = diet_tracker.db");
            }
        }
    }
}
