using MicroBoincAPI.Dtos.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Binaries
{
    public class ProjectBinaryReadDto
    {
        public long ID { get; set; }
        public int Priority { get; set; }
        public long PlatformID { get; set; }
        public BinaryReadDto Binary { get; set; }
        public ProjectReadDto Project { get; set; }
    }
}
