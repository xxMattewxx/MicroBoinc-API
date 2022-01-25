using System;
using MicroBoincAPI.Models.Tasks;
using MicroBoincAPI.Models.Accounts;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Models.Assignments
{
    public class Assignment
    {
        [Key]
        public long ID { get; set; }
        public Task Task { get; set; }
        public long TaskID { get; set; }
        public DateTime Deadline { get; set; }
        public long AssignedToID { get; set; }
        public AccountKey AssignedTo { get; set; }
        public AssignmentStatus Status { get; set; }
    }
}
