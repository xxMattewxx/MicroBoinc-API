using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Dtos.Binaries
{
    public class CreateProjectBinaryDto
    {
        [Required]
        public int? Priority { get; set; }

        [Required]
        public string Checksum { get; set; }

        [Required]
        public long? ProjectID { get; set; }

        [Required]
        public long? PlatformID { get; set; }

        [Required]
        public string DownloadURL { get; set; }
    }
}
