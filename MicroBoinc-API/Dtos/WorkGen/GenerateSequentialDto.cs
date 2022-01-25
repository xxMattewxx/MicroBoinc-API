using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.WorkGen
{
    public class GenerateSequentialDto
    {
        [Required]
        public long? ProjectID { get; set; }

        [Required]
        public long? Start { get; set; }

        [Required]
        public long? End { get; set; }

        [Required]
        public long? Offset { get; set; }

        [Required]
        public ushort? QuorumNeeded { get; set; }
    }
}
