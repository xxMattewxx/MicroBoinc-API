using AutoMapper;
using MicroBoincAPI.Dtos.Leaderboards;
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

        public IEnumerable<LeaderboardEntryReadDto> GetLeaderboardEntries(Project project, Account account)
        {
            var entries = _context.LeaderboardsEntries
                .Where(x => x.AccountID == account.ID)
                .Where(x => x.ProjectID == project.ID)
                .Select(x => new LeaderboardEntryReadDto
                {
                    DisplayName = x.Key.DisplayName,
                    TotalPoints = x.TotalPoints,
                    ValidatedPoints = x.ValidatedPoints,
                    InvalidatedPoints = x.InvalidatedPoints
                });

            return entries;
        }

        public IEnumerable<LeaderboardEntryReadDto> GetSummedLeaderboardEntries(Project project)
        {
            var entries = _context.LeaderboardsEntries
                .Where(x => x.ProjectID == project.ID)
                .ToList()
                .GroupBy(x => x.AccountID)
                .Select(x => new LeaderboardEntryReadDto { 
                    DisplayName = x.Select(x => x.Account.DisplayName).FirstOrDefault(),
                    TotalPoints = x.Sum(x => x.TotalPoints),
                    ValidatedPoints = x.Sum(x => x.ValidatedPoints),
                    InvalidatedPoints = x.Sum(x => x.InvalidatedPoints)
                });

            return entries;
        }

        public IEnumerable<LeaderboardSnapshotReadDto> GetHistoricalLeaderboard(Project project)
        {
            var entries = _context.LeaderboardsSnapshots
                .Where(x => x.ProjectID == project.ID)
                .ToList()
                .GroupBy(x => x.AccountID)
                .Select(x => new LeaderboardSnapshotReadDto
                {
                    DisplayName = x.Select(x => x.Account.DisplayName).FirstOrDefault(),
                    ValidStamps = x
                        .Where(x => x.ExecutedAction == LeaderboardActionEnum.Validated)
                        .Select(x => (long)x.SnapshotTime.Subtract(DateTime.UnixEpoch).TotalSeconds),

                    InvalidStamps = x
                        .Where(x => x.ExecutedAction == LeaderboardActionEnum.Invalidated)
                        .Select(x => (long)x.SnapshotTime.Subtract(DateTime.UnixEpoch).TotalSeconds),

                    SubmittedStamps = x
                        .Where(x => x.ExecutedAction == LeaderboardActionEnum.Submitted)
                        .Select(x => (long)x.SnapshotTime.Subtract(DateTime.UnixEpoch).TotalSeconds)
                });

            return entries;
        }

        public LeaderboardEntry GetEntryForKey(AccountKey key, Project project)
        {
            return _context.LeaderboardsEntries
                .Where(x => x.ProjectID == project.ID)
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
            var entry = GetEntryForKey(key, project);
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
                KeyID = entry.KeyID,
                ExecutedAction = action,
                AccountID = entry.AccountID,
                ProjectID = entry.ProjectID,
                TotalPoints = entry.TotalPoints,
                ValidatedPoints = entry.ValidatedPoints,
                InvalidatedPoints = entry.InvalidatedPoints,
                SnapshotTime = DateTime.Now
            };
            CreateSnapshot(ret);
            return ret;
        }
    }
}
