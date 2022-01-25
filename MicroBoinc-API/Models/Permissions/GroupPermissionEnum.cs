using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Models.Permissions
{
    [Flags]
    public enum GroupPermissionEnum
    {
        CanCreateProjects = 1,
        CanAddTasks = 2
    }
}
