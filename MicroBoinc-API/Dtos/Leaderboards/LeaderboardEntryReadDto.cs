namespace MicroBoincAPI.Dtos.Leaderboards
{
    public class LeaderboardEntryReadDto
    {
        public long TotalPoints { get; set; }
        public string DisplayName { get; set; }
        public long ValidatedPoints { get; set; }
        public long InvalidatedPoints { get; set; }
    }
}
