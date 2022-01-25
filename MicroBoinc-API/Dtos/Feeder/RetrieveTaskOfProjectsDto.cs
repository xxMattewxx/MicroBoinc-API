using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Dtos.Feeder
{
    public class RetrieveTaskOfProjectsDto
    {
        [Required]
        public IEnumerable<long> AcceptedProjectsIDs { get; set; }

        [Required]
        public int? TaskCount { get; set; }
    }
}