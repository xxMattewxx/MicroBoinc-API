using MicroBoincAPI.Models.Platforms;
using MicroBoincAPI.Models.Projects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroBoincAPI.Models.Binaries
{
    public class ProjectBinary
    {
        [Key]
        public long ID { get; set; }
        public int Priority { get; set; }
        public Binary Binary { get; set; }
        public bool Deprecated { get; set; }

        [ForeignKey("ProjectID")]
        public long ProjectID { get; set; }
        public Project Project { get; set; }

        [ForeignKey("Platform")]
        public long PlatformID { get; set; }
        public Platform Platform { get; set; }
    }
}
