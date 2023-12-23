using Project__1.Models;

namespace Project__1.Services
{
    public interface IStampService
    {
        Task<StampResponseDto> AddStampAsync(StampRequestDto stampRequest);

    }
}
