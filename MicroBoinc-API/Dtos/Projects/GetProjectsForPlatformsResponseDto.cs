using System.Collections.Generic;
using MicroBoincAPI.Dtos.Binaries;

namespace MicroBoincAPI.Dtos.Projects
{
    public class GetProjectsForPlatformsResponseDto
    {
        public IEnumerable<long> ProjectsIDs { get; set; }
        public IEnumerable<ProjectBinaryReadDto> ProjectsBinaries { get; set; }
    }
}
