using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Models.Binaries
{
    public class Binary
    {
        [Key]
        public long ID { get; set; }
        public string Checksum { get; set; }
        public string DownloadURL { get; set; }
    }
}
