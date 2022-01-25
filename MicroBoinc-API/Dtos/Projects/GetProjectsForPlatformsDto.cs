using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Projects
{
    public class GetProjectsForPlatformsDto
    {
        [Required]
        public IEnumerable<long> PlatformsIDs { get; set; }
    }
}
