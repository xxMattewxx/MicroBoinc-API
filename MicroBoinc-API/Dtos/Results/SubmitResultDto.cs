using System.ComponentModel.DataAnnotations;

namespace MicroBoincAPI.Dtos.Results
{
    public class SubmitResultDto
    {
        [Required(AllowEmptyStrings = true)]
        public string StdErr { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string StdOut { get; set; }

        [Required]
        public short? ExitCode { get; set; }

        [Required]
        public long? AssignmentID { get; set; }

        [Required]
        public long? ExecutionTime { get; set; }
    }
}
