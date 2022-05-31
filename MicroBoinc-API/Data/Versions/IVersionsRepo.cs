using MicroBoincAPI.Models.Versions;

namespace MicroBoincAPI.Data.Versions
{
    public interface IVersionsRepo
    {
        public void CreateVersion(VersionInfo version);
        public VersionInfo GetLatest();
        public bool SaveChanges();
    }
}
