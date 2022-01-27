using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Leaderboards
{
    public class HistoricalLeaderboardReadDto
    {
        public string ProjectName { get; set; }
        public IEnumerable<LeaderboardSnapshotReadDto> Entries { get; set; }
    }
}
