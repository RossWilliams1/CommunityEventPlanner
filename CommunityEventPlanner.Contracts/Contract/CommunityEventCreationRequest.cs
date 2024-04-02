namespace CommunityEventPlanner.Shared.Contract
{
    public class CommunityEventCreationRequest
    {
        public required string Name { get; set; } = string.Empty!;

        public string? Summary { get; set; }

        public required DateTime StartDate { get; set; } = DateTime.Now!;

        public DateTime? EndDate { get; set; }
    }
}
