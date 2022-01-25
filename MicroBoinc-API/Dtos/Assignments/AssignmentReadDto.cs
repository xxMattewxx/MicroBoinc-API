using MicroBoincAPI.Dtos.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Assignments
{
    public class AssignmentReadDto
    {
        public long ID { get; set; }
        public TaskReadDto Task { get; set; }
        public DateTime Deadline { get; set; }
    }
}
