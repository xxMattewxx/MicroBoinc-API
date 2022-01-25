using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Models.Assignments
{
    public enum AssignmentStatus
    {
        Available = 0,
        Sent,
        Received,
        Deadlined,
        Validated,
        Invalidated
    }
}
