using System;
using MicroBoincAPI.Models.Accounts;
using MicroBoincAPI.Models.Projects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroBoincAPI.Models.Leaderboard
{
    public class LeaderboardSnapshot
    {
        [Key]
        public long ID { get; set; }
        public long TotalPoints { get; set; }
        public long ValidatedPoints { get; set; }
        public DateTime SnapshotTime { get; set; }
        public long InvalidatedPoints { get; set; }
        public LeaderboardActionEnum ExecutedAction { get; set; }

        [ForeignKey("Account")]
        public long AccountID { get; set; }
        public Account Account { get; set; }

        [ForeignKey("Key")]
        public long KeyID { get; set; }
        public AccountKey Key { get; set; }

        [ForeignKey("Project")]
        public long ProjectID { get; set; }
        public Project Project { get; set; }
    }
}
