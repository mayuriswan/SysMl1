using Project__1.Models;

namespace Project__1.Services
{
    public interface IOpenDoorService
    {
        Task<OpenDoorResponseDto> OpenDoorAsync(OpenDoorRequestDto request);

    }
}
