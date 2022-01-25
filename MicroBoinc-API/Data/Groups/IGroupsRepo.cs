using MicroBoincAPI.Models.Groups;
using MicroBoincAPI.Models.Permissions;

namespace MicroBoincAPI.Data.Groups
{
    public interface IGroupsRepo
    {
        public void CreateGroup(Group group);
        public Group GetGroupByID(long groupID);
        public GroupPermission GetGroupPermission(long groupID, long accountID);

        public bool SaveChanges();
    }
}
