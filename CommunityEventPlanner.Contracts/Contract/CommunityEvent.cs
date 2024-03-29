namespace CommunityEventPlanner.Shared.Contract
{
    public class CommunityEvent
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Summary { get; set; }

        public required DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
