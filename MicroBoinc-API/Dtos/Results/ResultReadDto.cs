namespace MicroBoincAPI.Dtos.Results
{
    public class ResultReadDto
    {
        public long ID { get; set; }
        public long TaskID { get; set; }
        public string StdErr { get; set; }
        public string StdOut { get; set; }
        public short ExitCode { get; set; }
        public long AssignmentID { get; set; }
        public long ExecutionTime { get; set; }
    }
}
