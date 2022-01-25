using MicroBoincAPI.Models.Accounts;
using MicroBoincAPI.Models.Leaderboard;
using MicroBoincAPI.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Data.Leaderboards
{
    public class LeaderboardsRepo : ILeaderboardsRepo
    {
        private readonly AppDbContext _context;

        public LeaderboardsRepo(AppDbContext context)
        {
            _context = context;
        }

        public void AddPointsToAccount(AccountKey key, Project project)
        {
            var entry = GetOrCreateEntry(key, project);
            entry.TotalPoints++;
            CreateSnapshot(entry, LeaderboardActionEnum.Submitted);
        }

        public void AddValidPointsToAccount(AccountKey key, Project project)
        {
            var entry = GetOrCreateEntry(key, project);
            entry.ValidatedPoints++;
            CreateSnapshot(entry, LeaderboardActionEnum.Validated);
        }

        public void AddInvalidPointsToAccount(AccountKey key, Project project)
        {
            var entry = GetOrCreateEntry(key, project);
            entry.InvalidatedPoints++;
            CreateSnapshot(entry, LeaderboardActionEnum.Invalidated);
        }

        public void CreateEntry(LeaderboardEntry entry)
        {
            _context.LeaderboardsEntries.Add(entry);
        }

        public void CreateSnapshot(LeaderboardSnapshot snapshot)
        {
            _context.LeaderboardsSnapshots.Add(snapshot);
        }

        public LeaderboardEntry GetEntryForKey(AccountKey key)
        {
            return _context.LeaderboardsEntries
                .FirstOrDefault(x => x.KeyID == key.ID);
        }

        public void AttachEntity(object obj)
        {
            _context.Attach(obj);
        }

        public void ResetContext()
        {
            _context.ChangeTracker.Clear();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        /* Internal functions */
        private LeaderboardEntry GetOrCreateEntry(AccountKey key, Project project)
        {
            var entry = GetEntryForKey(key);
            if (entry == null) //first time adding points to the (keyid, projectid) pair
            {
                entry = new LeaderboardEntry
                {
                    Account = key.Account,
                    Key = key,
                    Project = project,
                    TotalPoints = 0,
                    ValidatedPoints = 0
                };
                CreateEntry(entry);
            }
            return entry;
        }

        private LeaderboardSnapshot CreateSnapshot(LeaderboardEntry entry, LeaderboardActionEnum action)
        {
            var ret = new LeaderboardSnapshot
            {
                Key = entry.Key,
                Account = entry.Account,
                ExecutedAction = action,
                Project = entry.Project,
                TotalPoints = entry.TotalPoints,
                ValidatedPoints = entry.ValidatedPoints,
                InvalidatedPoints = entry.ValidatedPoints,
                SnapshotTime = DateTime.Now
            };
            CreateSnapshot(ret);
            return ret;
        }
    }
}
