using MicroBoincAPI.Models.Tasks;
using MicroBoincAPI.Models.Projects;
using MicroBoincAPI.Models.Assignments;
using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Models.Results
{
    public class Result
    {
        [Key]
        public long ID { get; set; }
        public Task Task { get; set; }
        public long TaskID { get; set; }
        public string StdErr { get; set; }
        public string StdOut { get; set; }
        public short ExitCode { get; set; }
        public long ProjectID { get; set; }
        public Project Project { get; set; }
        public long ExecutionTime { get; set; }
        public Assignment Assignment { get; set; }
    }
}
