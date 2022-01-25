using MicroBoincAPI.Models.Groups;
using MicroBoincAPI.Models.Accounts;
using System.ComponentModel.DataAnnotations;
using System;

namespace MicroBoincAPI.Models.Permissions
{
    public class GroupPermission
    {
        [Key]
        public long ID { get; set; }
        public Group Group { get; set; }
        public Account Account { get; set; }
        public GroupPermissionEnum Permissions { get; set; }
    }
}
