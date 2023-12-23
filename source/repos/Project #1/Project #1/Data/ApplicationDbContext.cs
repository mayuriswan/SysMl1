using Microsoft.EntityFrameworkCore;
using Project__1.Models;

namespace Project__1.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<AssignedBadgeModel> vw_assignedbadge { get; set; }
        public DbSet<StamperModel> Stampers { get; set; }
        public DbSet<StampModel> Stamps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use the connection string from the configuration
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaulConnectionString"));
        }
    }
}
