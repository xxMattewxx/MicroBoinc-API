using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Accounts
{
    public class CreateAccountDto
    {
        [Required]
        public long? DiscordID { get; set; }

        [Required]
        public string DisplayName { get; set; }
    }
}
