using System.Collections.Generic;

namespace MicroBoincAPI.Dtos.Tasks
{
    public class GetTasksToValidateResponseDto
    {
        public IEnumerable<long> TaskIDs { get; set; }
    }
}
