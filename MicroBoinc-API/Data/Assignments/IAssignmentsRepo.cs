using System.Collections.Generic;
using MicroBoincAPI.Models.Accounts;
using MicroBoincAPI.Models.Assignments;

namespace MicroBoincAPI.Data.Assignments
{
    public interface IAssignmentsRepo
    {
        public void CreateAssignment(Assignment assignment);
        public void CreateAssignments(List<Assignment> assignments);

        public Assignment GetAssignmentByID(long id);

        public void UpdateStatus(Assignment assignment, AssignmentStatus received);

        public void AttachEntity(object obj);
        public void ResetContext();
        public bool SaveChanges();
    }
}
