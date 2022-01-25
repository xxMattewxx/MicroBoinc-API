using MicroBoincAPI.Models.Groups;
using MicroBoincAPI.Models.Permissions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Data.Groups
{
    public class GroupsRepo : IGroupsRepo
    {
        private readonly AppDbContext _context;

        public GroupsRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        public Group GetGroupByID(long groupID)
        {
            return _context.Groups
                .Include(x => x.OwnedBy)
                .FirstOrDefault(x => x.ID == groupID);
        }

        public GroupPermission GetGroupPermission(long groupID, long accountID)
        {
            return _context.GroupsPermissions
                .Where(x => x.ID == groupID)
                .FirstOrDefault(x => x.Account.ID == accountID);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
