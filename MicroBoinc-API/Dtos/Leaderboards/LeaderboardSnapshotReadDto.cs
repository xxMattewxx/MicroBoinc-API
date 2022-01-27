using System.Collections.Generic;

namespace MicroBoincAPI.Dtos.Leaderboards
{
    public class LeaderboardSnapshotReadDto
    {
        public string DisplayName { get; set; }
        public IEnumerable<long> ValidStamps { get; set; }
        public IEnumerable<long> InvalidStamps { get; set; }
        public IEnumerable<long> SubmittedStamps { get; set; }
    }
}
