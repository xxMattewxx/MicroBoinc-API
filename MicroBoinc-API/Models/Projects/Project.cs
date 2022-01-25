using System;
using MicroBoincAPI.Models.Groups;
using MicroBoincAPI.Models.Accounts;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Models.Projects
{
    public class Project
    {
        [Key]
        public long ID { get; set; }
        public Group Group { get; set; }
        public string Name { get; set; }
        public long GroupID { get; set; }
        public long AddedByID { get; set; }
        public Account AddedBy { get; set; }
        public DateTime AddedAt { get; set; }
        public string Description { get; set; }
        public string InputServerURL { get; set; }
        public bool UseExternalInputServer { get; set; }
    }
}
