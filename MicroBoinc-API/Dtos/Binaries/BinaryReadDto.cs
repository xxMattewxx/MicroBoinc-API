using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Dtos.Binaries
{
    public class BinaryReadDto
    {
        public long ID { get; set; }
        public string Checksum { get; set; }
        public string DownloadURL { get; set; }
    }
}
