using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Dtos.Validators
{
    public class ApplyValidationDataDto
    {
        [Required]
        public IEnumerable<long> TaskIDsToValidate { get; set; }

        [Required]
        public IEnumerable<long> TaskIDsToRegenerate { get; set; }

        [Required]
        public IEnumerable<long> RefusedAssignmentIDs { get; set; }

        [Required]
        public IEnumerable<long> ApprovedAssignmentIDs { get; set; }
    }
}
