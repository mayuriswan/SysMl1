namespace Project__1.Models
{
    public class AssignedBadgeModel
    {
        public int Id { get; set; }
        public string? GivenName { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public int EmployeeId { get; set; }

        public string? CardNumber { get; set; }

    }
}
