using MicroBoincAPI.Models.Accounts;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Models.Groups
{
    public class Group
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public long OwnedByID { get; set; }
        public Account OwnedBy { get; set; }
    }
}
