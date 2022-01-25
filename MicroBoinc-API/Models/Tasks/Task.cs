using System;
using MicroBoincAPI.Models.Groups;
using MicroBoincAPI.Models.Projects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroBoincAPI.Models.Tasks
{
    public class Task
    {
        [Key]
        public long ID { get; set; }
        public string InputID { get; set; } //used for identifying the input data in the project's input server
        public string InputData { get; set; }
        public ushort SlotsLeft { get; set; }
        public TaskStatus Status { get; set; }
        public ushort ResultsLeft { get; set; }

        [ForeignKey("Group")]
        public long GroupID { get; set; }
        public Group Group { get; set; }

        [ForeignKey("Project")]
        public long ProjectID { get; set; }
        public Project Project { get; set; }
    }
}
