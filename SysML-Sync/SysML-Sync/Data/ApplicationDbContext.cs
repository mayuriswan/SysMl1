using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SysML_Sync.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Model> Moddels { get; set; }
        public DbSet<Mapper> Mappers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}