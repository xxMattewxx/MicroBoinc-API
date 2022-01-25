using MicroBoincAPI.Models.Accounts;
using MicroBoincAPI.Models.Projects;
using MicroBoincAPI.Models.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroBoincAPI.Data.Tasks
{
    public class TasksRepo : ITasksRepo
    {
        private readonly AppDbContext _context;

        public TasksRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateTask(Task task)
        {
            _context.Tasks.Add(task);
        }

        public void CreateTasks(List<Task> tasks)
        {
            _context.Tasks.AddRange(tasks);
        }

        public IEnumerable<long> GetTaskIDsToValidate(long projectID)
        {
            return _context.Tasks
                .Where(x => x.Status == TaskStatus.PendingValidation)
                .Where(x => x.ProjectID == projectID)
                .Select(x => x.ID);
        }

        public IEnumerable<Task> RetrieveTaskOfGroups(AccountKey accountKey, IEnumerable<long> acceptedGroups, int count)
        {
            var userAssignmentsTaskIDs = _context.Assignments
                .Where(x => x.AssignedToID == accountKey.ID)
                .Select(x => x.TaskID);

            var query = _context.Tasks
                .Where(x => x.ID > accountKey.Account.Offset)
                .Where(x => x.SlotsLeft > 0 && x.Status == TaskStatus.Available)
                .Where(x => acceptedGroups.Contains(x.GroupID))
                .Where(x => !userAssignmentsTaskIDs.Contains(x.ID))
                .OrderBy(x => x.ID)
                .Take(count);

            return query;
        }

        public IEnumerable<Task> RetrieveTaskOfProjects(AccountKey accountKey, IEnumerable<long> acceptedProjects, int count)
        {
            var userAssignmentsTaskIDs = _context.Assignments
                .Where(x => x.AssignedTo.ID == accountKey.ID)
                .Select(x => x.Task.ID);

            return _context.Tasks
                .OrderBy(x => x.ID)
                .Where(x => x.ID > accountKey.Account.Offset)
                .Where(x => acceptedProjects.Contains(x.Project.ID))
                .Where(x => !userAssignmentsTaskIDs.Contains(x.ID))
                .Where(x => x.SlotsLeft > 0 && x.Status == TaskStatus.Available)
                .Take(count);
        }

        public void AttachEntity(object obj)
        {
            _context.Attach(obj);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
