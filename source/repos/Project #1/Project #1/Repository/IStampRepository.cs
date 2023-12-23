using Project__1.Models;

namespace Project__1.Repository
{

    public interface IStampRepository
    {
        Task AddStampAsync(StampModel stamp);
    }
}
