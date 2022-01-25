using MicroBoincAPI.Dtos.Projects;
using MicroBoincAPI.Models.Binaries;
using MicroBoincAPI.Models.Platforms;
using MicroBoincAPI.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Data.Binaries
{
    public interface IBinariesRepo
    {
        public void CreateProjectBinary(ProjectBinary ret);
        public GetProjectsForPlatformsResponseDto GetProjectsBinariesForPlatforms(IEnumerable<long> platformsIDs);
        public void DeprecateAllBinaries(Project project, Platform platform);

        public bool SaveChanges();
    }
}
