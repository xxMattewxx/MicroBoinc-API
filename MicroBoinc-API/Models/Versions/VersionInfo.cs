using MicroBoincAPI.Models.Binaries;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Models.Versions
{
    public class VersionInfo
    {
        [Key]
        public long Version { get; set; }
        public Binary Binary { get; set; }
        public string Codename { get; set; }
        public string FriendlyName { get; set; }
    }
}
