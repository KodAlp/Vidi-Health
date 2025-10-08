using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Vidi_Health.Models
{
    public class DietContextFactory : IDesignTimeDbContextFactory<DietContext>
    {
        public DietContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DietContext>();

            // Design-time için basit SQLite bağlantısı
            optionsBuilder.UseSqlite("Data Source=diet_tracker.db");

            return new DietContext(optionsBuilder.Options);
        }
    }
}