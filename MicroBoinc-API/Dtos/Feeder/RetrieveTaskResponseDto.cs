using MicroBoincAPI.Dtos.Assignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Feeder
{
    public class RetrieveTaskResponseDto
    {
        public IEnumerable<AssignmentReadDto> Assignments { get; set; }
    }
}
