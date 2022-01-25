using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Dtos.Feeder
{
    public class RetrieveTaskOfGroupsDto
    {
        [Required]
        public IEnumerable<long> AcceptedGroupsIDs { get; set; }

        [Required]
        public int? TaskCount { get; set; }
    }
}