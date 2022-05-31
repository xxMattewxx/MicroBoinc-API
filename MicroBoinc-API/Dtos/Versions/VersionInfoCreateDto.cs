using MicroBoincAPI.Dtos.Binaries;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Dtos.Versions
{
    public class VersionInfoCreateDto
    {
        [Required]
        public string Codename { get; set; }

        [Required]
        public string FriendlyName { get; set; }

        [Required]
        public CreateBinaryDto Binary { get; set; }
    }
}
