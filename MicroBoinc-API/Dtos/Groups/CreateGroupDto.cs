using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Dtos.Groups
{
    public class CreateGroupDto
    {
        [Required]
        public string Name { get; set; }
    }
}
