namespace MicroBoincAPI.Dtos.Projects
{
    public class GetProgressDto
    {
        public string Name { get; set; }
        public int TotalDone { get; set; }
        public int TotalGenerated { get; set; }
    }
}
