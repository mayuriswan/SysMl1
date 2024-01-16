
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SysML_Sync.Data;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SysML_Sync.Services
{
    public class ModdelService : IModdelService
    {
        private readonly ApplicationDbContext _dbContext;

        public ModdelService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

       
        public async Task<List<Model>> GetAllAsync()
        {
           return await _dbContext.Moddels.ToListAsync();
        }
    }
}
