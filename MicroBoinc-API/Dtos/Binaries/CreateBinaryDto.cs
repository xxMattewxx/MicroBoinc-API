using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Binaries
{
    public class CreateBinaryDto
    {
        [Required]
        public string Checksum { get; set; }

        [Required]
        public string DownloadURL { get; set; }
    }
}
