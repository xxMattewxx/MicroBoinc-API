using MicroBoincAPI.Models.Binaries;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Models.Platforms
{
    public class Platform
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }
        public bool IsSpecialized { get; set; } //GPUs, FPGAs, USB ASICs
        public Binary DetectorBinary { get; set; } //Binary for detecting if the platform is compatible
    }
}
