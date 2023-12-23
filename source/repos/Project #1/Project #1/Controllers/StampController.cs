using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project__1.Models;
using Project__1.Services;

namespace Project__1.Controllers
{
    

    [ApiController]
    [Route("stamp")]
    public class StampController : ControllerBase
    {
        private readonly IStampService _stampService;

        public StampController(IStampService stampService)
        {
            _stampService = stampService;
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddStamp([FromBody] StampRequestDto stampRequest)
        {
            try
            {
                var result = await _stampService.AddStampAsync(stampRequest);

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

}
