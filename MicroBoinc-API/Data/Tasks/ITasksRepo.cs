using MicroBoincAPI.Models.Accounts;
using MicroBoincAPI.Models.Projects;
using MicroBoincAPI.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroBoincAPI.Data.Tasks
{
    public interface ITasksRepo
    {
        public void CreateTask(Task task);
        public void CreateTasks(List<Task> tasks);

        public Task GetTaskByID(long id);

        public void IncreaseTaskSlots(long id);
        public void DecreaseResultsLeft(long id);
        public void UpdateStatus(long id, TaskStatus status);

        public IEnumerable<long> GetTaskIDsToValidate(long projectID);
        public IEnumerable<Task> RetrieveTaskOfGroups(AccountKey accountKey, IEnumerable<long> acceptedGroups, int count);
        public IEnumerable<Task> RetrieveTaskOfProjects(AccountKey accountKey, IEnumerable<long> acceptedProjects, int count);

        public void AttachEntity(object obj);
        public void ResetContext();
        public bool SaveChanges();
    }
}
