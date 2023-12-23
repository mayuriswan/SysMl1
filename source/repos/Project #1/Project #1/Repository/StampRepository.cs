using Project__1.Data;
using Project__1.Models;

namespace Project__1.Repository
{
    

        public class StampRepository : IStampRepository
        {
            private readonly ApplicationDbContext _context;

            public StampRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task AddStampAsync(StampModel stamp)
            {
                _context.Stamps.Add(stamp);
                await _context.SaveChangesAsync();
            }

               
    }

}
