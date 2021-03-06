using System.Collections.Generic;
using MicroBoincAPI.Models.Binaries;
using MicroBoincAPI.Models.Projects;

namespace MicroBoincAPI.Data.Projects
{
    public interface IProjectsRepo
    {
        public void CreateProject(Project project);
        public Project GetProjectByID(long id);
        public IEnumerable<long> GetProjectsIDs();
        public (int totalDone, int totalGenerated) GetProjectProgress(long id);
        public IEnumerable<ProjectBinary> GetProjectsBinariesForPlatforms(IEnumerable<long> acceptedPlatforms);
        public bool SaveChanges();
    }
}
