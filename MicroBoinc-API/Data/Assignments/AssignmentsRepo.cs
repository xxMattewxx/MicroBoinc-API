using MicroBoincAPI.Models.Accounts;
using MicroBoincAPI.Models.Assignments;
using MicroBoincAPI.Models.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MicroBoincAPI.Data.Assignments
{
    public class AssignmentsRepo : IAssignmentsRepo
    {
        private readonly AppDbContext _context;

        public AssignmentsRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateAssignment(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
        }

        public void CreateAssignments(List<Assignment> assignments)
        {
            _context.Assignments.AddRange(assignments);
        }

        public Assignment GetAssignmentByID(long id)
        {
            return _context.Assignments
                .Include(x => x.AssignedTo)
                .ThenInclude(x => x.Account)
                .Include(x => x.Task)
                .ThenInclude(x => x.Project)
                .FirstOrDefault(x => x.ID == id);
        }

        public void UpdateStatus(long id, AssignmentStatus status)
        {
            var assignment = _context.Assignments.FirstOrDefault(x => x.ID == id);
            UpdateStatus(assignment, status);
        }

        public void UpdateStatus(Assignment assignment, AssignmentStatus status)
        {
            assignment.Status = status;
        }

        public void AttachEntity(object obj)
        {
            _context.Attach(obj);
        }

        public void ResetContext()
        {
            _context.ChangeTracker.Clear();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
