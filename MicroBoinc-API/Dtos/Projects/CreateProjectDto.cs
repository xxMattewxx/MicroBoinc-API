using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Dtos.Projects
{
    public class CreateProjectDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public long? GroupID { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string InputServerURL { get; set; }
    }
}
