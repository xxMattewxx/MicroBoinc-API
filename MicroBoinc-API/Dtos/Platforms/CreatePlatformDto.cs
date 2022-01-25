using MicroBoincAPI.Dtos.Binaries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Platforms
{
    public class CreatePlatformDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool? IsSpecialized { get; set; }

        [Required]
        public CreateBinaryDto DetectorBinary { get; set; }
    }
}
