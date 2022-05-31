using MicroBoincAPI.Models.Accounts;
using MicroBoincAPI.Models.Assignments;
using MicroBoincAPI.Models.Binaries;
using MicroBoincAPI.Models.Groups;
using MicroBoincAPI.Models.Leaderboard;
using MicroBoincAPI.Models.Permissions;
using MicroBoincAPI.Models.Platforms;
using MicroBoincAPI.Models.Projects;
using MicroBoincAPI.Models.Results;
using MicroBoincAPI.Models.Tasks;
using MicroBoincAPI.Models.Versions;
using Microsoft.EntityFrameworkCore;

namespace MicroBoincAPI.Data
{
    public class AppDbContext : DbContext
    {
        //Accounts
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountKey> AccountsKeys { get; set; }

        //Assignments
        public DbSet<Assignment> Assignments { get; set; }

        //Binaries
        public DbSet<Binary> Binaries { get; set; }
        public DbSet<ProjectBinary> ProjectsBinaries { get; set; }

        //Leaderboards
        public DbSet<LeaderboardEntry> LeaderboardsEntries { get; set; }
        public DbSet<LeaderboardSnapshot> LeaderboardsSnapshots { get; set; }

        //Groups
        public DbSet<Group> Groups { get; set; }

        //Permissions
        public DbSet<GroupPermission> GroupsPermissions { get; set; }

        //Platforms
        public DbSet<Platform> Platforms { get; set; }

        //Projects
        public DbSet<Project> Projects { get; set; }

        //Results
        public DbSet<Result> Results { get; set; }

        //Tasks
        public DbSet<Task> Tasks { get; set; }

        //Versions
        public DbSet<VersionInfo> ClientsVersions { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var taskBuilder = modelBuilder.Entity<Task>()
                .UseXminAsConcurrencyToken(); //# of slots left

            taskBuilder.HasIndex(x => x.Status);
            taskBuilder.HasIndex(x => new { x.ID, x.SlotsLeft });

            modelBuilder.Entity<Account>().UseXminAsConcurrencyToken(); //offset
            modelBuilder.Entity<LeaderboardEntry>()
                .UseXminAsConcurrencyToken() //points, validpoints, invalidpoints
                .HasIndex(x => new { x.KeyID, x.ProjectID }).IsUnique();
        }
    }
}
