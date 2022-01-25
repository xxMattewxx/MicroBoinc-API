using MicroBoincAPI.Models.Accounts;
using MicroBoincAPI.Models.Leaderboard;
using MicroBoincAPI.Models.Projects;

namespace MicroBoincAPI.Data.Leaderboards
{
    public interface ILeaderboardsRepo
    {
        public void CreateEntry(LeaderboardEntry entry);
        public void CreateSnapshot(LeaderboardSnapshot snapshot);

        public void AddPointsToAccount(AccountKey key, Project project);
        public void AddValidPointsToAccount(AccountKey key, Project project);
        public void AddInvalidPointsToAccount(AccountKey key, Project project);

        public LeaderboardEntry GetEntryForKey(AccountKey key);

        public void ResetContext();
        public bool SaveChanges();
        public void AttachEntity(object obj);
    }
}
