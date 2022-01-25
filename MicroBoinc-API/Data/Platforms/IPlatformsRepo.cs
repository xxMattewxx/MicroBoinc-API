using System.Collections.Generic;
using MicroBoincAPI.Dtos.Platforms;
using MicroBoincAPI.Models.Platforms;

namespace MicroBoincAPI.Data.Platforms
{
    public interface IPlatformsRepo
    {
        public void CreatePlatform(Platform platform);
        public Platform GetPlatformByID(long id);
        public IEnumerable<Platform> GetAllPlatforms();

        public bool SaveChanges();
    }
}
