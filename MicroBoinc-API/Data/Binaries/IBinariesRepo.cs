using MicroBoincAPI.Dtos.Projects;
using MicroBoincAPI.Models.Binaries;
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

        public bool SaveChanges();
    }
}
