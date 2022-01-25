using System.Linq;
using System.Collections.Generic;
using MicroBoincAPI.Dtos.Platforms;
using MicroBoincAPI.Models.Platforms;
using Microsoft.EntityFrameworkCore;

namespace MicroBoincAPI.Data.Platforms
{
    public class PlatformsRepo : IPlatformsRepo
    {
        private readonly AppDbContext _context;

        public PlatformsRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePlatform(Platform platform)
        {
            _context.Platforms.Add(platform);
        }

        public Platform GetPlatformByID(long id)
        {
            return _context.Platforms.FirstOrDefault(x => x.ID == id);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms
                .Include(x => x.DetectorBinary)
                .AsEnumerable();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
