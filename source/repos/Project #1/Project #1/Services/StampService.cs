using Microsoft.EntityFrameworkCore;
using Project__1.Data;
using Project__1.Models;
using Project__1.Repository;

namespace Project__1.Services
{
    public class StampService : IStampService
    {
        private readonly IStampRepository _stampRepository;
        private readonly ApplicationDbContext _context;

        public StampService(IStampRepository stampRepository, ApplicationDbContext context)
        {
            _stampRepository = stampRepository;
            _context = context;
        }

        public async Task<StampResponseDto> AddStampAsync(StampRequestDto stampRequest)
        {
            try
            {
                var assignedBadge = await _context.vw_assignedbadge
                    .FirstOrDefaultAsync(b => b.CardNumber == stampRequest.BadgeId);

                if (assignedBadge == null)
                {
                    return new StampResponseDto { Status = false, Employeename = null, Datetime = null };
                }

                // Simulate stamper lookup based on stampId
                var stamper = await _context.Stampers
                    .FirstOrDefaultAsync(s => s.StamperId == stampRequest.StampId);

                if (stamper == null)
                {
                    return new StampResponseDto { Status = false, Employeename = null, Datetime = null };
                }

                // Check if the last stamp for the employee is "IN"
                var lastStamp = await _context.Stamps
                    .Where(s => s.EmployeeId == assignedBadge.EmployeeId)
                    .OrderByDescending(s => s.TimeStamp)
                    .FirstOrDefaultAsync();

                var newStatus = (lastStamp != null && lastStamp.Status == "IN") ? "OUT" : "IN";

                // Insert a new stamp record using the repository
                var newStamp = new StampModel
                {
                    BadgeId = stampRequest.BadgeId,
                    TimeStamp = DateTime.UtcNow,
                    Stamper = stamper.StamperName,
                    GivenName = assignedBadge.GivenName,
                    Surname = assignedBadge.Surname,
                    EmployeeId = assignedBadge.EmployeeId,
                    EmployeeEmail = assignedBadge.Email,
                    Status = newStatus
                };

                await _stampRepository.AddStampAsync(newStamp);

                // Return the result
                var result = new StampResponseDto
                {
                    Status = true,
                    Employeename = $"{assignedBadge.GivenName} {assignedBadge.Surname}",
                    Datetime = newStamp.TimeStamp
                };

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in StampService: {ex.Message}");
                throw;
            }
        }
    }
}
