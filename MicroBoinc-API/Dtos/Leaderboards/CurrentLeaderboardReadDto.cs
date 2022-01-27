using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Leaderboards
{
    public class CurrentLeaderboardReadDto
    {
        public string ProjectName { get; set; }
        public IEnumerable<LeaderboardEntryReadDto> Entries { get; set; }
    }
}
