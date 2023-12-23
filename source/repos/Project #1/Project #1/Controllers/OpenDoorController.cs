using Microsoft.AspNetCore.Mvc;
using Project__1.Services;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("security")]
public class OpenDoorController : ControllerBase
{
    private readonly IOpenDoorService _openDoorService;

    public OpenDoorController(IOpenDoorService openDoorService)
    {
        _openDoorService = openDoorService;
    }

    [HttpPost("opendoor")]
    public async Task<IActionResult> OpenDoor([FromBody] OpenDoorRequestDto request)
    {
        try
        {
            var result = await _openDoorService.OpenDoorAsync(request);

            if (result.Status)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
