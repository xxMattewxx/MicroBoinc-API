using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Accounts
{
    public class AccountReadDto
    {
        public long ID { get; set; }
        public long DiscordID { get; set; }
        public string DisplayName { get; set; }
    }
}
