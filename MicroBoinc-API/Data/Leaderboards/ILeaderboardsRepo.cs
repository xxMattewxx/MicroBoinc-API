using MicroBoincAPI.Dtos.Leaderboards;
using MicroBoincAPI.Models.Accounts;
using MicroBoincAPI.Models.Leaderboard;
using MicroBoincAPI.Models.Projects;
using System.Collections.Generic;

namespace MicroBoincAPI.Data.Leaderboards
{
    public interface ILeaderboardsRepo
    {
        public void CreateEntry(LeaderboardEntry entry);
        public void CreateSnapshot(LeaderboardSnapshot snapshot);

        public IEnumerable<LeaderboardEntryReadDto> GetSummedLeaderboardEntries(Project project);
        public IEnumerable<LeaderboardEntryReadDto> GetLeaderboardEntries(Project project, Account account);
        public IEnumerable<LeaderboardSnapshotReadDto> GetHistoricalLeaderboard(Project project);

        public void AddPointsToAccount(AccountKey key, Project project);
        public void AddValidPointsToAccount(AccountKey key, Project project);
        public void AddInvalidPointsToAccount(AccountKey key, Project project);

        public LeaderboardEntry GetEntryForKey(AccountKey key, Project project);

        public void ResetContext();
        public bool SaveChanges();
        public void AttachEntity(object obj);
    }
}
