using Microsoft.EntityFrameworkCore;
using SysML_Sync.Data;

namespace SysML_Sync.Services
{
    public class MapperService : IMapperService
    {
        private readonly ApplicationDbContext _dbContext;

        public MapperService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public async Task<List<Mapper>> GetAllAsync()
        {
            return await _dbContext.Mappers.ToListAsync();
        }
    }
}
