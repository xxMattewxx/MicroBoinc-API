using MicroBoincAPI.Dtos.Groups;
using MicroBoincAPI.Dtos.Projects;
using MicroBoincAPI.Models.Groups;
using MicroBoincAPI.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Tasks
{
    public class TaskReadDto
    {
        public long ID { get; set; }
        public long GroupID { get; set; }
        public long ProjectID { get; set; }
        public string InputID { get; set; } //used for identifying the input data in the project's input server
        public string InputData { get; set; }
        public TaskStatus Status { get; set; }
    }
}
