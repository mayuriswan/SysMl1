using Microsoft.EntityFrameworkCore;
using Project__1.Data;
using Project__1.Models;
using Project__1.Repository;
using System;
using System.Threading.Tasks;
namespace Project__1.Services
{


    public class OpenDoorService : IOpenDoorService
    {
        private readonly IOpenDoorRepository _openDoorRepository;
        private readonly ApplicationDbContext _context;

        public OpenDoorService(IOpenDoorRepository openDoorRepository, ApplicationDbContext context)
        {
            _openDoorRepository = openDoorRepository;
            _context = context;
        }

        public async Task<OpenDoorResponseDto> OpenDoorAsync(OpenDoorRequestDto request)
        {
            try
            {
                

                // Query to get the owner of the badge
                var assignedBadge = await _context.vw_assignedbadge
                    .FirstOrDefaultAsync(b => b.CardNumber == request.BadgeId);

                if (assignedBadge == null)
                {
                    return new OpenDoorResponseDto { Status = false, Employeename = null, Datetime = null };
                }

                // Simulate stamper lookup based on stampId
                var stamper = await _context.Stampers
                    .FirstOrDefaultAsync(s => s.StamperId == request.StampId);

                if (stamper == null)
                {
                    return new OpenDoorResponseDto { Status = false, Employeename = null, Datetime = null };
                }

                // Call another API to open the door
                var doorResult = await _openDoorRepository.OpenDoorApiCallAsync();

                if (!doorResult)
                {
                    return new OpenDoorResponseDto { Status = false, Employeename = null, Datetime = null };
                }

                // Return the result
                var result = new OpenDoorResponseDto
                {
                    Status = true,
                    Employeename = $"{assignedBadge.GivenName} {assignedBadge.Surname}",
                    Datetime = DateTime.UtcNow
                };

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in OpenDoorService: {ex.Message}");
                throw;
            }
        }
    }

}
