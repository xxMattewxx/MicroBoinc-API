using MicroBoincAPI.Models.Versions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MicroBoincAPI.Data.Versions
{
    public class VersionsRepo : IVersionsRepo
    {
        private readonly AppDbContext _context;

        public VersionsRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateVersion(VersionInfo version)
        {
            _context.ClientsVersions.Add(version);
        }

        public VersionInfo GetLatest()
        {
            return _context.ClientsVersions
                .Include(x => x.Binary)
                .OrderByDescending(x => x.Version)
                .FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
