using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Dtos.Versions
{
    public class VersionInfoCreateDto
    {
        [Required]
        public long? BinaryID { get; set; }

        [Required]
        public string Codename { get; set; }

        [Required]
        public string FriendlyName { get; set; }
    }
}
