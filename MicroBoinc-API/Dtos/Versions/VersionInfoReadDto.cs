using MicroBoincAPI.Dtos.Binaries;

namespace MicroBoincAPI.Dtos.Versions
{
    public class VersionInfoReadDto
    {
        public long Version { get; set; }
        public string Codename { get; set; }
        public string FriendlyName { get; set; }
        public BinaryReadDto Binary { get; set; }
    }
}
