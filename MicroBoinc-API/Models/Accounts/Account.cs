using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Models.Accounts
{
    public class Account
    {
        [Key]
        public long ID { get; set; }
        public long Offset { get; set; }
        public long DiscordID { get; set; }
        public string DisplayName { get; set; }
    }
}
