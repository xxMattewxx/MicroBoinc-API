using MicroBoincAPI.Dtos.Binaries;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Dtos.Platforms
{
    public class PlatformReadDto
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public bool IsSpecialized { get; set; }
        public BinaryReadDto DetectorBinary { get; set; }
    }
}
