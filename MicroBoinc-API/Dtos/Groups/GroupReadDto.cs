using MicroBoincAPI.Dtos.Accounts;

namespace MicroBoincAPI.Dtos.Groups
{
    public class GroupReadDto
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public AccountReadDto OwnedBy { get; set; }
    }
}
