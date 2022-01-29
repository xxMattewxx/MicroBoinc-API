using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroBoincAPI.Models.Accounts
{
    public class AccountKey
    {
        [Key]
        public long ID { get; set; }
        public string Identifier { get; set; }
        public bool IsWeak { get; set; }
        public bool IsRoot { get; set; }
        public string DisplayName { get; set; }

        [ForeignKey("Account")]
        public long AccountID { get; set; }
        public Account Account { get; set; }
    }
}
