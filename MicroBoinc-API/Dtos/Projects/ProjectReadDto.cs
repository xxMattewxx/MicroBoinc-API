using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Projects
{
    public class ProjectReadDto
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string InputServerURL { get; set; }
        public bool UseExternalInputServer { get; set; }
    }
}
